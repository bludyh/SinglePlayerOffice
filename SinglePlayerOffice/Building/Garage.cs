using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Interactions;
using SinglePlayerOffice.Vehicles;

namespace SinglePlayerOffice.Buildings {
    class Garage : Location, IInterior {

        public static List<InteriorStyle> DecorationStyles { get; set; }
        public static List<InteriorStyle> LightingStyles { get; set; }
        public static List<InteriorStyle> NumberingStylesGarageOne { get; set; }
        public static List<InteriorStyle> NumberingStylesGarageTwo { get; set; }
        public static List<InteriorStyle> NumberingStylesGarageThree { get; set; }

        private List<VehicleInfo> vehicleInfoList;

        public string IPL { get; set; }
        public List<string> ExteriorIPLs { get; set; }
        public int InteriorID { get; set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }
        public Vector3 DecorationCamPos { get; set; }
        public Vector3 DecorationCamRot { get; set; }
        public float DecorationCamFOV { get; set; }
        public Camera DecorationCam { get; set; }
        public InteriorStyle DecorationStyle { get; set; }
        public Vector3 LightingCamPos { get; set; }
        public Vector3 LightingCamRot { get; set; }
        public float LightingCamFOV { get; set; }
        public Camera LightingCam { get; set; }
        public InteriorStyle LightingStyle { get; set; }
        public Vector3 NumberingCamPos { get; set; }
        public Vector3 NumberingCamRot { get; set; }
        public float NumberingCamFOV { get; set; }
        public Camera NumberingCam { get; set; }
        public InteriorStyle NumberingStyle { get; set; }
        public VehicleElevator VehicleElevatorInteraction { get; set; }
        public VehicleInfo VehicleInfoInteraction { get; private set; }

        static Garage() {
            DecorationStyles = new List<InteriorStyle> {
                new InteriorStyle("Decoration 1", 0, "garage_decor_01"),
                new InteriorStyle("Decoration 2", 285000, "garage_decor_02"),
                new InteriorStyle("Decoration 3", 415000, "garage_decor_04"),
                new InteriorStyle("Decoration 4", 500000, "garage_decor_03")
            };
            LightingStyles = new List<InteriorStyle> {
                new InteriorStyle("Lighting 1", 0, "lighting_option01"),
                new InteriorStyle("Lighting 2", 81500, "lighting_option04"),
                new InteriorStyle("Lighting 3", 85000, "lighting_option03"),
                new InteriorStyle("Lighting 4", 87500, "lighting_option07"),
                new InteriorStyle("Lighting 5", 92500, "lighting_option06"),
                new InteriorStyle("Lighting 6", 99500, "lighting_option05"),
                new InteriorStyle("Lighting 7", 105000, "lighting_option08"),
                new InteriorStyle("Lighting 8", 127500, "lighting_option02"),
                new InteriorStyle("Lighting 9", 150000, "lighting_option09")
            };
            NumberingStylesGarageOne = new List<InteriorStyle> {
                new InteriorStyle("Signage 1", 0, "numbering_style07_n1"),
                new InteriorStyle("Signage 2", 62500, "numbering_style08_n1"),
                new InteriorStyle("Signage 3", 75000, "numbering_style09_n1"),
                new InteriorStyle("Signage 4", 87500, "numbering_style02_n1"),
                new InteriorStyle("Signage 5", 100000, "numbering_style03_n1"),
                new InteriorStyle("Signage 6", 132500, "numbering_style01_n1"),
                new InteriorStyle("Signage 7", 165000, "numbering_style06_n1"),
                new InteriorStyle("Signage 8", 197500, "numbering_style05_n1"),
                new InteriorStyle("Signage 9", 250000, "numbering_style04_n1"),
            };
            NumberingStylesGarageTwo = new List<InteriorStyle> {
                new InteriorStyle("Signage 1", 0, "numbering_style07_n2"),
                new InteriorStyle("Signage 2", 62500, "numbering_style08_n2"),
                new InteriorStyle("Signage 3", 75000, "numbering_style09_n2"),
                new InteriorStyle("Signage 4", 87500, "numbering_style02_n2"),
                new InteriorStyle("Signage 5", 100000, "numbering_style03_n2"),
                new InteriorStyle("Signage 6", 132500, "numbering_style01_n2"),
                new InteriorStyle("Signage 7", 165000, "numbering_style06_n2"),
                new InteriorStyle("Signage 8", 197500, "numbering_style05_n2"),
                new InteriorStyle("Signage 9", 250000, "numbering_style04_n2"),
            };
            NumberingStylesGarageThree = new List<InteriorStyle> {
                new InteriorStyle("Signage 1", 0, "numbering_style07_n3"),
                new InteriorStyle("Signage 2", 62500, "numbering_style08_n3"),
                new InteriorStyle("Signage 3", 75000, "numbering_style09_n3"),
                new InteriorStyle("Signage 4", 87500, "numbering_style02_n3"),
                new InteriorStyle("Signage 5", 100000, "numbering_style03_n3"),
                new InteriorStyle("Signage 6", 132500, "numbering_style01_n3"),
                new InteriorStyle("Signage 7", 165000, "numbering_style06_n3"),
                new InteriorStyle("Signage 8", 197500, "numbering_style05_n3"),
                new InteriorStyle("Signage 9", 250000, "numbering_style04_n3"),
            };
        }

