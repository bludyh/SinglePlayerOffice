using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Office : Location, IInterior {

        public static List<string> ExtraDecors { get; set; }

        public List<string> ExteriorIPLs { get; set; }
        public List<int> InteriorIDs { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }
        public List<InteriorStyle> InteriorStyles { get; set; }
        public InteriorStyle InteriorStyle { get; set; }
        public bool HasExtraDecors { get; set; }
        public int ExtraDecorsPrice { get; set; }
        public OfficeScene Scene { get; set; }

        static Office() {
            ExtraDecors = new List<string> {
                "cash_set_01", "cash_set_02", "cash_set_03", "cash_set_04", "cash_set_05", "cash_set_06", "cash_set_07", "cash_set_08",
                "cash_set_09", "cash_set_10", "cash_set_11", "cash_set_12", "cash_set_13", "cash_set_14", "cash_set_15", "cash_set_16",
                "cash_set_17", "cash_set_18", "cash_set_19", "cash_set_20", "cash_set_21", "cash_set_22", "cash_set_23", "cash_set_24",
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
            ActiveInteractions.AddRange(new List<Action> {
                TeleportOnTick,
                Interactions.SofaOnTick,
                Interactions.SofaTVOnTick,
                Interactions.TVOnTick,
                Interactions.ComputerOnTick,
                Interactions.LeftSafeOnTick,
                Interactions.RightSafeOnTick,
                Interactions.RadioOnTick,
                Interactions.BossChairOnTick,
                Interactions.StaffChairOnTick,
                Interactions.LaptopChairOnTick,
                Interactions.ShowerOnTick,
                Interactions.WardrobeOnTick
            });
        }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, InteriorStyle.Style);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
            foreach (string decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
            if (HasExtraDecors) foreach (string decor in ExtraDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void LoadInterior(InteriorStyle interiorStyle) {
            Function.Call(Hash.REQUEST_IPL, interiorStyle.Style);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
            foreach (string decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
            if (HasExtraDecors) foreach (string decor in ExtraDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void UnloadInterior() {
            foreach (InteriorStyle style in InteriorStyles) Function.Call(Hash.REMOVE_IPL, style.Style);
        }

        public void LoadExterior() {
            foreach (var ipl in ExteriorIPLs) Function.Call(Hash.REQUEST_IPL, ipl);
        }

        public void UnloadExterior() {
            foreach (var ipl in ExteriorIPLs) Function.Call(Hash.REMOVE_IPL, ipl);
        }

        public void ChangeInteriorStyle(InteriorStyle interiorStyle) {
            UnloadInterior();
            Function.Call(Hash.REQUEST_IPL, interiorStyle.Style);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
            foreach (string decor in ExtraDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
            if (HasExtraDecors) foreach (string decor in ExtraDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public string GetRadioEmitter() {
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
            }
            return null;
        }

        protected override void TeleportOnTick() {
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
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

        public void Dispose() {
            Scene.Dispose();
        }

    }
}
