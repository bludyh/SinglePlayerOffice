using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {
    class GarageEntrance : Location {

        public int InteriorID { get; set; }
        public VehicleElevatorEntrance Interaction { get; set; }

        protected override void HandleTrigger() {
            var currentBuilding = Utilities.CurrentBuilding;

            if (!Game.Player.Character.IsDead && Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 10f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (currentBuilding.IsOwned) {
                    if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                        if (World.CurrentDate.CompareTo(currentBuilding.ConstructionTime) > 0) {
                            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the vehicle elevator");
                            if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                Interaction.State = 1;
                                SinglePlayerOffice.IsHudHidden = true;
                                currentBuilding.GarageEntranceMenu.Visible = true;
                            }
                        }
                        else Utilities.DisplayHelpTextThisFrame("Building is under construction. Come back later");
                    }
                    else Utilities.DisplayHelpTextThisFrame("Only the owner can use the vehicle elevator");
                }
                else Utilities.DisplayHelpTextThisFrame("You do not own this building");
            }
        }

        public override void Update() {
            base.Update();

            Interaction.Update();
        }

    }
}
