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
                blipPosition = new Vector3(-118.791f, -608.376f, 36.281f);
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(buildingName, "Owner", -1);
                entranceTrigger = new Vector3(-118.791f, -608.376f, 36.281f);
                entranceSpawn = new Vector3(-117.505f, -608.885f, 36.281f);
                entranceHeading = 250.669f;
                purchaseCamPos = new Vector3(-167.906f, -487.694f, 40f);
                purchaseCamRot = new Vector3(30f, 0, -170f);
                purchaseFOV = 70f;
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
                garageIPL = "imp_dt1_02_cargarage_a";
                garageInteriorID = 253441;
                garageTrigger = new Vector3(-198.651f, -580.700f, 136.001f);
                garageSpawn = new Vector3(-196.910f, -580.757f, 136.001f);
                garageHeading = 267.449f;
                garageCamPos = new Vector3(-196.910f, -580.757f, 138f);
                garageCamRot = new Vector3(-5f, -1f, -98f);
                garageCamFOV = 90f;
                garageDecorationCamPos = new Vector3(-191.562f, -589.096f, 136f);
                garageDecorationCamRot = new Vector3(5f, 0f, -34f);
                garageDecorationCamFOV = 60f;
                garageDecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageDecorationStyle"));
                garageLightingCamPos = new Vector3(-192.426f, -585.685f, 136f);
                garageLightingCamRot = new Vector3(55f, 0f, -135f);
                garageLightingCamFOV = 70f;
                garageLightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageLightingStyle"));
                garageNumberingCamPos = new Vector3(-190.784f, -575.345f, 136f);
                garageNumberingCamRot = new Vector3(12f, 0f, 6f);
                garageNumberingCamFOV = 60f;
                garageNumberingStyle = GetGarageNumberingStyle(SinglePlayerOffice.Configs.GetValue(buildingName, "GarageNumberingStyle"));
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
                exteriors = new List<string>() { "hei_dt1_02_w01", "dt1_02_helipad_01", "dt1_02_dt1_emissive_dt1_02" };
                officeSofaPos = new Vector3(-137.806f, -644.631f, 167.820f);
                officeSofaRot = new Vector3(0f, 0f, 186f);
                //officeSofaStartPos = new Vector3(-137.859f, -644.034f, 168.820f);
                //officeSofaStartHeading = 6f;
                officeTVTrigger = new Vector3(-138.406f, -639.809f, 169.235f);
                //officeComputerChairPos = new Vector3(-125.645f, -641.945f, 168.441f);
                //officeComputerChairRot = new Vector3(0f, 0f, -129f);
                //officeLeftSafeTrigger = new Vector3(-124.192f, -639.577f, 168.821f);
                //officeLeftSafeStartPos = new Vector3(-125.492f, -639.708f, 168.820f);
                //officeLeftSafeStartHeading = 276f;
                //officeRightSafeTrigger = new Vector3(-123.742f, -643.171f, 168.821f);
                //officeRightSafeStartPos = new Vector3(-125.272f, -643.433f, 168.820f);
                //officeRightSafeStartHeading = 276f;
                officeRadioTrigger = new Vector3(-128.186f, -638.605f, 168.821f);
                officeRadioHeading = 5.807f;
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
                officeWardrobeTrigger = new Vector3(-132.303f, -632.859f, 168.820f);
                officeWardrobeHeading = 276.090f;
                officeWardrobeCamPos = new Vector3(-130.797f, -632.694f, 168.820f);
                officeWardrobeCamRot = new Vector3(0f, 0f, 96.009f);
                officeWardrobeCamFOV = 75f;
                officePaChairPos = new Vector3(-138.994f, -634.089f, 168.423f);
                officePaChairRot = new Vector3(0f, 0f, -174f);

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
