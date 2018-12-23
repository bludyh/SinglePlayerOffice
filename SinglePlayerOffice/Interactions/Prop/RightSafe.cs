using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class RightSafeInteraction : Interaction {

        private Prop door;

        public override string HelpText {
            get {
                return (!IsSafeOpened) ? "Press ~INPUT_CONTEXT~ to open the safe" : "Press ~INPUT_CONTEXT~ to close the safe";
            }
        }
        public override string RejectHelpText {
            get {
                return "Only the owner can open the safe";
            }
        }
        public bool IsSafeOpened { get; private set; }

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1.4f)) {
                            switch (prop.Model.Hash) {
                                case -1176373441:
                                case -1149617688:
                                case -548219756:
                                case 1854960432:
                                case 682108925:
                                case 1002451519:
                                    if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                                        Utilities.DisplayHelpTextThisFrame(HelpText);
                                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                            door = prop;
                                            SinglePlayerOffice.IsHudHidden = true;
                                            State = 1;
                                        }
                                    }
                                    else Utilities.DisplayHelpTextThisFrame(RejectHelpText);
                                    break;
                            }
                        }
                    }
                    break;
                case 1:
                    if (!IsSafeOpened) {
                        initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@vault@right@male@", "open", door.Position.X, door.Position.Y, door.Position.Z, door.Rotation.X, door.Rotation.Y, door.Rotation.Z, 0, 2);
                        initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@vault@right@male@", "open", door.Position.X, door.Position.Y, door.Position.Z, door.Rotation.X, door.Rotation.Y, door.Rotation.Z, 0, 2);
                    }
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y, initialPos.Z, 1f, -1, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    if (!IsSafeOpened) {
                        Utilities.SavedPos = door.Position;
                        Utilities.SavedRot = door.Rotation;
                    }
                    State = 3;
                    break;
                case 3:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Utilities.SavedPos.X, Utilities.SavedPos.Y, Utilities.SavedPos.Z, 0f, 0f, Utilities.SavedRot.Z, 2);
                    if (!IsSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@right@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, door, syncSceneHandle, "open_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        IsSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@right@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, door, syncSceneHandle, "close_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        IsSafeOpened = false;
                    }
                    State = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    State = 0;
                    break;
            }
        }

        public override void Reset() {
            IsSafeOpened = false;
        }

    }
}
