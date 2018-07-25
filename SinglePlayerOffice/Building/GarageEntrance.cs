using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class GarageEntrance : Location {

        public int InteriorID { get; set; }
        public Vector3 ElevatorCamPos { get; set; }
        public Vector3 ElevatorCamRot { get; set; }
        public float ElevatorCamFOV { get; set; }
        public Camera ElevatorCam { get; set; }
        public int GarageEntranceStatus { get; set; }

        protected override void TeleportOnTick() {
            if (!Game.Player.Character.IsDead && Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 10f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (Building.Owner != Owner.None) {
                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Building.Owner) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the vehicle elevator");
                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                            GarageEntranceStatus = 1;
                            SinglePlayerOffice.IsHudHidden = true;
                            Building.GarageEntranceMenu.Visible = true;
                        }
                    }
                    else SinglePlayerOffice.DisplayHelpTextThisFrame("Only the owner can use the vehicle elevator");
                }
                else SinglePlayerOffice.DisplayHelpTextThisFrame("You do not own this building");
            }
        }

        private void ElevatorEnterOnTick() {
            switch (GarageEntranceStatus) {
                case 1:
                    ElevatorCam = World.CreateCamera(ElevatorCamPos, ElevatorCamRot, ElevatorCamFOV);
                    World.RenderingCamera = ElevatorCam;
                    Game.Player.Character.Task.DriveTo(Game.Player.Character.CurrentVehicle, TriggerPos, 1f, 10f);
                    GarageEntranceStatus = 2;
                    break;
                case 2:
                    World.RenderingCamera.PointAt(Game.Player.Character.CurrentVehicle);
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x21d33957) != 1) {
                        Function.Call(Hash._0x260BE8F09E326A20, Game.Player.Character.CurrentVehicle, 1f, 1, 0);
                        if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0xc572e06a) != 1) Game.Player.Character.Task.StandStill(-1);
                    }
                    break;
            }
        }

        public override void OnTick() {
            if (Building == null) Building = SinglePlayerOffice.GetCurrentBuilding();
            TeleportOnTick();
            ElevatorEnterOnTick();
        }

    }
}
