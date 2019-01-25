using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {

    internal class Office : Location, IInterior {

        static Office() {
            ExtraDecors = new List<string> {
                "cash_set_01", "cash_set_02", "cash_set_03", "cash_set_04", "cash_set_05", "cash_set_06", "cash_set_07",
                "cash_set_08",
                "cash_set_09", "cash_set_10", "cash_set_11", "cash_set_12", "cash_set_13", "cash_set_14", "cash_set_15",
                "cash_set_16",
                "cash_set_17", "cash_set_18", "cash_set_19", "cash_set_20", "cash_set_21", "cash_set_22", "cash_set_23",
                "cash_set_24",
                "swag_art", "swag_art2", "swag_art3",
                "swag_booze_cigs", "swag_booze_cigs2", "swag_booze_cigs3",
                "swag_counterfeit", "swag_counterfeit2", "swag_counterfeit3",
                "swag_drugbags", "swag_drugbags2", "swag_drugbags3",
                "swag_drugstatue", "swag_drugstatue2", "swag_drugstatue3",
                "swag_electronic", "swag_electronic2", "swag_electronic3",
                "swag_furcoats", "swag_furcoats2", "swag_furcoats3",
                "swag_gems", "swag_gems2", "swag_gems3",
                "swag_guns", "swag_guns2", "swag_guns3",
                "swag_ivory", "swag_ivory2", "swag_ivory3",
                "swag_jewelwatch", "swag_jewelwatch2", "swag_jewelwatch3",
                "swag_med", "swag_med2", "swag_med3",
                "swag_pills", "swag_pills2", "swag_pills3",
                "swag_silver", "swag_silver2", "swag_silver3"
            };
        }

        public Office() {
            BoardRoomChair = new BoardRoomChair();
            BossChair = new BossChair();
            BuildingNameScaleform = new BuildingNameScaleform();
            Computer = new Computer();
            Laptop = new Laptop();
            LeftSafe = new LeftSafe();
            Radio = new Radio();
            RightSafe = new RightSafe();
            Shower = new Shower();
            Tv = new Tv();
        }

        public static List<string> ExtraDecors { get; set; }

        public override string RadioEmitter => GetRadioEmitter();
        public List<int> InteriorIDs { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFov { get; set; }
        public Camera PurchaseCam { get; set; }
        public List<InteriorStyle> InteriorStyles { get; set; }
        public InteriorStyle InteriorStyle { get; set; }
        public bool HasExtraDecors { get; set; }
        public int ExtraDecorsPrice { get; set; }
        public Boss Boss { get; set; }
        public Pa Pa { get; set; }
        public List<Staff> Staffs { get; set; }
        public BoardRoomChair BoardRoomChair { get; }
        public BossChair BossChair { get; }
        public BuildingNameScaleform BuildingNameScaleform { get; }
        public Computer Computer { get; }
        public Laptop Laptop { get; }
        public LeftSafe LeftSafe { get; }
        public Radio Radio { get; }
        public RightSafe RightSafe { get; }
        public Shower Shower { get; }
        public List<Sofa> Sofas { get; set; }
        public SofaAndTv SofaAndTv { get; set; }
        public Tv Tv { get; }
        public Wardrobe Wardrobe { get; set; }
        public List<string> ExteriorIpLs { get; set; }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, InteriorStyle.Style);
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_chairs"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_booze"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_booze");
            foreach (var decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, decor);
            if (HasExtraDecors)
                foreach (var decor in ExtraDecors)
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        public void UnloadInterior() {
            foreach (var style in InteriorStyles) Function.Call(Hash.REMOVE_IPL, style.Style);
        }

        public void LoadExterior() {
            foreach (var ipl in ExteriorIpLs) Function.Call(Hash.REQUEST_IPL, ipl);
        }

        public void UnloadExterior() {
            foreach (var ipl in ExteriorIpLs) Function.Call(Hash.REMOVE_IPL, ipl);
        }

        private string GetRadioEmitter() {
            switch (InteriorStyle.Name) {
                case "Old Spice Warms": return "SE_ex_int_office_01a_Radio_01";
                case "Old Spice Classical": return "SE_ex_int_office_01b_Radio_01";
                case "Old Spice Vintage": return "SE_ex_int_office_01c_Radio_01";
                case "Executive Contrast": return "SE_ex_int_office_02a_Radio_01";
                case "Executive Rich": return "SE_ex_int_office_02b_Radio_01";
                case "Executive Cool": return "SE_ex_int_office_02c_Radio_01";
                case "Power Broker Ice": return "SE_ex_int_office_03a_Radio_01";
                case "Power Broker Conservative": return "SE_ex_int_office_03b_Radio_01";
                case "Power Broker Polished": return "SE_ex_int_office_03c_Radio_01";
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void LoadInterior(InteriorStyle interiorStyle) {
            Function.Call(Hash.REQUEST_IPL, interiorStyle.Style);
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_chairs"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_booze"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_booze");
            foreach (var decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, decor);
            if (HasExtraDecors)
                foreach (var decor in ExtraDecors)
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        public void ChangeInteriorStyle(InteriorStyle interiorStyle) {
            UnloadInterior();
            Function.Call(Hash.REQUEST_IPL, interiorStyle.Style);
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_chairs"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorId, "office_booze"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, "office_booze");
            foreach (var decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorId, decor);
            if (HasExtraDecors)
                foreach (var decor in ExtraDecors)
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorId, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorId);
        }

        protected override List<Interaction> GetInteractions() {
            var interactions = new List<Interaction>();

            if (!SinglePlayerOffice.CurrentBuilding.IsOwnedBy(Game.Player.Character))
                interactions.Add(Boss);
            interactions.Add(Pa);
            interactions.AddRange(Staffs);
            interactions.Add(BoardRoomChair);
            interactions.Add(BossChair);
            interactions.Add(BuildingNameScaleform);
            interactions.Add(Computer);
            interactions.Add(Laptop);
            interactions.Add(LeftSafe);
            interactions.Add(Radio);
            interactions.Add(RightSafe);
            interactions.Add(Shower);
            interactions.AddRange(Sofas);
            interactions.Add(SofaAndTv);
            interactions.Add(Tv);
            interactions.Add(Wardrobe);

            return interactions;
        }

        protected override void HandleTrigger() {
            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() ||
                !(Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f) ||
                UI.MenuPool.IsAnyMenuOpen()) return;

            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");

            if (!Game.IsControlJustPressed(2, Control.Context)) return;

            Game.Player.Character.Task.StandStill(-1);
            UI.IsHudHidden = true;
            UI.TeleportMenu.Visible = true;
        }

    }

}