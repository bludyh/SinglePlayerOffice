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
        protected Owner owner;
        protected Vector3 blipPosition;
        protected Blip buildingBlip;
        protected Vector3 entranceTrigger;
        protected Vector3 entranceSpawn;
        protected float entranceHeading;
        protected Vector3 purchaseCamPos;
        protected Vector3 purchaseCamRot;
        protected float purchaseFOV;
        private Camera purchaseCam;
        protected List<int> interiorIDs;
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
        protected string garageOneIPL;
        protected int garageOneInteriorID;
        protected Vector3 garageOneTrigger;
        protected Vector3 garageOneSpawn;
        protected float garageOneHeading;
        protected Vector3 garageOneCamPos;
        protected Vector3 garageOneCamRot;
        protected float garageOneCamFOV;
        private Camera garageOneCam;
        protected Vector3 garageOneDecorationCamPos;
        protected Vector3 garageOneDecorationCamRot;
        protected float garageOneDecorationCamFOV;
        private Camera garageOneDecorationCam;
        private List<GarageDecorationStyle> garageDecorationStyles;
        protected GarageDecorationStyle garageOneDecorationStyle;
        protected Vector3 garageOneLightingCamPos;
        protected Vector3 garageOneLightingCamRot;
        protected float garageOneLightingCamFOV;
        private Camera garageOneLightingCam;
        private List<GarageLightingStyle> garageLightingStyles;
        protected GarageLightingStyle garageOneLightingStyle;
        protected Vector3 garageOneNumberingCamPos;
        protected Vector3 garageOneNumberingCamRot;
        protected float garageOneNumberingCamFOV;
        private Camera garageOneNumberingCam;
        private List<GarageNumberingStyle> garageOneNumberingStyles;
        protected GarageNumberingStyle garageOneNumberingStyle;
        protected string garageTwoIPL;
        protected int garageTwoInteriorID;
        protected Vector3 garageTwoTrigger;
        protected Vector3 garageTwoSpawn;
        protected float garageTwoHeading;
        protected Vector3 garageTwoCamPos;
        protected Vector3 garageTwoCamRot;
        protected float garageTwoCamFOV;
        private Camera garageTwoCam;
        protected Vector3 garageTwoDecorationCamPos;
        protected Vector3 garageTwoDecorationCamRot;
        protected float garageTwoDecorationCamFOV;
        private Camera garageTwoDecorationCam;
        protected GarageDecorationStyle garageTwoDecorationStyle;
        protected Vector3 garageTwoLightingCamPos;
        protected Vector3 garageTwoLightingCamRot;
        protected float garageTwoLightingCamFOV;
        private Camera garageTwoLightingCam;
        protected GarageLightingStyle garageTwoLightingStyle;
        protected Vector3 garageTwoNumberingCamPos;
        protected Vector3 garageTwoNumberingCamRot;
        protected float garageTwoNumberingCamFOV;
        private Camera garageTwoNumberingCam;
        private List<GarageNumberingStyle> garageTwoNumberingStyles;
        protected GarageNumberingStyle garageTwoNumberingStyle;
        protected string garageThreeIPL;
        protected int garageThreeInteriorID;
        protected Vector3 garageThreeTrigger;
        protected Vector3 garageThreeSpawn;
        protected float garageThreeHeading;
        protected Vector3 garageThreeCamPos;
        protected Vector3 garageThreeCamRot;
        protected float garageThreeCamFOV;
        private Camera garageThreeCam;
        protected Vector3 garageThreeDecorationCamPos;
        protected Vector3 garageThreeDecorationCamRot;
        protected float garageThreeDecorationCamFOV;
        private Camera garageThreeDecorationCam;
        protected GarageDecorationStyle garageThreeDecorationStyle;
        protected Vector3 garageThreeLightingCamPos;
        protected Vector3 garageThreeLightingCamRot;
        protected float garageThreeLightingCamFOV;
        private Camera garageThreeLightingCam;
        protected GarageLightingStyle garageThreeLightingStyle;
        protected Vector3 garageThreeNumberingCamPos;
        protected Vector3 garageThreeNumberingCamRot;
        protected float garageThreeNumberingCamFOV;
        private Camera garageThreeNumberingCam;
        private List<GarageNumberingStyle> garageThreeNumberingStyles;
        protected GarageNumberingStyle garageThreeNumberingStyle;
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
        protected List<string> exteriorIPLs;

        public Owner Owner { get { return owner; } }
        public List<int> InteriorIDs { get { return interiorIDs; } }
        public Vector3 EntranceTrigger { get { return entranceTrigger; } }
        public Vector3 HeliPadTrigger { get { return heliPadTrigger; } }

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
            garageOneNumberingStyles = new List<GarageNumberingStyle>() {
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
            garageTwoNumberingStyles = new List<GarageNumberingStyle>() {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n2"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n2"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n2"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n2"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n2"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n2"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n2"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n2"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n2"),
            };
            garageThreeNumberingStyles = new List<GarageNumberingStyle>() {
                new GarageNumberingStyle("Signage 1", 0, "numbering_style07_n3"),
                new GarageNumberingStyle("Signage 2", 62500, "numbering_style08_n3"),
                new GarageNumberingStyle("Signage 3", 75000, "numbering_style09_n3"),
                new GarageNumberingStyle("Signage 4", 87500, "numbering_style02_n3"),
                new GarageNumberingStyle("Signage 5", 100000, "numbering_style03_n3"),
                new GarageNumberingStyle("Signage 6", 132500, "numbering_style01_n3"),
                new GarageNumberingStyle("Signage 7", 165000, "numbering_style06_n3"),
                new GarageNumberingStyle("Signage 8", 197500, "numbering_style05_n3"),
                new GarageNumberingStyle("Signage 9", 250000, "numbering_style04_n3"),
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
        }

        protected OfficeInteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (OfficeInteriorStyle style in officeInteriorStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageDecorationStyle GetGarageDecorationStyle(string name) {
            foreach (GarageDecorationStyle style in garageDecorationStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageLightingStyle GetGarageLightingStyle(string name) {
            foreach (GarageLightingStyle style in garageLightingStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageOneNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in garageOneNumberingStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageTwoNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in garageTwoNumberingStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageThreeNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in garageThreeNumberingStyles) if (style.Name == name) return style;
            return null;
        }

        protected ModShopFloorStyle GetModShopFloorStyle(string name) {
            foreach (ModShopFloorStyle style in modShopFloorStyles) if (style.Name == name) return style;
            return null;
        }

        protected void CreateBuildingBlip() {
            buildingBlip = World.CreateBlip(blipPosition);
            Function.Call(Hash.SET_BLIP_SPRITE, buildingBlip, 475);
            Function.Call(Hash._0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, buildingName);
            Function.Call(Hash._0xBC38B49BCB83BC9B, buildingBlip);
            SetBuildingBlipColor();
        }

        private void SetBuildingBlipColor() {
            switch (owner) {
                case Owner.Michael: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 3); break;
                case Owner.Franklin: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 11); break;
                case Owner.Trevor: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 17); break;
                default: Function.Call(Hash.SET_BLIP_COLOUR, buildingBlip, 4); break;
            }
        }

        private void RemoveAllIPLs() {
            foreach (OfficeInteriorStyle style in officeInteriorStyles) Function.Call(Hash.REMOVE_IPL, style.IPL);
            Function.Call(Hash.REMOVE_IPL, garageOneIPL);
            Function.Call(Hash.REMOVE_IPL, garageTwoIPL);
            Function.Call(Hash.REMOVE_IPL, garageThreeIPL);
            Function.Call(Hash.REMOVE_IPL, modShopIPL);
        }

        protected void CreatePurchaseMenu() {
            purchaseMenu = new UIMenu(buildingName, "~b~Purchase Options") { MouseEdgeEnabled = false };
            
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

                foreach (OfficeInteriorStyle style in officeInteriorStyles) Function.Call(Hash.REMOVE_IPL, style.IPL);
                Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[index].IPL);
                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                if (hasExtraOfficeDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                officeInteriorStyle = officeInteriorStyles[index];
                foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageOneInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Garage One Interiors", buildingDescription);
            UIMenu garageOneDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in garageDecorationStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneDecorationsMenu.AddItem(item);
            }
            garageOneDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneDecorationsMenu.RefreshIndex();
            garageOneDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageOneDecorationStyle = garageDecorationStyles[index];
                foreach (UIMenuItem i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageOneLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in garageLightingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneLightingsMenu.AddItem(item);
            }
            garageOneLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneLightingsMenu.RefreshIndex();
            garageOneLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageOneLightingStyle = garageLightingStyles[index];
                foreach (UIMenuItem i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageOneNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in garageOneNumberingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneNumberingsMenu.AddItem(item);
            }
            garageOneNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneNumberingsMenu.RefreshIndex();
            garageOneNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageNumberingStyle style in garageOneNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneNumberingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageOneNumberingStyle = garageOneNumberingStyles[index];
                foreach (UIMenuItem i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageOneInteriorsMenu.RefreshIndex();
            garageOneInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageOneDecorationsMenu && World.RenderingCamera != garageOneDecorationCam) {
                    garageOneDecorationCam = World.CreateCamera(garageOneDecorationCamPos, garageOneDecorationCamRot, garageOneDecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageOneDecorationCam, 1000, true, true);
                }
                if (nextMenu == garageOneLightingsMenu && World.RenderingCamera != garageOneLightingCam) {
                    garageOneLightingCam = World.CreateCamera(garageOneLightingCamPos, garageOneLightingCamRot, garageOneLightingCamFOV);
                    World.RenderingCamera.InterpTo(garageOneLightingCam, 1000, true, true);
                }
                if (nextMenu == garageOneNumberingsMenu && World.RenderingCamera != garageOneNumberingCam) {
                    garageOneNumberingCam = World.CreateCamera(garageOneNumberingCamPos, garageOneNumberingCamRot, garageOneNumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageOneNumberingCam, 1000, true, true);
                }
            };

            UIMenu garageTwoInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Garage Two Interiors", buildingDescription);
            UIMenu garageTwoDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in garageDecorationStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoDecorationsMenu.AddItem(item);
            }
            garageTwoDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoDecorationsMenu.RefreshIndex();
            garageTwoDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageTwoDecorationStyle = garageDecorationStyles[index];
                foreach (UIMenuItem i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageTwoLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in garageLightingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoLightingsMenu.AddItem(item);
            }
            garageTwoLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoLightingsMenu.RefreshIndex();
            garageTwoLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageTwoLightingStyle = garageLightingStyles[index];
                foreach (UIMenuItem i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageTwoNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in garageTwoNumberingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoNumberingsMenu.AddItem(item);
            }
            garageTwoNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoNumberingsMenu.RefreshIndex();
            garageTwoNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageNumberingStyle style in garageTwoNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoNumberingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageTwoNumberingStyle = garageTwoNumberingStyles[index];
                foreach (UIMenuItem i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageTwoInteriorsMenu.RefreshIndex();
            garageTwoInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageTwoDecorationsMenu && World.RenderingCamera != garageTwoDecorationCam) {
                    garageTwoDecorationCam = World.CreateCamera(garageTwoDecorationCamPos, garageTwoDecorationCamRot, garageTwoDecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageTwoDecorationCam, 1000, true, true);
                }
                if (nextMenu == garageTwoLightingsMenu && World.RenderingCamera != garageTwoLightingCam) {
                    garageTwoLightingCam = World.CreateCamera(garageTwoLightingCamPos, garageTwoLightingCamRot, garageTwoLightingCamFOV);
                    World.RenderingCamera.InterpTo(garageTwoLightingCam, 1000, true, true);
                }
                if (nextMenu == garageTwoNumberingsMenu && World.RenderingCamera != garageTwoNumberingCam) {
                    garageTwoNumberingCam = World.CreateCamera(garageTwoNumberingCamPos, garageTwoNumberingCamRot, garageTwoNumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageTwoNumberingCam, 1000, true, true);
                }
            };

            UIMenu garageThreeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Garage Three Interiors", buildingDescription);
            UIMenu garageThreeDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in garageDecorationStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeDecorationsMenu.AddItem(item);
            }
            garageThreeDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeDecorationsMenu.RefreshIndex();
            garageThreeDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageThreeDecorationStyle = garageDecorationStyles[index];
                foreach (UIMenuItem i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageThreeLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in garageLightingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeLightingsMenu.AddItem(item);
            }
            garageThreeLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeLightingsMenu.RefreshIndex();
            garageThreeLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageThreeLightingStyle = garageLightingStyles[index];
                foreach (UIMenuItem i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageThreeNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in garageThreeNumberingStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeNumberingsMenu.AddItem(item);
            }
            garageThreeNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeNumberingsMenu.RefreshIndex();
            garageThreeNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (GarageNumberingStyle style in garageThreeNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeNumberingStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageThreeNumberingStyle = garageThreeNumberingStyles[index];
                foreach (UIMenuItem i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageThreeInteriorsMenu.RefreshIndex();
            garageThreeInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageThreeDecorationsMenu && World.RenderingCamera != garageThreeDecorationCam) {
                    garageThreeDecorationCam = World.CreateCamera(garageThreeDecorationCamPos, garageThreeDecorationCamRot, garageThreeDecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageThreeDecorationCam, 1000, true, true);
                }
                if (nextMenu == garageThreeLightingsMenu && World.RenderingCamera != garageThreeLightingCam) {
                    garageThreeLightingCam = World.CreateCamera(garageThreeLightingCamPos, garageThreeLightingCamRot, garageThreeLightingCamFOV);
                    World.RenderingCamera.InterpTo(garageThreeLightingCam, 1000, true, true);
                }
                if (nextMenu == garageThreeNumberingsMenu && World.RenderingCamera != garageThreeNumberingCam) {
                    garageThreeNumberingCam = World.CreateCamera(garageThreeNumberingCamPos, garageThreeNumberingCamRot, garageThreeNumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageThreeNumberingCam, 1000, true, true);
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

                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                foreach (ModShopFloorStyle style in modShopFloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, modShopFloorStyles[index].PropName);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            modShopInteriorsMenu.OnItemSelect += (sender, item, index) => {
                modShopFloorStyle = modShopFloorStyles[index];
                foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
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

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    if (hasExtraOfficeDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == garageOneInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageOneIPL);
                    Game.Player.Character.Position = garageOneSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageOneNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageOneDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageOneLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneNumberingStyles[garageOneNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageOneCam = World.CreateCamera(garageOneCamPos, garageOneCamRot, garageOneCamFOV);
                    World.RenderingCamera = garageOneCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == garageTwoInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageTwoIPL);
                    Game.Player.Character.Position = garageTwoSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageTwoNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageTwoDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageTwoLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoNumberingStyles[garageTwoNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageTwoCam = World.CreateCamera(garageTwoCamPos, garageTwoCamRot, garageTwoCamFOV);
                    World.RenderingCamera = garageTwoCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == garageThreeInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageThreeIPL);
                    Game.Player.Character.Position = garageThreeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageThreeNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageThreeDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageThreeLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeNumberingStyles[garageThreeNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageThreeCam = World.CreateCamera(garageThreeCamPos, garageThreeCamRot, garageThreeCamFOV);
                    World.RenderingCamera = garageThreeCam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (nextMenu == modShopInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, modShopIPL);
                    Game.Player.Character.Position = modShopSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (ModShopFloorStyle style in modShopFloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, modShopFloorStyles[modShopInteriorsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
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

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, decor)) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    extraOfficeDecorsPrice = 1650000;

                    Script.Wait(500);
                    Game.FadeScreenIn(500);
                }
                else {
                    Game.FadeScreenOut(500);
                    Script.Wait(500);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    officeCam = World.CreateCamera(officeCamPos, officeCamRot, officeCamFOV);
                    World.RenderingCamera = officeCam;

                    extraOfficeDecorsPrice = 0;

                    Script.Wait(500);
                    Game.FadeScreenIn(500);
                }
                UpdatePurchaseMenuPriceDescription();
            };
            purchaseMenu.OnItemSelect += (sender, item, index) => {
                if (item.Text == "Purchase") {
                    if (Game.Player.Money < GetToTalPrice()) UI.ShowSubtitle("~r~Not enough money!");
                    else {
                        owner = (Owner)Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character);
                        try {
                            SinglePlayerOffice.Configs.SetValue(buildingName, "Owner", (int)owner);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "OfficeInteriorStyle", officeInteriorStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneDecorationStyle", garageOneDecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneLightingStyle", garageOneLightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneNumberingStyle", garageOneNumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoDecorationStyle", garageTwoDecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoLightingStyle", garageTwoLightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoNumberingStyle", garageTwoNumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeDecorationStyle", garageThreeDecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeLightingStyle", garageThreeLightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeNumberingStyle", garageThreeNumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "ModShopFloorStyle", modShopFloorStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "HasExtraOfficeDecors", hasExtraOfficeDecors);
                            SinglePlayerOffice.Configs.Save();
                        }
                        catch (Exception ex) {
                            Logger.Log(ex.ToString());
                        }
                        SetBuildingBlipColor();

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
                //ScenesController.ResetScenes();
                if (item.Text == "Office") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, officeInteriorStyle.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = officeSpawn;
                    Game.Player.Character.Heading = officeHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    if (hasExtraOfficeDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Garage One") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageOneIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageOneSpawn;
                    Game.Player.Character.Heading = garageOneHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageOneNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneDecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneLightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneNumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Garage Two") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageTwoIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageTwoSpawn;
                    Game.Player.Character.Heading = garageTwoHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageTwoNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoDecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoLightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoNumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Garage Three") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageThreeIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageThreeSpawn;
                    Game.Player.Character.Heading = garageThreeHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageThreeNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeDecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeLightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeNumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Mod Shop") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, modShopIPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = modShopSpawn;
                    Game.Player.Character.Heading = modShopHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (ModShopFloorStyle style in modShopFloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, modShopFloorStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                if (item.Text == "Heli Pad") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
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

                    RemoveAllIPLs();
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

        private Location GetCurrentLocation() {
            int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);
            if (Game.Player.Character.Position.DistanceTo(entranceTrigger) < 10f) return Location.Entrance;
            else if (officeInteriorIDs.Contains(currentInteriorID)) return Location.Office;
            else if (currentInteriorID == garageOneInteriorID) return Location.GarageOne;
            else if (currentInteriorID == garageTwoInteriorID) return Location.GarageTwo;
            else if (currentInteriorID == garageThreeInteriorID) return Location.GarageThree;
            else if (currentInteriorID == modShopInteriorID) return Location.ModShop;
            else if (Game.Player.Character.Position.DistanceTo(heliPadTrigger) < 10f) return Location.HeliPad;
            return Location.None;
        }

        private int GetToTalPrice() {
            return price + officeInteriorStyle.Price
                + garageOneDecorationStyle.Price + garageOneLightingStyle.Price + garageOneNumberingStyle.Price
                + garageTwoDecorationStyle.Price + garageTwoLightingStyle.Price + garageTwoNumberingStyle.Price
                + garageThreeDecorationStyle.Price + garageThreeLightingStyle.Price + garageThreeNumberingStyle.Price
                + modShopFloorStyle.Price + extraOfficeDecorsPrice;
        }

        private void UpdatePurchaseMenuPriceDescription() {
            purchaseMenu.RemoveItemAt(purchaseMenu.MenuItems.Count - 1);
            purchaseMenu.AddItem(new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice())));
        }

        private void UpdateTeleportMenuButtons() {
            teleportMenu.Clear();
            UIMenuItem goToOfficeBtn = new UIMenuItem("Office");
            UIMenuItem goToGarageOneBtn = new UIMenuItem("Garage One");
            UIMenuItem goToGarageTwoBtn = new UIMenuItem("Garage Two");
            UIMenuItem goToGarageThreeBtn = new UIMenuItem("Garage Three");
            UIMenuItem goToModShopBtn = new UIMenuItem("Mod Shop");
            UIMenuItem goToHeliPadBtn = new UIMenuItem("Heli Pad");
            UIMenuItem exitBuildingBtn = new UIMenuItem("Exit the building");
            switch (GetCurrentLocation()) {
                case Location.Entrance:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    break;
                case Location.Office:
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Location.GarageOne:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Location.GarageTwo:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Location.GarageThree:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Location.ModShop:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToHeliPadBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
                case Location.HeliPad:
                    teleportMenu.AddItem(goToOfficeBtn);
                    teleportMenu.AddItem(goToGarageOneBtn);
                    teleportMenu.AddItem(goToGarageTwoBtn);
                    teleportMenu.AddItem(goToGarageThreeBtn);
                    teleportMenu.AddItem(goToModShopBtn);
                    teleportMenu.AddItem(exitBuildingBtn);
                    break;
            }
            teleportMenu.RefreshIndex();
        }

        private void HideBuildingExteriors() {
            Function.Call(Hash._0x4B5CFC83122DF602);
            foreach (string exterior in exteriorIPLs) {
                int exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash)5819624144786551657, exteriorHash);
            }
            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        public string GetRadioEmitter() {
            switch (officeInteriorStyle.Name) {
                case "Old Spice Warms": return "SE_ex_int_office_01a_Radio_01";
                case "Old Spice Classical": return "SE_ex_int_office_01b_Radio_01";
                case "Old Spice Vintage": return "SE_ex_int_office_01c_Radio_01";
                case "Executive Contrast": return "SE_ex_int_office_02a_Radio_01";
                case "Executive Rich": return "SE_ex_int_office_02b_Radio_01";
                case "Executive Cool": return "SE_ex_int_office_02c_Radio_01";
                case "Power Broker Ice": return "SE_ex_int_office_03a_Radio_01";
                case "Power Broker Conservative": return "SE_ex_int_office_03b_Radio_01";
                case "Power Broker Polished": return "SE_ex_int_office_03c_Radio_01";
            }
            return null;
        }

        public void OnTick() {
            switch (GetCurrentLocation()) {
                case Location.Entrance:
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
                case Location.Office:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(officeTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Location.GarageOne:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageOneTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Location.GarageTwo:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageTwoTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Location.GarageThree:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageThreeTrigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the elevator");
                        if (Game.IsControlPressed(2, GTA.Control.Context)) {
                            Game.Player.Character.Task.StandStill(-1);
                            UpdateTeleportMenuButtons();
                            SinglePlayerOffice.IsHudHidden = true;
                            teleportMenu.Visible = true;
                        }
                    }
                    break;
                case Location.ModShop:
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
                case Location.HeliPad:
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
