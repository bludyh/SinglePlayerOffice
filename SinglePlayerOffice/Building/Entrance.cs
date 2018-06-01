using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Entrance : Location {

        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }

        protected override void TeleportOnTick() {
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (building.Owner != Owner.None) {
                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)building.Owner) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the building");
                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            building.UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            building.TeleportMenu.Visible = true;
                        }
                    }
                    else {
                        if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 8 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 17) {
                            SinglePlayerOffice.DisplayHelpTextThisFrame(String.Format("Press ~INPUT_CONTEXT~ to visit {0}'s building", building.Owner));
                            if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                Game.Player.Character.Task.StandStill(-1);
                                building.UpdateTeleportMenuButtons();
                                SinglePlayerOffice.IsHudHidden = true;
                                building.TeleportMenu.Visible = true;
                            }
                        }
                        else SinglePlayerOffice.DisplayHelpTextThisFrame("Building is closed. You can come back between 9:00 and 17:00");
                    }
                }
                else {
                    SinglePlayerOffice.DisplayHelpTextThisFrame("You do not own this building~n~Press ~INPUT_CONTEXT~ to purchase");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        Game.Player.Character.Task.StandStill(-1);

                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);

                        SinglePlayerOffice.IsHudHidden = true;
                        building.PurchaseMenu.Visible = true;
                        PurchaseCam = World.CreateCamera(PurchaseCamPos, PurchaseCamRot, PurchaseCamFOV);
                        World.RenderingCamera = PurchaseCam;

                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                    }
                }
            }
        }

        public override void OnTick() {
            if (building == null) building = SinglePlayerOffice.GetCurrentBuilding();
            TeleportOnTick();
        }

    }
}
