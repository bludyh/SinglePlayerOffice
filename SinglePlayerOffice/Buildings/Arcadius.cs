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
                name = "Arcadius Business Center";
                description = "The City within the City, the Arcadius Business Center boats more AAA hedge funds, smoothie bars and executive suicides per square foot of office space than any other building in the business district. Welcome to Cutting edge.";
                price = 2250000;
                owner = (Owner)SinglePlayerOffice.Configs.GetValue(name, "Owner", -1);
                blipPos = new Vector3(-118.791f, -608.376f, 36.281f);
                interiorIDs = new List<int>() { 134657, 237313, 237569, 237057, 236289, 236545, 236801, 237825, 238081, 238337, 253441, 253697, 253953, 254209 };
                exteriorIPLs = new List<string>() { "hei_dt1_02_w01", "dt1_02_helipad_01", "dt1_02_dt1_emissive_dt1_02" };
                entrance = new Entrance {
                    TriggerPos = new Vector3(-118.791f, -608.376f, 36.281f),
                    SpawnPos = new Vector3(-117.505f, -608.885f, 36.281f),
                    SpawnHeading = 250.669f,
                    PurchaseCamPos = new Vector3(-167.906f, -487.694f, 40f),
                    PurchaseCamRot = new Vector3(30f, 0, -170f),
                    PurchaseCamFOV = 70f
                };
                garageEntrance = new GarageEntrance {
                    TriggerPos = new Vector3(-143.998f, -576.076f, 32.060f),
                    SpawnPos = new Vector3(-143.998f, -576.076f, 32.060f),
                    SpawnHeading = 160f,
                    InteriorID = 134657,
                    ElevatorCamPos = new Vector3(-140.868f, -573.453f, 33.425f),
                    ElevatorCamRot = new Vector3(-10f, 0f, 125f),
                    ElevatorCamFOV = 60f
                };
                office = new Office {
                    TriggerPos = new Vector3(-141.670f, -620.949f, 168.821f),
                    SpawnPos = new Vector3(-140.327f, -622.087f, 168.820f),
                    SpawnHeading = 184.412f,
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
                    HasExtraDecors = SinglePlayerOffice.Configs.GetValue(name, "HasExtraOfficeDecors", false)
                };
                office.InteriorStyle = GetOfficeInteriorStyle(SinglePlayerOffice.Configs.GetValue(name, "OfficeInteriorStyle"));
                garageOne = new Garage {
                    TriggerPos = new Vector3(-198.649f, -580.730f, 136.001f),
                    SpawnPos = new Vector3(-196.790f, -580.510f, 136.001f),
                    SpawnHeading = -84f,
                    IPL = "imp_dt1_02_cargarage_a",
                    InteriorID = 253441,
                    PurchaseCamPos = new Vector3(-196.790f, -580.510f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -84f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-191.550f, -588.963f, 136.000f),
                    DecorationCamRot = new Vector3(5f, 0f, -34f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneDecorationStyle")),
                    LightingCamPos = new Vector3(-192.656f, -585.665f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, -135f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneLightingStyle")),
                    NumberingCamPos = new Vector3(-191.463f, -573.653f, 136.001f),
                    NumberingCamRot = new Vector3(12f, 0f, 6.520f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageOneNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageOneNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-181.798f, -581.548f, 134.116f),
                    ElevatorLevelBPos = new Vector3(-181.798f, -581.548f, 139.466f),
                    ElevatorLevelCPos = new Vector3(-181.798f, -581.548f, 144.816f),
                    ElevatorRot = new Vector3(0f, 0f, 96.096f)
                };
                garageTwo = new Garage {
                    TriggerPos = new Vector3(-124.515f, -571.676f, 136.001f),
                    SpawnPos = new Vector3(-122.979f, -571.062f, 136.001f),
                    SpawnHeading = -69f,
                    IPL = "imp_dt1_02_cargarage_b",
                    InteriorID = 253697,
                    PurchaseCamPos = new Vector3(-122.979f, -571.062f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, -69f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-115.602f, -577.715f, 136.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -19f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoDecorationStyle")),
                    LightingCamPos = new Vector3(-117.451f, -574.830f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, -120f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoLightingStyle")),
                    NumberingCamPos = new Vector3(-119.268f, -563.180f, 136.000f),
                    NumberingCamRot = new Vector3(12f, 0f, 21f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageTwoNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageTwoNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-107.975f, -568.080f, 134.116f),
                    ElevatorLevelBPos = new Vector3(-107.975f, -568.080f, 139.466f),
                    ElevatorLevelCPos = new Vector3(-107.975f, -568.080f, 144.816f),
                    ElevatorRot = new Vector3(0f, 0f, 110.928f)
                };
                garageThree = new Garage {
                    TriggerPos = new Vector3(-135.621f, -622.349f, 136.001f),
                    SpawnPos = new Vector3(-135.882f, -623.975f, 136.001f),
                    SpawnHeading = 171f,
                    IPL = "imp_dt1_02_cargarage_c",
                    InteriorID = 253953,
                    PurchaseCamPos = new Vector3(-135.882f, -623.975f, 138f),
                    PurchaseCamRot = new Vector3(-5f, -1f, 171f),
                    PurchaseCamFOV = 90f,
                    DecorationCamPos = new Vector3(-145.349f, -627.036f, 136.001f),
                    DecorationCamRot = new Vector3(5f, 0f, -139f),
                    DecorationCamFOV = 60f,
                    DecorationStyle = GetGarageDecorationStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeDecorationStyle")),
                    LightingCamPos = new Vector3(-141.992f, -626.889f, 136.000f),
                    LightingCamRot = new Vector3(55f, 0f, 120f),
                    LightingCamFOV = 70f,
                    LightingStyle = GetGarageLightingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeLightingStyle")),
                    NumberingCamPos = new Vector3(-130.550f, -631.243f, 136.000f),
                    NumberingCamRot = new Vector3(12f, 0f, -98f),
                    NumberingCamFOV = 60f,
                    NumberingStyle = GetGarageThreeNumberingStyle(SinglePlayerOffice.Configs.GetValue(name, "GarageThreeNumberingStyle")),
                    ElevatorLevelAPos = new Vector3(-140.817f, -638.450f, 134.116f),
                    ElevatorLevelBPos = new Vector3(-140.817f, -638.450f, 139.466f),
                    ElevatorLevelCPos = new Vector3(-140.817f, -638.450f, 144.816f),
                    ElevatorRot = new Vector3(0f, 0f, -9.082f)
                };
                modShop = new ModShop {
                    TriggerPos = new Vector3(-138.322f, -592.926f, 167.000f),
                    SpawnPos = new Vector3(-139.104f, -591.805f, 167.000f),
                    SpawnHeading = 34.856f,
                    IPL = "imp_dt1_02_modgarage",
                    InteriorID = 254209,
                    PurchaseCamPos = new Vector3(-142.051f, -591.137f, 169f),
                    PurchaseCamRot = new Vector3(-20f, 0f, 130f),
                    PurchaseCamFOV = 70f,
                    FloorStyle = GetModShopFloorStyle(SinglePlayerOffice.Configs.GetValue(name, "ModShopFloorStyle"))
                };
                heliPad = new HeliPad {
                    TriggerPos = new Vector3(-155.139f, -602.231f, 201.735f),
                    SpawnPos = new Vector3(-156.460f, -603.294f, 201.735f),
                    SpawnHeading = 128.035f
                };

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
                CreateGarageEntranceMenu();
                CreateVehicleElevatorMenu();
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }

    }
}
