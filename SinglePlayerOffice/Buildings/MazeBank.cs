using System;
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
                Name = "Maze Bank Tower";
                description = "The building that defined the LS Skyline for a generation. Taller, Classier, more opulent, more shamelessly phallic, less structurally sound - you name it, the Maze Bank Tower is best in class.";
                price = 4000000;
                InteriorIDs = new List<int>() { 257537, 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641, 254465, 254721, 254977, 255233 };
                exteriorMapObjects = new List<string>() { "dt1_11_dt1_emissive_dt1_11", "dt1_11_dt1_tower" };
                data = ScriptSettings.Load(String.Format(@"scripts\SinglePlayerOffice\{0}\data.ini", Name));
                Owner = (Owner)data.GetValue("Owner", "Owner", -1);
                Entrance = new Entrance {
                    TriggerPos = new Vector3(-79.214f, -796.513f, 44.227f),
                    SpawnPos = new Vector3(-80.690f, -795.859f, 44.227f),
                    SpawnHeading = 94.827f,
                };
                GarageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnHeading = -10f,
                    InteriorID = 257537,
                    ElevatorCamPos = new Vector3(-86.760f, -824.716f, 37.028f),
                    ElevatorCamRot = new Vector3(-10f, 0f, -45f),
                    ElevatorCamFOV = 60f
                };
                Office = new Office {
                    TriggerPos = new Vector3(-75.771f, -827.188f, 243.386f),
                    SpawnPos = new Vector3(-76.042f, -825.297f, 243.386f),
                    SpawnHeading = 335.794f,
                    ExteriorIPLs = new List<string> { "hei_dt1_11_exshadowmesh" },
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
                    HasExtraDecors = data.GetValue("Interiors", "HasExtraOfficeDecors", false),
                    Scene = new OfficeScene {
                        BossChairPos = new Vector3(-60.116f, -806.817f, 242.982f),
                        StaffChairPosList = new List<Vector3> {
                            new Vector3(-61.479f, -807.526f, 242.982f),
                            new Vector3(-61.895f, -808.669f, 242.982f),
                            new Vector3(-62.296f, -809.771f, 242.982f),
                            new Vector3(-59.472f, -808.292f, 242.982f),
                            new Vector3(-59.879f, -809.410f, 242.982f),
                            new Vector3(-60.289f, -810.537f, 242.982f)
                        },
                        PaChairPos = new Vector3(-72.069f, -814.251f, 242.988f),
                        PaChairRot = new Vector3(0f, 0f, -20)
                    }
                };
                Office.InteriorStyle = GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.ExtraDecorsPrice = (Office.HasExtraDecors) ? 1650000 : 0;
                Office.ActiveScenes.Add(Office.Scene);
                GarageOne = new Garage {
                    TriggerPos = new Vector3(-91.308f, -821.317f, 222.001f),
                    SpawnPos = new Vector3(-90.054f, -821.810f, 222.000f),
                    SpawnHeading = -110f,
                    IPL = "imp_dt1_11_cargarage_a",
                    ExteriorIPLs = new List<string> { "hei_dt1_11_impexpproxy_a" },
                    InteriorID = 254465,
                    PurchaseCamPos = new Vector3(-90.054f, -821.810f, 224.000f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -110f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-89.055f, -831.615f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -60f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-88.599f, -828.209f, 222.000f),
                    LightingCamRot = new Vector3(55f, 0f, -160f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-82.111f, -817.941f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, -20f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-77.004f, -829.297f, 220.109f),
                        ElevatorLevelBPos = new Vector3(-77.004f, -829.297f, 225.459f),
                        ElevatorLevelCPos = new Vector3(-77.004f, -829.297f, 230.809f),
                        ElevatorRot = new Vector3(0f, 0f, 70.058f)
                    }
                };
                GarageOne.ActiveScenes.Add(GarageOne.Scene);
                GarageTwo = new Garage {
                    TriggerPos = new Vector3(-71.733f, -832.331f, 222.000f),
                    SpawnPos = new Vector3(-71.108f, -830.692f, 222.001f),
                    SpawnHeading = -20f,
                    IPL = "imp_dt1_11_cargarage_b",
                    ExteriorIPLs = new List<string> { "hei_dt1_11_impexpproxy_b" },
                    InteriorID = 254721,
                    PurchaseCamPos = new Vector3(-71.108f, -830.692f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -20f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-61.260f, -829.552f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, 30f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-64.710f, -829.159f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, -70f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-75.044f, -822.666f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, 70f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-63.646f, -817.533f, 220.109f),
                        ElevatorLevelBPos = new Vector3(-63.646f, -817.533f, 225.459f),
                        ElevatorLevelCPos = new Vector3(-63.646f, -817.533f, 230.809f),
                        ElevatorRot = new Vector3(0f, 0f, 160f)
                    }
                };
                GarageTwo.ActiveScenes.Add(GarageTwo.Scene);
                GarageThree = new Garage {
                    TriggerPos = new Vector3(-78.528f, -805.686f, 222.001f),
                    SpawnPos = new Vector3(-79.146f, -807.408f, 222.001f),
                    SpawnHeading = 160f,
                    IPL = "imp_dt1_11_cargarage_c",
                    ExteriorIPLs = new List<string> { "hei_dt1_11_impexpproxy_c" },
                    InteriorID = 254977,
                    PurchaseCamPos = new Vector3(-79.146f, -807.408f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 160f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-88.997f, -808.442f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -150f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-85.524f, -808.880f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, 110f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-75.209f, -815.338f, 222.000f),
                    NumberingCamRot = new Vector3(12f, 0f, -110f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-86.653f, -820.463f, 220.109f),
                        ElevatorLevelBPos = new Vector3(-86.653f, -820.463f, 225.459f),
                        ElevatorLevelCPos = new Vector3(-86.653f, -820.463f, 230.809f),
                        ElevatorRot = new Vector3(0f, 0f, -20f)
                    }
                };
                GarageThree.ActiveScenes.Add(GarageThree.Scene);
                ModShop = new ModShop {
                    TriggerPos = new Vector3(-68.824f, -814.253f, 285.000f),
                    SpawnPos = new Vector3(-69.980f, -813.785f, 285.000f),
                    SpawnHeading = 66.700f,
                    IPL = "imp_dt1_11_modgarage",
                    ExteriorIPLs = new List<string> { "hei_dt1_11_impexpproxy_modshop" },
                    InteriorID = 255233,
                    PurchaseCamPos = new Vector3(-72.767f, -814.608f, 287f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 160f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))
                };
                HeliPad = new HeliPad {
                    TriggerPos = new Vector3(-67.784f, -821.609f, 321.287f),
                    SpawnPos = new Vector3(-65.938f, -822.145f, 321.285f),
                    SpawnHeading = 253.635f
                };
                PurchaseCamPos = new Vector3(-86.710f, -724.772f, 48f);
                PurchaseCamRot = new Vector3(32f, 0f, 180f);
                PurchaseCamFOV = 90f;

                CreateEntranceBlip();
                if (Owner != Owner.None) CreateGarageEntranceBlip();
                CreatePurchaseMenu();
                CreateTeleportMenu();
                CreateGarageEntranceMenu();
                CreateVehicleElevatorMenu();
                CreatePAMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }
}
