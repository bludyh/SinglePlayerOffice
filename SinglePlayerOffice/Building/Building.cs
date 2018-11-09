using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    abstract class Building {

        protected string name;
        protected string description;
        protected int price;
        protected Vector3 entranceBlipPos;
        protected Blip entranceBlip;
        protected Vector3 garageEntranceBlipPos;
        protected Blip garageEntranceBlip;
        protected List<int> interiorIDs;
        protected List<string> exteriorMapObjects;
        protected ScriptSettings data;
        protected Owner owner;
        protected Entrance entrance;
        protected GarageEntrance garageEntrance;
        protected Office office;
        protected Garage garageOne;
        protected Garage garageTwo;
        protected Garage garageThree;
        protected ModShop modShop;
        protected HeliPad heliPad;

        public string Name { get { return name; } }
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
        public UIMenu PAMenu { get; set; }
        public DateTime? ConstructionTime { get; set; }

        protected InteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (InteriorStyle style in office.InteriorStyles) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetGarageDecorationStyle(string name) {
            foreach (InteriorStyle style in Garage.DecorationStyles) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetGarageLightingStyle(string name) {
            foreach (InteriorStyle style in Garage.LightingStyles) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetGarageOneNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageOne) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetGarageTwoNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageTwo) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetGarageThreeNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageThree) if (style.Name == name) return style;
            return null;
        }

        protected InteriorStyle GetModShopFloorStyle(string name) {
            foreach (InteriorStyle style in ModShop.FloorStyles) if (style.Name == name) return style;
            return null;
        }

        protected void CreateEntranceBlip() {
            entranceBlip = World.CreateBlip(entranceBlipPos);
            if (owner != Owner.None) entranceBlip.Sprite = (BlipSprite)475;
            else entranceBlip.Sprite = (BlipSprite)476;
            entranceBlip.Name = name;
            SetBlipColor(entranceBlip);
        }

        protected void CreateGarageEntranceBlip() {
            garageEntranceBlip = World.CreateBlip(garageEntranceBlipPos);
            garageEntranceBlip.Sprite = (BlipSprite)357;
            garageEntranceBlip.Name = "Office Garage";
            SetBlipColor(garageEntranceBlip);
        }

        private void SetBlipColor(Blip blip) {
            switch (owner) {
                case Owner.Michael: blip.Color = BlipColor.Blue; break;
                case Owner.Franklin: blip.Color = (BlipColor)11; break;
                case Owner.Trevor: blip.Color = (BlipColor)17; break;
                default: blip.Color = BlipColor.White; break;
            }
        }

        private UIMenu CreateBaseMenu() {
            var baseMenu = new UIMenu("", "~b~Purchase Options");
            baseMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            var officeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Office Interiors", description);
            officeInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in office.InteriorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                officeInteriorsMenu.AddItem(item);
            }
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
            };
            var extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations", office.HasExtraDecors, String.Format("Price: ~g~${0:n0}", 1650000));
            baseMenu.AddItem(extraDecorsOption);
            var garageOneInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage One Interiors", description);
            garageOneInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            var garageOneDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Decorations");
            garageOneDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneDecorationsMenu.AddItem(item);
            }
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
            };
            var garageOneLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Lightings");
            garageOneLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneLightingsMenu.AddItem(item);
            }
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
            };
            var garageOneNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageOneInteriorsMenu, "Numberings");
            garageOneNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.NumberingStylesGarageOne) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageOneNumberingsMenu.AddItem(item);
            }
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
            var garageTwoInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage Two Interiors", description);
            garageTwoInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            var garageTwoDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Decorations");
            garageTwoDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoDecorationsMenu.AddItem(item);
            }
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
            };
            var garageTwoLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Lightings");
            garageTwoLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoLightingsMenu.AddItem(item);
            }
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
            };
            var garageTwoNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Numberings");
            garageTwoNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.NumberingStylesGarageTwo) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageTwoNumberingsMenu.AddItem(item);
            }
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
            var garageThreeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage Three Interiors", description);
            garageThreeInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            var garageThreeDecorationsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Decorations");
            garageThreeDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeDecorationsMenu.AddItem(item);
            }
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
            };
            var garageThreeLightingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Lightings");
            garageThreeLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeLightingsMenu.AddItem(item);
            }
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
            };
            var garageThreeNumberingsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Numberings");
            garageThreeNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Garage.NumberingStylesGarageThree) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                garageThreeNumberingsMenu.AddItem(item);
            }
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
            var modShopInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Mod Shop Interiors", description);
            modShopInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in ModShop.FloorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                modShopInteriorsMenu.AddItem(item);
            }
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
            };
            baseMenu.RefreshIndex();
            baseMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (!forward) return;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                UnloadAllInteriors();
                UnloadAllExteriors();
                if (nextMenu == officeInteriorsMenu) {
                    foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    officeInteriorsMenu.CurrentSelection = office.InteriorStyles.IndexOf(office.InteriorStyle);
                    officeInteriorsMenu.MenuItems[officeInteriorsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = office.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    office.LoadInterior(office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                    office.LoadExterior();
                    office.PurchaseCam = World.CreateCamera(office.PurchaseCamPos, office.PurchaseCamRot, office.PurchaseCamFOV);
                    World.RenderingCamera = office.PurchaseCam;
                }
                else if (nextMenu == garageOneInteriorsMenu) {
                    foreach (UIMenuItem i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(garageOne.DecorationStyle);
                    garageOneDecorationsMenu.MenuItems[garageOneDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(garageOne.LightingStyle);
                    garageOneLightingsMenu.MenuItems[garageOneLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageOne.IndexOf(garageOne.NumberingStyle);
                    garageOneNumberingsMenu.MenuItems[garageOneNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = garageOne.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageOne.LoadInterior(Garage.DecorationStyles[garageOneDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageOneLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageOne[garageOneNumberingsMenu.CurrentSelection]);
                    garageOne.LoadExterior();
                    garageOne.PurchaseCam = World.CreateCamera(garageOne.PurchaseCamPos, garageOne.PurchaseCamRot, garageOne.PurchaseCamFOV);
                    World.RenderingCamera = garageOne.PurchaseCam;
                }
                else if (nextMenu == garageTwoInteriorsMenu) {
                    foreach (UIMenuItem i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(garageTwo.DecorationStyle);
                    garageTwoDecorationsMenu.MenuItems[garageTwoDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(garageTwo.LightingStyle);
                    garageTwoLightingsMenu.MenuItems[garageTwoLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageTwo.IndexOf(garageTwo.NumberingStyle);
                    garageTwoNumberingsMenu.MenuItems[garageTwoNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = garageTwo.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageTwo.LoadInterior(Garage.DecorationStyles[garageTwoDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageTwoLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageTwo[garageTwoNumberingsMenu.CurrentSelection]);
                    garageTwo.LoadExterior();
                    garageTwo.PurchaseCam = World.CreateCamera(garageTwo.PurchaseCamPos, garageTwo.PurchaseCamRot, garageTwo.PurchaseCamFOV);
                    World.RenderingCamera = garageTwo.PurchaseCam;
                }
                else if (nextMenu == garageThreeInteriorsMenu) {
                    foreach (UIMenuItem i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(garageThree.DecorationStyle);
                    garageThreeDecorationsMenu.MenuItems[garageThreeDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(garageThree.LightingStyle);
                    garageThreeLightingsMenu.MenuItems[garageThreeLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageThree.IndexOf(garageThree.NumberingStyle);
                    garageThreeNumberingsMenu.MenuItems[garageThreeNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = garageThree.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    garageThree.LoadInterior(Garage.DecorationStyles[garageThreeDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageThreeLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageThree[garageThreeNumberingsMenu.CurrentSelection]);
                    garageThree.LoadExterior();
                    garageThree.PurchaseCam = World.CreateCamera(garageThree.PurchaseCamPos, garageThree.PurchaseCamRot, garageThree.PurchaseCamFOV);
                    World.RenderingCamera = garageThree.PurchaseCam;
                }
                else if (nextMenu == modShopInteriorsMenu) {
                    foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    modShopInteriorsMenu.CurrentSelection = ModShop.FloorStyles.IndexOf(modShop.FloorStyle);
                    modShopInteriorsMenu.MenuItems[modShopInteriorsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = modShop.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    modShop.LoadInterior(ModShop.FloorStyles[modShopInteriorsMenu.CurrentSelection]);
                    modShop.LoadExterior();
                    modShop.PurchaseCam = World.CreateCamera(modShop.PurchaseCamPos, modShop.PurchaseCamRot, modShop.PurchaseCamFOV);
                    World.RenderingCamera = modShop.PurchaseCam;
                }
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            baseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                office.HasExtraDecors = isChecked;
                office.ExtraDecorsPrice = (office.HasExtraDecors) ? 1650000 : 0;
                Game.FadeScreenOut(500);
                Script.Wait(500);
                UnloadAllInteriors();
                Game.Player.Character.Position = office.SpawnPos;
                Game.Player.Character.Task.StandStill(-1);
                officeInteriorsMenu.CurrentSelection = office.InteriorStyles.IndexOf(office.InteriorStyle);
                office.LoadInterior(office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                office.PurchaseCam = World.CreateCamera(office.PurchaseCamPos, office.PurchaseCamRot, office.PurchaseCamFOV);
                World.RenderingCamera = office.PurchaseCam;
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            baseMenu.OnMenuClose += (sender) => {
                office.InteriorStyle = GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                office.HasExtraDecors = data.GetValue("Interiors", "HasExtraOfficeDecors", false);
                garageOne.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1"));
                garageOne.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1"));
                garageOne.NumberingStyle = GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1"));
                garageTwo.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1"));
                garageTwo.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1"));
                garageTwo.NumberingStyle = GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1"));
                garageThree.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1"));
                garageThree.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1"));
                garageThree.NumberingStyle = GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1"));
                modShop.FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"));
                extraDecorsOption.Checked = office.HasExtraDecors;
            };
            return baseMenu;
        }

        protected void CreatePurchaseMenu() {
            PurchaseMenu = CreateBaseMenu();
            PurchaseMenu.MouseEdgeEnabled = false;
            var purchaseBtn = new UIMenuItem("Purchase");
            PurchaseMenu.AddItem(purchaseBtn);
            PurchaseMenu.RefreshIndex();
            PurchaseMenu.OnIndexChange += (sender, index) => { if (sender.MenuItems[index] == purchaseBtn) purchaseBtn.Description = String.Format("Total Price: ~g~${0:n0}", GetBuyingPrice()); };
            PurchaseMenu.OnItemSelect += (sender, item, index) => {
                if (item == purchaseBtn) {
                    var price = GetBuyingPrice();
                    if (Game.Player.Money < price) UI.ShowSubtitle("~r~Not enough money!");
                    else {
                        owner = (Owner)Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character);
                        try {
                            data.SetValue("Owner", "Owner", (int)owner);
                            data.SetValue("Interiors", "OfficeInteriorStyle", office.InteriorStyle.Name);
                            data.SetValue("Interiors", "HasExtraOfficeDecors", office.HasExtraDecors);
                            data.SetValue("Interiors", "GarageOneDecorationStyle", garageOne.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageOneLightingStyle", garageOne.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageOneNumberingStyle", garageOne.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoDecorationStyle", garageTwo.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageTwoLightingStyle", garageTwo.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoNumberingStyle", garageTwo.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeDecorationStyle", garageThree.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageThreeLightingStyle", garageThree.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeNumberingStyle", garageThree.NumberingStyle.Name);
                            data.SetValue("Interiors", "ModShopFloorStyle", modShop.FloorStyle.Name);
                            data.Save();
                        }
                        catch (Exception ex) {
                            Logger.Log(ex.ToString());
                        }
                        entranceBlip.Sprite = (BlipSprite)475;
                        SetBlipColor(entranceBlip);
                        CreateGarageEntranceBlip();
                        ConstructionTime = World.CurrentDate.AddDays(2);
                        SinglePlayerOffice.MenuPool.CloseAllMenus();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);
                        World.RenderingCamera = null;
                        World.DestroyAllCameras();
                        UnloadAllInteriors();
                        UnloadAllExteriors();
                        Game.Player.Character.Position = entrance.SpawnPos;
                        Game.Player.Character.Heading = entrance.SpawnHeading;
                        Game.Player.Character.Task.ClearAll();
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                        Script.Wait(1000);
                        Game.PlaySound("PROPERTY_PURCHASE", "HUD_AWARDS");
                        Game.Player.Money -= price;
                        BigMessageThread.MessageInstance.ShowSimpleShard("Buiding Purchased", name);
                        SinglePlayerOffice.DisplayNotification("Hi boss! I'm your new Personal Assistant, who will help you with businesses at ~b~" + name + "~w~.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                        SinglePlayerOffice.DisplayNotification("Currently, your newly owned building is still undergoing final construction phase. It will take 2 more days before all the facilities become available.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                        SinglePlayerOffice.DisplayNotification("I'll give you further notice in the future.~n~Have a nice day!", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                    }
                }
            };
            PurchaseMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                UnloadAllInteriors();
                UnloadAllExteriors();
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

        protected void CreateTeleportMenu() {
            TeleportMenu = new UIMenu("", "~b~Floor Options", new Point(0, -107));
            TeleportMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            TeleportMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                Interactions.Reset();
                var currentLocation = GetCurrentLocation();
                if (currentLocation is Garage currentGarage) {
                    currentGarage.Scene.SaveVehicleInfoList();
                }
                UnloadAllInteriors();
                UnloadAllExteriors();
                if (item.Text == "Office") {
                    Game.Player.Character.Position = office.SpawnPos;
                    Game.Player.Character.Heading = office.SpawnHeading;
                    office.LoadInterior();
                    office.LoadExterior();
                    office.Scene.Reset();
                }
                else if (item.Text == "Garage One") {
                    Game.Player.Character.Position = garageOne.SpawnPos;
                    Game.Player.Character.Heading = garageOne.SpawnHeading;
                    garageOne.LoadInterior();
                    garageOne.LoadExterior();
                }
                else if (item.Text == "Garage Two") {
                    Game.Player.Character.Position = garageTwo.SpawnPos;
                    Game.Player.Character.Heading = garageTwo.SpawnHeading;
                    garageTwo.LoadInterior();
                    garageTwo.LoadExterior();
                }
                else if (item.Text == "Garage Three") {
                    Game.Player.Character.Position = garageThree.SpawnPos;
                    Game.Player.Character.Heading = garageThree.SpawnHeading;
                    garageThree.LoadInterior();
                    garageThree.LoadExterior();
                }
                else if (item.Text == "Mod Shop") {
                    Game.Player.Character.Position = modShop.SpawnPos;
                    Game.Player.Character.Heading = modShop.SpawnHeading;
                    modShop.LoadInterior();
                    modShop.LoadExterior();
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
            GarageEntranceMenu = new UIMenu("", "~b~Garage Options", new Point(0, -107)) { MouseEdgeEnabled = false };
            GarageEntranceMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
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
                UnloadAllInteriors();
                UnloadAllExteriors();
                if (item.Text == "Garage One") {
                    Game.Player.Character.CurrentVehicle.Position = garageOne.Scene.ElevatorLevelAPos;
                    Game.Player.Character.CurrentVehicle.Heading = garageOne.SpawnHeading + 30f;
                    garageOne.LoadInterior();
                    garageOne.LoadExterior();
                    garageOne.Scene.ElevatorStatus = 1;
                }
                else if (item.Text == "Garage Two") {
                    Game.Player.Character.CurrentVehicle.Position = garageTwo.Scene.ElevatorLevelAPos;
                    Game.Player.Character.CurrentVehicle.Heading = garageTwo.SpawnHeading + 30f;
                    garageTwo.LoadInterior();
                    garageTwo.LoadExterior();
                    garageTwo.Scene.ElevatorStatus = 1;
                }
                else if (item.Text == "Garage Three") {
                    Game.Player.Character.CurrentVehicle.Position = garageThree.Scene.ElevatorLevelAPos;
                    Game.Player.Character.CurrentVehicle.Heading = garageThree.SpawnHeading + 30f;
                    garageThree.LoadInterior();
                    garageThree.LoadExterior();
                    garageThree.Scene.ElevatorStatus = 1;
                }
                else if (item.Text == "Mod Shop") {

                }
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            GarageEntranceMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                Game.Player.Character.Task.ClearAll();
                garageEntrance.GarageEntranceStatus = 0;
            };
            SinglePlayerOffice.MenuPool.Add(GarageEntranceMenu);
        }

        protected void CreateVehicleElevatorMenu() {
            VehicleElevatorMenu = new UIMenu("", "~b~Elevator Options", new Point(0, -107));
            VehicleElevatorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            VehicleElevatorMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                var currentLocation = GetCurrentLocation();
                if (item.Text == "Level A" || item.Text == "Level B" || item.Text == "Level C") {
                    var currentGarage = GetCurrentLocation() as Garage;
                    if (item.Text == "Level A") currentGarage.Scene.ElevatorPos = currentGarage.Scene.ElevatorLevelAPos;
                    else if (item.Text == "Level B") currentGarage.Scene.ElevatorPos = currentGarage.Scene.ElevatorLevelBPos;
                    else if (item.Text == "Level C") currentGarage.Scene.ElevatorPos = currentGarage.Scene.ElevatorLevelCPos;
                    currentGarage.Scene.ElevatorStatus = 3;
                }
                else {
                    Game.Player.Character.Task.StandStill(-1);
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    Interactions.Reset();
                    UnloadAllInteriors();
                    UnloadAllExteriors();
                    if (currentLocation is Garage currentGarage) {
                        currentGarage.Scene.RemoveVehicleInfo(Game.Player.Character.CurrentVehicle);
                        currentGarage.Scene.SaveVehicleInfoList();
                    }
                    if (item.Text == "Garage One") {
                        Game.Player.Character.CurrentVehicle.Position = garageOne.Scene.ElevatorLevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = garageOne.SpawnHeading + 30f;
                        garageOne.LoadInterior();
                        garageOne.LoadExterior();
                        garageOne.Scene.ElevatorStatus = 1;
                    }
                    else if (item.Text == "Garage Two") {
                        Game.Player.Character.CurrentVehicle.Position = garageTwo.Scene.ElevatorLevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = garageTwo.SpawnHeading + 30f;
                        garageTwo.LoadInterior();
                        garageTwo.LoadExterior();
                        garageTwo.Scene.ElevatorStatus = 1;
                    }
                    else if (item.Text == "Garage Three") {
                        Game.Player.Character.CurrentVehicle.Position = garageThree.Scene.ElevatorLevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = garageThree.SpawnHeading + 30f;
                        garageThree.LoadInterior();
                        garageThree.LoadExterior();
                        garageThree.Scene.ElevatorStatus = 1;
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
                if (garage.Scene.ElevatorPos == garage.Scene.ElevatorLevelAPos) {
                    VehicleElevatorMenu.AddItem(goToLevelBBtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.Scene.ElevatorPos == garage.Scene.ElevatorLevelBPos) {
                    VehicleElevatorMenu.AddItem(goToLevelABtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.Scene.ElevatorPos == garage.Scene.ElevatorLevelCPos) {
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

        protected void CreatePAMenu() {
            PAMenu = new UIMenu("", "~b~Executive Options", new Point(0, -107));
            PAMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            var manageMenuBtn = new UIMenuItem("Manage Building");
            var requestVehicleMenuBtn = new UIMenuItem("Request A Special Vehicle");
            PAMenu.AddItem(manageMenuBtn);
            PAMenu.AddItem(requestVehicleMenuBtn);
            var manageMenu = new UIMenu("", "~b~Management Options", new Point(0, -107));
            manageMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            var refurbishMenuBtn = new UIMenuItem("Refurbish Building");
            var sellBtn = new UIMenuItem("Sell Building");
            manageMenu.AddItem(refurbishMenuBtn);
            manageMenu.AddItem(sellBtn);
            var refurbishMenu = CreateBaseMenu();
            var refurbishBtn = new UIMenuItem("Refurbish");
            refurbishMenu.AddItem(refurbishBtn);
            refurbishMenu.RefreshIndex();
            refurbishMenu.OnIndexChange += (sender, index) => { if (refurbishMenu.MenuItems[index] == refurbishBtn) refurbishBtn.Description = String.Format("Total Price: ~g~${0:n0}", GetRefurbishingPrice()); };
            refurbishMenu.OnItemSelect += (sender, item, index) => {
                if (item == refurbishBtn) {
                    var price = GetRefurbishingPrice();
                    if (Game.Player.Money < price) UI.ShowSubtitle("~r~Not enough money!");
                    else {
                        try {
                            data.SetValue("Interiors", "OfficeInteriorStyle", office.InteriorStyle.Name);
                            data.SetValue("Interiors", "HasExtraOfficeDecors", office.HasExtraDecors);
                            data.SetValue("Interiors", "GarageOneDecorationStyle", garageOne.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageOneLightingStyle", garageOne.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageOneNumberingStyle", garageOne.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoDecorationStyle", garageTwo.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageTwoLightingStyle", garageTwo.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoNumberingStyle", garageTwo.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeDecorationStyle", garageThree.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageThreeLightingStyle", garageThree.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeNumberingStyle", garageThree.NumberingStyle.Name);
                            data.SetValue("Interiors", "ModShopFloorStyle", modShop.FloorStyle.Name);
                            data.Save();
                        }
                        catch (Exception ex) {
                            Logger.Log(ex.ToString());
                        }
                        ConstructionTime = World.CurrentDate.AddDays(1);
                        SinglePlayerOffice.MenuPool.CloseAllMenus();
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.FadeScreenOut(1000);
                        Script.Wait(1000);
                        World.RenderingCamera = null;
                        World.DestroyAllCameras();
                        UnloadAllInteriors();
                        UnloadAllExteriors();
                        Game.Player.Character.Position = entrance.SpawnPos;
                        Game.Player.Character.Heading = entrance.SpawnHeading;
                        Game.Player.Character.Task.ClearAll();
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                        Script.Wait(1000);
                        Game.Player.Money -= price;
                        SinglePlayerOffice.DisplayNotification("Boss, ~b~" + name + "~w~ is undergoing interior refurbishment. Come back after a day to see the result!", "CHAR_PA_FEMALE", 1, "Personal Assistant", "");
                    }
                }
            };
            refurbishMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                var currentLocation = GetCurrentLocation();
                if (currentLocation != office || (currentLocation == office && World.RenderingCamera == office.PurchaseCam)) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    World.RenderingCamera = null;
                    World.DestroyAllCameras();
                    UnloadAllInteriors();
                    UnloadAllExteriors();
                    Game.Player.Character.Position = SinglePlayerOffice.SavedPos;
                    Game.Player.Character.Heading = SinglePlayerOffice.SavedRot.Z;
                    office.LoadInterior();
                    office.LoadExterior();
                    office.Scene.Reset(false);
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            SinglePlayerOffice.MenuPool.Add(refurbishMenu);
            manageMenu.BindMenuToItem(refurbishMenu, refurbishMenuBtn);
            manageMenu.RefreshIndex();
            SinglePlayerOffice.MenuPool.Add(manageMenu);
            PAMenu.BindMenuToItem(manageMenu, manageMenuBtn);
            PAMenu.RefreshIndex();
            SinglePlayerOffice.MenuPool.Add(PAMenu);
        }

        public int GetBuyingPrice() {
            return (
                price +
                office.InteriorStyle.Price +
                garageOne.DecorationStyle.Price + garageOne.LightingStyle.Price + garageOne.NumberingStyle.Price +
                garageTwo.DecorationStyle.Price + garageTwo.LightingStyle.Price + garageTwo.NumberingStyle.Price +
                garageThree.DecorationStyle.Price + garageThree.LightingStyle.Price + garageThree.NumberingStyle.Price +
                modShop.FloorStyle.Price +
                office.ExtraDecorsPrice
            );
        }

        public int GetRefurbishingPrice() {
            int price = 0;
            if (office.InteriorStyle != GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"))) price += office.InteriorStyle.Price;
            if (office.HasExtraDecors != data.GetValue("Interiors", "HasExtraOfficeDecors", false)) price += office.ExtraDecorsPrice;
            if (garageOne.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1"))) price += garageOne.DecorationStyle.Price;
            if (garageOne.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1"))) price += garageOne.LightingStyle.Price;
            if (garageOne.NumberingStyle != GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1"))) price += garageOne.NumberingStyle.Price;
            if (garageTwo.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1"))) price += garageTwo.DecorationStyle.Price;
            if (garageTwo.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1"))) price += garageTwo.LightingStyle.Price;
            if (garageTwo.NumberingStyle != GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1"))) price += garageTwo.NumberingStyle.Price;
            if (garageThree.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1"))) price += garageThree.DecorationStyle.Price;
            if (garageThree.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1"))) price += garageThree.LightingStyle.Price;
            if (garageThree.NumberingStyle != GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1"))) price += garageThree.NumberingStyle.Price;
            if (modShop.FloorStyle != GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"))) price += modShop.FloorStyle.Price;
            return price;
        }

        public int GetSellingPrice() {
            return (GetBuyingPrice() * 75) / 100;
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

        public void HideExteriorMapObjects() {
            Function.Call(Hash._0x4B5CFC83122DF602);
            foreach (string exterior in exteriorMapObjects) {
                int exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash)5819624144786551657, exteriorHash);
            }
            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        private void UnloadAllInteriors() {
            office.UnloadInterior();
            garageOne.UnloadInterior();
            garageTwo.UnloadInterior();
            garageThree.UnloadInterior();
            modShop.UnloadInterior();
        }

        private void UnloadAllExteriors() {
            office.UnloadExterior();
            garageOne.UnloadExterior();
            garageTwo.UnloadExterior();
            garageThree.UnloadExterior();
            modShop.UnloadExterior();
        }

        public void OnTick() {
            var currentLocation = GetCurrentLocation();
            var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);
            if (owner != Owner.None && Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)owner && (hours < 9 || hours > 16) && currentLocation is IInterior) {
                UpdateTeleportMenuButtons();
                TeleportMenu.GoUp();
                TeleportMenu.SelectItem();
            }
            currentLocation.OnTick();
        }

        public void Dispose() {
            if (entranceBlip != null) entranceBlip.Remove();
            if (garageEntranceBlip != null) garageEntranceBlip.Remove();
            office.Dispose();
            garageOne.Dispose();
            garageTwo.Dispose();
            garageThree.Dispose();
        }

    }
}
