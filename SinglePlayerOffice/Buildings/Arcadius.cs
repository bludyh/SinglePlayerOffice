using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Arcadius : Building {

        public Arcadius() {
            try {
                Name = "Arcadius Business Center";
                description = "The City within the City, the Arcadius Business Center boats more AAA hedge funds, smoothie bars and executive suicides per square foot of office space than any other building in the business district. Welcome to Cutting edge.";
                price = 2250000;
                InteriorIDs = new List<int>() { 134657, 237313, 237569, 237057, 236289, 236545, 236801, 237825, 238081, 238337, 253441, 253697, 253953, 254209 };
                exteriorMapObjects = new List<string>() { "hei_dt1_02_w01", "dt1_02_helipad_01", "dt1_02_dt1_emissive_dt1_02" };
                data = ScriptSettings.Load(String.Format(@"scripts\SinglePlayerOffice\{0}\data.ini", Name));
                Owner = (Owner)data.GetValue("Owner", "Owner", -1);
                Entrance = new Entrance {
                    TriggerPos = new Vector3(-118.791f, -608.376f, 36.281f),
                    SpawnPos = new Vector3(-117.505f, -608.885f, 36.281f),
                    SpawnHeading = 250.669f,
                };
                GarageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-143.998f, -576.076f, 32.060f),
                    SpawnPos = new Vector3(-143.998f, -576.076f, 32.060f),
                    SpawnHeading = 160f,
                    InteriorID = 134657,
                    ElevatorCamPos = new Vector3(-140.868f, -573.453f, 33.425f),
                    ElevatorCamRot = new Vector3(-10f, 0f, 125f),
                    ElevatorCamFOV = 60f
                };
                Office = new Office {
                    TriggerPos = new Vector3(-141.670f, -620.949f, 168.821f),
                    SpawnPos = new Vector3(-140.327f, -622.087f, 168.820f),
                    SpawnHeading = 184.412f,
                    ExteriorIPLs = new List<string> { "hei_dt1_02_exshadowmesh" },
                    InteriorIDs = new List<int>() { 237313, 237569, 237057, 236289, 236545, 236801, 237825, 238081, 238337 },
                    PurchaseCamPos = new Vector3(-142.224f, -646.676f, 170f),
                    PurchaseCamRot = new Vector3(-5f, 0f, -43f),
                    PurchaseCamFOV = 60f,
                    InteriorStyles = new List<InteriorStyle>() {
                        new InteriorStyle("Executive Rich", 0, "ex_dt1_02_office_02b"),
                        new InteriorStyle("Executive Cool", 415000, "ex_dt1_02_office_02c"),
                        new InteriorStyle("Executive Contrast", 500000, "ex_dt1_02_office_02a"),
                        new InteriorStyle("Old Spice Classical", 685000, "ex_dt1_02_office_01b"),
                        new InteriorStyle("Old Spice Vintage", 760000, "ex_dt1_02_office_01c"),
                        new InteriorStyle("Old Spice Warms", 950000, "ex_dt1_02_office_01a"),
                        new InteriorStyle("Power Broker Conservative", 835000, "ex_dt1_02_office_03b"),
                        new InteriorStyle("Power Broker Polished", 910000, "ex_dt1_02_office_03c"),
                        new InteriorStyle("Power Broker Ice", 1000000, "ex_dt1_02_office_03a")
                    },
                    HasExtraDecors = data.GetValue("Interiors", "HasExtraOfficeDecors", false),
                    Scene = new OfficeScene {
                        BossChairPos = new Vector3(-146.516f, -645.987f, 168.417f),
                        StaffChairPosList = new List<Vector3> {
                            new Vector3(-145.602f, -644.751f, 168.417f),
                            new Vector3(-145.729f, -643.541f, 168.417f),
                            new Vector3(-145.851f, -642.375f, 168.417f),
                            new Vector3(-147.741f, -644.944f, 168.417f),
                            new Vector3(-147.866f, -643.760f, 168.417f),
                            new Vector3(-147.991f, -642.567f, 168.417f)
                        },
                        PaChairPos = new Vector3(-138.994f, -634.089f, 168.423f),
                        PaChairRot = new Vector3(0f, 0f, -174f)
                    }
                };
                Office.InteriorStyle = GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.ExtraDecorsPrice = (Office.HasExtraDecors) ? 1650000 : 0;
                Office.ActiveScenes.Add(Office.Scene);
                GarageOne = new Garage {
                    TriggerPos = new Vector3(-198.649f, -580.730f, 136.001f),
                    SpawnPos = new Vector3(-196.790f, -580.510f, 136.001f),
                    SpawnHeading = -84f,
                    IPL = "imp_dt1_02_cargarage_a",
                    ExteriorIPLs = new List<string> { "hei_dt1_02_impexpproxy_a", "hei_dt1_02_impexpemproxy_a" },
                    InteriorID = 253441,
                    PurchaseCamPos = new Vector3(-196.790f, -580.510f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -84f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-191.550f, -588.963f, 136.000f),
                    DecorationCamRot = new Vector3(5f, 0f, -34f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-192.656f, -585.665f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, -135f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-191.463f, -573.653f, 136.001f),
                    NumberingCamRot = new Vector3(12f, 0f, 6.520f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-181.798f, -581.548f, 134.116f),
                        ElevatorLevelBPos = new Vector3(-181.798f, -581.548f, 139.466f),
                        ElevatorLevelCPos = new Vector3(-181.798f, -581.548f, 144.816f),
                        ElevatorRot = new Vector3(0f, 0f, 96.096f)
                    }
                };
                GarageOne.ActiveScenes.Add(GarageOne.Scene);
                GarageTwo = new Garage {
                    TriggerPos = new Vector3(-124.515f, -571.676f, 136.001f),
                    SpawnPos = new Vector3(-122.979f, -571.062f, 136.001f),
                    SpawnHeading = -69f,
                    IPL = "imp_dt1_02_cargarage_b",
                    ExteriorIPLs = new List<string> { "hei_dt1_02_impexpproxy_b", "hei_dt1_02_impexpemproxy_b" },
                    InteriorID = 253697,
                    PurchaseCamPos = new Vector3(-122.979f, -571.062f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -69f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-115.602f, -577.715f, 136.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -19f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-117.451f, -574.830f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, -120f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-119.268f, -563.180f, 136.000f),
                    NumberingCamRot = new Vector3(12f, 0f, 21f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-107.975f, -568.080f, 134.116f),
                        ElevatorLevelBPos = new Vector3(-107.975f, -568.080f, 139.466f),
                        ElevatorLevelCPos = new Vector3(-107.975f, -568.080f, 144.816f),
                        ElevatorRot = new Vector3(0f, 0f, 110.928f)
                    }
                };
                GarageTwo.ActiveScenes.Add(GarageTwo.Scene);
                GarageThree = new Garage {
                    TriggerPos = new Vector3(-135.621f, -622.349f, 136.001f),
                    SpawnPos = new Vector3(-135.882f, -623.975f, 136.001f),
                    SpawnHeading = 171f,
                    IPL = "imp_dt1_02_cargarage_c",
                    ExteriorIPLs = new List<string> { "hei_dt1_02_impexpproxy_c", "hei_dt1_02_impexpemproxy_c" },
                    InteriorID = 253953,
                    PurchaseCamPos = new Vector3(-135.882f, -623.975f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 171f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-145.349f, -627.036f, 136.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -139f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1")),
                    LightingCamPos = new Vector3(-141.992f, -626.889f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, 120f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-130.550f, -631.243f, 136.000f),
                    NumberingCamRot = new Vector3(12f, 0f, -98f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1")),
                    Scene = new GarageScene {
                        ElevatorLevelAPos = new Vector3(-140.817f, -638.450f, 134.116f),
                        ElevatorLevelBPos = new Vector3(-140.817f, -638.450f, 139.466f),
                        ElevatorLevelCPos = new Vector3(-140.817f, -638.450f, 144.816f),
                        ElevatorRot = new Vector3(0f, 0f, -9.082f)
                    }
                };
                GarageThree.ActiveScenes.Add(GarageThree.Scene);
                ModShop = new ModShop {
                    TriggerPos = new Vector3(-138.322f, -592.926f, 167.000f),
                    SpawnPos = new Vector3(-139.104f, -591.805f, 167.000f),
                    SpawnHeading = 34.856f,
                    IPL = "imp_dt1_02_modgarage",
                    ExteriorIPLs = new List<string> { "hei_dt1_02_impexpproxy_modshop", "hei_dt1_02_impexpemproxy_modshop" },
                    InteriorID = 254209,
                    PurchaseCamPos = new Vector3(-142.051f, -591.137f, 169f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 130f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))
                };
                HeliPad = new HeliPad {
                    TriggerPos = new Vector3(-155.139f, -602.231f, 201.735f),
                    SpawnPos = new Vector3(-156.460f, -603.294f, 201.735f),
                    SpawnHeading = 128.035f
                };
                PurchaseCamPos = new Vector3(-167.906f, -487.694f, 40f);
                PurchaseCamRot = new Vector3(30f, 0, -170f);
                PurchaseCamFOV = 70f;

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
