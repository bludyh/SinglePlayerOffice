using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class LomBank : Building {

        public LomBank() {
            try {
                name = "Lombank West";
                description = "In a radical new step towards diversification of the banking sector. Industry giant Lombank is selling office space to results-driven, high-liquidity kill squads and drug cartels. Be part of the change our financial industry needs.";
                price = 3100000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(name, "Owner", -1);
                blipPos = new Vector3(-1581.127f, -558.608f, 34.953f);
                interiorIDs = new List<int>() { 257793, 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945, 255489, 255745, 256001, 256257 };
                exteriorIPLs = new List<string>() { "sm_13_emissive", "sm_13_bld1", "sm_13_bld1_LOD" };
                entrance = new Entrance {
                    TriggerPos = new Vector3(-1581.127f, -558.608f, 34.953f),
                    SpawnPos = new Vector3(-1582.192f, -556.967f, 34.954f),
                    SpawnHeading = 32.916f,
                    PurchaseCamPos = new Vector3(-1609.816f, -637.368f, 35f),
                    PurchaseCamRot = new Vector3(24f, 0f, -30f),
                    PurchaseCamFOV = 67f
                };
                garageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-1537.423f, -577.773f, 25.344f),
                    SpawnPos = new Vector3(-1537.423f, -577.773f, 25.344f),
                    SpawnHeading = 35f,
                    InteriorID = 257793,
                    ElevatorCamPos = new Vector3(-1536.926f, -581.923f, 26.708f),
                    ElevatorCamRot = new Vector3(-10f, 0f, 0f),
                    ElevatorCamFOV = 60f
                };
                office = new Office {
                    TriggerPos = new Vector3(-1579.750f, -565.049f, 108.523f),
                    SpawnPos = new Vector3(-1578.013f, -565.397f, 108.523f),
                    SpawnHeading = 214.074f,
                    InteriorIDs = new List<int>() { 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945 },
                    PurchaseCamPos = new Vector3(-1567.370f, -587.616f, 109.5f),
                    PurchaseCamRot = new Vector3(-5f, 0f, -12f),
                    PurchaseCamFOV = 60f,
                    InteriorStyles = new List<InteriorStyle>() {
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
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(name, "HasExtraOfficeDecors", false),
                    Scene = new OfficeScene {
                        BossChairPos = new Vector3(-1571.435f, -589.159f, 108.119f),
                        StaffChairPosList = new List<Vector3> {
                            new Vector3(-1571.261f, -587.632f, 108.119f),
                            new Vector3(-1571.976f, -586.648f, 108.119f),
                            new Vector3(-1572.665f, -585.699f, 108.119f),
                            new Vector3(-1573.018f, -588.868f, 108.119f),
                            new Vector3(-1573.717f, -587.906f, 108.119f),
                            new Vector3(-1574.422f, -586.935f, 108.119f)
                        },
                        PaChairPos = new Vector3(-1570.820f, -575.100f, 108.125f),
                        PaChairRot = new Vector3(0f, 0f, -144f)
                    }
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(name, "OfficeInteriorStyle"));
                garageOne = new Garage {
                    TriggerPos = new Vector3(-1586.341f, -561.390f, 86.501f),
                    SpawnPos = new Vector3(-1585.244f, -562.935f, 86.501f),
                    SpawnHeading = -144f,
                    IPL = "imp_sm_13_cargarage_a",
                    InteriorID = 255489,
                    PurchaseCamPos = new Vector3(-1585.244f, -562.935f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -144f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1589.886f, -571.585f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, -94f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-1587.638f, -569.089f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 165f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-1576.517f, -564.090f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -54f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-1578.596f, -576.434f, 84.613f),
                    ElevatorLevelBPos = new Vector3(-1578.596f, -576.434f, 89.963f),
                    ElevatorLevelCPos = new Vector3(-1578.596f, -576.434f, 95.313f),
                    ElevatorRot = new Vector3(0f, 0f, 36.061f)
                };
                garageTwo = new Garage {
                    TriggerPos = new Vector3(-1562.919f, -556.852f, 86.501f),
                    SpawnPos = new Vector3(-1564.414f, -557.834f, 86.501f),
                    SpawnHeading = 126f,
                    IPL = "imp_sm_13_cargarage_b",
                    InteriorID = 255745,
                    PurchaseCamPos = new Vector3(-1564.414f, -557.834f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 126f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1573.072f, -553.264f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 176f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-1570.563f, -555.513f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 76f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-1565.695f, -566.624f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -142f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-1577.924f, -564.567f, 84.613f),
                    ElevatorLevelBPos = new Vector3(-1577.924f, -564.567f, 89.963f),
                    ElevatorLevelCPos = new Vector3(-1577.924f, -564.567f, 95.313f),
                    ElevatorRot = new Vector3(0f, 0f, -53.872f)
                };
                garageThree = new Garage {
                    TriggerPos = new Vector3(-1558.330f, -580.248f, 86.501f),
                    SpawnPos = new Vector3(-1559.331f, -578.897f, 86.501f),
                    SpawnHeading = 36f,
                    IPL = "imp_sm_13_cargarage_c",
                    InteriorID = 256001,
                    PurchaseCamPos = new Vector3(-1559.331f, -578.897f, 88.501f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 36f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-1554.784f, -570.093f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 86f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-1557.047f, -572.635f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, -14f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-1568.135f, -577.467f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, 128f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-1566.078f, -565.253f, 84.613f),
                    ElevatorLevelBPos = new Vector3(-1566.078f, -565.253f, 89.963f),
                    ElevatorLevelCPos = new Vector3(-1566.078f, -565.253f, 95.313f),
                    ElevatorRot = new Vector3(0f, 0f, -143.848f)
                };
                modShop = new ModShop {
                    TriggerPos = new Vector3(-1569.469f, -573.342f, 105.200f),
                    SpawnPos = new Vector3(-1570.438f, -571.884f, 105.200f),
                    SpawnHeading = 30.991f,
                    IPL = "imp_sm_13_modgarage",
                    InteriorID = 256257,
                    PurchaseCamPos = new Vector3(-1573.166f, -571.276f, 107.2f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 126f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(name, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    TriggerPos = new Vector3(-1560.776f, -569.241f, 114.448f),
                    SpawnPos = new Vector3(-1561.777f, -567.676f, 114.448f),
                    SpawnHeading = 31.058f
                };

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
