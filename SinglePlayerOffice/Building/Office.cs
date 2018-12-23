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
    class Office : Location, IInterior {

        public static List<string> ExtraDecors { get; set; }

        private readonly List<int> occupiedIndexes;

        public override string RadioEmitter { get => GetRadioEmitter(); }
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
        public Vector3 BossChairPos { get; set; }
        public Boss BossInteraction { get; private set; }
        public Vector3 PAChairPos { get; set; }
        public Vector3 PAChairRot { get; set; }
        public PA PAInteraction { get; private set; }
        public List<Vector3> StaffChairPosList { get; set; }
        public MaleStaff MaleStaffInteraction { get; private set; }
        public FemaleStaff FemaleStaffInteraction { get; private set; }
        public BuildingNameScaleform BuildingNameInteraction { get; private set; }
        public BossChair BossChairInteraction { get; private set; }
        public Computer ComputerInteraction { get; private set; }
        public Laptop LaptopInteraction { get; private set; }
        public LeftSafe LeftSafeInteraction { get; private set; }
        public Radio RadioInteraction { get; private set; }
        public RightSafe RightSafeInteraction { get; private set; }
        public SofaInteraction SofaInteraction { get; set; }
        public SofaAndTV SofaWithTVInteraction { get; set; }
        public BoardRoomChair StaffChairInteraction { get; private set; }
        public TV TVInteration { get; private set; }
        public WardrobeInteraction WardrobeInteraction { get; set; }

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
            occupiedIndexes = new List<int>();
            BossInteraction = new Boss();
            PAInteraction = new PA();
            MaleStaffInteraction = new MaleStaff();
            FemaleStaffInteraction = new FemaleStaff();
            BuildingNameInteraction = new BuildingNameScaleform();
            BossChairInteraction = new BossChair();
            ComputerInteraction = new Computer();
            LaptopInteraction = new Laptop();
            LeftSafeInteraction = new LeftSafe();
            RadioInteraction = new Radio();
            RightSafeInteraction = new RightSafe();
            SofaInteraction = new SofaInteraction();
            StaffChairInteraction = new BoardRoomChair();
            TVInteration = new TV();
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

        private void CreateBoss() {
            var currentBuilding = Utilities.CurrentBuilding;
            var boss = BossInteraction.Ped;
            switch (currentBuilding.Owner) {
                case Owner.Michael:
                    boss = World.CreatePed(PedHash.Michael, BossChairPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 4, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 2, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 0, 1, 2);
                    break;
                case Owner.Franklin:
                    boss = World.CreatePed(PedHash.Franklin, BossChairPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 3, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 2, 0, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 22, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 21, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 17, 9, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 8, 14, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 11, 7, 0, 2);
                    break;
                case Owner.Trevor:
                    boss = World.CreatePed(PedHash.Trevor, BossChairPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 5, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 27, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 20, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 19, 12, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 8, 14, 0, 2);
                    break;
            }
        }

        private void CreatePA() {
            var pa = PAInteraction.Ped;
            pa = World.CreatePed(PedHash.ExecutivePAFemale01, PAChairPos);
            pa.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 0, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 2, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 3, 1, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 4, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 6, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 7, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 2), Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 8, 3, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 11, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
        }

        private void CreatePAChair() {
            var paChair = PAInteraction.Chair;
            Model model = new Model("ex_prop_offchair_exec_03");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                paChair = World.CreateProp(model, Vector3.Zero, false, false);
                paChair.Position = PAChairPos;
                paChair.Rotation = PAChairRot;
            }
            model.MarkAsNoLongerNeeded();
        }

        private int GetRandomStaffChairIndex() {
            int index;
            do {
                index = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, StaffChairPosList.Count);
            } while (occupiedIndexes.Contains(index));
            occupiedIndexes.Add(index);
            return index;
        }

        private void CreateMaleStaff() {
            var maleStaff = MaleStaffInteraction.Ped;
            maleStaff = World.CreatePed(PedHash.Business01AMM, StaffChairPosList[GetRandomStaffChairIndex()]);
            Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
        }

        private void CreateFemaleStaff() {
            var femaleStaff = FemaleStaffInteraction.Ped;
            femaleStaff = World.CreatePed(PedHash.Business04AFY, StaffChairPosList[GetRandomStaffChairIndex()]);
            Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
        }

        public override void OnLocationArrived() {
            var currentBuilding = Utilities.CurrentBuilding;

            if (!currentBuilding.IsOwnedBy(Game.Player.Character))
                CreateBoss();
            if (PAInteraction.Chair == null)
                CreatePAChair();
            if (PAInteraction.Ped == null)
                CreatePA();
        }

        public override void OnLocationLeft() {
            BossInteraction.Reset();
            PAInteraction.Reset();
            MaleStaffInteraction.Reset();
            FemaleStaffInteraction.Reset();
            LeftSafeInteraction.Reset();
            RadioInteraction.Reset();
            RightSafeInteraction.Reset();
            SofaWithTVInteraction.Reset();
            TVInteration.Reset();
        }

        protected override void HandleTrigger() {
            var currentBuilding = Utilities.CurrentBuilding;
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.UpdateTeleportMenuButtons();
                    SinglePlayerOffice.IsHudHidden = true;
                    currentBuilding.TeleportMenu.Visible = true;
                }
            }
        }

        public override void Update() {
            base.Update();

            var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);
            if ((hours > 8 && hours < 17) && (MaleStaffInteraction.Ped == null && FemaleStaffInteraction.Ped == null)) {
                CreateMaleStaff();
                CreateFemaleStaff();
            }
            else if (hours < 9 || hours > 16) {
                MaleStaffInteraction.Ped?.Delete();
                FemaleStaffInteraction.Ped?.Delete();
            }

            BossInteraction.Update();
            PAInteraction.Update();
            MaleStaffInteraction.Update();
            FemaleStaffInteraction.Update();
            BuildingNameInteraction.Update();
            BossChairInteraction.Update();
            ComputerInteraction.Update();
            LaptopInteraction.Update();
            LeftSafeInteraction.Update();
            RadioInteraction.Update();
            RightSafeInteraction.Update();
            SofaInteraction.Update();
            SofaWithTVInteraction.Update();
            StaffChairInteraction.Update();
            TVInteration.Update();
            WardrobeInteraction.Update();
        }

        public override void Dispose() {
            BossInteraction.Dispose();
            PAInteraction.Dispose();
            MaleStaffInteraction.Dispose();
            FemaleStaffInteraction.Dispose();
            BuildingNameInteraction.Dispose();
            ComputerInteraction.Dispose();
            RadioInteraction.Dispose();
            SofaWithTVInteraction.Dispose();
            TVInteration.Dispose();
        }

    }
}
