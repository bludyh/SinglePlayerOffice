using GTA;

namespace SinglePlayerOffice.Buildings {
    internal class HeliPad : Location {
        protected override void HandleTrigger() {
            var currentBuilding = Utilities.CurrentBuilding;
            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f) ||
                SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) return;
            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the stairs");
            if (!Game.IsControlJustPressed(2, Control.Context)) return;
            Game.Player.Character.Task.StandStill(-1);
            currentBuilding.UpdateTeleportMenuButtons();
            SinglePlayerOffice.IsHudHidden = true;
            currentBuilding.TeleportMenu.Visible = true;
        }
    }
}