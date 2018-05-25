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
                buildingName = "Maze Bank Tower";
                buildingDescription = "The building that defined the LS Skyline for a generation. Taller, Classier, more opulent, more shamelessly phallic, less structurally sound - you name it, the Maze Bank Tower is best in class.";
                price = 4000000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                blipPosition = new Vector3(-79.214f, -796.513f, 44.227f);
                entranceTrigger = new Vector3(-79.214f, -796.513f, 44.227f);
                entranceSpawn = new Vector3(-80.690f, -795.859f, 44.227f);
                entranceHeading = 94.827f;
                purchaseCamPos = new Vector3(-86.710f, -724.772f, 48f);
                purchaseCamRot = new Vector3(32f, 0f, 180f);
                purchaseFOV = 90f;
                interiorIDs = new List<int>() { 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641, 254465, 254721, 254977, 255233 };
                officeInteriorIDs = new List<int>() { 239617, 239873, 239361, 238593, 238849, 239105, 240129, 240385, 240641 };
                officeTrigger = new Vector3(-75.771f, -827.188f, 243.386f);
                officeSpawn = new Vector3(-76.042f, -825.297f, 243.386f);
                officeHeading = 335.794f;
                officeCamPos = new Vector3(-63.668f, -804.309f, 244.5f);
                officeCamRot = new Vector3(-5f, 0f, 111f);
                officeCamFOV = 60f;
                officeInteriorStyles = new List<OfficeInteriorStyle>() {
                    new OfficeInteriorStyle("Executive Rich", 0, "ex_dt1_11_office_02b"),
                    new OfficeInteriorStyle("Executive Cool", 415000, "ex_dt1_11_office_02c"),
                    new OfficeInteriorStyle("Executive Contrast", 500000, "ex_dt1_11_office_02a"),
                    new OfficeInteriorStyle("Old Spice Classical", 685000, "ex_dt1_11_office_01b"),
                    new OfficeInteriorStyle("Old Spice Vintage", 760000, "ex_dt1_11_office_01c"),
                    new OfficeInteriorStyle("Old Spice Warms", 950000, "ex_dt1_11_office_01a"),
                    new OfficeInteriorStyle("Power Broker Conservative", 830000, "ex_dt1_11_office_03b"),
                    new OfficeInteriorStyle("Power Broker Polished", 910000, "ex_dt1_11_office_03c"),
                    new OfficeInteriorStyle("Power Broker Ice", 1000000, "ex_dt1_11_office_03a")
                };
                officeInteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "OfficeInteriorStyle"));
                garageOneIPL = "imp_dt1_11_cargarage_a";
                garageOneInteriorID = 254465;
                garageOneTrigger = new Vector3(-91.308f, -821.317f, 222.001f);
                garageOneSpawn = new Vector3(-90.054f, -821.810f, 222.000f);
                garageOneHeading = -110f;
                garageOneCamPos = new Vector3(-90.054f, -821.810f, 224.000f);
                garageOneCamRot = new Vector3(-5f, -1f, -110f);
                garageOneCamFOV = 90f;
                garageOneDecorationCamPos = new Vector3(-89.055f, -831.615f, 222.001f);
                garageOneDecorationCamRot = new Vector3(5f, 0f, -60f);
                garageOneDecorationCamFOV = 60f;
                garageOneDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneDecorationStyle"));
                garageOneLightingCamPos = new Vector3(-88.599f, -828.209f, 222.000f);
                garageOneLightingCamRot = new Vector3(55f, 0f, -160f);
                garageOneLightingCamFOV = 70f;
                garageOneLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneLightingStyle"));
                garageOneNumberingCamPos = new Vector3(-82.111f, -817.941f, 222.001f);
                garageOneNumberingCamRot = new Vector3(12f, 0f, -20f);
                garageOneNumberingCamFOV = 60f;
                garageOneNumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneNumberingStyle"));
                garageTwoIPL = "imp_dt1_11_cargarage_b";
                garageTwoInteriorID = 254721;
                garageTwoTrigger = new Vector3(-71.733f, -832.331f, 222.000f);
                garageTwoSpawn = new Vector3(-71.108f, -830.692f, 222.001f);
                garageTwoHeading = -20f;
                garageTwoCamPos = new Vector3(-71.108f, -830.692f, 224.001f);
                garageTwoCamRot = new Vector3(-5f, -1f, -20f);
                garageTwoCamFOV = 90f;
                garageTwoDecorationCamPos = new Vector3(-61.260f, -829.552f, 222.001f);
                garageTwoDecorationCamRot = new Vector3(5f, 0f, 30f);
                garageTwoDecorationCamFOV = 60f;
                garageTwoDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoDecorationStyle"));
                garageTwoLightingCamPos = new Vector3(-64.710f, -829.159f, 222.001f);
                garageTwoLightingCamRot = new Vector3(55f, 0f, -70f);
                garageTwoLightingCamFOV = 70f;
                garageTwoLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoLightingStyle"));
                garageTwoNumberingCamPos = new Vector3(-75.044f, -822.666f, 222.001f);
                garageTwoNumberingCamRot = new Vector3(12f, 0f, 70f);
                garageTwoNumberingCamFOV = 60f;
                garageTwoNumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoNumberingStyle"));
                garageThreeIPL = "imp_dt1_11_cargarage_c";
                garageThreeInteriorID = 254977;
                garageThreeTrigger = new Vector3(-78.528f, -805.686f, 222.001f);
                garageThreeSpawn = new Vector3(-79.146f, -807.408f, 222.001f);
                garageThreeHeading = 160f;
                garageThreeCamPos = new Vector3(-79.146f, -807.408f, 224.001f);
                garageThreeCamRot = new Vector3(-5f, -1f, 160f);
                garageThreeCamFOV = 90f;
                garageThreeDecorationCamPos = new Vector3(-88.997f, -808.442f, 222.001f);
                garageThreeDecorationCamRot = new Vector3(5f, 0f, -150f);
                garageThreeDecorationCamFOV = 60f;
                garageThreeDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeDecorationStyle"));
                garageThreeLightingCamPos = new Vector3(-85.524f, -808.880f, 222.001f);
                garageThreeLightingCamRot = new Vector3(55f, 0f, 110f);
                garageThreeLightingCamFOV = 70f;
                garageThreeLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeLightingStyle"));
                garageThreeNumberingCamPos = new Vector3(-75.209f, -815.338f, 222.000f);
                garageThreeNumberingCamRot = new Vector3(12f, 0f, -110f);
                garageThreeNumberingCamFOV = 60f;
                garageThreeNumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeNumberingStyle"));
                modShopIPL = "imp_dt1_11_modgarage";
                modShopInteriorID = 255233;
                modShopTrigger = new Vector3(-68.824f, -814.253f, 285.000f);
                modShopSpawn = new Vector3(-69.980f, -813.785f, 285.000f);
                modShopHeading = 66.700f;
                modShopCamPos = new Vector3(-72.767f, -814.608f, 287f);
                modShopCamRot = new Vector3(-20f, 0f, 160f);
                modShopCamFOV = 70f;
                modShopFloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "ModShopFloorStyle"));
                hasExtraOfficeDecors = SinglePlayerOffice.Configs.GetValue(buildingName, "HasExtraOfficeDecors", false);
                heliPadTrigger = new Vector3(-67.784f, -821.609f, 321.287f);
                heliPadSpawn = new Vector3(-65.938f, -822.145f, 321.285f);
                heliPadHeading = 253.635f;
                exteriorIPLs = new List<string>() { "dt1_11_dt1_emissive_dt1_11", "dt1_11_dt1_tower" };

                //officeSofaPos = new Vector3(-68.486f, -804.237f, 242.386f);
                //officeSofaRot = new Vector3(0f, 0f, 340f);
                //officeSofaStartPos = new Vector3(-68.700f, -804.797f, 243.386f);
                //officeSofaStartHeading = 160f;
                //officeTVTrigger = new Vector3(-70.083f, -808.826f, 243.801f);
                //officeComputerChairPos = new Vector3(-80.645f, -801.3f, 243.006f);
                //officeComputerChairRot = new Vector3(0f, 0f, 25f);
                //officeLeftSafeTrigger = new Vector3(-83.064f, -802.753f, 243.386f);
                //officeLeftSafeStartPos = new Vector3(-81.764f, -803.244f, 243.386f);
                //officeLeftSafeStartHeading = 70f;
                //officeRightSafeTrigger = new Vector3(-81.779f, -799.318f, 243.386f);
                //officeRightSafeStartPos = new Vector3(-80.329f, -799.799f, 243.386f);
                //officeRightSafeStartHeading = 70f;
                //officeRadioTrigger = new Vector3(-79.807f, -805.420f, 243.386f);
                //officeRadioHeading = 158.076f;
                //officeBossChairTrigger = new Vector3(-59.807f, -805.983f, 243.386f);
                //officeBossChairHeading = 159.971f;
                //officeStaffChairTriggers = new List<Vector3>() {
                //new Vector3(-58.570f, -808.573f, 243.386f),
                //    new Vector3(-59.061f, -809.654f, 243.386f),
                //    new Vector3(-59.457f, -810.764f, 243.386f),
                //    new Vector3(-62.394f, -807.197f, 243.386f),
                //    new Vector3(-62.813f, -808.352f, 243.386f),
                //    new Vector3(-63.273f, -809.462f, 243.386f)


                //};
                //officeStaffChairHeadings = new List<float>() { 70.543f, 69.427f, 70.151f, 248.893f, 250.610f, 249.552f };
                //officeLaptopChairTriggers = new List<Vector3>() {
                //    new Vector3(-63.980f, -813.590f, 243.386f),
                //    new Vector3(-64.423f, -815.084f, 243.386f),
                //    new Vector3(-64.970f, -816.533f, 243.386f)
                //};
                //officeLaptopChairHeadings = new List<float>() { 69.990f, 70.530f, 70.000f };
                //officeWardrobeTrigger = new Vector3(-78.625f, -812.353f, 243.386f);
                //officeWardrobeHeading = 70.638f;
                //officeWardrobeCamPos = new Vector3(-80.053f, -811.888f, 243.386f);
                //officeWardrobeCamRot = new Vector3(0f, 0f, 249.754f);
                //officeWardrobeCamFOV = 75f;
                //officePaChairPos = new Vector3(-72.069f, -814.251f, 242.988f);
                //officePaChairRot = new Vector3(0f, 0f, -20);

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
