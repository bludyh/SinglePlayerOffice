using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Buildings {
    class HeliPad : Location {

        protected override void HandleTrigger() {
            var currentBuilding = Utilities.CurrentBuilding;
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the stairs");
                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.UpdateTeleportMenuButtons();
                    SinglePlayerOffice.IsHudHidden = true;
                    currentBuilding.TeleportMenu.Visible = true;
                }
            }
        }

    }
}
