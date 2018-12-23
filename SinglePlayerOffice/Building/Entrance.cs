using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Buildings {
    class Entrance : Location {

        protected override void HandleTrigger() {
            var currentBuilding = Utilities.CurrentBuilding;

            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (currentBuilding.IsOwned) {
                    if (World.CurrentDate.CompareTo(currentBuilding.ConstructionTime) > 0) {
                        if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the building");
                            if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                Game.Player.Character.Task.StandStill(-1);
                                currentBuilding.UpdateTeleportMenuButtons();
                                SinglePlayerOffice.IsHudHidden = true;
                                currentBuilding.TeleportMenu.Visible = true;
                            }
                        }
                        else {
                            if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 8 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 17) {
                                Utilities.DisplayHelpTextThisFrame(String.Format("Press ~INPUT_CONTEXT~ to visit {0}'s building", currentBuilding.Owner));
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    Game.Player.Character.Task.StandStill(-1);
                                    currentBuilding.UpdateTeleportMenuButtons();
                                    SinglePlayerOffice.IsHudHidden = true;
                                    currentBuilding.TeleportMenu.Visible = true;
                                }
                            }
                            else Utilities.DisplayHelpTextThisFrame("Building is closed. You can come back between 9:00 and 17:00");
                        }
                    }
                    else Utilities.DisplayHelpTextThisFrame("Building is under construction. Come back later");
                }
                else {
                    Utilities.DisplayHelpTextThisFrame("You do not own this building~n~Press ~INPUT_CONTEXT~ to purchase");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        Game.Player.Character.Task.StandStill(-1);
                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);
                        SinglePlayerOffice.IsHudHidden = true;
                        currentBuilding.PurchaseMenu.Visible = true;
                        currentBuilding.PurchaseCam = World.CreateCamera(currentBuilding.PurchaseCamPos, currentBuilding.PurchaseCamRot, currentBuilding.PurchaseCamFOV);
                        World.RenderingCamera = currentBuilding.PurchaseCam;
                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                    }
                }
            }
        }

    }
}
