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
                buildingName = "Maze Bank West";
                buildingDescription = "Maze has been the target for more boycotts, demonstrations and civil rights lawsuits than any other bank in America. Access to that wealth of experience doesn't come cheap, but a good education is an investment worth any price.";
                price = 1000000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                blipPosition = new Vector3(-1370.370f, -503.067f, 33.157f);
                interiorIDs = new List<int>() { 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249, 256513, 256769, 257025, 257281 };
                exteriorIPLs = new List<string>() { "sm_15_bld2_dtl", "hei_sm_15_bld2", "sm_15_bld2_LOD", "sm_15_bld2_dtl3", "sm_15_bld1_dtl3", "sm_15_bld2_railing", "sm_15_emissive", "sm_15_emissive_LOD" };
                entrance = new Entrance {
                    Trigger = new Vector3(-1370.370f, -503.067f, 33.157f),
                    Spawn = new Vector3(-1371.608f, -503.967f, 33.157f),
                    Heading = 125.509f
                };
                office = new Office {
                    InteriorIDs = new List<int>() { 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249 },
                    Trigger = new Vector3(-1392.566f, -480.734f, 72.042f),
                    Spawn = new Vector3(-1391.416f, -479.347f, 72.042f),
                    Heading = 281.429f,
                    CamPos = new Vector3(-1366.876f, -480.338f, 73f),
                    CamRot = new Vector3(-5f, 0f, 50f),
                    CamFOV = 60f,
                    InteriorStyles = new List<OfficeInteriorStyle>() {
                        new OfficeInteriorStyle("Executive Rich", 0, "ex_sm_15_office_02b"),
                        new OfficeInteriorStyle("Executive Cool", 415000, "ex_sm_15_office_02c"),
                        new OfficeInteriorStyle("Executive Contrast", 500000, "ex_sm_15_office_02a"),
                        new OfficeInteriorStyle("Old Spice Classical", 685000, "ex_sm_15_office_01b"),
                        new OfficeInteriorStyle("Old Spice Vintage", 760000, "ex_sm_15_office_01c"),
                        new OfficeInteriorStyle("Old Spice Warms", 950000, "ex_sm_15_office_01a"),
                        new OfficeInteriorStyle("Power Broker Conservative", 835000, "ex_sm_15_office_03b"),
                        new OfficeInteriorStyle("Power Broker Polished", 910000, "ex_sm_15_office_03c"),
                        new OfficeInteriorStyle("Power Broker Ice", 1000000, "ex_sm_15_office_03a")
                    },
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(buildingName, "HasExtraOfficeDecors", false)
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "OfficeInteriorStyle"));
                garageOne = new Garage {
                    IPL = "imp_sm_15_cargarage_a",
                    InteriorID = 256513,
                    Trigger = new Vector3(-1396.394f, -480.688f, 57.100f),
                    Spawn = new Vector3(-1394.713f, -480.488f, 57.100f),
                    Heading = -81f,
                    CamPos = new Vector3(-1394.713f, -480.488f, 59.100f),
                    CamRot = new Vector3(-5f, -1f, -81f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1388.986f, -488.544f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-1390.211f, -485.338f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-1389.410f, -473.260f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneNumberingStyle"))
                };
                garageTwo = new Garage {
                    IPL = "imp_sm_15_cargarage_b",
                    InteriorID = 256769,
                    Trigger = new Vector3(-1396.407f, -480.761f, 49.101f),
                    Spawn = new Vector3(-1394.687f, -480.479f, 49.101f),
                    Heading = -81f,
                    CamPos = new Vector3(-1394.687f, -480.479f, 51.101f),
                    CamRot = new Vector3(-5f, -1f, -81f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1389.047f, -488.542f, 49.101f),
                    DecorationCamRot = new Vector3(5f, 0f, -31f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-1390.268f, -485.402f, 49.101f),
                    LightingCamRot = new Vector3(55f, 0f, -131f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-1389.405f, -473.322f, 49.101f),
                    NumberingCamRot = new Vector3(12f, 0f, 10f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoNumberingStyle"))
                };
                garageThree = new Garage {
                    IPL = "imp_sm_15_cargarage_c",
                    InteriorID = 257025,
                    Trigger = new Vector3(-1367.133f, -472.392f, 57.101f),
                    Spawn = new Vector3(-1368.905f, -472.632f, 57.101f),
                    Heading = 99f,
                    CamPos = new Vector3(-1368.905f, -472.632f, 59.101f),
                    CamRot = new Vector3(-5f, -1f, 99f),
                    CamFOV = 90f,
                    DecorationCamPos = new Vector3(-1374.548f, -464.566f, 57.100f),
                    DecorationCamRot = new Vector3(5f, 0f, 149f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-1373.297f, -467.776f, 57.100f),
                    LightingCamRot = new Vector3(55f, 0f, 49f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-1374.141f, -479.781f, 57.100f),
                    NumberingCamRot = new Vector3(12f, 0f, -170f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeNumberingStyle"))
                };
                modShop = new ModShop {
                    IPL = "imp_sm_15_modgarage",
                    InteriorID = 257281,
                    Trigger = new Vector3(-1392.375f, -482.942f, 78.200f),
                    Spawn = new Vector3(-1390.757f, -482.723f, 78.200f),
                    Heading = 275.322f,
                    CamPos = new Vector3(-1388.860f, -480.712f, 80.2f),
                    CamRot = new Vector3(-20f, 0f, 9f),
                    CamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    Trigger = new Vector3(-1369.535f, -472.037f, 84.447f),
                    Spawn = new Vector3(-1368.162f, -471.002f, 84.447f),
                    Heading = 304.157f
                };
                purchaseCamPos = new Vector3(-1320.468f, -534.793f, 40f);
                purchaseCamRot = new Vector3(12f, 0f, 40f);
                purchaseFOV = 60f;

                //officeSofaPos = new Vector3(-1369.017f, -476.016f, 71.05f);
                //officeSofaRot = new Vector3(0f, 0f, 262f);
                //officeSofaStartPos = new Vector3(-1369.649f, -475.963f, 72.050f);
                //officeSofaStartHeading = 82f;
                //officeTVTrigger = new Vector3(-1373.887f, -476.763f, 72.457f);
                //officeComputerChairPos = new Vector3(-1372.186f, -463.935f, 71.662f);
                //officeComputerChairRot = new Vector3(0f, 0f, -37f);
                //officeLeftSafeTrigger = new Vector3(-1374.602f, -462.492f, 72.042f);
                //officeLeftSafeStartPos = new Vector3(-1374.428f, -463.860f, 72.042f);
                //officeLeftSafeStartHeading = 8f;
                //officeRightSafeTrigger = new Vector3(-1371.012f, -461.985f, 72.042f);
                //officeRightSafeStartPos = new Vector3(-1370.773f, -463.448f, 72.042f);
                //officeRightSafeStartHeading = 8f;
                //officeRadioTrigger = new Vector3(-1375.435f, -466.592f, 72.042f);
                //officeRadioHeading = 99.656f;
                //officeBossChairTrigger = new Vector3(-1366.565f, -484.533f, 72.042f);
                //officeBossChairHeading = 98.170f;
                //officeStaffChairTriggers = new List<Vector3>() {
                //    new Vector3(-1368.276f, -486.732f, 72.042f),
                //    new Vector3(-1369.445f, -487.079f, 72.042f),
                //    new Vector3(-1370.602f, -487.322f, 72.042f),
                //    new Vector3(-1368.891f, -482.801f, 72.042f),
                //    new Vector3(-1370.087f, -482.876f, 72.042f),
                //    new Vector3(-1371.214f, -483.069f, 72.042f)

                //};
                //officeStaffChairHeadings = new List<float>() { 8.117f, 10.439f, 8.851f, 186.431f, 187.639f, 188.926f };
                //officeLaptopChairTriggers = new List<Vector3>() {
                //    new Vector3(-1375.267f, -484.522f, 72.042f),
                //    new Vector3(-1376.765f, -484.648f, 72.042f),
                //    new Vector3(-1378.255f, -484.893f, 72.042f)
                //};
                //officeLaptopChairHeadings = new List<float>() { 7.009f, 8.154f, 10.243f };
                //officeWardrobeTrigger = new Vector3(-1381.006f, -470.913f, 72.042f);
                //officeWardrobeHeading = 8.162f;
                //officeWardrobeCamPos = new Vector3(-1381.236f, -469.422f, 72.042f);
                //officeWardrobeCamRot = new Vector3(0f, 0f, 187.570f);
                //officeWardrobeCamFOV = 75f;
                //officePaChairPos = new Vector3(-1379.528f, -477.600f, 71.644f);
                //officePaChairRot = new Vector3(0f, 0f, -82f);

                CreateBuildingBlip();
                CreatePurchaseMenu();
                CreateTeleportMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }
}
