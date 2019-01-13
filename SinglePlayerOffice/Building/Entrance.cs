using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Buildings {

    internal class Entrance : Location {

        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFov { get; set; }
        public Camera PurchaseCam { get; set; }

        protected override void HandleTrigger() {
            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f) ||
                UI.MenuPool.IsAnyMenuOpen()) return;

            if (currentBuilding.IsOwned) {
                if (World.CurrentDate.CompareTo(currentBuilding.ConstructionTime) > 0) {
                    if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                        Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the building");

                        if (!Game.IsControlJustPressed(2, Control.Context)) return;

                        Game.Player.Character.Task.StandStill(-1);
                        UI.IsHudHidden = true;
                        UI.TeleportMenu.Visible = true;
                    }
                    else {
                        if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 8 &&
                            Function.Call<int>(Hash.GET_CLOCK_HOURS) < 17) {
                            Utilities.DisplayHelpTextThisFrame(
                                $"Press ~INPUT_CONTEXT~ to visit {currentBuilding.Owner}'s building");

                            if (!Game.IsControlJustPressed(2, Control.Context)) return;

                            Game.Player.Character.Task.StandStill(-1);
                            UI.IsHudHidden = true;
                            UI.TeleportMenu.Visible = true;
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
                UI.IsHudHidden = true;
                UI.PurchaseMenu.Visible = true;
                PurchaseCam = World.CreateCamera(PurchaseCamPos,
                    PurchaseCamRot, PurchaseCamFov);
                World.RenderingCamera = PurchaseCam;
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            }
        }

    }

}