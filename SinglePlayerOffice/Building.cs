using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    abstract class Building {

        protected string buildingName;
        protected string buildingDescription;
        protected int price;
        protected Vector3 blipPosition;
        protected Blip buildingBlip;
        protected Owner owner;
        protected Vector3 entranceTrigger;
        protected Vector3 entranceSpawn;
        protected float entranceHeading;
        protected Vector3 purchaseCamPos;
        protected Vector3 purchaseCamRot;
        protected float purchaseFOV;
        private Camera purchaseCam;
        private int officeInteriorID;
        protected List<int> officeInteriorIDs;
        protected Vector3 officeTrigger;
        protected Vector3 officeSpawn;
        protected float officeHeading;
        protected Vector3 officeCamPos;
        protected Vector3 officeCamRot;
        protected float officeCamFOV;
        private Camera officeCam;
        protected List<OfficeInteriorStyle> officeInteriorStyles;
        protected OfficeInteriorStyle officeInteriorStyle;
        protected string garageIPL;
        protected int garageInteriorID;
        protected Vector3 garageTrigger;
        protected Vector3 garageSpawn;
        protected float garageHeading;
        protected Vector3 garageCamPos;
        protected Vector3 garageCamRot;
        protected float garageCamFOV;
        private Camera garageCam;
        protected Vector3 garageDecorationCamPos;
        protected Vector3 garageDecorationCamRot;
        protected float garageDecorationCamFOV;
        private Camera garageDecorationCam;
        private List<GarageDecorationStyle> garageDecorationStyles;
        protected GarageDecorationStyle garageDecorationStyle;
        protected Vector3 garageLightingCamPos;
        protected Vector3 garageLightingCamRot;
        protected float garageLightingCamFOV;
        private Camera garageLightingCam;
        private List<GarageLightingStyle> garageLightingStyles;
        protected GarageLightingStyle garageLightingStyle;
        protected Vector3 garageNumberingCamPos;
        protected Vector3 garageNumberingCamRot;
        protected float garageNumberingCamFOV;
        private Camera garageNumberingCam;
        private List<GarageNumberingStyle> garageNumberingStyles;
        protected GarageNumberingStyle garageNumberingStyle;
        protected string modShopIPL;
        protected int modShopInteriorID;
        protected Vector3 modShopTrigger;
        protected Vector3 modShopSpawn;
        protected float modShopHeading;
        protected Vector3 modShopCamPos;
        protected Vector3 modShopCamRot;
        protected float modShopCamFOV;
        private Camera modShopCam;
        private List<ModShopFloorStyle> modShopFloorStyles;
        protected ModShopFloorStyle modShopFloorStyle;
        private List<string> extraOfficeDecors;
        protected bool hasExtraOfficeDecors;
        private int extraOfficeDecorsPrice;
        protected Vector3 heliPadTrigger;
        protected Vector3 heliPadSpawn;
        protected float heliPadHeading;
        private UIMenu purchaseMenu;
        private UIMenu teleportMenu;
        protected List<string> exteriors;
        protected Vector3 officeSofaPos;
        protected Vector3 officeSofaRot;
        //protected Vector3 officeSofaStartPos;
        //protected float officeSofaStartHeading;
        protected Vector3 officeTVTrigger;
        //protected Vector3 officeComputerChairPos;
        //protected Vector3 officeComputerChairRot;
        //protected Vector3 officeLeftSafeTrigger;
        //protected Vector3 officeLeftSafeStartPos;
        //protected float officeLeftSafeStartHeading;
        //protected Vector3 officeRightSafeTrigger;
        //protected Vector3 officeRightSafeStartPos;
        //protected float officeRightSafeStartHeading;
        protected Vector3 officeRadioTrigger;
        protected float officeRadioHeading;
        protected Vector3 officeBossChairTrigger;
        //protected float officeBossChairHeading;
        protected List<Vector3> officeStaffChairTriggers;
        protected List<float> officeStaffChairHeadings;
        //protected List<Vector3> officeLaptopChairTriggers;
        //protected List<float> officeLaptopChairHeadings;
        protected Vector3 officeWardrobeTrigger;
        protected float officeWardrobeHeading;
        protected Vector3 officeWardrobeCamPos;
        protected Vector3 officeWardrobeCamRot;
        protected float officeWardrobeCamFOV;
        protected Vector3 officePaChairPos;
        protected Vector3 officePaChairRot;

        public InteractionsController InteractionsController { get; set; }

        protected Building() {
            garageDecorationStyles = new List<GarageDecorationStyle>() {
                new GarageDecorationStyle("Decoration 1", 0, "garage_decor_01"),
                new GarageDecorationStyle("Decoration 2", 285000, "garage_decor_02"),
                new GarageDecorationStyle("Decoration 3", 415000, "garage_decor_04"),
                new GarageDecorationStyle("Decoration 4", 500000, "garage_decor_03")
            };
            garageLightingStyles = new List<GarageLightingStyle>() {
                new GarageLightingStyle("Lighting 1", 0, "lighting_option01"),
                new GarageLightingStyle("Lighting 2", 81500, "lighting_option04"),
                new GarageLightingStyle("Lighting 3", 85000, "lighting_option03"),
                new GarageLightingStyle("Lighting 4", 87500, "lighting_option07"),
                new GarageLightingStyle("Lighting 5", 92500, "lighting_option06"),
                new GarageLightingStyle("Lighting 6", 99500, "lighting_option05"),
                new GarageLightingStyle("Lighting 7", 105000, "lighting_option08"),
                new GarageLightingStyle("Lighting 8", 127500, "lighting_option02"),
                new GarageLightingStyle("Lighting 9", 150000, "lighting_option09")
            };
            garageNumberingStyles = new List<GarageNumberingStyle>() {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n1"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n1"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n1"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n1"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n1"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n1"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n1"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n1"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n1"),
            };
            modShopFloorStyles = new List<ModShopFloorStyle>() {
                new ModShopFloorStyle("Floor 1", 0, ""),
                new ModShopFloorStyle("Floor 2", 120000, "floor_vinyl_18"),
                new ModShopFloorStyle("Floor 3", 132500, "floor_vinyl_16"),
                new ModShopFloorStyle("Floor 4", 145000, "floor_vinyl_17"),
                new ModShopFloorStyle("Floor 5", 157500, "floor_vinyl_19"),
                new ModShopFloorStyle("Floor 6", 170000, "floor_vinyl_06"),
                new ModShopFloorStyle("Floor 7", 182500, "floor_vinyl_08"),
                new ModShopFloorStyle("Floor 8", 195000, "floor_vinyl_07"),
                new ModShopFloorStyle("Floor 9", 207500, "floor_vinyl_09"),
                new ModShopFloorStyle("Floor 10", 220000, "floor_vinyl_10"),
                new ModShopFloorStyle("Floor 11", 245000, "floor_vinyl_14"),
                new ModShopFloorStyle("Floor 12", 270000, "floor_vinyl_15"),
                new ModShopFloorStyle("Floor 13", 295000, "floor_vinyl_13"),
                new ModShopFloorStyle("Floor 14", 320000, "floor_vinyl_12"),
                new ModShopFloorStyle("Floor 15", 345000, "floor_vinyl_11"),
                new ModShopFloorStyle("Floor 16", 370000, "floor_vinyl_05"),
                new ModShopFloorStyle("Floor 17", 395000, "floor_vinyl_04"),
                new ModShopFloorStyle("Floor 18", 420000, "floor_vinyl_01"),
                new ModShopFloorStyle("Floor 19", 465000, "floor_vinyl_02"),
                new ModShopFloorStyle("Floor 20", 500000, "floor_vinyl_03")
            };
            extraOfficeDecors = new List<string>() {
                "cash_set_01",
                "cash_set_02",
                "cash_set_03",
                "cash_set_04",
                "cash_set_05",
                "cash_set_06",
                "cash_set_07",
                "cash_set_08",
                "cash_set_09",
                "cash_set_10",
                "cash_set_11",
                "cash_set_12",
                "cash_set_13",
                "cash_set_14",
                "cash_set_15",
                "cash_set_16",
                "cash_set_17",
                "cash_set_18",
                "cash_set_19",
                "cash_set_20",
                "cash_set_21",
                "cash_set_22",
                "cash_set_23",
                "cash_set_24",
                "swag_art",
                "swag_art2",
                "swag_art3",
                "swag_booze_cigs",
                "swag_booze_cigs2",
                "swag_booze_cigs3",
                "swag_counterfeit",
                "swag_counterfeit2",
                "swag_counterfeit3",
                "swag_drugbags",
                "swag_drugbags2",
                "swag_drugbags3",
                "swag_drugstatue",
                "swag_drugstatue2",
                "swag_drugstatue3",
                "swag_electronic",
                "swag_electronic2",
                "swag_electronic3",
                "swag_furcoats",
                "swag_furcoats2",
                "swag_furcoats3",
                "swag_gems",
                "swag_gems2",
                "swag_gems3",
                "swag_guns",
                "swag_guns2",
                "swag_guns3",
                "swag_ivory",
                "swag_ivory2",
                "swag_ivory3",
                "swag_jewelwatch",
                "swag_jewelwatch2",
                "swag_jewelwatch3",
                "swag_med",
                "swag_med2",
                "swag_med3",
                "swag_pills",
                "swag_pills2",
                "swag_pills3",
                "swag_silver",
                "swag_silver2",
                "swag_silver3"
            };
            extraOfficeDecorsPrice = 0;
            InteractionsController = new InteractionsController();
        }

        protected void CreateBuildingBlip() {
            buildingBlip = World.CreateBlip(blipPosition);
            Function.Call(Hash.SET_BLIP_SPRITE, buildingBlip, 475);
            Function.Call(Hash._0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, buildingName);
            Function.Call(Hash._0xBC38B49BCB83BC9B, buildingBlip);
            SetBuildingBlipColor();
        }

        public void SetBuildingBlipColor() {
            switch (owner) {
                case Owner.Michael: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 3); break;
                case Owner.Franklin: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 11); break;
                case Owner.Trevor: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 17); break;
                default: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 4); break;
            }
        }

        protected OfficeInteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                if (style.Name == name) {
                    return style;
                }
            }
            return null;
        }

        protected GarageDecorationStyle GetGarageDecorationStyle(string name) {
            foreach (GarageDecorationStyle style in garageDecorationStyles) {
                if (style.Name == name) {
                    return style;
                }
            }
            return null;
        }

        protected GarageLightingStyle GetGarageLightingStyle(string name) {
            foreach (GarageLightingStyle style in garageLightingStyles) {
                if (style.Name == name) {
                    return style;
                }
            }
            return null;
        }

        protected GarageNumberingStyle GetGarageNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in garageNumberingStyles) {
                if (style.Name == name) {
                    return style;
                }
            }
            return null;
        }

        protected ModShopFloorStyle GetModShopFloorStyle(string name) {
            foreach (ModShopFloorStyle style in modShopFloorStyles) {
                if (style.Name == name) {
                    return style;
                }
            }
            return null;
        }

        private Floor GetCurrentFloor() {
            int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);
            Vector3 currentPlayerPos = new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
            if (currentPlayerPos.DistanceTo(entranceTrigger) < 10f) {
                return Floor.Entrance;
            }
            if (officeInteriorIDs.Contains(currentInteriorID)) {
                return Floor.Office;
            }
            if (currentInteriorID == garageInteriorID) {
                return Floor.Garage;
            }
            if (currentInteriorID == modShopInteriorID) {
                return Floor.ModShop;
            }
            if (currentPlayerPos.DistanceTo(heliPadTrigger) < 10f) {
                return Floor.HeliPad;
            }
            return Floor.None;
        }

        private int GetToTalPrice() {
            return price + officeInteriorStyle.Price + garageDecorationStyle.Price + garageLightingStyle.Price + garageNumberingStyle.Price + modShopFloorStyle.Price + extraOfficeDecorsPrice;
        }

        private void UpdatePurchaseMenuPriceDescription() {
            purchaseMenu.RemoveItemAt(purchaseMenu.MenuItems.Count - 1);
            purchaseMenu.AddItem(new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice())));
        }

        private void UpdateTeleportMenuButtons() {
            teleportMenu.Clear();
            UIMenuItem goToOfficeBtn = new UIMenuItem("Office");
            UIMenuItem goToGarageBtn = new UIMenuItem("Garage");
            UIMenuItem goToModShopBtn = new UIMenuItem("Mod Shop");
            UIMenuItem goToHeliPadBtn = new UIMenuItem("Heli Pad");
            UIMenuItem exitBuildingBtn = new UIMenuItem("Exit the building");
            switch (GetCurrentFloor()) {
                case Floor.Entrance:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    break;
                case Floor.Office:
                    teleportMenu.AddItem(goToGarageBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Floor.Garage:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Floor.ModShop:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Floor.HeliPad:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
            }
            teleportMenu.RefreshIndex();
        }

        private void HideBuildingExteriors() {
            Function.Call(Hash._0x4B5CFC83122DF602);
            foreach (string exterior in exteriors) {
                int exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash)5819624144786551657, exteriorHash);
            }
            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        protected void CreatePurchaseMenu() {
            purchaseMenu = new UIMenu(buildingName, "~b~Purchase Options") {
                MouseEdgeEnabled = false,
            };
            
            UIMenu officeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Office Interiors", buildingDescription);
            foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                officeInteriorsMenu.AddItem(item);
            }
            officeInteriorsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            officeInteriorsMenu.RefreshIndex();
            officeInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                    Function.Call(Hash.REMOVE_IPL, style.IPL);
                }
                Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[index].IPL);
                officeInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_chairs");
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_booze");
                foreach (string decor in extraOfficeDecors) {
                    Function.Call(Hash._DISABLE_INTERIOR_PROP, officeInteriorID, decor);
                }
                if (hasExtraOfficeDecors) {
                    foreach (string decor in extraOfficeDecors) {
                        Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, decor);
                    }
                }
                Function.Call(Hash.REFRESH_INTERIOR, officeInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                officeInteriorStyle = officeInteriorStyles[index];
                foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) {
                    i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                }
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Garage Interiors", buildingDescription);
            UIMenu garageDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in garageDecorationStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageDecorationsMenu.AddItem(item);
            }
            garageDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageDecorationsMenu.RefreshIndex();
            garageDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (GarageDecorationStyle style in garageDecorationStyles) {
                    Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                }
                Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageDecorationStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, garageInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageDecorationStyle = garageDecorationStyles[index];
                foreach (UIMenuItem i in garageDecorationsMenu.MenuItems) {
                    i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                }
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in garageLightingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageLightingsMenu.AddItem(item);
            }
            garageLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageLightingsMenu.RefreshIndex();
            garageLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (GarageLightingStyle style in garageLightingStyles) {
                    Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                }
                Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageLightingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, garageInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageLightingStyle = garageLightingStyles[index];
                foreach (UIMenuItem i in garageLightingsMenu.MenuItems) {
                    i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                }
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in garageNumberingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageNumberingsMenu.AddItem(item);
            }
            garageNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageNumberingsMenu.RefreshIndex();
            garageNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (GarageNumberingStyle style in garageNumberingStyles) {
                    Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                }
                Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageNumberingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, garageInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageNumberingStyle = garageNumberingStyles[index];
                foreach (UIMenuItem i in garageNumberingsMenu.MenuItems) {
                    i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                }
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageInteriorsMenu.RefreshIndex();
            garageInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageDecorationsMenu && World.RenderingCamera != garageDecorationCam) {
                    garageDecorationCam = World.CreateCamera(garageDecorationCamPos, garageDecorationCamRot, garageDecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageDecorationCam, 1000, true, true);
                }
                if (nextMenu == garageLightingsMenu && World.RenderingCamera != garageLightingCam) {
                    garageLightingCam = World.CreateCamera(garageLightingCamPos, garageLightingCamRot, garageLightingCamFOV);
                    World.RenderingCamera.InterpTo(garageLightingCam, 1000, true, true);
                }
                if (nextMenu == garageNumberingsMenu && World.RenderingCamera != garageNumberingCam) {
                    garageNumberingCam = World.CreateCamera(garageNumberingCamPos, garageNumberingCamRot, garageNumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageNumberingCam, 1000, true, true);
                }
            };

            UIMenu modShopInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Mod Shop Interiors", buildingDescription);
            foreach (ModShopFloorStyle style in modShopFloorStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                modShopInteriorsMenu.AddItem(item);
            }
            modShopInteriorsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            modShopInteriorsMenu.RefreshIndex();
            modShopInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (ModShopFloorStyle style in modShopFloorStyles) {
                    Function.Call(Hash._DISABLE_INTERIOR_PROP, modShopInteriorID, style.PropName);
                }
                Function.Call(Hash._ENABLE_INTERIOR_PROP, modShopInteriorID, modShopFloorStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, modShopInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            modShopInteriorsMenu.OnItemSelect += (sender, item, index) => {
                modShopFloorStyle = modShopFloorStyles[index];
                foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) {
                    i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                }
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenuCheckboxItem extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations", false, String.Format("Price: ~g~${0:n0}", 1650000));
            purchaseMenu.AddItem(extraDecorsOption);
            
            UIMenuItem purchaseBtn = new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice()));
            purchaseMenu.AddItem(purchaseBtn);
            purchaseMenu.RefreshIndex();
            purchaseMenu.OnMenuChange += (sender, nextMenu, forward) => {
                InteractionsController.ResetInterations();
                if (nextMenu == officeInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    officeInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, officeInteriorID, decor);
                    }
                    if (hasExtraOfficeDecors) {
                        foreach (string decor in extraOfficeDecors) {
                            Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, decor);
                        }
                    }
                    Function.Call(Hash.REFRESH_INTERIOR, officeInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == garageInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (GarageDecorationStyle style in garageDecorationStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    foreach (GarageLightingStyle style in garageLightingStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    foreach (GarageNumberingStyle style in garageNumberingStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    if (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, garageIPL)) Function.Call(Hash.REQUEST_IPL, garageIPL);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageDecorationStyles[garageDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageLightingStyles[garageLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageNumberingStyles[garageNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, garageInteriorID);
                    Game.Player.Character.Position = garageSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    garageCam = World.CreateCamera(garageCamPos, garageCamRot, garageCamFOV);
                    World.RenderingCamera = garageCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == modShopInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (ModShopFloorStyle style in modShopFloorStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, modShopInteriorID, style.PropName);
                    }
                    if (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, modShopIPL)) Function.Call(Hash.REQUEST_IPL, modShopIPL);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, modShopInteriorID, modShopFloorStyles[modShopInteriorsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, modShopInteriorID);
                    Game.Player.Character.Position = modShopSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    modShopCam = World.CreateCamera(modShopCamPos, modShopCamRot, modShopCamFOV);
                    World.RenderingCamera = modShopCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            purchaseMenu.OnMenuClose += (sender) => {
                Game.FadeScreenOut(1000);
                Script.Wait(1000);

                World.RenderingCamera = null;
                World.DestroyAllCameras();
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Position = entranceSpawn;
                Game.Player.Character.Heading = entranceHeading;
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            purchaseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                InteractionsController.ResetInterations();
                hasExtraOfficeDecors = isChecked;
                if (hasExtraOfficeDecors) {
                    Game.FadeScreenOut(500);
                    Script.Wait(500);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    officeInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) {
                        if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, decor)) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, decor);
                    }
                    Function.Call(Hash.REFRESH_INTERIOR, officeInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    Script.Wait(500);
                    Game.FadeScreenIn(500);

                    extraOfficeDecorsPrice = 1650000;
                }
                else {
                    Game.FadeScreenOut(500);
                    Script.Wait(500);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    officeInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, officeInteriorID, decor);
                    }
                    Function.Call(Hash.REFRESH_INTERIOR, officeInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    Script.Wait(500);
                    Game.FadeScreenIn(500);

                    extraOfficeDecorsPrice = 0;
                }
                UpdatePurchaseMenuPriceDescription();
            };
            purchaseMenu.OnItemSelect += (sender, item, index) => {
                if (item.Text == "Purchase") {
                    if (Game.Player.Money < GetToTalPrice()) {
                        UI.ShowSubtitle("~r~Not enough money!");
                    }
                    else {
                        owner = (Owner)Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character);
                        SetBuildingBlipColor();
                        SinglePlayerOffice.Configs.SetValue(buildingName, "Owner", (int)owner);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "OfficeInteriorStyle", officeInteriorStyle.Name);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "GarageDecorationStyle", garageDecorationStyle.Name);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "GarageLightingStyle", garageLightingStyle.Name);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "GarageNumberingStyle", garageNumberingStyle.Name);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "ModShopFloorStyle", modShopFloorStyle.Name);
                        SinglePlayerOffice.Configs.SetValue(buildingName, "HasExtraOfficeDecors", hasExtraOfficeDecors);
                        SinglePlayerOffice.Configs.Save();

                        SinglePlayerOffice.MenuPool.CloseAllMenus();
                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);

                        World.RenderingCamera = null;
                        World.DestroyAllCameras();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Position = entranceSpawn;
                        Game.Player.Character.Heading = entranceHeading;
                        Game.Player.Character.Task.ClearAll();
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                        Script.Wait(1000);

                        Game.PlaySound("PROPERTY_PURCHASE", "HUD_AWARDS");
                        Game.Player.Money -= GetToTalPrice();
                        BigMessageThread.MessageInstance.ShowSimpleShard("Buiding Purchased", buildingName);
                    }
                }
            };
            SinglePlayerOffice.MenuPool.Add(purchaseMenu);
        }

        protected void CreateTeleportMenu() {
            teleportMenu = new UIMenu(buildingName, "");
            teleportMenu.OnItemSelect += (sender, item, index) => {
                InteractionsController.ResetInterations();
                ScenesController.ResetScenes();
                if (item.Text == "Office") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REMOVE_IPL, garageIPL);
                    Function.Call(Hash.REMOVE_IPL, modShopIPL);
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyle.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Heading = officeHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    officeInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, officeInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, officeInteriorID, decor);
                    }
                    if (hasExtraOfficeDecors) {
                        foreach (string decor in extraOfficeDecors) {
                            Function.Call(Hash._ENABLE_INTERIOR_PROP, officeInteriorID, decor);
                        }
                    }
                    Function.Call(Hash.REFRESH_INTERIOR, officeInteriorID);
                    ScenesController.StartOfficeScenes(officeInteriorID, owner, officeBossChairTrigger, officeStaffChairTriggers, officePaChairPos, officePaChairRot);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Garage") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (GarageDecorationStyle style in garageDecorationStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    foreach (GarageLightingStyle style in garageLightingStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    foreach (GarageNumberingStyle style in garageNumberingStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, garageInteriorID, style.PropName);
                    }
                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REMOVE_IPL, modShopIPL);
                    if (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, garageIPL)) Function.Call(Hash.REQUEST_IPL, garageIPL);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageDecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageLightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, garageInteriorID, garageNumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, garageInteriorID);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageSpawn;
                    Game.Player.Character.Heading = garageHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Mod Shop") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (ModShopFloorStyle style in modShopFloorStyles) {
                        Function.Call(Hash._DISABLE_INTERIOR_PROP, modShopInteriorID, style.PropName);
                    }
                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REMOVE_IPL, garageIPL);
                    if (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, modShopIPL)) Function.Call(Hash.REQUEST_IPL, modShopIPL);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, modShopInteriorID, modShopFloorStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, modShopInteriorID);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = modShopSpawn;
                    Game.Player.Character.Heading = modShopHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Heli Pad") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REMOVE_IPL, garageIPL);
                    Function.Call(Hash.REMOVE_IPL, modShopIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = heliPadSpawn;
                    Game.Player.Character.Heading = heliPadHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Exit the building") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    foreach (OfficeInteriorStyle style in officeInteriorStyles) {
                        Function.Call(Hash.REMOVE_IPL, style.IPL);
                    }
                    Function.Call(Hash.REMOVE_IPL, garageIPL);
                    Function.Call(Hash.REMOVE_IPL, modShopIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = entranceSpawn;
                    Game.Player.Character.Heading = entranceHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            teleportMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            SinglePlayerOffice.MenuPool.Add(teleportMenu);
        }

        public void OnTick() {
            switch (GetCurrentFloor()) {
                case Floor.Entrance:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(entranceTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        if (owner != Owner.None) {
                            if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the building");
                                if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                    Game.Player.Character.Task.StandStill(-1);
                                    UpdateTeleportMenuButtons();
                                    SinglePlayerOffice.IsHudHidden = true;
                                    teleportMenu.Visible = true;
                                }
                            }
                            else {
                                if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 8 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 17) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(String.Format("Press ~INPUT_CONTEXT~ to visit {0}'s building", owner));
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                        Game.Player.Character.Task.StandStill(-1);
                                        UpdateTeleportMenuButtons();
                                        SinglePlayerOffice.IsHudHidden = true;
                                        teleportMenu.Visible = true;
                                    }
                                }
                                else {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame("Building is closed. You can come back between 9:00 and 17:00");
                                }
                            }
                        }
                        else {
                            SinglePlayerOffice.DisplayHelpTextThisFrame("You do not own this building~n~Press ~INPUT_CONTEXT~ to purchase");
                            if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                Game.Player.Character.Task.StandStill(-1);

                                Game.FadeScreenOut(1000);
                                Script.Wait(1000);

                                SinglePlayerOffice.IsHudHidden = true;
                                purchaseMenu.Visible = true;
                                purchaseCam = World.CreateCamera(purchaseCamPos, purchaseCamRot, purchaseFOV);
                                World.RenderingCamera = purchaseCam;

                                Script.Wait(1000);
                                Game.FadeScreenIn(1000);
                            }
                        }
                    }
                    break;
                case Floor.Office:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        if (Game.Player.Character.Position.DistanceTo(officeTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                            SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                            if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                Game.Player.Character.Task.StandStill(-1);
                                UpdateTeleportMenuButtons();
                                SinglePlayerOffice.IsHudHidden = true;
                                teleportMenu.Visible = true;
                            }
                        }
                        if (Game.Player.Character.Position.DistanceTo(officeSofaPos) < 1.5f && InteractionsController.SofaInteractionStatus == -1) {
                            SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.SofaInteractionHelpText);
                            if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                InteractionsController.StartSofaInteraction(officeSofaPos, officeSofaRot);
                            }
                        }
                        if (Game.Player.Character.Position.DistanceTo(officeTVTrigger) < 1.5f && InteractionsController.TVInteractionStatus == -1) {
                            SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.TVInteractionHelpText);
                            if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                InteractionsController.StartTVInteraction();
                            }
                        }
                        if (Game.Player.Character.Position.DistanceTo(officeRadioTrigger) < 1.0f && InteractionsController.RadioInteractionStatus == -1) {
                            SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.RadioInteractionHelpText);
                            if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                InteractionsController.StartRadioInteraction(officeRadioTrigger, officeRadioHeading, officeInteriorStyle);
                            }
                        }
                        if (Game.Player.Character.Position.DistanceTo(officeWardrobeTrigger) < 1.0f && InteractionsController.WardrobeInteractionStatus == -1) {
                            if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.WardrobeInteractionHelpText);
                                if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                    InteractionsController.StartWardrobeInteraction(officeWardrobeTrigger, officeWardrobeHeading, officeWardrobeCamPos, officeWardrobeCamRot, officeWardrobeCamFOV);
                                }
                            }
                            else {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.WardrobeInteractionRejectHelpText);
                            }
                        }
                        Prop[] nearbyProps = World.GetNearbyProps(Game.Player.Character.Position, 1.4f);
                        if (nearbyProps.Length == 0) break;
                        Prop closestProp = World.GetClosest(Game.Player.Character.Position, nearbyProps);
                        switch (closestProp.Model.Hash) {
                            case -1626066319:
                            case 1339364336:
                                if (InteractionsController.ComputerInteractionStatus != -1) break;
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.ComputerInterationHelpText);
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartComputerInteraction(closestProp);
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.ComputerInteractionRejectHelpText);
                                break;
                            case 646926492:
                            case 845785021:
                            case -1126494299:
                            case -524920966:
                            case -1842578680:
                            case -1387653807:
                                if (InteractionsController.LeftSafeInteractionStatus != -1) break;
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.LeftSafeInteractionHelpText);
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartLeftSafeInteraction(closestProp);
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.SafeInteractionRejectHelpText);
                                break;
                            case -1176373441:
                            case -1149617688:
                            case -548219756:
                            case 1854960432:
                            case 682108925:
                            case 1002451519:
                                if (InteractionsController.RightSafeInteractionStatus != -1) break;
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.RightSafeInteractionHelpText);
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartRightSafeInteraction(closestProp);
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.SafeInteractionRejectHelpText);
                                break;
                            case -1278649385:
                                Prop[] nearbyChairs = World.GetNearbyProps(closestProp.Position, 1.4f, -1278649385);
                                Prop[] nearbyLaptops = World.GetNearbyProps(closestProp.Position, 1f, 1385417869);
                                if (nearbyChairs.Length == 1 && nearbyLaptops.Length == 0) {
                                    if (InteractionsController.BossChairInteractionStatus != -1) break;
                                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                        SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.BossChairInteractionHelpText);
                                        if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartBossChairInteraction(closestProp);
                                    }
                                    else SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.BossChairInteractionRejectHelpText);
                                }
                                else if (nearbyChairs.Length != 1 && nearbyLaptops.Length == 0) {
                                    if (InteractionsController.StaffChairInteractionStatus != -1) break;
                                    if (Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, closestProp, "anim@amb@office@boardroom@crew@male@var_b@base@", "base_chair", 2) || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, closestProp, "anim@amb@office@boardroom@crew@female@var_c@base@", "base_chair", 2)) break;
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.StaffChairInteractionHelpText);
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartStaffChairInteraction(closestProp);
                                }
                                else if (nearbyChairs.Length == 1 && nearbyLaptops.Length != 0) {
                                    if (InteractionsController.LaptopChairInteractionStatus != -1) break;
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(InteractionsController.LaptopChairInteractionHelpText);
                                    if (Game.IsControlPressed(2, GTA.Control.Context)) InteractionsController.StartLaptopChairInteraction(closestProp);
                                }
                                break;
                        }
                    }
                    break;
                case Floor.Garage:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Floor.ModShop:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(modShopTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Floor.HeliPad:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(heliPadTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the stairs");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
            }
        }

        public void Dispose() {
            buildingBlip.Remove();
        }

    }
}
