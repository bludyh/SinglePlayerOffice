using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice.Interactions {
    internal class VehicleElevator : Interaction {
        private Prop body;
        private List<Gate> gates;
        private Prop platform;

        public VehicleElevator(Vector3 rot, Vector3 levelAPos, Vector3 levelBPos, Vector3 levelCPos) {
            gates = new List<Gate>();
            Rotation = rot;
            LevelAPos = levelAPos;
            LevelBPos = levelBPos;
            LevelCPos = levelCPos;
        }

        public override string HelpText => "Press ~INPUT_CONTEXT~ to use the vehicle elevator";
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; }
        public Vector3 LevelAPos { get; }
        public Vector3 LevelBPos { get; }
        public Vector3 LevelCPos { get; }
        public override bool IsCreated => body != null && platform != null;

        public override void Create() {
            var model = new Model("imp_prop_ie_carelev01");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded)
                    Script.Wait(50);
                body = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
            }

            model.MarkAsNoLongerNeeded();

            model = new Model("imp_prop_ie_carelev02");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded)
                    Script.Wait(50);
                platform = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
                platform.Position = LevelAPos;
                platform.Rotation = Rotation;
                platform.FreezePosition = true;
            }

            model.MarkAsNoLongerNeeded();
            body?.AttachTo(platform, 0);
        }

        private Vector3? GetCurrentLevelElevatorPos() {
            if (Game.Player.Character.Position.Z > LevelAPos.Z && Game.Player.Character.Position.Z < LevelBPos.Z)
                return LevelAPos;
            if (Game.Player.Character.Position.Z > LevelBPos.Z && Game.Player.Character.Position.Z < LevelCPos.Z)
                return LevelBPos;
            if (Game.Player.Character.Position.Z > LevelCPos.Z)
                return LevelCPos;
            return null;
        }

        private bool MoveTo(Vector3 targetPos) {
            if (!(platform.Position.DistanceTo(targetPos) > 0.01f)) return false;
            platform.Position = Vector3.Add(platform.Position,
                platform.Position.Z < targetPos.Z ? new Vector3(0f, 0f, 0.005f) : new Vector3(0f, 0f, -0.005f));
            return true;
        }

        public override void Update() {
            var currentGarage = (Garage) Utilities.CurrentBuilding.CurrentLocation;
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead &&
                        (Game.Player.Character.Position.DistanceTo(LevelAPos) < 8f ||
                         Game.Player.Character.Position.DistanceTo(LevelBPos) < 8f ||
                         Game.Player.Character.Position.DistanceTo(LevelCPos) < 8f) &&
                        !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        Utilities.DisplayHelpTextThisFrame(HelpText);
                        if (Game.IsControlJustPressed(2, Control.Context)) {
                            Position = GetCurrentLevelElevatorPos().GetValueOrDefault();
                            State = 4;
                        }
                    }

                    break;
                case 1:
                    if (Function.Call<bool>(Hash.REQUEST_SCRIPT_AUDIO_BANK, "DLC_IMPORTEXPORT/GARAGE_ELEVATOR", false,
                        -1))
                        State = 2;
                    break;
                case 2:
                    currentGarage.AddVehicleInfo(Game.Player.Character.CurrentVehicle);
                    platform.Position = platform.GetOffsetInWorldCoords(new Vector3(0f, 0f, -1f));
                    State = 3;
                    break;
                case 3:
                    if (MoveTo(LevelAPos)) {
                        Game.Player.Character.CurrentVehicle.Position =
                            platform.GetOffsetInWorldCoords(new Vector3(0f, 0f, 1.3f));
                        Game.Player.Character.CurrentVehicle.Rotation =
                            Vector3.Add(Game.Player.Character.CurrentVehicle.Rotation, new Vector3(0f, 0f, 0.2f));
                    }
                    else {
                        Position = GetCurrentLevelElevatorPos().GetValueOrDefault();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Task.ClearAll();
                        if (currentGarage == Utilities.CurrentBuilding.GarageOne)
                            Audio.PlaySoundFrontend("Speech_Floor_1", "DLC_IE_Garage_Elevator_Sounds");
                        else if (currentGarage == Utilities.CurrentBuilding.GarageTwo)
                            Audio.PlaySoundFrontend("Speech_Floor_2", "DLC_IE_Garage_Elevator_Sounds");
                        else if (currentGarage == Utilities.CurrentBuilding.GarageThree)
                            Audio.PlaySoundFrontend("Speech_Floor_3", "DLC_IE_Garage_Elevator_Sounds");
                        Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_IMPORTEXPORT/GARAGE_ELEVATOR");
                        State = 4;
                    }

                    break;
                case 4:
                    foreach (var gate in gates)
                        gate.MoveTo(gate.InitialPos);
                    if (!MoveTo(Position)) {
                        var gates = new List<Gate>();
                        foreach (var prop in World.GetNearbyProps(platform.Position, 4.5f)) {
                            if (prop.Model.Hash != -2088725999 && prop.Model.Hash != -1238206604 &&
                                (prop.Model.Hash != 1593297148 || !(platform.Position.DistanceTo(LevelAPos) < 1f)))
                                continue;
                            gates.Add(new Gate(prop, prop.Position));
                            this.gates = gates;
                        }

                        State = 5;
                    }

                    break;
                case 5:
                    if (!gates.TrueForAll(gate => gate.MoveTo(Vector3.Add(gate.InitialPos, new Vector3(0f, 0f, 3f))))) {
                        Utilities.CurrentBuilding.UpdateVehicleElevatorMenuButtons();
                        SinglePlayerOffice.IsHudHidden = true;
                        Utilities.CurrentBuilding.VehicleElevatorMenu.Visible = true;
                        State = 0;
                    }

                    break;
            }
        }

        public override void Reset() {
            Dispose();
        }

        public override void Dispose() {
            body?.Delete();
            platform?.Delete();
        }

        private class Gate {
            private readonly Prop prop;

            public Gate(Prop prop, Vector3 pos) {
                this.prop = prop;
                InitialPos = pos;
            }

            public Vector3 InitialPos { get; }

            public bool MoveTo(Vector3 pos) {
                if (!(prop.Position.DistanceTo(pos) > 0.01f)) return false;
                prop.Position = Vector3.Add(prop.Position,
                    prop.Position.Z < pos.Z ? new Vector3(0f, 0f, 0.01f) : new Vector3(0f, 0f, -0.01f));
                return true;
            }
        }
    }
}