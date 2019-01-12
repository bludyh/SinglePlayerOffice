using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {
    internal class ModShop : Location, IInterior {
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
            Radio = new Radio();
            Tv = new Tv();
        }

        public static List<InteriorStyle> FloorStyles { get; set; }

        public override string RadioEmitter => "DLC_IE_Office_Garage_Mod_Shop_Radio_01";
        public string Ipl { get; set; }
        public int InteriorId { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFov { get; set; }
        public Camera PurchaseCam { get; set; }
        public InteriorStyle FloorStyle { get; set; }
        public Radio Radio { get; }
        public SofaAndTv SofaAndTv { get; set; }
        public Tv Tv { get; }
        public List<string> ExteriorIpLs { get; set; }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, Ipl);
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (var style in FloorStyles)
                Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, FloorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        public void UnloadInterior() {
            Function.Call(Hash.REMOVE_IPL, Ipl);
        }

        public void LoadExterior() {
            foreach (var ipl in ExteriorIpLs) Function.Call(Hash.REQUEST_IPL, ipl);
        }

        public void UnloadExterior() {
            foreach (var ipl in ExteriorIpLs) Function.Call(Hash.REMOVE_IPL, ipl);
        }

        public void LoadInterior(InteriorStyle floorStyle) {
            Function.Call(Hash.REQUEST_IPL, Ipl);
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (var style in FloorStyles)
                Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, floorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        public void ChangeFloorStyle(InteriorStyle floorStyle) {
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (var style in FloorStyles)
                Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, floorStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        protected override List<Interaction> GetInteractions() {
            return new List<Interaction> { Radio, SofaAndTv, Tv };
        }

        protected override void HandleTrigger() {
            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f) ||
                SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) return;
            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
            if (!Game.IsControlJustPressed(2, Control.Context)) return;
            Game.Player.Character.Task.StandStill(-1);
            Utilities.CurrentBuilding.UpdateTeleportMenuButtons();
            SinglePlayerOffice.IsHudHidden = true;
            Utilities.CurrentBuilding.TeleportMenu.Visible = true;
        }
    }
}