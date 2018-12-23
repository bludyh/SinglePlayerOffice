using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class RadioInteraction : Interaction {

        private Prop radio;
        private RadioStation radioStation;
        private UIMenu radioMenu;

        public override string HelpText {
            get {
                return (!IsRadioOn) ? "Press ~INPUT_CONTEXT~ to turn on the radio" : "Press ~INPUT_CONTEXT~ to turn off the radio";
            }
        }
        public bool IsRadioOn { get; private set; }

        public RadioInteraction() {
            CreateRadioMenu();
        }

        private void CreateRadioMenu() {
            radioMenu = new UIMenu("", "~b~Radio Stations", new Point(0, -107));
            radioMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            foreach (var station in RadioStation.Stations) {
                UIMenuItem radioStationBtn = new UIMenuItem(station.Name, station.Description);
                radioMenu.AddItem(radioStationBtn);
            }
            radioMenu.RefreshIndex();
            radioMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                radioStation = RadioStation.Stations[index];
                State = 3;
            };
            radioMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            SinglePlayerOffice.MenuPool.Add(radioMenu);
        }

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            switch (prop.Model.Hash) {
                                case -364924791:
                                    Utilities.DisplayHelpTextThisFrame(HelpText);
                                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                        radio = prop;
                                        SinglePlayerOffice.IsHudHidden = true;
                                        State = 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 1:
                    initialPos = radio.GetOffsetInWorldCoords(new Vector3(0f, -0.65f, 0f));
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@mp_radio@high_apment", "action_a_bedroom", radio.Position.X, radio.Position.Y, radio.Position.Z, radio.Rotation.X, radio.Rotation.Y, radio.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y, initialPos.Z, 1f, -1, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    Game.Player.Character.Task.StandStill(-1);
                    if (!IsRadioOn && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        radioMenu.Visible = true;
                        State = 0;
                    }
                    if (IsRadioOn) {
                        State = 3;
                    }
                    break;
                case 3:
                    Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom", 4f, -4f, -1, 0, 0, 0, 0, 0);
                    State = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom") > 0.5f) {
                        if (!IsRadioOn) {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, currentBuilding.CurrentLocation.RadioEmitter, true);
                            Function.Call(Hash._0x0E0CD610D5EB6C85, currentBuilding.CurrentLocation.RadioEmitter, radio);
                            Function.Call(Hash.SET_EMITTER_RADIO_STATION, currentBuilding.CurrentLocation.RadioEmitter, radioStation.GameName);
                            IsRadioOn = true;
                        }
                        else {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, currentBuilding.CurrentLocation.RadioEmitter, false);
                            IsRadioOn = false;
                        }
                        State = 5;
                    }
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom") > 0.9f) {
                        if (IsRadioOn && Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2) == 1) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "RADIO_LIKE", "SPEECH_PARAMS_FORCE");
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Task.ClearAll();
                        State = 0;
                    }
                    break;
            }
        }

        public override void Reset() {
            IsRadioOn = false;
            Dispose();
        }

        public override void Dispose() {
            var currentBuilding = Utilities.CurrentBuilding;
            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, currentBuilding.CurrentLocation.RadioEmitter, false);
        }

    }
}
