using GTA;
using GTA.Native;

namespace SinglePlayerOffice.Buildings {
    internal class Entrance : Location {
        protected override void HandleTrigger() {
            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f) ||
                SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) return;
            if (Utilities.CurrentBuilding.IsOwned) {
                if (World.CurrentDate.CompareTo(Utilities.CurrentBuilding.ConstructionTime) > 0) {
                    if (Utilities.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                        Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the building");
                        if (!Game.IsControlJustPressed(2, Control.Context)) return;
                        Game.Player.Character.Task.StandStill(-1);
                        Utilities.CurrentBuilding.UpdateTeleportMenuButtons();
                        SinglePlayerOffice.IsHudHidden = true;
                        Utilities.CurrentBuilding.TeleportMenu.Visible = true;
                    }
                    else {
                        if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 8 &&
                            Function.Call<int>(Hash.GET_CLOCK_HOURS) < 17) {
                            Utilities.DisplayHelpTextThisFrame(
                                $"Press ~INPUT_CONTEXT~ to visit {Utilities.CurrentBuilding.Owner}'s building");
                            if (!Game.IsControlJustPressed(2, Control.Context)) return;
                            Game.Player.Character.Task.StandStill(-1);
                            Utilities.CurrentBuilding.UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            Utilities.CurrentBuilding.TeleportMenu.Visible = true;
                        }
                        else {
                            Utilities.DisplayHelpTextThisFrame(
                                "Building is closed. You can come back between 9:00 and 17:00");
                        }
                    }
                }
                else {
                    Utilities.DisplayHelpTextThisFrame("Building is under construction. Come back later");
                }
            }
            else {
                Utilities.DisplayHelpTextThisFrame("You do not own this building~n~Press ~INPUT_CONTEXT~ to purchase");
                if (!Game.IsControlJustPressed(2, Control.Context)) return;
                Game.Player.Character.Task.StandStill(-1);
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                SinglePlayerOffice.IsHudHidden = true;
                Utilities.CurrentBuilding.PurchaseMenu.Visible = true;
                Utilities.CurrentBuilding.PurchaseCam = World.CreateCamera(Utilities.CurrentBuilding.PurchaseCamPos,
                    Utilities.CurrentBuilding.PurchaseCamRot, Utilities.CurrentBuilding.PurchaseCamFov);
                World.RenderingCamera = Utilities.CurrentBuilding.PurchaseCam;
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            }
        }
    }
}