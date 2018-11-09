using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class ModShop : Location, IInterior {

        public static List<InteriorStyle> FloorStyles { get; set; }

        public string IPL { get; set; }
        public List<string> ExteriorIPLs { get; set; }
        public int InteriorID { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }
        public InteriorStyle FloorStyle { get; set; }

        static ModShop() {
            FloorStyles = new List<InteriorStyle> {
                new InteriorStyle("Floor 1", 0, ""),
                new InteriorStyle("Floor 2", 120000, "floor_vinyl_18"),
                new InteriorStyle("Floor 3", 132500, "floor_vinyl_16"),
                new InteriorStyle("Floor 4", 145000, "floor_vinyl_17"),
                new InteriorStyle("Floor 5", 157500, "floor_vinyl_19"),
                new InteriorStyle("Floor 6", 170000, "floor_vinyl_06"),
                new InteriorStyle("Floor 7", 182500, "floor_vinyl_08"),
                new InteriorStyle("Floor 8", 195000, "floor_vinyl_07"),
                new InteriorStyle("Floor 9", 207500, "floor_vinyl_09"),
                new InteriorStyle("Floor 10", 220000, "floor_vinyl_10"),
                new InteriorStyle("Floor 11", 245000, "floor_vinyl_14"),
                new InteriorStyle("Floor 12", 270000, "floor_vinyl_15"),
                new InteriorStyle("Floor 13", 295000, "floor_vinyl_13"),
                new InteriorStyle("Floor 14", 320000, "floor_vinyl_12"),
                new InteriorStyle("Floor 15", 345000, "floor_vinyl_11"),
                new InteriorStyle("Floor 16", 370000, "floor_vinyl_05"),
                new InteriorStyle("Floor 17", 395000, "floor_vinyl_04"),
                new InteriorStyle("Floor 18", 420000, "floor_vinyl_01"),
                new InteriorStyle("Floor 19", 465000, "floor_vinyl_02"),
                new InteriorStyle("Floor 20", 500000, "floor_vinyl_03")
            };
        }

        public ModShop() {
            ActiveInteractions.AddRange(new List<Action> { TeleportOnTick, Interactions.SofaTVOnTick, Interactions.TVOnTick });
        }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in FloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, FloorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void LoadInterior(InteriorStyle floorStyle) {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in FloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, floorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void UnloadInterior() {
            Function.Call(Hash.REMOVE_IPL, IPL);
        }

        public void LoadExterior() {
            foreach (var ipl in ExteriorIPLs) Function.Call(Hash.REQUEST_IPL, ipl);
        }

        public void UnloadExterior() {
            foreach (var ipl in ExteriorIPLs) Function.Call(Hash.REMOVE_IPL, ipl);
        }

        public void ChangeFloorStyle(InteriorStyle floorStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in FloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, floorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        protected override void TeleportOnTick() {
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                    Game.Player.Character.Task.StandStill(-1);
                    Building.UpdateTeleportMenuButtons();
                    SinglePlayerOffice.IsHudHidden = true;
                    Building.TeleportMenu.Visible = true;
                }
            }
        }

        public override void OnTick() {
            Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
            if (Building == null) Building = SinglePlayerOffice.GetCurrentBuilding();
            Building.HideExteriorMapObjects();
            base.OnTick();
        }

    }
}
