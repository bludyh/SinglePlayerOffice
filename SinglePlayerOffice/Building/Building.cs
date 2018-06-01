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
        protected Vector3 buildingBlipPos;
        protected Blip buildingBlip;
        protected List<int> interiorIDs;
        protected List<string> exteriorIPLs;
        protected Entrance entrance;
        protected GarageEntrance garageEntrance;
        protected Office office;
        protected Garage garageOne;
        protected Garage garageTwo;
        protected Garage garageThree;
        protected ModShop modShop;
        protected HeliPad heliPad;

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
        public UIMenu PurchaseMenu { get; set; }
        public UIMenu TeleportMenu { get; set; }
        public UIMenu GarageEntranceMenu { get; set; }
        public UIMenu VehicleElevatorMenu { get; set; }

        protected OfficeInteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (OfficeInteriorStyle style in office.InteriorStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageDecorationStyle GetGarageDecorationStyle(string name) {
            foreach (GarageDecorationStyle style in Garage.DecorationStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageLightingStyle GetGarageLightingStyle(string name) {
            foreach (GarageLightingStyle style in Garage.LightingStyles) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageOneNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageOne) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageTwoNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageTwo) if (style.Name == name) return style;
            return null;
        }

        protected GarageNumberingStyle GetGarageThreeNumberingStyle(string name) {
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageThree) if (style.Name == name) return style;
            return null;
        }

        protected ModShopFloorStyle GetModShopFloorStyle(string name) {
            foreach (ModShopFloorStyle style in ModShop.FloorStyles) if (style.Name == name) return style;
            return null;
        }

        protected void CreateBuildingBlip() {
            buildingBlip = World.CreateBlip(buildingBlipPos);
            if (owner != Owner.None) buildingBlip.Sprite = (BlipSprite)475;
            else buildingBlip.Sprite = (BlipSprite)476;
            buildingBlip.Name = buildingName;
            SetBlipColor(buildingBlip);
        }

        private void SetBlipColor(Blip blip) {
            switch (owner) {
                case Owner.Michael: blip.Color = BlipColor.Blue; break;
                case Owner.Franklin: blip.Color = (BlipColor)11; break;
                case Owner.Trevor: blip.Color = (BlipColor)17; break;
                default: blip.Color = BlipColor.White; break;
            }
        }

        private void RemoveIPLs() {
            foreach (OfficeInteriorStyle style in office.InteriorStyles) Function.Call(Hash.REMOVE_IPL, style.IPL);
            Function.Call(Hash.REMOVE_IPL, garageOne.IPL);
            Function.Call(Hash.REMOVE_IPL, garageTwo.IPL);
            Function.Call(Hash.REMOVE_IPL, garageThree.IPL);
            Function.Call(Hash.REMOVE_IPL, modShop.IPL);
        }

        protected void CreatePurchaseMenu() {
            PurchaseMenu = new UIMenu(buildingName, "~b~Purchase Options") { MouseEdgeEnabled = false };
            
            UIMenu officeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(PurchaseMenu, "Office Interiors", buildingDescription);
            foreach (OfficeInteriorStyle style in office.InteriorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                officeInteriorsMenu.AddItem(item);
            }
            officeInteriorsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            officeInteriorsMenu.RefreshIndex();
            officeInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                office.ChangeInteriorStyle(office.InteriorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                office.InteriorStyle = office.InteriorStyles[index];
                foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            var extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations", false, String.Format("Price: ~g~${0:n0}", 1650000));
            PurchaseMenu.AddItem(extraDecorsOption);

            UIMenu garageOneInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(PurchaseMenu, "Garage One Interiors", buildingDescription);
            UIMenu garageOneDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneDecorationsMenu.AddItem(item);
            }
            garageOneDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneDecorationsMenu.RefreshIndex();
            garageOneDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageOne.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageOne.DecorationStyle = Garage.DecorationStyles[index];
                foreach (UIMenuItem i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageOneLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneLightingsMenu.AddItem(item);
            }
            garageOneLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneLightingsMenu.RefreshIndex();
            garageOneLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageOne.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageOne.LightingStyle = Garage.LightingStyles[index];
                foreach (UIMenuItem i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageOneNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageOne) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneNumberingsMenu.AddItem(item);
            }
            garageOneNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageOneNumberingsMenu.RefreshIndex();
            garageOneNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageOne.ChangeNumberingStyle(Garage.NumberingStylesGarageOne[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageOne.NumberingStyle = Garage.NumberingStylesGarageOne[index];
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

            UIMenu garageTwoInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(PurchaseMenu, "Garage Two Interiors", buildingDescription);
            UIMenu garageTwoDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoDecorationsMenu.AddItem(item);
            }
            garageTwoDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoDecorationsMenu.RefreshIndex();
            garageTwoDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageTwo.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageTwo.DecorationStyle = Garage.DecorationStyles[index];
                foreach (UIMenuItem i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageTwoLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoLightingsMenu.AddItem(item);
            }
            garageTwoLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoLightingsMenu.RefreshIndex();
            garageTwoLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageTwo.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageTwo.LightingStyle = Garage.LightingStyles[index];
                foreach (UIMenuItem i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageTwoNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageTwo) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoNumberingsMenu.AddItem(item);
            }
            garageTwoNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageTwoNumberingsMenu.RefreshIndex();
            garageTwoNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageTwo.ChangeNumberingStyle(Garage.NumberingStylesGarageTwo[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageTwo.NumberingStyle = Garage.NumberingStylesGarageTwo[index];
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

            UIMenu garageThreeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(PurchaseMenu, "Garage Three Interiors", buildingDescription);
            UIMenu garageThreeDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Decorations");
            foreach (GarageDecorationStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeDecorationsMenu.AddItem(item);
            }
            garageThreeDecorationsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeDecorationsMenu.RefreshIndex();
            garageThreeDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageThree.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeDecorationsMenu.OnItemSelect += (sender, item, index) => {
                garageThree.DecorationStyle = Garage.DecorationStyles[index];
                foreach (UIMenuItem i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageThreeLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Lightings");
            foreach (GarageLightingStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeLightingsMenu.AddItem(item);
            }
            garageThreeLightingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeLightingsMenu.RefreshIndex();
            garageThreeLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageThree.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeLightingsMenu.OnItemSelect += (sender, item, index) => {
                garageThree.LightingStyle = Garage.LightingStyles[index];
                foreach (UIMenuItem i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            UIMenu garageThreeNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Numberings");
            foreach (GarageNumberingStyle style in Garage.NumberingStylesGarageThree) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeNumberingsMenu.AddItem(item);
            }
            garageThreeNumberingsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            garageThreeNumberingsMenu.RefreshIndex();
            garageThreeNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                garageThree.ChangeNumberingStyle(Garage.NumberingStylesGarageThree[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeNumberingsMenu.OnItemSelect += (sender, item, index) => {
                garageThree.NumberingStyle = Garage.NumberingStylesGarageThree[index];
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

            UIMenu modShopInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(PurchaseMenu, "Mod Shop Interiors", buildingDescription);
            foreach (ModShopFloorStyle style in ModShop.FloorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                modShopInteriorsMenu.AddItem(item);
            }
            modShopInteriorsMenu.MenuItems[0].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            modShopInteriorsMenu.RefreshIndex();
            modShopInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                modShop.ChangeFloorStyle(ModShop.FloorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            modShopInteriorsMenu.OnItemSelect += (sender, item, index) => {
                modShop.FloorStyle = ModShop.FloorStyles[index];
                foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                UpdatePurchaseMenuPriceDescription();
            };

            var purchaseBtn = new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice()));
            PurchaseMenu.AddItem(purchaseBtn);
            PurchaseMenu.RefreshIndex();
            PurchaseMenu.OnMenuChange += (sender, nextMenu, forward) => {
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                RemoveIPLs();
                if (nextMenu == officeInteriorsMenu) {
                    Game.Player.Character.Position = office.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    office.LoadInterior(office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                    office.PurchaseCam = World.CreateCamera(office.PurchaseCamPos, office.PurchaseCamRot, office.PurchaseCamFOV);
                    World.RenderingCamera = office.PurchaseCam;
                }
                else if (nextMenu == garageOneInteriorsMenu) {
                    Game.Player.Character.Position = garageOne.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageOne.LoadInterior(Garage.DecorationStyles[garageOneDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageOneLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageOne[garageOneNumberingsMenu.CurrentSelection]);
                    garageOne.PurchaseCam = World.CreateCamera(garageOne.PurchaseCamPos, garageOne.PurchaseCamRot, garageOne.PurchaseCamFOV);
                    World.RenderingCamera = garageOne.PurchaseCam;
                }
                else if (nextMenu == garageTwoInteriorsMenu) {
                    Game.Player.Character.Position = garageTwo.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageTwo.LoadInterior(Garage.DecorationStyles[garageTwoDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageTwoLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageTwo[garageTwoNumberingsMenu.CurrentSelection]);
                    garageTwo.PurchaseCam = World.CreateCamera(garageTwo.PurchaseCamPos, garageTwo.PurchaseCamRot, garageTwo.PurchaseCamFOV);
                    World.RenderingCamera = garageTwo.PurchaseCam;
                }
                else if (nextMenu == garageThreeInteriorsMenu) {
                    Game.Player.Character.Position = garageThree.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageThree.LoadInterior(Garage.DecorationStyles[garageThreeDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageThreeLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageThree[garageThreeNumberingsMenu.CurrentSelection]);
                    garageThree.PurchaseCam = World.CreateCamera(garageThree.PurchaseCamPos, garageThree.PurchaseCamRot, garageThree.PurchaseCamFOV);
                    World.RenderingCamera = garageThree.PurchaseCam;
                }
                else if (nextMenu == modShopInteriorsMenu) {
                    Game.Player.Character.Position = modShop.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    modShop.LoadInterior(ModShop.FloorStyles[modShopInteriorsMenu.CurrentSelection]);
                    modShop.PurchaseCam = World.CreateCamera(modShop.PurchaseCamPos, modShop.PurchaseCamRot, modShop.PurchaseCamFOV);
                    World.RenderingCamera = modShop.PurchaseCam;
                }
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            PurchaseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                office.HasExtraDecors = isChecked;
                Game.FadeScreenOut(500);
                Script.Wait(500);
                RemoveIPLs();
                Game.Player.Character.Position = office.SpawnPos;
                Game.Player.Character.Task.StandStill(-1);
                office.LoadInterior(office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                office.PurchaseCam = World.CreateCamera(office.PurchaseCamPos, office.PurchaseCamRot, office.PurchaseCamFOV);
                World.RenderingCamera = office.PurchaseCam;
                if (office.HasExtraDecors) office.ExtraDecorsPrice = 1650000;
                else office.ExtraDecorsPrice = 0;
                Script.Wait(500);
                Game.FadeScreenIn(500);
                UpdatePurchaseMenuPriceDescription();
            };
            PurchaseMenu.OnItemSelect += (sender, item, index) => {
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
                        buildingBlip.Sprite = (BlipSprite)475;
                        SetBlipColor(buildingBlip);
                        SinglePlayerOffice.MenuPool.CloseAllMenus();
                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);
                        World.RenderingCamera = null;
                        World.DestroyAllCameras();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Position = entrance.SpawnPos;
                        Game.Player.Character.Heading = entrance.SpawnHeading;
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
            PurchaseMenu.OnMenuClose += (sender) => {
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Position = entrance.SpawnPos;
                Game.Player.Character.Heading = entrance.SpawnHeading;
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            SinglePlayerOffice.MenuPool.Add(PurchaseMenu);
        }

        private void UpdatePurchaseMenuPriceDescription() {
            PurchaseMenu.RemoveItemAt(PurchaseMenu.MenuItems.Count - 1);
            PurchaseMenu.AddItem(new UIMenuItem("Purchase", String.Format("Total Price: ~g~${0:n0}", GetToTalPrice())));
        }

        protected void CreateTeleportMenu() {
            TeleportMenu = new UIMenu(buildingName, "~b~Floor Options");
            TeleportMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                InteractionsController.ResetInterations();
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                RemoveIPLs();
                if (item.Text == "Office") {
                    Game.Player.Character.Position = office.SpawnPos;
                    Game.Player.Character.Heading = office.SpawnHeading;
                    office.LoadInterior();
                }
                else if (item.Text == "Garage One") {
                    Game.Player.Character.Position = garageOne.SpawnPos;
                    Game.Player.Character.Heading = garageOne.SpawnHeading;
                    garageOne.LoadInterior();
                }
                else if (item.Text == "Garage Two") {
                    Game.Player.Character.Position = garageTwo.SpawnPos;
                    Game.Player.Character.Heading = garageTwo.SpawnHeading;
                    garageTwo.LoadInterior();
                }
                else if (item.Text == "Garage Three") {
                    Game.Player.Character.Position = garageThree.SpawnPos;
                    Game.Player.Character.Heading = garageThree.SpawnHeading;
                    garageThree.LoadInterior();
                }
                else if (item.Text == "Mod Shop") {
                    Game.Player.Character.Position = modShop.SpawnPos;
                    Game.Player.Character.Heading = modShop.SpawnHeading;
                    modShop.LoadInterior();
                }
                else if (item.Text == "Heli Pad") {
                    Game.Player.Character.Position = heliPad.SpawnPos;
                    Game.Player.Character.Heading = heliPad.SpawnHeading;
                }
                else if (item.Text == "Exit the building") {
                    Game.Player.Character.Position = entrance.SpawnPos;
                    Game.Player.Character.Heading = entrance.SpawnHeading;
                }
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            TeleportMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            SinglePlayerOffice.MenuPool.Add(TeleportMenu);
        }

        public void UpdateTeleportMenuButtons() {
            TeleportMenu.Clear();
            var goToOfficeBtn = new UIMenuItem("Office");
            var goToGarageOneBtn = new UIMenuItem("Garage One");
            var goToGarageTwoBtn = new UIMenuItem("Garage Two");
            var goToGarageThreeBtn = new UIMenuItem("Garage Three");
            var goToModShopBtn = new UIMenuItem("Mod Shop");
            var goToHeliPadBtn = new UIMenuItem("Heli Pad");
            var exitBuildingBtn = new UIMenuItem("Exit the building");
            var currentLocation = GetCurrentLocation();
            if (currentLocation == entrance) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
            }
            else if (currentLocation == office) {
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentLocation == garageOne) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentLocation == garageTwo) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentLocation == garageThree) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentLocation == modShop) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentLocation == heliPad) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            TeleportMenu.RefreshIndex();
        }

        protected void CreateGarageEntranceMenu() {
            GarageEntranceMenu = new UIMenu(buildingName, "~b~Garage Options") { MouseEdgeEnabled = false };
            GarageEntranceMenu.AddItem(new UIMenuItem("Garage One"));
            GarageEntranceMenu.AddItem(new UIMenuItem("Garage Two"));
            GarageEntranceMenu.AddItem(new UIMenuItem("Garage Three"));
            GarageEntranceMenu.AddItem(new UIMenuItem("Mod Shop"));
            GarageEntranceMenu.RefreshIndex();
            GarageEntranceMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                garageEntrance.GarageEntranceStatus = 0;
                RemoveIPLs();
                if (item.Text == "Garage One") {
                    Game.Player.Character.CurrentVehicle.Position = garageOne.SpawnPos;
                    garageOne.LoadInterior();
                    garageOne.ElevatorStatus = 1;
                }
                else if (item.Text == "Garage Two") {
                    Game.Player.Character.CurrentVehicle.Position = garageTwo.SpawnPos;
                    garageTwo.LoadInterior();
                    garageTwo.ElevatorStatus = 1;
                }
                else if (item.Text == "Garage Three") {
                    Game.Player.Character.CurrentVehicle.Position = garageThree.SpawnPos;
                    garageThree.LoadInterior();
                    garageThree.ElevatorStatus = 1;
                }
                else if (item.Text == "Mod Shop") {

                }
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            GarageEntranceMenu.OnMenuClose += (sender) => {
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
                garageEntrance.GarageEntranceStatus = 0;
            };
            SinglePlayerOffice.MenuPool.Add(GarageEntranceMenu);
        }

        protected void CreateVehicleElevatorMenu() {
            VehicleElevatorMenu = new UIMenu(buildingName, "~b~Elevator Options");
            VehicleElevatorMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                var currentLocation = GetCurrentLocation();
                if (item.Text == "Level A" || item.Text == "Level B" || item.Text == "Level C") {
                    var currentGarage = GetCurrentLocation() as Garage;
                    if (item.Text == "Level A") currentGarage.ElevatorPos = currentGarage.ElevatorLevelAPos;
                    else if (item.Text == "Level B") currentGarage.ElevatorPos = currentGarage.ElevatorLevelBPos;
                    else if (item.Text == "Level C") currentGarage.ElevatorPos = currentGarage.ElevatorLevelCPos;
                    currentGarage.ElevatorStatus = 3;
                }
                else {
                    Game.Player.Character.Task.StandStill(-1);
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    RemoveIPLs();
                    if (item.Text == "Garage One") {
                        Game.Player.Character.CurrentVehicle.Position = garageOne.SpawnPos;
                        garageOne.LoadInterior();
                        garageOne.ElevatorStatus = 1;
                    }
                    else if (item.Text == "Garage Two") {
                        Game.Player.Character.CurrentVehicle.Position = garageTwo.SpawnPos;
                        garageTwo.LoadInterior();
                        garageTwo.ElevatorStatus = 1;
                    }
                    else if (item.Text == "Garage Three") {
                        Game.Player.Character.CurrentVehicle.Position = garageThree.SpawnPos;
                        garageThree.LoadInterior();
                        garageThree.ElevatorStatus = 1;
                    }
                    else if (item.Text == "Mod Shop") {

                    }
                    else if (item.Text == "Exit the building") {
                        Game.Player.Character.CurrentVehicle.Position = garageEntrance.SpawnPos;
                        Game.Player.Character.CurrentVehicle.Heading = garageEntrance.SpawnHeading;
                        Game.Player.Character.Task.ClearAll();
                    }
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            VehicleElevatorMenu.OnMenuClose += (sender) => SinglePlayerOffice.IsHudHidden = false;
            SinglePlayerOffice.MenuPool.Add(VehicleElevatorMenu);
        }

        public void UpdateVehicleElevatorMenuButtons() {
            VehicleElevatorMenu.Clear();
            var goToLevelABtn = new UIMenuItem("Level A");
            var goToLevelBBtn = new UIMenuItem("Level B");
            var goToLevelCBtn = new UIMenuItem("Level C");
            var goToGarageOneBtn = new UIMenuItem("Garage One");
            var goToGarageTwoBtn = new UIMenuItem("Garage Two");
            var goToGarageThreeBtn = new UIMenuItem("Garage Three");
            var goToModShopBtn = new UIMenuItem("Mod Shop");
            var exitBuildingBtn = new UIMenuItem("Exit the building");
            var currentLocation = GetCurrentLocation();
            if (currentLocation is Garage garage) {
                if (garage.ElevatorPos == garage.ElevatorLevelAPos) {
                    VehicleElevatorMenu.AddItem(goToLevelBBtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.ElevatorPos == garage.ElevatorLevelBPos) {
                    VehicleElevatorMenu.AddItem(goToLevelABtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.ElevatorPos == garage.ElevatorLevelCPos) {
                    VehicleElevatorMenu.AddItem(goToLevelABtn);
                    VehicleElevatorMenu.AddItem(goToLevelBBtn);
                }
            }
            if (Game.Player.Character.IsInVehicle()) {
                if (currentLocation == garageOne) {
                    VehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    VehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentLocation == garageTwo) {
                    VehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    VehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentLocation == garageThree) {
                    VehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    VehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentLocation == modShop) {
                    VehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    VehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    VehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                }
                VehicleElevatorMenu.AddItem(exitBuildingBtn);
            }
            VehicleElevatorMenu.RefreshIndex();
        }

        public Location GetCurrentLocation() {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);
            if (Game.Player.Character.Position.DistanceTo(entrance.TriggerPos) < 10f) return entrance;
            else if (currentInteriorID == garageEntrance.InteriorID) return garageEntrance;
            else if (office.InteriorIDs.Contains(currentInteriorID)) return office;
            else if (currentInteriorID == garageOne.InteriorID) return garageOne;
            else if (currentInteriorID == garageTwo.InteriorID) return garageTwo;
            else if (currentInteriorID == garageThree.InteriorID) return garageThree;
            else if (currentInteriorID == modShop.InteriorID) return modShop;
            else if (Game.Player.Character.Position.DistanceTo(heliPad.TriggerPos) < 10f) return heliPad;
            return null;
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

        public void HideBuildingExteriors() {
            Function.Call(Hash._0x4B5CFC83122DF602);
            foreach (string exterior in exteriorIPLs) {
                int exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash)5819624144786551657, exteriorHash);
            }
            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        public void OnTick() {
            var currentLocation = GetCurrentLocation();
            if (currentLocation == garageOne && !garageOne.IsElevatorCreated) {
                garageOne.CreateElevator();
                garageTwo.DeleteElevator();
                garageThree.DeleteElevator();
            }
            else if (currentLocation == garageTwo && !garageTwo.IsElevatorCreated) {
                garageTwo.CreateElevator();
                garageOne.DeleteElevator();
                garageThree.DeleteElevator();
            }
            else if (currentLocation == garageThree && !garageThree.IsElevatorCreated) {
                garageThree.CreateElevator();
                garageOne.DeleteElevator();
                garageTwo.DeleteElevator();
            }
            currentLocation.OnTick();
        }

        public void Dispose() {
            buildingBlip.Remove();
            garageOne.Dispose();
            garageTwo.Dispose();
            garageThree.Dispose();
        }

    }
}