        public Garage() {
            vehicleInfoList = new List<VehicleInfo>();
            VehicleInfoInteraction = new Interactions.VehicleInfoScaleform();
        }

        public void LoadInterior() {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, DecorationStyle.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, LightingStyle.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, NumberingStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void LoadInterior(InteriorStyle decorationStyle, InteriorStyle lightingStyle, InteriorStyle numberingStyle) {
            Function.Call(Hash.REQUEST_IPL, IPL);
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decorationStyle.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, lightingStyle.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, numberingStyle.Style);
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

        public void ChangeDecorationStyle(InteriorStyle decorationStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in DecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decorationStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void ChangeLightingStyle(InteriorStyle lightingStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in LightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, lightingStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        public void ChangeNumberingStyle(InteriorStyle numberingStyle) {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            foreach (InteriorStyle style in NumberingStylesGarageOne) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageTwo) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            foreach (InteriorStyle style in NumberingStylesGarageThree) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.Style);
            Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, numberingStyle.Style);
            Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
        }

        private void CreateElevator() {
            VehicleElevatorInteraction.Elevator = new Elevator();
            var elevator = VehicleElevatorInteraction.Elevator;
            var model = new Model("imp_prop_ie_carelev01");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                elevator.Base = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
            }
            model.MarkAsNoLongerNeeded();
            model = new Model("imp_prop_ie_carelev02");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                elevator.Platform = World.CreateProp(model, Vector3.Zero, Vector3.Zero, false, false);
                elevator.Platform.Position = VehicleElevatorInteraction.LevelAPos;
                elevator.Platform.Rotation = VehicleElevatorInteraction.ElevatorRot;
                elevator.Platform.FreezePosition = true;
            }
            model.MarkAsNoLongerNeeded();
            elevator.Base.AttachTo(elevator.Platform, 0);
        }

        private void DeleteElevator() {
            var elevator = VehicleElevatorInteraction.Elevator;
            elevator?.Base.Delete();
            elevator?.Platform.Delete();
            elevator = null;
        }

        public void AddVehicleInfo(Vehicle vehicle) {
            vehicle.IsPersistent = true;
            var info = new VehicleInfo(vehicle);
            vehicleInfoList.Add(info);
        }

        public void RemoveVehicleInfo(Vehicle vehicle) {
            foreach (var vehicleInfo in vehicleInfoList) {
                if (vehicleInfo.Vehicle == vehicle) {
                    vehicleInfoList.Remove(vehicleInfo);
                    break;
                }
            }
        }

        private string GetGarageFileName() {
            var currentBuilding = Utilities.CurrentBuilding;

            if (this == currentBuilding.GarageOne)
                return "GarageOne.xml";
            if (this == currentBuilding.GarageTwo)
                return "GarageTwo.xml";
            if (this == currentBuilding.GarageThree)
                return "GarageThree.xml";

            return null;
        }

        private void SaveVehicleInfoList() {
            var currentBuilding = Utilities.CurrentBuilding;

            foreach (var vehicleInfo in vehicleInfoList) {
                vehicleInfo.Position = vehicleInfo.Vehicle.Position;
                vehicleInfo.Rotation = vehicleInfo.Vehicle.Rotation;
            }

            var serializer = new XmlSerializer(typeof(List<VehicleInfo>));
            var fileName = GetGarageFileName();
            var writer = new StreamWriter($@"scripts\SinglePlayerOffice\{ currentBuilding.Name }\{ fileName }");
            serializer.Serialize(writer, vehicleInfoList);
            writer.Close();
        }

        private void LoadVehicleInfoList() {
            var currentBuilding = Utilities.CurrentBuilding;

            var serializer = new XmlSerializer(typeof(List<VehicleInfo>));
            var fileName = GetGarageFileName();
            var reader = new StreamReader($@"scripts\SinglePlayerOffice\{ currentBuilding.Name }\{ fileName }");
            vehicleInfoList = (List<VehicleInfo>)serializer.Deserialize(reader);
            reader.Close();
        }

        private void CreateVehicles() {
            LoadVehicleInfoList();

            foreach (var vehicleInfo in vehicleInfoList)
                vehicleInfo.CreateVehicle();
        }

        private void DeleteVehicles() {
            foreach (var vehicleInfo in vehicleInfoList)
                vehicleInfo.DeleteVehicle();
        }

        public override void OnLocationArrived() {
            CreateElevator();
            CreateVehicles();
        }

        public override void OnLocationLeft() {
            SaveVehicleInfoList();
            DeleteElevator();
            DeleteVehicles();
        }

        protected override void HandleTrigger() {
            var building = Utilities.CurrentBuilding;

            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(TriggerPos) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                    Game.Player.Character.Task.StandStill(-1);
                    building.UpdateTeleportMenuButtons();
                    SinglePlayerOffice.IsHudHidden = true;
                    building.TeleportMenu.Visible = true;
                }
            }
        }

        public override void Update() {
            base.Update();

            VehicleElevatorInteraction.Update();
            VehicleInfoInteraction.Update();
        }

        public override void Dispose() {
            DeleteElevator();
            DeleteVehicles();
            VehicleInfoInteraction.Dispose();
        }

    }
}
