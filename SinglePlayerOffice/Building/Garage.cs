using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Garage : Location, IInterior {

        private static VehicleInfo vehicleInfo;
        private static Scaleform vehicleInfoScaleform;

        public static List<GarageDecorationStyle> DecorationStyles { get; set; }
        public static List<GarageLightingStyle> LightingStyles { get; set; }
        public static List<GarageNumberingStyle> NumberingStylesGarageOne { get; set; }
        public static List<GarageNumberingStyle> NumberingStylesGarageTwo { get; set; }
        public static List<GarageNumberingStyle> NumberingStylesGarageThree { get; set; }

        private Prop elevator;
        private Prop elevatorPlatform;
        private Vector3 elevatorGateInitialPos;
        private Prop elevatorGate;

        public string IPL { get; set; }
        public int InteriorID { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }
        public Vector3 DecorationCamPos { get; set; }
        public Vector3 DecorationCamRot { get; set; }
        public float DecorationCamFOV { get; set; }
        public Camera DecorationCam { get; set; }
        public GarageDecorationStyle DecorationStyle { get; set; }
        public Vector3 LightingCamPos { get; set; }
        public Vector3 LightingCamRot { get; set; }
        public float LightingCamFOV { get; set; }
        public Camera LightingCam { get; set; }
        public GarageLightingStyle LightingStyle { get; set; }
        public Vector3 NumberingCamPos { get; set; }
        public Vector3 NumberingCamRot { get; set; }
        public float NumberingCamFOV { get; set; }
        public Camera NumberingCam { get; set; }
        public GarageNumberingStyle NumberingStyle { get; set; }
        public Vector3 ElevatorLevelAPos { get; set; }
        public Vector3 ElevatorLevelBPos { get; set; }
        public Vector3 ElevatorLevelCPos { get; set; }
        public Vector3 ElevatorPos { get; set; }
        public Vector3 ElevatorRot { get; set; }
        public bool IsElevatorCreated { get; set; }
        public int ElevatorStatus { get; set; }

        static Garage() {
            DecorationStyles = new List<GarageDecorationStyle> {
                new GarageDecorationStyle("Decoration 1", 0, "garage_decor_01"),
                new GarageDecorationStyle("Decoration 2", 285000, "garage_decor_02"),
                new GarageDecorationStyle("Decoration 3", 415000, "garage_decor_04"),
                new GarageDecorationStyle("Decoration 4", 500000, "garage_decor_03")
            };
            LightingStyles = new List<GarageLightingStyle> {
                new GarageLightingStyle("Lighting 1", 0, "lighting_option01"),
                new GarageLightingStyle("Lighting 2", 81500, "lighting_option04"),
                new GarageLightingStyle("Lighting 3", 85000, "lighting_option03"),
                new GarageLightingStyle("Lighting 4", 87500, "lighting_option07"),
                new GarageLightingStyle("Lighting 5", 92500, "lighting_option06"),
                new GarageLightingStyle("Lighting 6", 99500, "lighting_option05"),
                new GarageLightingStyle("Lighting 7", 105000, "lighting_option08"),
                new GarageLightingStyle("Lighting 8", 127500, "lighting_option02"),
                new GarageLightingStyle("Lighting 9", 150000, "lighting_option09")
            };
            NumberingStylesGarageOne = new List<GarageNumberingStyle> {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n1"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n1"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n1"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n1"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n1"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n1"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n1"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n1"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n1"),
            };
            NumberingStylesGarageTwo = new List<GarageNumberingStyle> {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n2"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n2"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n2"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n2"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n2"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n2"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n2"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n2"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n2"),
            };
            NumberingStylesGarageThree = new List<GarageNumberingStyle> {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n3"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n3"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n3"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n3"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n3"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n3"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n3"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n3"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n3"),
            };
        }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (GarageDecorationStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageLightingStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, DecorationStyle.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, LightingStyle.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, NumberingStyle.PropName);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void LoadInterior(GarageDecorationStyle decorationStyle, GarageLightingStyle lightingStyle, GarageNumberingStyle numberingStyle) {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (GarageDecorationStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageLightingStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decorationStyle.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, lightingStyle.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, numberingStyle.PropName);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void ChangeDecorationStyle(GarageDecorationStyle decorationStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (GarageDecorationStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decorationStyle.PropName);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void ChangeLightingStyle(GarageLightingStyle lightingStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (GarageLightingStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, lightingStyle.PropName);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void ChangeNumberingStyle(GarageNumberingStyle numberingStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (GarageNumberingStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            foreach (GarageNumberingStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, numberingStyle.PropName);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void CreateElevator() {
            var model = new Model("imp_prop_ie_carelev01");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                elevator = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
            }
            model.MarkAsNoLongerNeeded();
            model = new Model("imp_prop_ie_carelev02");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                elevatorPlatform = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
                elevatorPlatform.Position = ElevatorLevelAPos;
                elevatorPlatform.Rotation = ElevatorRot;
                elevatorPlatform.FreezePosition = true;
            }
            model.MarkAsNoLongerNeeded();
            elevator.AttachTo(elevatorPlatform, 0);
            IsElevatorCreated = true;
        }

        public void DeleteElevator() {
            if (elevator != null) elevator.Delete();
            if (elevatorPlatform != null) elevatorPlatform.Delete();
            IsElevatorCreated = false;
        }

        private bool MoveElevator(Vector3 pos) {
            if (elevatorPlatform.Position.Z < pos.Z) elevatorPlatform.Position = Vector3.Add(elevatorPlatform.Position, new Vector3(0f, 0f, 0.005f));
            else elevatorPlatform.Position = Vector3.Add(elevatorPlatform.Position, new Vector3(0f, 0f, -0.005f));
            if (elevatorPlatform.Position.DistanceTo(pos) < 0.01f) return false;
            return true;
        }

        private bool MoveElevatorGate(Vector3 pos) {
            if (elevatorGate.Position.Z < pos.Z) elevatorGate.Position = Vector3.Add(elevatorGate.Position, new Vector3(0f, 0f, 0.01f));
            else elevatorGate.Position = Vector3.Add(elevatorGate.Position, new Vector3(0f, 0f, -0.01f));
            if (elevatorGate.Position.DistanceTo(pos) < 0.01f) return false;
            return true;
        }

        public Vector3 GetCurrentLevelElevatorPos() {
            if (Game.Player.Character.Position.Z > ElevatorLevelAPos.Z && Game.Player.Character.Position.Z < ElevatorLevelBPos.Z) return ElevatorLevelAPos;
            else if (Game.Player.Character.Position.Z > ElevatorLevelBPos.Z && Game.Player.Character.Position.Z < ElevatorLevelCPos.Z) return ElevatorLevelBPos;
            else if (Game.Player.Character.Position.Z > ElevatorLevelCPos.Z) return ElevatorLevelCPos;
            return Vector3.Zero;
        }

        protected override void TeleportOnTick() {
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                    Game.Player.Character.Task.StandStill(-1);
                    building.UpdateTeleportMenuButtons();
                    SinglePlayerOffice.IsHudHidden = true;
                    building.TeleportMenu.Visible = true;
                }
            }
        }

        private void ElevatorOnTick() {
            switch (ElevatorStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && (Game.Player.Character.Position.DistanceTo(ElevatorLevelAPos) < 8f || Game.Player.Character.Position.DistanceTo(ElevatorLevelBPos) < 8f || Game.Player.Character.Position.DistanceTo(ElevatorLevelCPos) < 8f) && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the vehicle elevator");
                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                            ElevatorPos = GetCurrentLevelElevatorPos();
                            ElevatorStatus = 3;
                        }
                    }
                    break;
                case 1:
                    Game.Player.Character.CurrentVehicle.IsPersistent = true;
                    elevatorPlatform.Position = elevatorPlatform.GetOffsetInWorldCoords(new Vector3(0f, 0f, -1f));
                    ElevatorStatus = 2;
                    break;
                case 2:
                    if (MoveElevator(ElevatorLevelAPos)) {
                        Game.Player.Character.CurrentVehicle.Position = elevatorPlatform.GetOffsetInWorldCoords(new Vector3(0f, 0f, 1.3f));
                        Game.Player.Character.CurrentVehicle.Rotation = Vector3.Add(Game.Player.Character.CurrentVehicle.Rotation, new Vector3(0f, 0f, 0.2f));
                    }
                    else {
                        ElevatorPos = GetCurrentLevelElevatorPos();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Task.ClearAll();
                        if (this == building.GarageOne) Audio.PlaySoundFrontend("Speech_Floor_1", "DLC_IE_Garage_Elevator_Sounds");
                        else if (this == building.GarageTwo) Audio.PlaySoundFrontend("Speech_Floor_2", "DLC_IE_Garage_Elevator_Sounds");
                        else if (this == building.GarageThree) Audio.PlaySoundFrontend("Speech_Floor_3", "DLC_IE_Garage_Elevator_Sounds");
                        ElevatorStatus = 3;
                    }
                    break;
                case 3:
                    if (elevatorGate != null) MoveElevatorGate(elevatorGateInitialPos);
                    if (!MoveElevator(ElevatorPos)) {
                        var prop = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, elevatorPlatform.Position.X, elevatorPlatform.Position.Y, elevatorPlatform.Position.Z, 2f, -2088725999, 0, 0, 0);
                        if (prop.Model.Hash == -2088725999) {
                            elevatorGate = prop;
                            elevatorGateInitialPos = elevatorGate.Position;
                        }
                        ElevatorStatus = 4;
                    }
                    break;
                case 4:
                    if (!MoveElevatorGate(Vector3.Add(elevatorGateInitialPos, new Vector3(0f, 0f, 3f)))) {
                        building.UpdateVehicleElevatorMenuButtons();
                        SinglePlayerOffice.IsHudHidden = true;
                        building.VehicleElevatorMenu.Visible = true;
                        ElevatorStatus = 0;
                    }
                    break;
            }
        }

        private void VehicleInfoOnTick() {
            var vehicle = World.GetClosestVehicle(Game.Player.Character.Position, 3f);
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && vehicle != null) {
                if (vehicleInfo == null || vehicle != vehicleInfo.Vehicle) {
                    vehicleInfo = new VehicleInfo(vehicle);
                    if (vehicleInfoScaleform != null) {
                        vehicleInfoScaleform.Dispose();
                        Script.Wait(0);
                    }
                    vehicleInfoScaleform = new Scaleform("MP_CAR_STATS_01");
                    while (!vehicleInfoScaleform.IsLoaded) Script.Wait(0);
                    Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, vehicleInfoScaleform.Handle, "SET_VEHICLE_INFOR_AND_STATS");
                    Function.Call(Hash._0x80338406F3475E55, "FM_TWO_STRINGS");
                    Function.Call(Hash._0xC63CD5D2920ACBE7, vehicleInfo.GetBrandName(true));
                    Function.Call(Hash._0xC63CD5D2920ACBE7, vehicleInfo.Vehicle.DisplayName);
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._0x80338406F3475E55, "MP_PROP_CAR0");
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._0xE83A3E3557A56640, vehicleInfo.GetLogoTextureDict());
                    Function.Call(Hash._0xE83A3E3557A56640, vehicleInfo.GetBrandName(false));
                    Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_0");
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_1");
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_2");
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_3");
                    Function.Call(Hash._0x362E2D3FE93A9959);
                    Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Function.Call<int>(Hash.ROUND, vehicleInfo.GetMaxSpeedInPercentage()));
                    Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Function.Call<int>(Hash.ROUND, vehicleInfo.GetMaxAccelerationInPercentage()));
                    Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Function.Call<int>(Hash.ROUND, vehicleInfo.GetMaxBrakingInPercentage()));
                    Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, Function.Call<int>(Hash.ROUND, vehicleInfo.GetMaxTractionInPercentage()));
                    Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                }
                Vector3 pos = vehicle.Position + new Vector3(0f, 0f, vehicle.Model.GetDimensions().Z + 2f);
                Vector3 camCoord = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);
                Vector3 rot = new Vector3(0f, 0f, (180f - Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, camCoord.X - pos.X, camCoord.Y - pos.Y)));
                Vector3 scale = new Vector3(8f, 4.5f, 1f);
                vehicleInfoScaleform.Render3D(pos, rot, scale);
            }
            else vehicleInfo = null;
        }

        public override void OnTick() {
            Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
            if (building == null) building = SinglePlayerOffice.GetCurrentBuilding();
            building.HideBuildingExteriors();
            TeleportOnTick();
            ElevatorOnTick();
            VehicleInfoOnTick();
        }

        public void Dispose() {
            if (elevator != null) elevator.Delete();
            if (elevatorPlatform != null) elevatorPlatform.Delete();
            if (vehicleInfoScaleform != null) vehicleInfoScaleform.Dispose();
        }

    }
}
