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
                buildingName = "Arcadius Business Center";
                buildingDescription = "The City within the City, the Arcadius Business Center boats more AAA hedge funds, smoothie bars and executive suicides per square foot of office space than any other building in the business district. Welcome to Cutting edge.";
                price = 2250000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                blipPosition = new Vector3(-118.791f, -608.376f, 36.281f);
                entranceTrigger = new Vector3(-118.791f, -608.376f, 36.281f);
                entranceSpawn = new Vector3(-117.505f, -608.885f, 36.281f);
                entranceHeading = 250.669f;
                purchaseCamPos = new Vector3(-167.906f, -487.694f, 40f);
                purchaseCamRot = new Vector3(30f, 0, -170f);
                purchaseFOV = 70f;
                interiorIDs = new List<int>() { 237313, 237569, 237057, 236289, 236545, 236801, 237825, 238081, 238337, 253441, 253697, 253953, 254209 };
                officeInteriorIDs = new List<int>() { 237313, 237569, 237057, 236289, 236545, 236801, 237825, 238081, 238337 };
                officeTrigger = new Vector3(-141.670f, -620.949f, 168.821f);
                officeSpawn = new Vector3(-140.327f, -622.087f, 168.820f);
                officeHeading = 184.412f;
                officeCamPos = new Vector3(-142.224f, -646.676f, 170f);
                officeCamRot = new Vector3(-5f, 0f, -43f);
                officeCamFOV = 60f;
                officeInteriorStyles = new List<OfficeInteriorStyle>() {
                    new OfficeInteriorStyle("Executive Rich", 0, "ex_dt1_02_office_02b"),
                    new OfficeInteriorStyle("Executive Cool", 415000, "ex_dt1_02_office_02c"),
                    new OfficeInteriorStyle("Executive Contrast", 500000, "ex_dt1_02_office_02a"),
                    new OfficeInteriorStyle("Old Spice Classical", 685000, "ex_dt1_02_office_01b"),
                    new OfficeInteriorStyle("Old Spice Vintage", 760000, "ex_dt1_02_office_01c"),
                    new OfficeInteriorStyle("Old Spice Warms", 950000, "ex_dt1_02_office_01a"),
                    new OfficeInteriorStyle("Power Broker Conservative", 835000, "ex_dt1_02_office_03b"),
                    new OfficeInteriorStyle("Power Broker Polished", 910000, "ex_dt1_02_office_03c"),
                    new OfficeInteriorStyle("Power Broker Ice", 1000000, "ex_dt1_02_office_03a")
                };
                officeInteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "OfficeInteriorStyle"));
                garageOneIPL = "imp_dt1_02_cargarage_a";
                garageOneInteriorID = 253441;
                garageOneTrigger = new Vector3(-198.649f, -580.730f, 136.001f);
                garageOneSpawn = new Vector3(-196.790f, -580.510f, 136.001f);
                garageOneHeading = -84f;
                garageOneCamPos = new Vector3(-196.790f, -580.510f, 138f);
                garageOneCamRot = new Vector3(-5f, -1f, -84f);
                garageOneCamFOV = 90f;
                garageOneDecorationCamPos = new Vector3(-191.550f, -588.963f, 136.000f);
                garageOneDecorationCamRot = new Vector3(5f, 0f, -34f);
                garageOneDecorationCamFOV = 60f;
                garageOneDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneDecorationStyle"));
                garageOneLightingCamPos = new Vector3(-192.656f, -585.665f, 136.000f);
                garageOneLightingCamRot = new Vector3(55f, 0f, -135f);
                garageOneLightingCamFOV = 70f;
                garageOneLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneLightingStyle"));
                garageOneNumberingCamPos = new Vector3(-191.463f, -573.653f, 136.001f);
                garageOneNumberingCamRot = new Vector3(12f, 0f, 6.520f);
                garageOneNumberingCamFOV = 60f;
                garageOneNumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageOneNumberingStyle"));
                garageTwoIPL = "imp_dt1_02_cargarage_b";
                garageTwoInteriorID = 253697;
                garageTwoTrigger = new Vector3(-124.515f, -571.676f, 136.001f);
                garageTwoSpawn = new Vector3(-122.979f, -571.062f, 136.001f);
                garageTwoHeading = -69f;
                garageTwoCamPos = new Vector3(-122.979f, -571.062f, 138f);
                garageTwoCamRot = new Vector3(-5f, -1f, -69f);
                garageTwoCamFOV = 90f;
                garageTwoDecorationCamPos = new Vector3(-115.602f, -577.715f, 136.001f);
                garageTwoDecorationCamRot = new Vector3(5f, 0f, -19f);
                garageTwoDecorationCamFOV = 60f;
                garageTwoDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoDecorationStyle"));
                garageTwoLightingCamPos = new Vector3(-117.451f, -574.830f, 136.000f);
                garageTwoLightingCamRot = new Vector3(55f, 0f, -120f);
                garageTwoLightingCamFOV = 70f;
                garageTwoLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoLightingStyle"));
                garageTwoNumberingCamPos = new Vector3(-119.268f, -563.180f, 136.000f);
                garageTwoNumberingCamRot = new Vector3(12f, 0f, 21f);
                garageTwoNumberingCamFOV = 60f;
                garageTwoNumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageTwoNumberingStyle"));
                garageThreeIPL = "imp_dt1_02_cargarage_c";
                garageThreeInteriorID = 253953;
                garageThreeTrigger = new Vector3(-135.621f, -622.349f, 136.001f);
                garageThreeSpawn = new Vector3(-135.882f, -623.975f, 136.001f);
                garageThreeHeading = 171f;
                garageThreeCamPos = new Vector3(-135.882f, -623.975f, 138f);
                garageThreeCamRot = new Vector3(-5f, -1f, 171f);
                garageThreeCamFOV = 90f;
                garageThreeDecorationCamPos = new Vector3(-145.349f, -627.036f, 136.001f);
                garageThreeDecorationCamRot = new Vector3(5f, 0f, -139f);
                garageThreeDecorationCamFOV = 60f;
                garageThreeDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeDecorationStyle"));
                garageThreeLightingCamPos = new Vector3(-141.992f, -626.889f, 136.000f);
                garageThreeLightingCamRot = new Vector3(55f, 0f, 120f);
                garageThreeLightingCamFOV = 70f;
                garageThreeLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeLightingStyle"));
                garageThreeNumberingCamPos = new Vector3(-130.550f, -631.243f, 136.000f);
                garageThreeNumberingCamRot = new Vector3(12f, 0f, -98f);
                garageThreeNumberingCamFOV = 60f;
                garageThreeNumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageThreeNumberingStyle"));
                modShopIPL = "imp_dt1_02_modgarage";
                modShopInteriorID = 254209;
                modShopTrigger = new Vector3(-138.322f, -592.926f, 167.000f);
                modShopSpawn = new Vector3(-139.104f, -591.805f, 167.000f);
                modShopHeading = 34.856f;
                modShopCamPos = new Vector3(-142.051f, -591.137f, 169f);
                modShopCamRot = new Vector3(-20f, 0f, 130f);
                modShopCamFOV = 70f;
                modShopFloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "ModShopFloorStyle"));
                hasExtraOfficeDecors = SinglePlayerOffice.Configs.GetValue(buildingName, "HasExtraOfficeDecors", false);
                heliPadTrigger = new Vector3(-155.139f, -602.231f, 201.735f);
                heliPadSpawn = new Vector3(-156.460f, -603.294f, 201.735f);
                heliPadHeading = 128.035f;
                exteriorIPLs = new List<string>() { "hei_dt1_02_w01", "dt1_02_helipad_01", "dt1_02_dt1_emissive_dt1_02" };

                //officeSofaPos = new Vector3(-137.806f, -644.631f, 167.820f);
                //officeSofaRot = new Vector3(0f, 0f, 186f);
                //officeSofaStartPos = new Vector3(-137.859f, -644.034f, 168.820f);
                //officeSofaStartHeading = 6f;
                //officeTVTrigger = new Vector3(-138.406f, -639.809f, 169.235f);
                //officeComputerChairPos = new Vector3(-125.645f, -641.945f, 168.441f);
                //officeComputerChairRot = new Vector3(0f, 0f, -129f);
                //officeLeftSafeTrigger = new Vector3(-124.192f, -639.577f, 168.821f);
                //officeLeftSafeStartPos = new Vector3(-125.492f, -639.708f, 168.820f);
                //officeLeftSafeStartHeading = 276f;
                //officeRightSafeTrigger = new Vector3(-123.742f, -643.171f, 168.821f);
                //officeRightSafeStartPos = new Vector3(-125.272f, -643.433f, 168.820f);
                //officeRightSafeStartHeading = 276f;
                //officeRadioTrigger = new Vector3(-128.186f, -638.605f, 168.821f);
                //officeRadioHeading = 5.807f;
                //officeBossChairTrigger = new Vector3(-146.374f, -646.895f, 168.821f);
                //officeBossChairHeading = 6.006f;
                //officeStaffChairTriggers = new List<Vector3>() {
                //    new Vector3(-148.771f, -645.045f, 168.820f),
                //    new Vector3(-148.799f, -643.852f, 168.821f),
                //    new Vector3(-148.961f, -642.664f, 168.821f),
                //    new Vector3(-144.661f, -644.588f, 168.821f),
                //    new Vector3(-144.785f, -643.439f, 168.821f),
                //    new Vector3(-144.888f, -642.273f, 168.821f)
                    
                //};
                //officeStaffChairHeadings = new List<float>() { 276.992f, 277.038f, 276.835f, 94.423f, 95.754f, 98.838f };
                //officeLaptopChairTriggers = new List<Vector3>() {
                //    new Vector3(-146.074f, -638.172f, 168.820f),
                //    new Vector3(-146.187f, -636.645f, 168.820f),
                //    new Vector3(-146.405f, -635.169f, 168.820f)
                //};
                //officeLaptopChairHeadings = new List<float>() { 278.144f, 275.984f, 275.789f };
                //officeWardrobeTrigger = new Vector3(-132.303f, -632.859f, 168.820f);
                //officeWardrobeHeading = 276.090f;
                //officeWardrobeCamPos = new Vector3(-130.797f, -632.694f, 168.820f);
                //officeWardrobeCamRot = new Vector3(0f, 0f, 96.009f);
                //officeWardrobeCamFOV = 75f;
                //officePaChairPos = new Vector3(-138.994f, -634.089f, 168.423f);
                //officePaChairRot = new Vector3(0f, 0f, -174f);

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
