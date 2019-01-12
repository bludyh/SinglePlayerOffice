using System.Collections.Generic;
using GTA;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {
    internal class GarageEntrance : Location {
        public int InteriorId { get; set; }
        public VehicleElevatorEntrance VehicleElevatorEntrance { get; set; }

        protected override List<Interaction> GetInteractions() {
            return new List<Interaction> { VehicleElevatorEntrance };
        }

        protected override void HandleTrigger() {
            if (Game.Player.Character.IsDead || !Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 10f) ||
                SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) return;
            if (Utilities.CurrentBuilding.IsOwned) {
                if (Utilities.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                    if (World.CurrentDate.CompareTo(Utilities.CurrentBuilding.ConstructionTime) > 0) {
                        Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the vehicle elevator");
                        if (!Game.IsControlJustPressed(2, Control.Context)) return;
                        VehicleElevatorEntrance.State = 1;
                        SinglePlayerOffice.IsHudHidden = true;
                        Utilities.CurrentBuilding.GarageEntranceMenu.Visible = true;
                    }
                    else {
                        Utilities.DisplayHelpTextThisFrame("Building is under construction. Come back later");
                    }
                }
                else {
                    Utilities.DisplayHelpTextThisFrame("Only the owner can use the vehicle elevator");
                }
            }
            else {
                Utilities.DisplayHelpTextThisFrame("You do not own this building");
            }
        }
    }
}