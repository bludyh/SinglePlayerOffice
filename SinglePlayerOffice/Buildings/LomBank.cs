using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {

    internal class LomBank : Building {

        public LomBank() {
            try {
                SaveData = ScriptSettings.Load(@"scripts\SinglePlayerOffice\Lombank West\data.ini");
                Name = "Lombank West";
                Description =
                    "In a radical new step towards diversification of the banking sector. Industry giant Lombank is selling office space to results-driven, high-liquidity kill squads and drug cartels. Be part of the change our financial industry needs.";
                Price = 3100000;
                InteriorIDs = new List<int> {
                    257793, 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945, 255489, 255745,
                    256001, 256257
                };
                ExteriorMapObjects = new List<string> { "sm_13_emissive", "sm_13_bld1", "sm_13_bld1_LOD" };
                Owner = (Owner) SaveData.GetValue("Owner", "Owner", -1);
                Entrance = new Entrance {
                    TriggerPos = new Vector3(-1581.127f, -558.608f, 34.953f),
                    SpawnPos = new Vector3(-1582.192f, -556.967f, 34.954f),
                    SpawnHeading = 32.916f,
                    PurchaseCamPos = new Vector3(-1609.816f, -637.368f, 35f),
                    PurchaseCamRot = new Vector3(24f, 0f, -30f),
                    PurchaseCamFov = 67f
                };
                GarageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-1537.423f, -577.773f, 25.344f),
                    SpawnPos = new Vector3(-1537.423f, -577.773f, 25.344f),
                    SpawnHeading = 35f,
                    InteriorId = 257793,
                    VehicleElevatorEntrance = new VehicleElevatorEntrance(
                        new Vector3(-1536.926f, -581.923f, 26.708f),
                        new Vector3(-10f, 0f, 0f),
                        60f)
                };
                Office = new Office {
                    TriggerPos = new Vector3(-1579.750f, -565.049f, 108.523f),
                    SpawnPos = new Vector3(-1578.013f, -565.397f, 108.523f),
                    SpawnHeading = 214.074f,
                    ExteriorIpLs = new List<string> { "hei_sm_13_exshadowmesh" },
                    InteriorIDs =
                        new List<int> { 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945 },
                    PurchaseCamPos = new Vector3(-1567.370f, -587.616f, 109.5f),
                    PurchaseCamRot = new Vector3(-5f, 0f, -12f),
                    PurchaseCamFov = 60f,
                    InteriorStyles =
                        new List<InteriorStyle> {
                            new InteriorStyle("Executive Rich", 0, "ex_sm_13_office_02b"),
                            new InteriorStyle("Executive Cool", 415000, "ex_sm_13_office_02c"),
                            new InteriorStyle("Executive Contrast", 500000, "ex_sm_13_office_02a"),
                            new InteriorStyle("Old Spice Classical", 685000, "ex_sm_13_office_01b"),
                            new InteriorStyle("Old Spice Vintage", 760000, "ex_sm_13_office_01c"),
                            new InteriorStyle("Old Spice Warms", 950000, "ex_sm_13_office_01a"),
                            new InteriorStyle("Power Broker Conservative", 835000, "ex_sm_13_office_03b"),
                            new InteriorStyle("Power Broker Polished", 910000, "ex_sm_13_office_03c"),
                            new InteriorStyle("Power Broker Ice", 1000000, "ex_sm_13_office_03a")
                        },
                    HasExtraDecors = SaveData.GetValue("Interiors", "HasExtraOfficeDecors", false),
                    Boss = new Boss(new Vector3(-1571.435f, -589.159f, 108.119f)),
                    Pa = new Pa(new Vector3(-1570.820f, -575.100f, 108.125f), new Vector3(0f, 0f, -144f)),
                    Staffs =
                        new List<Staff> {
                            new Staff(PedHash.Business01AMM, new Vector3(-1573.717f, -587.906f, 108.119f)),
                            new Staff(PedHash.Business03AMY, new Vector3(-1571.976f, -586.648f, 108.119f)),
                            new Staff(PedHash.Business01AFY, new Vector3(-1571.261f, -587.632f, 108.119f)),
                            new Staff(PedHash.Business04AFY, new Vector3(-1572.665f, -585.699f, 108.119f))
                        },
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1565.078f, -579.629f, 107.547f), new Vector3(0f, 0f, -15f)),
                        new Sofa(new Vector3(-1568.516f, -581.993f, 107.547f), new Vector3(0f, 0f, 80f)),
                        new Sofa(new Vector3(-1581.158f, -575.387f, 107.547f), new Vector3(0f, 0f, 38f)),
                        new Sofa(new Vector3(-1582.231f, -577.760f, 107.547f), new Vector3(0f, 0f, 128f)),
                        new Sofa(new Vector3(-1581.452f, -578.836f, 107.547f), new Vector3(0f, 0f, 128f))
                    },
                    Wardrobe = new Wardrobe(new Vector3(-1565.723f, -570.756f, 108.523f), new Vector3(0f, 0f, -54f))
                };
                Office.InteriorStyle =
                    GetOfficeInteriorStyle(SaveData.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.ExtraDecorsPrice = Office.HasExtraDecors ? 1650000 : 0;
                Office.SofaAndTv = new SofaAndTv(Office.Tv, new Vector3(-1564.563f, -583.634f, 107.523f),
                    new Vector3(0f, 0f, -144f));
                GarageOne = new Garage {
                    TriggerPos = new Vector3(-1586.341f, -561.390f, 86.501f),
                    SpawnPos = new Vector3(-1585.244f, -562.935f, 86.501f),
                    SpawnHeading = -144f,
                    Ipl = "imp_sm_13_cargarage_a",
                    ExteriorIpLs = new List<string> { "hei_sm_13_impexpproxy_a" },
                    InteriorId = 255489,
                    PurchaseCamPos = new Vector3(-1585.244f, -562.935f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -144f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1589.886f, -571.585f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, -94f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageOneDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1587.638f, -569.089f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 165f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-1576.517f, -564.090f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -54f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageOneNumberingStyle(SaveData.GetValue("Interiors", "GarageOneNumberingStyle",
                            "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1589.006f, -570.107f, 85.52f), new Vector3(0f, 0f, 37f)),
                        new Sofa(new Vector3(-1589.547f, -573.458f, 85.52f), new Vector3(0f, 0f, 126f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, 36.061f),
                        new Vector3(-1578.596f, -576.434f, 84.613f),
                        new Vector3(-1578.596f, -576.434f, 89.963f),
                        new Vector3(-1578.596f, -576.434f, 95.313f))
                };
                GarageTwo = new Garage {
                    TriggerPos = new Vector3(-1562.919f, -556.852f, 86.501f),
                    SpawnPos = new Vector3(-1564.414f, -557.834f, 86.501f),
                    SpawnHeading = 126f,
                    Ipl = "imp_sm_13_cargarage_b",
                    ExteriorIpLs = new List<string> { "hei_sm_13_impexpproxy_b" },
                    InteriorId = 255745,
                    PurchaseCamPos = new Vector3(-1564.414f, -557.834f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 126f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1573.072f, -553.264f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 176f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageTwoDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1570.563f, -555.513f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 76f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")),
                    NumberingCamPos = new Vector3(-1565.695f, -566.624f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -142f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageTwoNumberingStyle(SaveData.GetValue("Interiors", "GarageTwoNumberingStyle",
                            "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1571.601f, -554.174f, 85.52f), new Vector3(0f, 0f, -54f)),
                        new Sofa(new Vector3(-1574.998f, -553.604f, 85.52f), new Vector3(0f, 0f, 37f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, -53.872f),
                        new Vector3(-1577.924f, -564.567f, 84.613f),
                        new Vector3(-1577.924f, -564.567f, 89.963f),
                        new Vector3(-1577.924f, -564.567f, 95.313f))
                };
                GarageThree = new Garage {
                    TriggerPos = new Vector3(-1558.330f, -580.248f, 86.501f),
                    SpawnPos = new Vector3(-1559.331f, -578.897f, 86.501f),
                    SpawnHeading = 36f,
                    Ipl = "imp_sm_13_cargarage_c",
                    ExteriorIpLs = new List<string> { "hei_sm_13_impexpproxy_c" },
                    InteriorId = 256001,
                    PurchaseCamPos = new Vector3(-1559.331f, -578.897f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 36f),
                    PurchaseCamFov = 90f,
                    DecorationCamPos = new Vector3(-1554.784f, -570.093f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 86f),
                    DecorationCamFov = 60f,
                    DecorationStyle =
                        GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageThreeDecorationStyle",
                            "Decoration 1")),
                    LightingCamPos = new Vector3(-1557.047f, -572.635f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, -14f),
                    LightingCamFov = 70f,
                    LightingStyle =
                        GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageThreeLightingStyle",
                            "Lighting 1")),
                    NumberingCamPos = new Vector3(-1568.135f, -577.467f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, 128f),
                    NumberingCamFov = 60f,
                    NumberingStyle =
                        GetGarageThreeNumberingStyle(SaveData.GetValue("Interiors", "GarageThreeNumberingStyle",
                            "Signage 1")),
                    Sofas = new List<Sofa> {
                        new Sofa(new Vector3(-1555.685f, -571.607f, 85.52f), new Vector3(0f, 0f, -145f)),
                        new Sofa(new Vector3(-1555.123f, -568.174f, 85.52f), new Vector3(0f, 0f, -54f))
                    },
                    VehicleElevator = new VehicleElevator(
                        new Vector3(0f, 0f, -143.848f),
                        new Vector3(-1566.078f, -565.253f, 84.613f),
                        new Vector3(-1566.078f, -565.253f, 89.963f),
                        new Vector3(-1566.078f, -565.253f, 95.313f))
                };
                ModShop = new ModShop {
                    TriggerPos = new Vector3(-1569.469f, -573.342f, 105.200f),
                    SpawnPos = new Vector3(-1570.438f, -571.884f, 105.200f),
                    SpawnHeading = 30.991f,
                    Ipl = "imp_sm_13_modgarage",
                    ExteriorIpLs = new List<string> { "hei_sm_13_impexpproxy_modshop" },
                    InteriorId = 256257,
                    PurchaseCamPos = new Vector3(-1573.166f, -571.276f, 107.2f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 126f),
                    PurchaseCamFov = 70f,
                    FloorStyle = GetModShopFloorStyle(SaveData.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))
                };
                ModShop.SofaAndTv = new SofaAndTv(ModShop.Tv, new Vector3(-1569.119f, -581.097f, 104.845f),
                    new Vector3(0f, 0f, -54f));
                HeliPad = new HeliPad {
                    TriggerPos = new Vector3(-1560.776f, -569.241f, 114.448f),
                    SpawnPos = new Vector3(-1561.777f, -567.676f, 114.448f),
                    SpawnHeading = 31.058f
                };

                CreateEntranceBlip();
                if (IsOwned)
                    CreateGarageEntranceBlip();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }

}