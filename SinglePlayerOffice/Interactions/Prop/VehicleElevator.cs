using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice.Interactions {
    class VehicleElevator : Interaction {

        private List<ElevatorGate> elevatorGates;

        public override string HelpText {
            get {
                return "Press ~INPUT_CONTEXT~ to use the vehicle elevator";
            }
        }
        public Elevator Elevator { get; set; }
        public Vector3 ElevatorPos { get; set; }
        public Vector3 ElevatorRot { get; set; }
        public Vector3 LevelAPos { get; set; }
        public Vector3 LevelBPos { get; set; }
        public Vector3 LevelCPos { get; set; }

        private Vector3? GetCurrentLevelElevatorPos() {
            if (Game.Player.Character.Position.Z > LevelAPos.Z && Game.Player.Character.Position.Z < LevelBPos.Z)
                return LevelAPos;
            if (Game.Player.Character.Position.Z > LevelBPos.Z && Game.Player.Character.Position.Z < LevelCPos.Z)
                return LevelBPos;
            if (Game.Player.Character.Position.Z > LevelCPos.Z)
                return LevelCPos;
            return null;
        }

        private bool MoveElevator(Vector3 pos) {
            if (Elevator.Platform.Position.DistanceTo(pos) > 0.01f) {
                if (Elevator.Platform.Position.Z < pos.Z)
                    Elevator.Platform.Position = Vector3.Add(Elevator.Platform.Position, new Vector3(0f, 0f, 0.005f));
                else
                    Elevator.Platform.Position = Vector3.Add(Elevator.Platform.Position, new Vector3(0f, 0f, -0.005f));
                return true;
            }
            return false;
        }

        private bool MoveElevatorGate(Prop gate, Vector3 pos) {
            if (gate.Position.DistanceTo(pos) > 0.01f) {
                if (gate.Position.Z < pos.Z)
                    gate.Position = Vector3.Add(gate.Position, new Vector3(0f, 0f, 0.01f));
                else
                    gate.Position = Vector3.Add(gate.Position, new Vector3(0f, 0f, -0.01f));
                return true;
            }
            return false;
        }

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            var currentGarage = (Garage)currentBuilding.CurrentLocation;
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead
                        && (Game.Player.Character.Position.DistanceTo(LevelAPos) < 8f
                        || Game.Player.Character.Position.DistanceTo(LevelBPos) < 8f
                        || Game.Player.Character.Position.DistanceTo(LevelCPos) < 8f)
                        && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {

                        Utilities.DisplayHelpTextThisFrame(HelpText);
                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                            ElevatorPos = GetCurrentLevelElevatorPos().GetValueOrDefault();
                            State = 3;
                        }
                    }
                    break;
                case 1:
                    currentGarage.AddVehicleInfo(Game.Player.Character.CurrentVehicle);
                    Elevator.Platform.Position = Elevator.Platform.GetOffsetInWorldCoords(new Vector3(0f, 0f, -1f));
                    State = 2;
                    break;
                case 2:
                    if (MoveElevator(LevelAPos)) {
                        Game.Player.Character.CurrentVehicle.Position = Elevator.Platform.GetOffsetInWorldCoords(new Vector3(0f, 0f, 1.3f));
                        Game.Player.Character.CurrentVehicle.Rotation = Vector3.Add(Game.Player.Character.CurrentVehicle.Rotation, new Vector3(0f, 0f, 0.2f));
                    }
                    else {
                        ElevatorPos = GetCurrentLevelElevatorPos().GetValueOrDefault();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Task.ClearAll();
                        if (currentGarage == currentBuilding.GarageOne)
                            Audio.PlaySoundFrontend("Speech_Floor_1", "DLC_IE_Garage_Elevator_Sounds");
                        else if (currentGarage == currentBuilding.GarageTwo)
                            Audio.PlaySoundFrontend("Speech_Floor_2", "DLC_IE_Garage_Elevator_Sounds");
                        else if (currentGarage == currentBuilding.GarageThree)
                            Audio.PlaySoundFrontend("Speech_Floor_3", "DLC_IE_Garage_Elevator_Sounds");
                        State = 3;
                    }
                    break;
                case 3:
                    foreach (var gate in elevatorGates)
                        MoveElevatorGate(gate.Prop, gate.InitialPos);
                    if (!MoveElevator(ElevatorPos)) {
                        var gates = new List<ElevatorGate>();
                        foreach (var prop in World.GetNearbyProps(Elevator.Platform.Position, 4.5f)) {
                            if (prop.Model.Hash == -2088725999 || prop.Model.Hash == -1238206604 || (prop.Model.Hash == 1593297148 && Elevator.Platform.Position.DistanceTo(LevelAPos) < 1f)) {
                                gates.Add(new ElevatorGate(prop, prop.Position));
                                elevatorGates = gates;
                            }
                        }
                        State = 4;
                    }
                    break;
                case 4:
                    if (!elevatorGates.TrueForAll(gate => MoveElevatorGate(gate.Prop, Vector3.Add(gate.InitialPos, new Vector3(0f, 0f, 3f))))) {
                        currentBuilding.UpdateVehicleElevatorMenuButtons();
                        SinglePlayerOffice.IsHudHidden = true;
                        currentBuilding.VehicleElevatorMenu.Visible = true;
                        State = 0;
                    }
                    break;
            }
        }

    }
}
