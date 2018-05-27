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
                buildingName = "LomBank West";
                buildingDescription = "In a radical new step towards diversification of the banking sector. Industry giant Lombank is selling office space to results-driven, high-liquidity kill squads and drug cartels. Be part of the change our financial industry needs.";
                price = 3100000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                blipPosition = new Vector3(-1581.127f, -558.608f, 34.953f);
                interiorIDs = new List<int>() { 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945, 255489, 255745, 256001, 256257 };
                exteriorIPLs = new List<string>() { "sm_13_emissive", "sm_13_bld1", "sm_13_bld1_LOD" };
                entrance = new Entrance {
                    Trigger = new Vector3(-1581.127f, -558.608f, 34.953f),
                    Spawn = new Vector3(-1582.192f, -556.967f, 34.954f),
                    Heading = 32.916f
                };
                garageEntrance = new GarageEntrance {
                    Trigger = new Vector3(-1537.423f, -577.773f, 25.344f),
                    Spawn = new Vector3(-1537.423f, -577.773f, 25.344f),
                    Heading = 35f,
                    CamPos = new Vector3(-1536.926f, -581.923f, 26.708f),
                    CamRot = new Vector3(-10f, 0f, 0f),
                    CamFOV = 60f
                };
                office = new Office {
                    InteriorIDs = new List<int>() { 241921, 242177, 241665, 240897, 241153, 241409, 242433, 242689, 242945 },
                    Trigger = new Vector3(-1579.750f, -565.049f, 108.523f),
                    Spawn = new Vector3(-1578.013f, -565.397f, 108.523f),
                    Heading = 214.074f,
                    CamPos = new Vector3(-1567.370f, -587.616f, 109.5f),
                    CamRot = new Vector3(-5f, 0f, -12f),
                    CamFOV = 60f,
                    InteriorStyles = new List<OfficeInteriorStyle>() {
                        new OfficeInteriorStyle("Executive Rich", 0, "ex_sm_13_office_02b"),
                        new OfficeInteriorStyle("Executive Cool", 415000, "ex_sm_13_office_02c"),
                        new OfficeInteriorStyle("Executive Contrast", 500000, "ex_sm_13_office_02a"),
                        new OfficeInteriorStyle("Old Spice Classical", 685000, "ex_sm_13_office_01b"),
                        new OfficeInteriorStyle("Old Spice Vintage", 760000, "ex_sm_13_office_01c"),
                        new OfficeInteriorStyle("Old Spice Warms", 950000, "ex_sm_13_office_01a"),
                        new OfficeInteriorStyle("Power Broker Conservative", 835000, "ex_sm_13_office_03b"),
                        new OfficeInteriorStyle("Power Broker Polished", 910000, "ex_sm_13_office_03c"),
                        new OfficeInteriorStyle("Power Broker Ice", 1000000, "ex_sm_13_office_03a")
                    },
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(buildingName, "HasExtraOfficeDecors", false)
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "OfficeInteriorStyle"));
                garageOne = new Garage {
                    IPL = "imp_sm_13_cargarage_a",
                    InteriorID = 255489,
                    Trigger = new Vector3(-1586.341f, -561.390f, 86.501f),
                    Spawn = new Vector3(-1585.244f, -562.935f, 86.501f),
                    Heading = -144f,
                    CamPos = new Vector3(-1585.244f, -562.935f, 88.501f),
                    CamRot = new Vector3(-5f, -1f, -144f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1589.886f, -571.585f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, -94f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-1587.638f, -569.089f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 165f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-1576.517f, -564.090f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -54f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneNumberingStyle"))
                };
                garageTwo = new Garage {
                    IPL = "imp_sm_13_cargarage_b",
                    InteriorID = 255745,
                    Trigger = new Vector3(-1562.919f, -556.852f, 86.501f),
                    Spawn = new Vector3(-1564.414f, -557.834f, 86.501f),
                    Heading = 126f,
                    CamPos = new Vector3(-1564.414f, -557.834f, 88.501f),
                    CamRot = new Vector3(-5f, -1f, 126f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1573.072f, -553.264f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 176f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-1570.563f, -555.513f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, 76f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-1565.695f, -566.624f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, -142f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoNumberingStyle"))
                };
                garageThree = new Garage {
                    IPL = "imp_sm_13_cargarage_c",
                    InteriorID = 256001,
                    Trigger = new Vector3(-1558.330f, -580.248f, 86.501f),
                    Spawn = new Vector3(-1559.331f, -578.897f, 86.501f),
                    Heading = 36f,
                    CamPos = new Vector3(-1559.331f, -578.897f, 88.501f),
                    CamRot = new Vector3(-5f, -1f, 36f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1554.784f, -570.093f, 86.501f),
                    DecorationCamRot = new Vector3(5f, 0f, 86f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-1557.047f, -572.635f, 86.500f),
                    LightingCamRot = new Vector3(55f, 0f, -14f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-1568.135f, -577.467f, 86.500f),
                    NumberingCamRot = new Vector3(12f, 0f, 128f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeNumberingStyle"))
                };
                modShop = new ModShop {
                    IPL = "imp_sm_13_modgarage",
                    InteriorID = 256257,
                    Trigger = new Vector3(-1569.469f, -573.342f, 105.200f),
                    Spawn = new Vector3(-1570.438f, -571.884f, 105.200f),
                    Heading = 30.991f,
                    CamPos = new Vector3(-1573.166f, -571.276f, 107.2f),
                    CamRot = new Vector3(-20f, 0f, 126f),
                    CamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    Trigger = new Vector3(-1560.776f, -569.241f, 114.448f),
                    Spawn = new Vector3(-1561.777f, -567.676f, 114.448f),
                    Heading = 31.058f
                };
                purchaseCamPos = new Vector3(-1609.816f, -637.368f, 35f);
                purchaseCamRot = new Vector3(24f, 0f, -30f);
                purchaseFOV = 67f;

                //officeSofaPos = new Vector3(-1564.563f, -583.634f, 107.523f);
                //officeSofaRot = new Vector3(0f, 0f, 216f);
                //officeSofaStartPos = new Vector3(-1564.908f, -583.143f, 108.523f);
                //officeSofaStartHeading = 36f;
                //officeTVTrigger = new Vector3(-1567.501f, -579.754f, 108.938f);
                //officeComputerChairPos = new Vector3(-1555.381f, -575.224f, 108.143f);
                //officeComputerChairRot = new Vector3(0f, 0f, -99f);
                //officeLeftSafeTrigger = new Vector3(-1555.248f, -572.382f, 108.523f);
                //officeLeftSafeStartPos = new Vector3(-1556.368f, -573.211f, 108.523f);
                //officeLeftSafeStartHeading = 306f;
                //officeRightSafeTrigger = new Vector3(-1553.079f, -575.399f, 108.523f);
                //officeRightSafeStartPos = new Vector3(-1554.314f, -576.325f, 108.523f);
                //officeRightSafeStartHeading = 306f;
                //officeRadioTrigger = new Vector3(-1559.245f, -573.592f, 108.523f);
                //officeRadioHeading = 38.517f;
                //officeBossChairTrigger = new Vector3(-1570.975f, -589.777f, 108.523f);
                //officeBossChairHeading = 39.281f;
                //officeStaffChairTriggers = new List<Vector3>() {
                //    new Vector3(-1573.8f, -589.477f, 108.523f),
                //    new Vector3(-1574.431f, -588.483f, 108.523f),
                //    new Vector3(-1575.140f, -587.551f, 108.523f),
                //    new Vector3(-1570.494f, -587.037f, 108.523f),
                //    new Vector3(-1571.157f, -586.029f, 108.523f),
                //    new Vector3(-1571.841f, -585.113f, 108.523f)

                //};
                //officeStaffChairHeadings = new List<float>() { 307.687f, 307.806f, 305.995f, 124.545f, 126.026f, 126.031f };
                //officeLaptopChairTriggers = new List<Vector3>() {
                //    new Vector3(-1574.990f, -582.261f, 108.523f),
                //    new Vector3(-1575.864f, -580.978f, 108.523f),
                //    new Vector3(-1576.795f, -579.749f, 108.523f)
                //};
                //officeLaptopChairHeadings = new List<float>() { 308.681f, 306.355f, 306.415f };
                //officeWardrobeTrigger = new Vector3(-1565.723f, -570.756f, 108.523f);
                //officeWardrobeHeading = 305.946f;
                //officeWardrobeCamPos = new Vector3(-1564.539f, -569.827f, 108.523f);
                //officeWardrobeCamRot = new Vector3(0f, 0f, 125.692f);
                //officeWardrobeCamFOV = 75f;
                //officePaChairPos = new Vector3(-1570.820f, -575.100f, 108.125f);
                //officePaChairRot = new Vector3(0f, 0f, -144f);

                CreateBuildingBlip();
                CreatePurchaseMenu();
                CreateTeleportMenu();
                CreateVehicleElevatorMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }
}
