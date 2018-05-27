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
        protected List<int> interiorIDs;
        protected List<string> exteriorIPLs;
        protected Entrance entrance;
        protected GarageEntrance garageEntrance;
        protected List<string> extraOfficeDecors;
        protected Office office;
        protected List<GarageDecorationStyle> garageDecorationStyles;
        protected List<GarageLightingStyle> garageLightingStyles;
        protected List<GarageNumberingStyle> garageOneNumberingStyles;
        protected List<GarageNumberingStyle> garageTwoNumberingStyles;
        protected List<GarageNumberingStyle> garageThreeNumberingStyles;
        protected Garage garageOne;
        protected Garage garageTwo;
        protected Garage garageThree;
        protected List<ModShopFloorStyle> modShopFloorStyles;
        protected ModShop modShop;
        protected HeliPad heliPad;
        protected Vector3 purchaseCamPos;
        protected Vector3 purchaseCamRot;
        protected float purchaseFOV;
        private Camera purchaseCam;
        private UIMenu purchaseMenu;
        private UIMenu teleportMenu;
        private UIMenu vehicleElevatorMenu;

        public Owner Owner { get { return owner; } }
        public List<int> InteriorIDs { get { return interiorIDs; } }
        public Entrance Entrance { get { return entrance; } }
        public GarageEntrance GarageEntrance { get { return garageEntrance; } }
        public Office Office { get { return office; } }
        public Garage GarageOne { get { return garageOne; } }
        public Garage GarageTwo { get { return garageTwo; } }
        public Garage GarageThree { get { return garageThree; } }
        public ModShop ModShop { get { return modShop; } }
        public HeliPad HeliPad { get { return heliPad; } }

        protected Building() {
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
        }

        protected OfficeInteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (OfficeInteriorStyle style in office.InteriorStyles) if (style.Name == name) return style;
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
            foreach (OfficeInteriorStyle style in office.InteriorStyles) Function.Call(Hash.REMOVE_IPL, style.IPL);
            Function.Call(Hash.REMOVE_IPL, garageOne.IPL);
            Function.Call(Hash.REMOVE_IPL, garageTwo.IPL);
            Function.Call(Hash.REMOVE_IPL, garageThree.IPL);
            Function.Call(Hash.REMOVE_IPL, modShop.IPL);
        }

        protected void CreatePurchaseMenu() {
            purchaseMenu = new UIMenu(buildingName, "~b~Purchase Options") { MouseEdgeEnabled = false };
            
            UIMenu officeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(purchaseMenu, "Office Interiors", buildingDescription);
            foreach (OfficeInteriorStyle style in office.InteriorStyles) {
                UIMenuItem item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                officeInteriorsMenu.AddItem(item);
            }
            officeInteriorsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            officeInteriorsMenu.RefreshIndex();
            officeInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);

                foreach (OfficeInteriorStyle style in office.InteriorStyles) Function.Call(Hash.REMOVE_IPL, style.IPL);
                Function.Call(Hash.REQUEST_IPL, office.InteriorStyles[index].IPL);
                int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                if (office.HasExtraDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                office.InteriorStyle = office.InteriorStyles[index];
                foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenuCheckboxItem extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations", false, String.Format("Price: ~g~${0:n0}", 1650000));
            purchaseMenu.AddItem(extraDecorsOption);

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
                garageOne.DecorationStyle = garageDecorationStyles[index];
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
                garageOne.LightingStyle = garageLightingStyles[index];
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
                garageOne.NumberingStyle = garageOneNumberingStyles[index];
                foreach (UIMenuItem i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageOneInteriorsMenu.RefreshIndex();
            garageOneInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageOneDecorationsMenu && World.RenderingCamera != garageOne.DecorationCam) {
                    garageOne.DecorationCam = World.CreateCamera(garageOne.DecorationCamPos, garageOne.DecorationCamRot, garageOne.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageOne.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageOneLightingsMenu && World.RenderingCamera != garageOne.LightingCam) {
                    garageOne.LightingCam = World.CreateCamera(garageOne.LightingCamPos, garageOne.LightingCamRot, garageOne.LightingCamFOV);
                    World.RenderingCamera.InterpTo(garageOne.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageOneNumberingsMenu && World.RenderingCamera != garageOne.NumberingCam) {
                    garageOne.NumberingCam = World.CreateCamera(garageOne.NumberingCamPos, garageOne.NumberingCamRot, garageOne.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageOne.NumberingCam, 1000, true, true);
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
                garageTwo.DecorationStyle = garageDecorationStyles[index];
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
                garageTwo.LightingStyle = garageLightingStyles[index];
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
                garageTwo.NumberingStyle = garageTwoNumberingStyles[index];
                foreach (UIMenuItem i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageTwoInteriorsMenu.RefreshIndex();
            garageTwoInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageTwoDecorationsMenu && World.RenderingCamera != garageTwo.DecorationCam) {
                    garageTwo.DecorationCam = World.CreateCamera(garageTwo.DecorationCamPos, garageTwo.DecorationCamRot, garageTwo.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageTwo.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageTwoLightingsMenu && World.RenderingCamera != garageTwo.LightingCam) {
                    garageTwo.LightingCam = World.CreateCamera(garageTwo.LightingCamPos, garageTwo.LightingCamRot, garageTwo.LightingCamFOV);
                    World.RenderingCamera.InterpTo(garageTwo.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageTwoNumberingsMenu && World.RenderingCamera != garageTwo.NumberingCam) {
                    garageTwo.NumberingCam = World.CreateCamera(garageTwo.NumberingCamPos, garageTwo.NumberingCamRot, garageTwo.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageTwo.NumberingCam, 1000, true, true);
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
                garageThree.DecorationStyle = garageDecorationStyles[index];
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
                garageThree.LightingStyle = garageLightingStyles[index];
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
                garageThree.NumberingStyle = garageThreeNumberingStyles[index];
                foreach (UIMenuItem i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            garageThreeInteriorsMenu.RefreshIndex();
            garageThreeInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageThreeDecorationsMenu && World.RenderingCamera != garageThree.DecorationCam) {
                    garageThree.DecorationCam = World.CreateCamera(garageThree.DecorationCamPos, garageThree.DecorationCamRot, garageThree.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(garageThree.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageThreeLightingsMenu && World.RenderingCamera != garageThree.LightingCam) {
                    garageThree.LightingCam = World.CreateCamera(garageThree.LightingCamPos, garageThree.LightingCamRot, garageThree.LightingCamFOV);
                    World.RenderingCamera.InterpTo(garageThree.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageThreeNumberingsMenu && World.RenderingCamera != garageThree.NumberingCam) {
                    garageThree.NumberingCam = World.CreateCamera(garageThree.NumberingCamPos, garageThree.NumberingCamRot, garageThree.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(garageThree.NumberingCam, 1000, true, true);
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
                modShop.FloorStyle = modShopFloorStyles[index];
                foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenuItem purchaseBtn = new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice()));
            purchaseMenu.AddItem(purchaseBtn);
            purchaseMenu.RefreshIndex();
            purchaseMenu.OnMenuChange += (sender, nextMenu, forward) => {
                InteractionsController.ResetInterations();
                if (nextMenu == officeInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, office.InteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = office.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    if (office.HasExtraDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    office.Cam = World.CreateCamera(office.CamPos, office.CamRot, office.CamFOV);
                    World.RenderingCamera = office.Cam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (nextMenu == garageOneInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageOne.IPL);
                    Game.Player.Character.Position = garageOne.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageOneNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageOneDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageOneLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOneNumberingStyles[garageOneNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageOne.Cam = World.CreateCamera(garageOne.CamPos, garageOne.CamRot, garageOne.CamFOV);
                    World.RenderingCamera = garageOne.Cam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (nextMenu == garageTwoInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageTwo.IPL);
                    Game.Player.Character.Position = garageTwo.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageTwoNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageTwoDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageTwoLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwoNumberingStyles[garageTwoNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageTwo.Cam = World.CreateCamera(garageTwo.CamPos, garageTwo.CamRot, garageTwo.CamFOV);
                    World.RenderingCamera = garageTwo.Cam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (nextMenu == garageThreeInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageThree.IPL);
                    Game.Player.Character.Position = garageThree.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageThreeNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageDecorationStyles[garageThreeDecorationsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageLightingStyles[garageThreeLightingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThreeNumberingStyles[garageThreeNumberingsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    garageThree.Cam = World.CreateCamera(garageThree.CamPos, garageThree.CamRot, garageThree.CamFOV);
                    World.RenderingCamera = garageThree.Cam;

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (nextMenu == modShopInteriorsMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, modShop.IPL);
                    Game.Player.Character.Position = modShop.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (ModShopFloorStyle style in modShopFloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, modShopFloorStyles[modShopInteriorsMenu.CurrentSelection].PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    modShop.Cam = World.CreateCamera(modShop.CamPos, modShop.CamRot, modShop.CamFOV);
                    World.RenderingCamera = modShop.Cam;

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
                Game.Player.Character.Position = entrance.Spawn;
                Game.Player.Character.Heading = entrance.Heading;
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            purchaseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                InteractionsController.ResetInterations();
                office.HasExtraDecors = isChecked;
                if (office.HasExtraDecors) {
                    Game.FadeScreenOut(500);
                    Script.Wait(500);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, office.InteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = office.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, decor)) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    office.Cam = World.CreateCamera(office.CamPos, office.CamRot, office.CamFOV);
                    World.RenderingCamera = office.Cam;

                    office.ExtraDecorsPrice = 1650000;

                    Script.Wait(500);
                    Game.FadeScreenIn(500);
                }
                else {
                    Game.FadeScreenOut(500);
                    Script.Wait(500);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, office.InteriorStyles[officeInteriorsMenu.CurrentSelection].IPL);
                    Game.Player.Character.Position = office.Spawn;
                    Game.Player.Character.Task.StandStill(-1);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);
                    office.Cam = World.CreateCamera(office.CamPos, office.CamRot, office.CamFOV);
                    World.RenderingCamera = office.Cam;

                    office.ExtraDecorsPrice = 0;

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
                            SinglePlayerOffice.Configs.SetValue(buildingName, "OfficeInteriorStyle", office.InteriorStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneDecorationStyle", garageOne.DecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneLightingStyle", garageOne.LightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageOneNumberingStyle", garageOne.NumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoDecorationStyle", garageTwo.DecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoLightingStyle", garageTwo.LightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageTwoNumberingStyle", garageTwo.NumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeDecorationStyle", garageThree.DecorationStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeLightingStyle", garageThree.LightingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "GarageThreeNumberingStyle", garageThree.NumberingStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "ModShopFloorStyle", modShop.FloorStyle.Name);
                            SinglePlayerOffice.Configs.SetValue(buildingName, "HasExtraOfficeDecors", office.HasExtraDecors);
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
                        Game.Player.Character.Position = entrance.Spawn;
                        Game.Player.Character.Heading = entrance.Heading;
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

        private void UpdatePurchaseMenuPriceDescription() {
            purchaseMenu.RemoveItemAt(purchaseMenu.MenuItems.Count - 1);
            purchaseMenu.AddItem(new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice())));
        }

        protected void CreateTeleportMenu() {
            teleportMenu = new UIMenu(buildingName, "~b~Floor Options");

            teleportMenu.OnItemSelect += (sender, item, index) => {
                InteractionsController.ResetInterations();
                if (item.Text == "Office") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, office.InteriorStyle.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = office.Spawn;
                    Game.Player.Character.Heading = office.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_chairs")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_chairs");
                    if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, currentInteriorID, "office_booze")) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, "office_booze");
                    foreach (string decor in extraOfficeDecors) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, decor);
                    if (office.HasExtraDecors) foreach (string decor in extraOfficeDecors) Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, decor);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Garage One") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageOne.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageOne.Spawn;
                    Game.Player.Character.Heading = garageOne.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageOneNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOne.DecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOne.LightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageOne.NumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Garage Two") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageTwo.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageTwo.Spawn;
                    Game.Player.Character.Heading = garageTwo.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageTwoNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwo.DecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwo.LightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageTwo.NumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Garage Three") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, garageThree.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = garageThree.Spawn;
                    Game.Player.Character.Heading = garageThree.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (GarageDecorationStyle style in garageDecorationStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageLightingStyle style in garageLightingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    foreach (GarageNumberingStyle style in garageThreeNumberingStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThree.DecorationStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThree.LightingStyle.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, garageThree.NumberingStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Mod Shop") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    Function.Call(Hash.REQUEST_IPL, modShop.IPL);
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = modShop.Spawn;
                    Game.Player.Character.Heading = modShop.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                    foreach (ModShopFloorStyle style in modShopFloorStyles) Function.Call(Hash._DISABLE_INTERIOR_PROP, currentInteriorID, style.PropName);
                    Function.Call(Hash._ENABLE_INTERIOR_PROP, currentInteriorID, modShop.FloorStyle.PropName);
                    Function.Call(Hash.REFRESH_INTERIOR, currentInteriorID);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Heli Pad") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = heliPad.Spawn;
                    Game.Player.Character.Heading = heliPad.Heading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);

                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
                else if (item.Text == "Exit the building") {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);

                    RemoveAllIPLs();
                    SinglePlayerOffice.MenuPool.CloseAllMenus();
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Position = entrance.Spawn;
                    Game.Player.Character.Heading = entrance.Heading;
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

        protected void CreateVehicleElevatorMenu() {
            vehicleElevatorMenu = new UIMenu(buildingName, "~b~Garage Options") { MouseEdgeEnabled = false };
            vehicleElevatorMenu.AddItem(new UIMenuItem("Garage One"));
            vehicleElevatorMenu.AddItem(new UIMenuItem("Garage Two"));
            vehicleElevatorMenu.AddItem(new UIMenuItem("Garage Three"));
            vehicleElevatorMenu.RefreshIndex();
            vehicleElevatorMenu.OnItemSelect += (sender, item, index) => {
            };
            vehicleElevatorMenu.OnMenuClose += (sender) => {
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
                garageEntrance.Status = 0;
            };
            SinglePlayerOffice.MenuPool.Add(vehicleElevatorMenu);
        }

        public Location GetCurrentLocation() {
            int currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);
            if (Game.Player.Character.Position.DistanceTo(entrance.Trigger) < 10f) return Location.Entrance;
            else if (Game.Player.Character.Position.DistanceTo(garageEntrance.Trigger) < 10f) return Location.GarageEntrance;
            else if (office.InteriorIDs.Contains(currentInteriorID)) return Location.Office;
            else if (currentInteriorID == garageOne.InteriorID) return Location.GarageOne;
            else if (currentInteriorID == garageTwo.InteriorID) return Location.GarageTwo;
            else if (currentInteriorID == garageThree.InteriorID) return Location.GarageThree;
            else if (currentInteriorID == modShop.InteriorID) return Location.ModShop;
            else if (Game.Player.Character.Position.DistanceTo(heliPad.Trigger) < 10f) return Location.HeliPad;
            return Location.None;
        }

        private int GetToTalPrice() {
            return price + office.InteriorStyle.Price
                + garageOne.DecorationStyle.Price + garageOne.LightingStyle.Price + garageOne.NumberingStyle.Price
                + garageTwo.DecorationStyle.Price + garageTwo.LightingStyle.Price + garageTwo.NumberingStyle.Price
                + garageThree.DecorationStyle.Price + garageThree.LightingStyle.Price + garageThree.NumberingStyle.Price
                + modShop.FloorStyle.Price + office.ExtraDecorsPrice;
        }

        public string GetRadioEmitter() {
            switch (office.InteriorStyle.Name) {
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

        private void HideBuildingExteriors() {
            Function.Call(Hash._0x4B5CFC83122DF602);
            foreach (string exterior in exteriorIPLs) {
                int exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash)5819624144786551657, exteriorHash);
            }
            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        public void OnTick() {
            switch (GetCurrentLocation()) {
                case Location.Entrance:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(entrance.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                case Location.GarageEntrance:
                    if (!Game.Player.Character.IsDead && Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageEntrance.Trigger) < 10f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        if (owner != Owner.None) {
                            if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the vehicle elevator");
                                if (Game.IsControlPressed(2, GTA.Control.Context)) {
                                    garageEntrance.Status = 1;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    vehicleElevatorMenu.Visible = true;
                                }
                            }
                            else SinglePlayerOffice.DisplayHelpTextThisFrame("Only the owner can use the vehicle elevator");
                        }
                        else SinglePlayerOffice.DisplayHelpTextThisFrame("You do not own this building");
                    }
                    break;
                case Location.Office:
                    Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                    HideBuildingExteriors();
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(office.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageOne.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageTwo.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(garageThree.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(modShop.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Game.Player.Character.Position.DistanceTo(heliPad.Trigger) < 1.0f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
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
