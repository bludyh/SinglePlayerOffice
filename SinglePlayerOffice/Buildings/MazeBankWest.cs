using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {
    internal class MazeBankWest : Building {
        public MazeBankWest() {
            try {
                Name = "Maze Bank West";
                data = ScriptSettings.Load($@"scripts\SinglePlayerOffice\{Name}\data.ini");
                Description =
                    "Maze has been the target for more boycotts, demonstrations and civil rights lawsuits than any other bank in America. Access to that wealth of experience doesn't come cheap, but a good education is an investment worth any price.";
                Price = 1000000;
                InteriorIDs = new List<int> {
                    258049, 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249, 256513, 256769,
                    257025, 257281
                };
                exteriorMapObjects = new List<string> {
                    "sm_15_bld2_dtl", "hei_sm_15_bld2", "sm_15_bld2_LOD", "sm_15_bld2_dtl3", "sm_15_bld1_dtl3",
                    "sm_15_bld2_railing", "sm_15_emissive", "sm_15_emissive_LOD"
                };
                Owner = (Owner) data.GetValue("Owner", "Owner", -1);
                Entrance = new Entrance {
                    TriggerPos = new Vector3(-1370.370f, -503.067f, 33.157f),
                    SpawnPos = new Vector3(-1371.608f, -503.967f, 33.157f),
                    SpawnHeading = 125.509f
                };
                GarageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-1361.918f, -472.036f, 31.233f),
                    SpawnPos = new Vector3(-1361.918f, -472.036f, 31.233f),
                    SpawnHeading = 100f,
                    InteriorId = 258049,
                    VehicleElevatorEntrance = new VehicleElevatorEntrance(
                        new Vector3(-1358.206f, -473.482f, 32.596f),
                        new Vector3(-10f, 0f, 65f),
                        60f)
                };
                Office = new Office {
                    TriggerPos = new Vector3(-1392.566f, -480.734f, 72.042f),
                    SpawnPos = new Vector3(-1391.416f, -479.347f, 72.042f),
                    SpawnHeading = 281.429f,
                    ExteriorIpLs = new List<string> {"hei_sm_15_exshadowmesh"},
                    InteriorIDs =
                        new List<int> {244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249},
                    PurchaseCamPos = new Vector3(-1366.876f, -480.338f, 73f),
                    PurchaseCamRot = new Vector3(-5f, 0f, 50f),
                    PurchaseCamFov = 60f,
                    InteriorStyles =
                        new List<InteriorStyle> {
                            new InteriorStyle("Executive Rich", 0, "ex_sm_15_office_02b"),
                            new InteriorStyle("Executive Cool", 415000, "ex_sm_15_office_02c"),
                            new InteriorStyle("Executive Contrast", 500000, "ex_sm_15_office_02a"),
                            new InteriorStyle("Old Spice Classical", 685000, "ex_sm_15_office_01b"),
                            new InteriorStyle("Old Spice Vintage", 760000, "ex_sm_15_office_01c"),
                            new InteriorStyle("Old Spice Warms", 950000, "ex_sm_15_office_01a"),
                            new InteriorStyle("Power Broker Conservative", 835000, "ex_sm_15_office_03b"),
                            new InteriorStyle("Power Broker Polished", 910000, "ex_sm_15_office_03c"),
                            new InteriorStyle("Power Broker Ice", 1000000, "ex_sm_15_office_03a")
                        },
                    HasExtraDecors = data.GetValue("Interiors", "HasExtraOfficeDecors", false),
                    Boss = new Boss(new Vector3(-1367.419f, -484.652f, 71.638f)),
                    Pa = new Pa(new Vector3(-1379.528f, -477.600f, 71.644f), new Vector3(0f, 0f, -82f)),
                    Staffs =
                        new List<Staff> {
                            new Staff(PedHash.Business01AMM, new Vector3(-1371.052f, -484.114f, 71.638f)),
                            new Staff(PedHash.Business03AMY, new Vector3(-1369.597f, -486.079f, 71.638f)),
                            new Staff(PedHash.Business01AFY, new Vector3(-1370.785f, -486.246f, 71.638f)),
                            new Staff(PedHash.Business04AFY, new Vector3(-1368.685f, -483.781f, 71.638f))
                        },
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1372.839f, -474.535f, 71.05f), new Vector3(0f, 0f, 48f)),
                        new Sofa(new Vector3(-1372.322f, -478.740f, 71.05f), new Vector3(0f, 0f, 142f)),
                        new Sofa(new Vector3(-1384.105f, -486.700f, 71.05f), new Vector3(0f, 0f, 98f)),
                        new Sofa(new Vector3(-1382.594f, -488.915f, 71.05f), new Vector3(0f, 0f, 188f)),
                        new Sofa(new Vector3(-1381.133f, -488.716f, 71.05f), new Vector3(0f, 0f, 188f))
                    },
                    Wardrobe = new Wardrobe(new Vector3(-1381.006f, -470.913f, 72.042f), new Vector3(0f, 0f, 8f))
                };
                Office.InteriorStyle =
                    GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.ExtraDecorsPrice = Office.HasExtraDecors ? 1650000 : 0;
                Office.SofaAndTv = new SofaAndTv(Office.Tv, new Vector3(-1369.017f, -476.016f, 71.05f),
                    new Vector3(0f, 0f, -82f));
                GarageOne = new Garage {
                    TriggerPos = new Vector3(-1396.394f, -480.688f, 57.100f),
                    SpawnPos = new Vector3(-1394.713f, -480.488f, 57.100f),
                    SpawnHeading = -81f,
                    Ipl = "imp_sm_15_cargarage_a",
                    ExteriorIpLs = new List<string> {"hei_sm_15_impexpproxy_a"},
                    InteriorId = 256513,
                    PurchaseCamPos = new Vector3(-1394.713f, -480.488f, 59.100f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -81f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1388.986f, -488.544f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1390.211f, -485.338f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-1389.410f, -473.260f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1389.950f, -487.038f, 56.12f), new Vector3(0f, 0f, 100f)),
                        new Sofa(new Vector3(-1387.157f, -489.082f, 56.12f), new Vector3(0f, 0f, 190f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, 98.725f),
                        new Vector3(-1379.532f, -480.730f, 55.213f),
                        new Vector3(-1379.532f, -480.730f, 60.563f),
                        new Vector3(-1379.532f, -480.730f, 65.913f))
                };
                GarageTwo = new Garage {
                    TriggerPos = new Vector3(-1396.407f, -480.761f, 49.101f),
                    SpawnPos = new Vector3(-1394.687f, -480.479f, 49.101f),
                    SpawnHeading = -81f,
                    Ipl = "imp_sm_15_cargarage_b",
                    ExteriorIpLs = new List<string> {"hei_sm_15_impexpproxy_b"},
                    InteriorId = 256769,
                    PurchaseCamPos = new Vector3(-1394.687f, -480.479f, 51.101f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -81f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1389.047f, -488.542f, 49.101f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1390.268f, -485.402f, 49.101f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-1389.405f, -473.322f, 49.101f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1389.920f, -487.057f, 48.12f), new Vector3(0f, 0f, 99f)),
                        new Sofa(new Vector3(-1387.182f, -489.103f, 48.12f), new Vector3(0f, 0f, -171f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, 98.714f),
                        new Vector3(-1379.543f, -480.745f, 47.220f),
                        new Vector3(-1379.543f, -480.745f, 52.570f),
                        new Vector3(-1379.543f, -480.745f, 57.920f))
                };
                GarageThree = new Garage {
                    TriggerPos = new Vector3(-1367.133f, -472.392f, 57.101f),
                    SpawnPos = new Vector3(-1368.905f, -472.632f, 57.101f),
                    SpawnHeading = 99f,
                    Ipl = "imp_sm_15_cargarage_c",
                    ExteriorIpLs = new List<string> {"hei_sm_15_impexpproxy_c"},
                    InteriorId = 257025,
                    PurchaseCamPos = new Vector3(-1368.905f, -472.632f, 59.101f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 99f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1374.548f, -464.566f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, 149f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1373.297f, -467.776f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, 49f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-1374.141f, -479.781f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, -170f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle",
                            "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1373.640f, -466.054f, 56.12f), new Vector3(0f, 0f, -81f)),
                        new Sofa(new Vector3(-1376.365f, -463.999f, 56.12f), new Vector3(0f, 0f, 8f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, -81.081f),
                        new Vector3(-1383.997f, -472.4f, 55.213f),
                        new Vector3(-1383.997f, -472.4f, 60.563f),
                        new Vector3(-1383.997f, -472.4f, 65.913f))
                };
                ModShop = new ModShop {
                    TriggerPos = new Vector3(-1392.375f, -482.942f, 78.200f),
                    SpawnPos = new Vector3(-1390.757f, -482.723f, 78.200f),
                    SpawnHeading = 275.322f,
                    Ipl = "imp_sm_15_modgarage",
                    ExteriorIpLs = new List<string> {"hei_sm_15_impexpproxy_modshops"},
                    InteriorId = 257281,
                    PurchaseCamPos = new Vector3(-1388.860f, -480.712f, 80.2f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 9f),
                    PurchaseCamFov = 70f,
                    FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))
                };
                ModShop.SofaAndTv = new SofaAndTv(ModShop.Tv, new Vector3(-1399.425f, -479.745f, 77.85f),
                    new Vector3(0f, 0f, 189f));
                HeliPad = new HeliPad {
                    TriggerPos = new Vector3(-1369.535f, -472.037f, 84.447f),
                    SpawnPos = new Vector3(-1368.162f, -471.002f, 84.447f),
                    SpawnHeading = 304.157f
                };
                PurchaseCamPos = new Vector3(-1320.468f, -534.793f, 40f);
                PurchaseCamRot = new Vector3(12f, 0f, 40f);
                PurchaseCamFov = 60f;

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