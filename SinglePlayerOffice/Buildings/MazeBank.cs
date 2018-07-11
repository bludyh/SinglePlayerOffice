﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class MazeBank : Building {

        public MazeBank() {
            try {
                name = "Maze Bank Tower";
                description = "The building that defined the LS Skyline for a generation. Taller, Classier, more opulent, more shamelessly phallic, less structurally sound - you name it, the Maze Bank Tower is best in class.";
                price = 4000000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(name, "Owner", -1);
                blipPos = new Vector3(-79.214f, -796.513f, 44.227f);
                interiorIDs = new List<int>() { 257537, 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641, 254465, 254721, 254977, 255233 };
                exteriorIPLs = new List<string>() { "dt1_11_dt1_emissive_dt1_11", "dt1_11_dt1_tower" };
                entrance = new Entrance {
                    TriggerPos = new Vector3(-79.214f, -796.513f, 44.227f),
                    SpawnPos = new Vector3(-80.690f, -795.859f, 44.227f),
                    SpawnHeading = 94.827f,
                    PurchaseCamPos = new Vector3(-86.710f, -724.772f, 48f),
                    PurchaseCamRot = new Vector3(32f, 0f, 180f),
                    PurchaseCamFOV = 90f
                };
                garageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnHeading = -10f,
                    InteriorID = 257537,
                    ElevatorCamPos = new Vector3(-86.760f, -824.716f, 37.028f),
                    ElevatorCamRot = new Vector3(-10f, 0f, -45f),
                    ElevatorCamFOV = 60f
                };
                office = new Office {
                    TriggerPos = new Vector3(-75.771f, -827.188f, 243.386f),
                    SpawnPos = new Vector3(-76.042f, -825.297f, 243.386f),
                    SpawnHeading = 335.794f,
                    InteriorIDs = new List<int>() { 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641 },
                    PurchaseCamPos = new Vector3(-63.668f, -804.309f, 244.5f),
                    PurchaseCamRot = new Vector3(-5f, 0f, 111f),
                    PurchaseCamFOV = 60f,
                    InteriorStyles = new List<InteriorStyle>() {
                        new InteriorStyle("Executive Rich", 0, "ex_dt1_11_office_02b"),
                        new InteriorStyle("Executive Cool", 415000, "ex_dt1_11_office_02c"),
                        new InteriorStyle("Executive Contrast", 500000, "ex_dt1_11_office_02a"),
                        new InteriorStyle("Old Spice Classical", 685000, "ex_dt1_11_office_01b"),
                        new InteriorStyle("Old Spice Vintage", 760000, "ex_dt1_11_office_01c"),
                        new InteriorStyle("Old Spice Warms", 950000, "ex_dt1_11_office_01a"),
                        new InteriorStyle("Power Broker Conservative", 830000, "ex_dt1_11_office_03b"),
                        new InteriorStyle("Power Broker Polished", 910000, "ex_dt1_11_office_03c"),
                        new InteriorStyle("Power Broker Ice", 1000000, "ex_dt1_11_office_03a")
                    },
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(name, "HasExtraOfficeDecors", false)
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(name, "OfficeInteriorStyle"));
                garageOne = new Garage {
                    TriggerPos = new Vector3(-91.308f, -821.317f, 222.001f),
                    SpawnPos = new Vector3(-90.054f, -821.810f, 222.000f),
                    SpawnHeading = -110f,
                    IPL = "imp_dt1_11_cargarage_a",
                    InteriorID = 254465,
                    PurchaseCamPos = new Vector3(-90.054f, -821.810f, 224.000f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -110f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-89.055f, -831.615f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -60f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-88.599f, -828.209f, 222.000f),
                    LightingCamRot = new Vector3(55f, 0f, -160f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-82.111f, -817.941f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, -20f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-77.004f, -829.297f, 220.109f),
                    ElevatorLevelBPos = new Vector3(-77.004f, -829.297f, 225.459f),
                    ElevatorLevelCPos = new Vector3(-77.004f, -829.297f, 230.809f),
                    ElevatorRot = new Vector3(0f, 0f, 70.058f)
                };
                garageTwo = new Garage {
                    TriggerPos = new Vector3(-71.733f, -832.331f, 222.000f),
                    SpawnPos = new Vector3(-71.108f, -830.692f, 222.001f),
                    SpawnHeading = -20f,
                    IPL = "imp_dt1_11_cargarage_b",
                    InteriorID = 254721,
                    PurchaseCamPos = new Vector3(-71.108f, -830.692f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -20f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-61.260f, -829.552f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, 30f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-64.710f, -829.159f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, -70f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-75.044f, -822.666f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, 70f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-63.646f, -817.533f, 220.109f),
                    ElevatorLevelBPos = new Vector3(-63.646f, -817.533f, 225.459f),
                    ElevatorLevelCPos = new Vector3(-63.646f, -817.533f, 230.809f),
                    ElevatorRot = new Vector3(0f, 0f, 160f)
                };
                garageThree = new Garage {
                    TriggerPos = new Vector3(-78.528f, -805.686f, 222.001f),
                    SpawnPos = new Vector3(-79.146f, -807.408f, 222.001f),
                    SpawnHeading = 160f,
                    IPL = "imp_dt1_11_cargarage_c",
                    InteriorID = 254977,
                    PurchaseCamPos = new Vector3(-79.146f, -807.408f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 160f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-88.997f, -808.442f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -150f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-85.524f, -808.880f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, 110f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-75.209f, -815.338f, 222.000f),
                    NumberingCamRot = new Vector3(12f, 0f, -110f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-86.653f, -820.463f, 220.109f),
                    ElevatorLevelBPos = new Vector3(-86.653f, -820.463f, 225.459f),
                    ElevatorLevelCPos = new Vector3(-86.653f, -820.463f, 230.809f),
                    ElevatorRot = new Vector3(0f, 0f, -20f)
                };
                modShop = new ModShop {
                    TriggerPos = new Vector3(-68.824f, -814.253f, 285.000f),
                    SpawnPos = new Vector3(-69.980f, -813.785f, 285.000f),
                    SpawnHeading = 66.700f,
                    IPL = "imp_dt1_11_modgarage",
                    InteriorID = 255233,
                    PurchaseCamPos = new Vector3(-72.767f, -814.608f, 287f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 160f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(name, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    TriggerPos = new Vector3(-67.784f, -821.609f, 321.287f),
                    SpawnPos = new Vector3(-65.938f, -822.145f, 321.285f),
                    SpawnHeading = 253.635f
                };

                //officeSofaPos = new Vector3(-68.486f, -804.237f, 242.386f);
                //officeSofaRot = new Vector3(0f, 0f, 340f);
                //officeSofaStartPos = new Vector3(-68.700f, -804.797f, 243.386f);
                //officeSofaStartHeading = 160f;
                //officeTVTrigger = new Vector3(-70.083f, -808.826f, 243.801f);
                //officeComputerChairPos = new Vector3(-80.645f, -801.3f, 243.006f);
                //officeComputerChairRot = new Vector3(0f, 0f, 25f);
                //officeLeftSafeTrigger = new Vector3(-83.064f, -802.753f, 243.386f);
                //officeLeftSafeStartPos = new Vector3(-81.764f, -803.244f, 243.386f);
                //officeLeftSafeStartHeading = 70f;
                //officeRightSafeTrigger = new Vector3(-81.779f, -799.318f, 243.386f);
                //officeRightSafeStartPos = new Vector3(-80.329f, -799.799f, 243.386f);
                //officeRightSafeStartHeading = 70f;
                //officeRadioTrigger = new Vector3(-79.807f, -805.420f, 243.386f);
                //officeRadioHeading = 158.076f;
                //officeBossChairTrigger = new Vector3(-59.807f, -805.983f, 243.386f);
                //officeBossChairHeading = 159.971f;
                //officeStaffChairTriggers = new List<Vector3>() {
                //new Vector3(-58.570f, -808.573f, 243.386f),
                //    new Vector3(-59.061f, -809.654f, 243.386f),
                //    new Vector3(-59.457f, -810.764f, 243.386f),
                //    new Vector3(-62.394f, -807.197f, 243.386f),
                //    new Vector3(-62.813f, -808.352f, 243.386f),
                //    new Vector3(-63.273f, -809.462f, 243.386f)


                //};
                //officeStaffChairHeadings = new List<float>() { 70.543f, 69.427f, 70.151f, 248.893f, 250.610f, 249.552f };
                //officeLaptopChairTriggers = new List<Vector3>() {
                //    new Vector3(-63.980f, -813.590f, 243.386f),
                //    new Vector3(-64.423f, -815.084f, 243.386f),
                //    new Vector3(-64.970f, -816.533f, 243.386f)
                //};
                //officeLaptopChairHeadings = new List<float>() { 69.990f, 70.530f, 70.000f };
                //officeWardrobeTrigger = new Vector3(-78.625f, -812.353f, 243.386f);
                //officeWardrobeHeading = 70.638f;
                //officeWardrobeCamPos = new Vector3(-80.053f, -811.888f, 243.386f);
                //officeWardrobeCamRot = new Vector3(0f, 0f, 249.754f);
                //officeWardrobeCamFOV = 75f;
                //officePaChairPos = new Vector3(-72.069f, -814.251f, 242.988f);
                //officePaChairRot = new Vector3(0f, 0f, -20);

                CreateBuildingBlip();
                CreatePurchaseMenu();
                CreateTeleportMenu();
                CreateGarageEntranceMenu();
                CreateVehicleElevatorMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }
}
