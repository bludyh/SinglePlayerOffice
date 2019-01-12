using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {
    internal class MazeBank : Building {
        public MazeBank() {
            try {
                Name = "Maze Bank Tower";
                data = ScriptSettings.Load($@"scripts\SinglePlayerOffice\{Name}\data.ini");
                Description =
                    "The building that defined the LS Skyline for a generation. Taller, Classier, more opulent, more shamelessly phallic, less structurally sound - you name it, the Maze Bank Tower is best in class.";
                Price = 4000000;
                InteriorIDs = new List<int> {
                    257537, 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641, 254465, 254721,
                    254977, 255233
                };
                exteriorMapObjects = new List<string> { "dt1_11_dt1_emissive_dt1_11", "dt1_11_dt1_tower" };
                Owner = (Owner) data.GetValue("Owner", "Owner", -1);
                Entrance = new Entrance {
                    TriggerPos = new Vector3(-79.214f, -796.513f, 44.227f),
                    SpawnPos = new Vector3(-80.690f, -795.859f, 44.227f),
                    SpawnHeading = 94.827f
                };
                GarageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnPos = new Vector3(-84.243f, -821.827f, 35.665f),
                    SpawnHeading = -10f,
                    InteriorId = 257537,
                    VehicleElevatorEntrance = new VehicleElevatorEntrance(
                        new Vector3(-86.760f, -824.716f, 37.028f),
                        new Vector3(-10f, 0f, -45f),
                        60f)
                };
                Office = new Office {
                    TriggerPos = new Vector3(-75.771f, -827.188f, 243.386f),
                    SpawnPos = new Vector3(-76.042f, -825.297f, 243.386f),
                    SpawnHeading = 335.794f,
                    ExteriorIpLs = new List<string> { "hei_dt1_11_exshadowmesh" },
                    InteriorIDs =
                        new List<int> { 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641 },
                    PurchaseCamPos = new Vector3(-63.668f, -804.309f, 244.5f),
                    PurchaseCamRot = new Vector3(-5f, 0f, 111f),
                    PurchaseCamFov = 60f,
                    InteriorStyles =
                        new List<InteriorStyle> {
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
                    Boss = new Boss(new Vector3(-60.116f, -806.817f, 242.982f)),
                    Pa = new Pa(new Vector3(-72.069f, -814.251f, 242.988f), new Vector3(0f, 0f, -20)),
                    Staffs =
                        new List<Staff> {
                            new Staff(PedHash.Business01AMM, new Vector3(-60.289f, -810.537f, 242.982f)),
                            new Staff(PedHash.Business03AMY, new Vector3(-62.296f, -809.771f, 242.982f)),
                            new Staff(PedHash.Business01AFY, new Vector3(-59.879f, -809.410f, 242.982f)),
                            new Staff(PedHash.Business04AFY, new Vector3(-61.479f, -807.526f, 242.982f))
                        },
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-71.572f, -806.881f, 242.395f), new Vector3(0f, 0f, 110f)),
                        new Sofa(new Vector3(-67.666f, -808.398f, 242.395f), new Vector3(0f, 0f, -155f)),
                        new Sofa(new Vector3(-66.094f, -822.530f, 242.395f), new Vector3(0f, 0f, 160f)),
                        new Sofa(new Vector3(-63.468f, -822.204f, 242.395f), new Vector3(0f, 0f, -110f)),
                        new Sofa(new Vector3(-62.976f, -820.828f, 242.395f), new Vector3(0f, 0f, -110f))
                    },
                    Wardrobe = new Wardrobe(new Vector3(-78.625f, -812.353f, 243.386f), new Vector3(0f, 0f, 70f))
                };
                Office.InteriorStyle =
                    GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.ExtraDecorsPrice = Office.HasExtraDecors ? 1650000 : 0;
                Office.SofaAndTv = new SofaAndTv(Office.Tv, new Vector3(-68.486f, -804.237f, 242.386f),
                    new Vector3(0f, 0f, -20f));
                GarageOne = new Garage {
                    TriggerPos = new Vector3(-91.308f, -821.317f, 222.001f),
                    SpawnPos = new Vector3(-90.054f, -821.810f, 222.000f),
                    SpawnHeading = -110f,
                    Ipl = "imp_dt1_11_cargarage_a",
                    ExteriorIpLs = new List<string> { "hei_dt1_11_impexpproxy_a" },
                    InteriorId = 254465,
                    PurchaseCamPos = new Vector3(-90.054f, -821.810f, 224.000f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -110f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-89.055f, -831.615f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -60f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-88.599f, -828.209f, 222.000f),
                    LightingCamRot = new Vector3(55f, 0f, -160f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-82.111f, -817.941f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, -20f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-89.163f, -829.880f, 221.02f), new Vector3(0f, 0f, 70f)),
                        new Sofa(new Vector3(-87.723f, -832.991f, 221.02f), new Vector3(0f, 0f, 159f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, 70.058f),
                        new Vector3(-77.004f, -829.297f, 220.109f),
                        new Vector3(-77.004f, -829.297f, 225.459f),
                        new Vector3(-77.004f, -829.297f, 230.809f))
                };
                GarageTwo = new Garage {
                    TriggerPos = new Vector3(-71.733f, -832.331f, 222.000f),
                    SpawnPos = new Vector3(-71.108f, -830.692f, 222.001f),
                    SpawnHeading = -20f,
                    Ipl = "imp_dt1_11_cargarage_b",
                    ExteriorIpLs = new List<string> { "hei_dt1_11_impexpproxy_b" },
                    InteriorId = 254721,
                    PurchaseCamPos = new Vector3(-71.108f, -830.692f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -20f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-61.260f, -829.552f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, 30f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-64.710f, -829.159f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, -70f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-75.044f, -822.666f, 222.001f),
                    NumberingCamRot = new Vector3(12f, 0f, 70f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-63.093f, -829.658f, 221.02f), new Vector3(0f, 0f, 161f)),
                        new Sofa(new Vector3(-59.932f, -828.260f, 221.02f), new Vector3(0f, 0f, -110f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, 160f),
                        new Vector3(-63.646f, -817.533f, 220.109f),
                        new Vector3(-63.646f, -817.533f, 225.459f),
                        new Vector3(-63.646f, -817.533f, 230.809f))
                };
                GarageThree = new Garage {
                    TriggerPos = new Vector3(-78.528f, -805.686f, 222.001f),
                    SpawnPos = new Vector3(-79.146f, -807.408f, 222.001f),
                    SpawnHeading = 160f,
                    Ipl = "imp_dt1_11_cargarage_c",
                    ExteriorIpLs = new List<string> { "hei_dt1_11_impexpproxy_c" },
                    InteriorId = 254977,
                    PurchaseCamPos = new Vector3(-79.146f, -807.408f, 224.001f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 160f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-88.997f, -808.442f, 222.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -150f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-85.524f, -808.880f, 222.001f),
                    LightingCamRot = new Vector3(55f, 0f, 110f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-75.209f, -815.338f, 222.000f),
                    NumberingCamRot = new Vector3(12f, 0f, -110f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle",
                            "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-87.225f, -808.333f, 221.02f), new Vector3(0f, 0f, -20f)),
                        new Sofa(new Vector3(-90.331f, -809.733f, 221.02f), new Vector3(0f, 0f, 67f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, -20f),
                        new Vector3(-86.653f, -820.463f, 220.109f),
                        new Vector3(-86.653f, -820.463f, 225.459f),
                        new Vector3(-86.653f, -820.463f, 230.809f))
                };
                ModShop = new ModShop {
                    TriggerPos = new Vector3(-68.824f, -814.253f, 285.000f),
                    SpawnPos = new Vector3(-69.980f, -813.785f, 285.000f),
                    SpawnHeading = 66.700f,
                    Ipl = "imp_dt1_11_modgarage",
                    ExteriorIpLs = new List<string> { "hei_dt1_11_impexpproxy_modshop" },
                    InteriorId = 255233,
                    PurchaseCamPos = new Vector3(-72.767f, -814.608f, 287f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 160f),
                    PurchaseCamFov = 70f,
                    FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))
                };
                ModShop.SofaAndTv = new SofaAndTv(ModShop.Tv, new Vector3(-63.968f, -820.524f, 284.645f),
                    new Vector3(0f, 0f, -21f));
                HeliPad = new HeliPad {
                    TriggerPos = new Vector3(-67.784f, -821.609f, 321.287f),
                    SpawnPos = new Vector3(-65.938f, -822.145f, 321.285f),
                    SpawnHeading = 253.635f
                };
                PurchaseCamPos = new Vector3(-86.710f, -724.772f, 48f);
                PurchaseCamRot = new Vector3(32f, 0f, 180f);
                PurchaseCamFov = 90f;

                CreateEntranceBlip();
                if (IsOwned)
                    CreateGarageEntranceBlip();
                CreatePurchaseMenu();
                CreateTeleportMenu();
                CreateGarageEntranceMenu();
                CreateVehicleElevatorMenu();
                CreatePaMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }
    }
}