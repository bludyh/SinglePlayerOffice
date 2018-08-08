using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class MazeBankWest : Building {

        public MazeBankWest() {
            try {
                name = "Maze Bank West";
                description = "Maze has been the target for more boycotts, demonstrations and civil rights lawsuits than any other bank in America. Access to that wealth of experience doesn't come cheap, but a good education is an investment worth any price.";
                price = 1000000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(name, "Owner", -1);
                blipPos = new Vector3(-1370.370f, -503.067f, 33.157f);
                interiorIDs = new List<int>() { 258049, 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249, 256513, 256769, 257025, 257281 };
                exteriorMapObjects = new List<string>() { "sm_15_bld2_dtl", "hei_sm_15_bld2", "sm_15_bld2_LOD", "sm_15_bld2_dtl3", "sm_15_bld1_dtl3", "sm_15_bld2_railing", "sm_15_emissive", "sm_15_emissive_LOD" };
                entrance = new Entrance {
                    TriggerPos = new Vector3(-1370.370f, -503.067f, 33.157f),
                    SpawnPos = new Vector3(-1371.608f, -503.967f, 33.157f),
                    SpawnHeading = 125.509f,
                    PurchaseCamPos = new Vector3(-1320.468f, -534.793f, 40f),
                    PurchaseCamRot = new Vector3(12f, 0f, 40f),
                    PurchaseCamFOV = 60f
                };
                garageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-1361.918f, -472.036f, 31.233f),
                    SpawnPos = new Vector3(-1361.918f, -472.036f, 31.233f),
                    SpawnHeading = 100f,
                    InteriorID = 258049,
                    ElevatorCamPos = new Vector3(-1358.206f, -473.482f, 32.596f),
                    ElevatorCamRot = new Vector3(-10f, 0f, 65f),
                    ElevatorCamFOV = 60f
                };
                office = new Office {
                    TriggerPos = new Vector3(-1392.566f, -480.734f, 72.042f),
                    SpawnPos = new Vector3(-1391.416f, -479.347f, 72.042f),
                    SpawnHeading = 281.429f,
                    ExteriorIPLs = new List<string> { "hei_sm_15_exshadowmesh" },
                    InteriorIDs = new List<int>() { 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249 },
                    PurchaseCamPos = new Vector3(-1366.876f, -480.338f, 73f),
                    PurchaseCamRot = new Vector3(-5f, 0f, 50f),
                    PurchaseCamFOV = 60f,
                    InteriorStyles = new List<InteriorStyle>() {
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
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(name, "HasExtraOfficeDecors", false),
                    Scene = new OfficeScene {
                        BossChairPos = new Vector3(-1367.419f, -484.652f, 71.638f),
                        StaffChairPosList = new List<Vector3> {
                            new Vector3(-1368.685f, -483.781f, 71.638f),
                            new Vector3(-1369.890f, -483.951f, 71.638f),
                            new Vector3(-1371.052f, -484.114f, 71.638f),
                            new Vector3(-1368.419f, -485.913f, 71.638f),
                            new Vector3(-1369.597f, -486.079f, 71.638f),
                            new Vector3(-1370.785f, -486.246f, 71.638f)
                        },
                        PaChairPos = new Vector3(-1379.528f, -477.600f, 71.644f),
                        PaChairRot = new Vector3(0f, 0f, -82f)
                    }
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(name, "OfficeInteriorStyle"));
                office.ExtraDecorsPrice = (office.HasExtraDecors) ? 1650000 : 0;
                office.ActiveScenes.Add(office.Scene);
                garageOne = new Garage {
                    TriggerPos = new Vector3(-1396.394f, -480.688f, 57.100f),
                    SpawnPos = new Vector3(-1394.713f, -480.488f, 57.100f),
                    SpawnHeading = -81f,
                    IPL = "imp_sm_15_cargarage_a",
                    ExteriorIPLs = new List<string> { "hei_sm_15_impexpproxy_a" },
                    InteriorID = 256513,
                    PurchaseCamPos = new Vector3(-1394.713f, -480.488f, 59.100f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -81f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1388.986f, -488.544f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-1390.211f, -485.338f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-1389.410f, -473.260f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneNumberingStyle")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-1379.532f, -480.730f, 55.213f),
                        ElevatorLevelBPos = new Vector3(-1379.532f, -480.730f, 60.563f),
                        ElevatorLevelCPos = new Vector3(-1379.532f, -480.730f, 65.913f),
                        ElevatorRot = new Vector3(0f, 0f, 98.725f)
                    }
                };
                garageOne.ActiveScenes.Add(garageOne.Scene);
                garageTwo = new Garage {
                    TriggerPos = new Vector3(-1396.407f, -480.761f, 49.101f),
                    SpawnPos = new Vector3(-1394.687f, -480.479f, 49.101f),
                    SpawnHeading = -81f,
                    IPL = "imp_sm_15_cargarage_b",
                    ExteriorIPLs = new List<string> { "hei_sm_15_impexpproxy_b" },
                    InteriorID = 256769,
                    PurchaseCamPos = new Vector3(-1394.687f, -480.479f, 51.101f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -81f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1389.047f, -488.542f, 49.101f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-1390.268f, -485.402f, 49.101f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-1389.405f, -473.322f, 49.101f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoNumberingStyle")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-1379.543f, -480.745f, 47.220f),
                        ElevatorLevelBPos = new Vector3(-1379.543f, -480.745f, 52.570f),
                        ElevatorLevelCPos = new Vector3(-1379.543f, -480.745f, 57.920f),
                        ElevatorRot = new Vector3(0f, 0f, 98.714f)
                    }
                };
                garageTwo.ActiveScenes.Add(garageTwo.Scene);
                garageThree = new Garage {
                    TriggerPos = new Vector3(-1367.133f, -472.392f, 57.101f),
                    SpawnPos = new Vector3(-1368.905f, -472.632f, 57.101f),
                    SpawnHeading = 99f,
                    IPL = "imp_sm_15_cargarage_c",
                    ExteriorIPLs = new List<string> { "hei_sm_15_impexpproxy_c" },
                    InteriorID = 257025,
                    PurchaseCamPos = new Vector3(-1368.905f, -472.632f, 59.101f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 99f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1374.548f, -464.566f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, 149f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-1373.297f, -467.776f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, 49f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-1374.141f, -479.781f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, -170f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeNumberingStyle")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-1383.997f, -472.4f, 55.213f),
                        ElevatorLevelBPos = new Vector3(-1383.997f, -472.4f, 60.563f),
                        ElevatorLevelCPos = new Vector3(-1383.997f, -472.4f, 65.913f),
                        ElevatorRot = new Vector3(0f, 0f, -81.081f)
                    }
                };
                garageThree.ActiveScenes.Add(garageThree.Scene);
                modShop = new ModShop {
                    TriggerPos = new Vector3(-1392.375f, -482.942f, 78.200f),
                    SpawnPos = new Vector3(-1390.757f, -482.723f, 78.200f),
                    SpawnHeading = 275.322f,
                    IPL = "imp_sm_15_modgarage",
                    ExteriorIPLs = new List<string> { "hei_sm_15_impexpproxy_modshops" },
                    InteriorID = 257281,
                    PurchaseCamPos = new Vector3(-1388.860f, -480.712f, 80.2f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 9f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(name, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    TriggerPos = new Vector3(-1369.535f, -472.037f, 84.447f),
                    SpawnPos = new Vector3(-1368.162f, -471.002f, 84.447f),
                    SpawnHeading = 304.157f
                };

                CreateBuildingBlip();
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
