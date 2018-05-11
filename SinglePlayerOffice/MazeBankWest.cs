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
                blipPosition = new Vector3(-1370.370f, -503.067f, 33.157f);
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                entranceTrigger = new Vector3(-1370.370f, -503.067f, 33.157f);
                entranceSpawn = new Vector3(-1371.608f, -503.967f, 33.157f);
                entranceHeading = 125.509f;
                purchaseCamPos = new Vector3(-1320.468f, -534.793f, 40f);
                purchaseCamRot = new Vector3(12f, 0f, 40f);
                purchaseFOV = 60f;
                officeInteriorIDs = new List<int>() { 244225, 244481, 243969, 243201, 243457, 243713, 244737, 244993, 245249 };
                officeTrigger = new Vector3(-1392.566f, -480.734f, 72.042f);
                officeSpawn = new Vector3(-1391.416f, -479.347f, 72.042f);
                officeHeading = 281.429f;
                officeCamPos = new Vector3(-1366.876f, -480.338f, 73f);
                officeCamRot = new Vector3(-5f, 0f, 50f);
                officeCamFOV = 60f;
                officeInteriorStyles = new List<OfficeInteriorStyle>() {
                    new OfficeInteriorStyle("Executive Rich", 0, "ex_sm_15_office_02b"),
                    new OfficeInteriorStyle("Executive Cool", 415000, "ex_sm_15_office_02c"),
                    new OfficeInteriorStyle("Executive Contrast", 500000, "ex_sm_15_office_02a"),
                    new OfficeInteriorStyle("Old Spice Classical", 685000, "ex_sm_15_office_01b"),
                    new OfficeInteriorStyle("Old Spice Vintage", 760000, "ex_sm_15_office_01c"),
                    new OfficeInteriorStyle("Old Spice Warms", 950000, "ex_sm_15_office_01a"),
                    new OfficeInteriorStyle("Power Broker Conservative", 835000, "ex_sm_15_office_03b"),
                    new OfficeInteriorStyle("Power Broker Polished", 910000, "ex_sm_15_office_03c"),
                    new OfficeInteriorStyle("Power Broker Ice", 1000000, "ex_sm_15_office_03a")
                };
                officeInteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "OfficeInteriorStyle"));
                garageIPL = "imp_sm_15_cargarage_a";
                garageInteriorID = 256513;
                garageTrigger = new Vector3(-1396.394f, -480.688f, 57.100f);
                garageSpawn = new Vector3(-1394.964f, -480.362f, 57.100f);
                garageHeading = 279.062f;
                garageCamPos = new Vector3(-1394.964f, -480.362f, 59f);
                garageCamRot = new Vector3(-5f, -1f, -93f);
                garageCamFOV = 90f;
                garageDecorationCamPos = new Vector3(-1389.028f, -488.630f, 57f);
                garageDecorationCamRot = new Vector3(5f, 0f, -32f);
                garageDecorationCamFOV = 60f;
                garageDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageDecorationStyle"));
                garageLightingCamPos = new Vector3(-1390f, -485.350f, 57f);
                garageLightingCamRot = new Vector3(55f, 0f, -135f);
                garageLightingCamFOV = 70f;
                garageLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageLightingStyle"));
                garageNumberingCamPos = new Vector3(-1389.340f, -473.314f, 57f);
                garageNumberingCamRot = new Vector3(12f, 0f, 9f);
                garageNumberingCamFOV = 60f;
                garageNumberingStyle = GetGarageNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageNumberingStyle"));
                modShopIPL = "imp_sm_15_modgarage";
                modShopInteriorID = 257281;
                modShopTrigger = new Vector3(-1392.375f, -482.942f, 78.200f);
                modShopSpawn = new Vector3(-1390.757f, -482.723f, 78.200f);
                modShopHeading = 275.322f;
                modShopCamPos = new Vector3(-1388.860f, -480.712f, 80.2f);
                modShopCamRot = new Vector3(-20f, 0f, 9f);
                modShopCamFOV = 70f;
                modShopFloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "ModShopFloorStyle"));
                hasExtraOfficeDecors = SinglePlayerOffice.Configs.GetValue(buildingName, "HasExtraOfficeDecors", false);
                heliPadTrigger = new Vector3(-1369.535f, -472.037f, 84.447f);
                heliPadSpawn = new Vector3(-1368.162f, -471.002f, 84.447f);
                heliPadHeading = 304.157f;
                exteriors = new List<string>() { "sm_15_bld2_dtl", "hei_sm_15_bld2", "sm_15_bld2_LOD", "sm_15_bld2_dtl3", "sm_15_bld1_dtl3", "sm_15_bld2_railing", "sm_15_emissive", "sm_15_emissive_LOD" };
                officeSofaPos = new Vector3(-1369.017f, -476.016f, 71.05f);
                officeSofaRot = new Vector3(0f, 0f, 262f);
                //officeSofaStartPos = new Vector3(-1369.649f, -475.963f, 72.050f);
                //officeSofaStartHeading = 82f;
                officeTVTrigger = new Vector3(-1373.887f, -476.763f, 72.457f);
                //officeComputerChairPos = new Vector3(-1372.186f, -463.935f, 71.662f);
                //officeComputerChairRot = new Vector3(0f, 0f, -37f);
                //officeLeftSafeTrigger = new Vector3(-1374.602f, -462.492f, 72.042f);
                //officeLeftSafeStartPos = new Vector3(-1374.428f, -463.860f, 72.042f);
                //officeLeftSafeStartHeading = 8f;
                //officeRightSafeTrigger = new Vector3(-1371.012f, -461.985f, 72.042f);
                //officeRightSafeStartPos = new Vector3(-1370.773f, -463.448f, 72.042f);
                //officeRightSafeStartHeading = 8f;
                officeRadioTrigger = new Vector3(-1375.435f, -466.592f, 72.042f);
                officeRadioHeading = 99.656f;
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
                officeWardrobeTrigger = new Vector3(-1381.006f, -470.913f, 72.042f);
                officeWardrobeHeading = 8.162f;
                officeWardrobeCamPos = new Vector3(-1381.236f, -469.422f, 72.042f);
                officeWardrobeCamRot = new Vector3(0f, 0f, 187.570f);
                officeWardrobeCamFOV = 75f;
                officePaChairPos = new Vector3(-1379.528f, -477.600f, 71.644f);
                officePaChairRot = new Vector3(0f, 0f, -82f);

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
