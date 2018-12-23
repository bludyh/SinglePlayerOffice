using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Buildings {
    abstract class Building {

        private Blip entranceBlip;
        private Blip garageEntranceBlip;
        private Location currentLocation;
        protected ScriptSettings data;
        protected List<string> exteriorMapObjects;

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Price { get; protected set; }
        public Owner Owner { get; protected set; }
        public bool IsOwned { get { return Owner != Owner.None; } }
        public List<int> InteriorIDs { get; protected set; }
        public Entrance Entrance { get; protected set; }
        public GarageEntrance GarageEntrance { get; protected set; }
        public Office Office { get; protected set; }
        public Garage GarageOne { get; protected set; }
        public Garage GarageTwo { get; protected set; }
        public Garage GarageThree { get; protected set; }
        public ModShop ModShop { get; protected set; }
        public HeliPad HeliPad { get; protected set; }
        public Vector3 PurchaseCamPos { get; set; }
        public Vector3 PurchaseCamRot { get; set; }
        public float PurchaseCamFOV { get; set; }
        public Camera PurchaseCam { get; set; }
        public UIMenu PurchaseMenu { get; set; }
        public UIMenu TeleportMenu { get; set; }
        public UIMenu GarageEntranceMenu { get; set; }
        public UIMenu VehicleElevatorMenu { get; set; }
        public UIMenu PAMenu { get; set; }
        public DateTime? ConstructionTime { get; set; }
        public Location CurrentLocation {
            get { return currentLocation; }
            set {
                if (currentLocation != value) {
                    currentLocation.OnLocationLeft();
                    currentLocation = value;
                    currentLocation.OnLocationArrived();
                }
            }
        }

        protected InteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (InteriorStyle style in Office.InteriorStyles)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetGarageDecorationStyle(string name) {
            foreach (InteriorStyle style in Garage.DecorationStyles)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetGarageLightingStyle(string name) {
            foreach (InteriorStyle style in Garage.LightingStyles)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetGarageOneNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageOne)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetGarageTwoNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageTwo)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetGarageThreeNumberingStyle(string name) {
            foreach (InteriorStyle style in Garage.NumberingStylesGarageThree)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected InteriorStyle GetModShopFloorStyle(string name) {
            foreach (InteriorStyle style in ModShop.FloorStyles)
                if (style.Name == name)
                    return style;
            return null;
        }

        protected void CreateEntranceBlip() {
            entranceBlip = World.CreateBlip(Entrance.TriggerPos);
            if (IsOwned)
                entranceBlip.Sprite = (BlipSprite)475;
            else
                entranceBlip.Sprite = (BlipSprite)476;
            entranceBlip.Name = Name;
            SetBlipColor(entranceBlip);
        }

        protected void CreateGarageEntranceBlip() {
            garageEntranceBlip = World.CreateBlip(GarageEntrance.TriggerPos);
            garageEntranceBlip.Sprite = (BlipSprite)357;
            garageEntranceBlip.Name = "Office Garage";
            SetBlipColor(garageEntranceBlip);
        }

        private void SetBlipColor(Blip blip) {
            switch (Owner) {
                case Owner.Michael: blip.Color = BlipColor.Blue; break;
                case Owner.Franklin: blip.Color = (BlipColor)11; break;
                case Owner.Trevor: blip.Color = (BlipColor)17; break;
                default: blip.Color = BlipColor.White; break;
            }
        }

        private UIMenu CreateBaseMenu() {
            var baseMenu = new UIMenu("", "~b~Purchase Options");
            baseMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            var officeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Office Interiors", Description);
            officeInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in Office.InteriorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                officeInteriorsMenu.AddItem(item);
            }
            officeInteriorsMenu.RefreshIndex();
            officeInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                Office.ChangeInteriorStyle(Office.InteriorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                Office.InteriorStyle = Office.InteriorStyles[index];
                foreach (UIMenuItem i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            var extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations", Office.HasExtraDecors, String.Format("Price: ~g~${0:n0}", 1650000));
            baseMenu.AddItem(extraDecorsOption);
            var garageOneInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage One Interiors", Description);
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
                GarageOne.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneDecorationsMenu.OnItemSelect += (sender, item, index) => {
                GarageOne.DecorationStyle = Garage.DecorationStyles[index];
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
                GarageOne.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneLightingsMenu.OnItemSelect += (sender, item, index) => {
                GarageOne.LightingStyle = Garage.LightingStyles[index];
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
                GarageOne.ChangeNumberingStyle(Garage.NumberingStylesGarageOne[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneNumberingsMenu.OnItemSelect += (sender, item, index) => {
                GarageOne.NumberingStyle = Garage.NumberingStylesGarageOne[index];
                foreach (UIMenuItem i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            garageOneInteriorsMenu.RefreshIndex();
            garageOneInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageOneDecorationsMenu && World.RenderingCamera != GarageOne.DecorationCam) {
                    GarageOne.DecorationCam = World.CreateCamera(GarageOne.DecorationCamPos, GarageOne.DecorationCamRot, GarageOne.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(GarageOne.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageOneLightingsMenu && World.RenderingCamera != GarageOne.LightingCam) {
                    GarageOne.LightingCam = World.CreateCamera(GarageOne.LightingCamPos, GarageOne.LightingCamRot, GarageOne.LightingCamFOV);
                    World.RenderingCamera.InterpTo(GarageOne.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageOneNumberingsMenu && World.RenderingCamera != GarageOne.NumberingCam) {
                    GarageOne.NumberingCam = World.CreateCamera(GarageOne.NumberingCamPos, GarageOne.NumberingCamRot, GarageOne.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(GarageOne.NumberingCam, 1000, true, true);
                }
            };
            var garageTwoInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage Two Interiors", Description);
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
                GarageTwo.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoDecorationsMenu.OnItemSelect += (sender, item, index) => {
                GarageTwo.DecorationStyle = Garage.DecorationStyles[index];
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
                GarageTwo.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoLightingsMenu.OnItemSelect += (sender, item, index) => {
                GarageTwo.LightingStyle = Garage.LightingStyles[index];
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
                GarageTwo.ChangeNumberingStyle(Garage.NumberingStylesGarageTwo[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoNumberingsMenu.OnItemSelect += (sender, item, index) => {
                GarageTwo.NumberingStyle = Garage.NumberingStylesGarageTwo[index];
                foreach (UIMenuItem i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            garageTwoInteriorsMenu.RefreshIndex();
            garageTwoInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageTwoDecorationsMenu && World.RenderingCamera != GarageTwo.DecorationCam) {
                    GarageTwo.DecorationCam = World.CreateCamera(GarageTwo.DecorationCamPos, GarageTwo.DecorationCamRot, GarageTwo.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(GarageTwo.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageTwoLightingsMenu && World.RenderingCamera != GarageTwo.LightingCam) {
                    GarageTwo.LightingCam = World.CreateCamera(GarageTwo.LightingCamPos, GarageTwo.LightingCamRot, GarageTwo.LightingCamFOV);
                    World.RenderingCamera.InterpTo(GarageTwo.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageTwoNumberingsMenu && World.RenderingCamera != GarageTwo.NumberingCam) {
                    GarageTwo.NumberingCam = World.CreateCamera(GarageTwo.NumberingCamPos, GarageTwo.NumberingCamRot, GarageTwo.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(GarageTwo.NumberingCam, 1000, true, true);
                }
            };
            var garageThreeInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Garage Three Interiors", Description);
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
                GarageThree.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeDecorationsMenu.OnItemSelect += (sender, item, index) => {
                GarageThree.DecorationStyle = Garage.DecorationStyles[index];
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
                GarageThree.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeLightingsMenu.OnItemSelect += (sender, item, index) => {
                GarageThree.LightingStyle = Garage.LightingStyles[index];
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
                GarageThree.ChangeNumberingStyle(Garage.NumberingStylesGarageThree[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeNumberingsMenu.OnItemSelect += (sender, item, index) => {
                GarageThree.NumberingStyle = Garage.NumberingStylesGarageThree[index];
                foreach (UIMenuItem i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            garageThreeInteriorsMenu.RefreshIndex();
            garageThreeInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageThreeDecorationsMenu && World.RenderingCamera != GarageThree.DecorationCam) {
                    GarageThree.DecorationCam = World.CreateCamera(GarageThree.DecorationCamPos, GarageThree.DecorationCamRot, GarageThree.DecorationCamFOV);
                    World.RenderingCamera.InterpTo(GarageThree.DecorationCam, 1000, true, true);
                }
                else if (nextMenu == garageThreeLightingsMenu && World.RenderingCamera != GarageThree.LightingCam) {
                    GarageThree.LightingCam = World.CreateCamera(GarageThree.LightingCamPos, GarageThree.LightingCamRot, GarageThree.LightingCamFOV);
                    World.RenderingCamera.InterpTo(GarageThree.LightingCam, 1000, true, true);
                }
                else if (nextMenu == garageThreeNumberingsMenu && World.RenderingCamera != GarageThree.NumberingCam) {
                    GarageThree.NumberingCam = World.CreateCamera(GarageThree.NumberingCamPos, GarageThree.NumberingCamRot, GarageThree.NumberingCamFOV);
                    World.RenderingCamera.InterpTo(GarageThree.NumberingCam, 1000, true, true);
                }
            };
            var modShopInteriorsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(baseMenu, "Mod Shop Interiors", Description);
            modShopInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.dynasty8_executive.png"));
            foreach (InteriorStyle style in ModShop.FloorStyles) {
                var item = new UIMenuItem(style.Name, String.Format("Price: ~g~${0:n0}", style.Price));
                modShopInteriorsMenu.AddItem(item);
            }
            modShopInteriorsMenu.RefreshIndex();
            modShopInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                ModShop.ChangeFloorStyle(ModShop.FloorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            modShopInteriorsMenu.OnItemSelect += (sender, item, index) => {
                ModShop.FloorStyle = ModShop.FloorStyles[index];
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
                    officeInteriorsMenu.CurrentSelection = Office.InteriorStyles.IndexOf(Office.InteriorStyle);
                    officeInteriorsMenu.MenuItems[officeInteriorsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = Office.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    Office.LoadInterior(Office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                    Office.LoadExterior();
                    Office.PurchaseCam = World.CreateCamera(Office.PurchaseCamPos, Office.PurchaseCamRot, Office.PurchaseCamFOV);
                    World.RenderingCamera = Office.PurchaseCam;
                }
                else if (nextMenu == garageOneInteriorsMenu) {
                    foreach (UIMenuItem i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(GarageOne.DecorationStyle);
                    garageOneDecorationsMenu.MenuItems[garageOneDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(GarageOne.LightingStyle);
                    garageOneLightingsMenu.MenuItems[garageOneLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageOne.IndexOf(GarageOne.NumberingStyle);
                    garageOneNumberingsMenu.MenuItems[garageOneNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = GarageOne.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    GarageOne.LoadInterior(Garage.DecorationStyles[garageOneDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageOneLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageOne[garageOneNumberingsMenu.CurrentSelection]);
                    GarageOne.LoadExterior();
                    GarageOne.PurchaseCam = World.CreateCamera(GarageOne.PurchaseCamPos, GarageOne.PurchaseCamRot, GarageOne.PurchaseCamFOV);
                    World.RenderingCamera = GarageOne.PurchaseCam;
                }
                else if (nextMenu == garageTwoInteriorsMenu) {
                    foreach (UIMenuItem i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(GarageTwo.DecorationStyle);
                    garageTwoDecorationsMenu.MenuItems[garageTwoDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(GarageTwo.LightingStyle);
                    garageTwoLightingsMenu.MenuItems[garageTwoLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageTwo.IndexOf(GarageTwo.NumberingStyle);
                    garageTwoNumberingsMenu.MenuItems[garageTwoNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = GarageTwo.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    GarageTwo.LoadInterior(Garage.DecorationStyles[garageTwoDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageTwoLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageTwo[garageTwoNumberingsMenu.CurrentSelection]);
                    GarageTwo.LoadExterior();
                    GarageTwo.PurchaseCam = World.CreateCamera(GarageTwo.PurchaseCamPos, GarageTwo.PurchaseCamRot, GarageTwo.PurchaseCamFOV);
                    World.RenderingCamera = GarageTwo.PurchaseCam;
                }
                else if (nextMenu == garageThreeInteriorsMenu) {
                    foreach (UIMenuItem i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeDecorationsMenu.CurrentSelection = Garage.DecorationStyles.IndexOf(GarageThree.DecorationStyle);
                    garageThreeDecorationsMenu.MenuItems[garageThreeDecorationsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeLightingsMenu.CurrentSelection = Garage.LightingStyles.IndexOf(GarageThree.LightingStyle);
                    garageThreeLightingsMenu.MenuItems[garageThreeLightingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (UIMenuItem i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeNumberingsMenu.CurrentSelection = Garage.NumberingStylesGarageThree.IndexOf(GarageThree.NumberingStyle);
                    garageThreeNumberingsMenu.MenuItems[garageThreeNumberingsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = GarageThree.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    GarageThree.LoadInterior(Garage.DecorationStyles[garageThreeDecorationsMenu.CurrentSelection], Garage.LightingStyles[garageThreeLightingsMenu.CurrentSelection], Garage.NumberingStylesGarageThree[garageThreeNumberingsMenu.CurrentSelection]);
                    GarageThree.LoadExterior();
                    GarageThree.PurchaseCam = World.CreateCamera(GarageThree.PurchaseCamPos, GarageThree.PurchaseCamRot, GarageThree.PurchaseCamFOV);
                    World.RenderingCamera = GarageThree.PurchaseCam;
                }
                else if (nextMenu == modShopInteriorsMenu) {
                    foreach (UIMenuItem i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    modShopInteriorsMenu.CurrentSelection = ModShop.FloorStyles.IndexOf(ModShop.FloorStyle);
                    modShopInteriorsMenu.MenuItems[modShopInteriorsMenu.CurrentSelection].SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = ModShop.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    ModShop.LoadInterior(ModShop.FloorStyles[modShopInteriorsMenu.CurrentSelection]);
                    ModShop.LoadExterior();
                    ModShop.PurchaseCam = World.CreateCamera(ModShop.PurchaseCamPos, ModShop.PurchaseCamRot, ModShop.PurchaseCamFOV);
                    World.RenderingCamera = ModShop.PurchaseCam;
                }
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            baseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                Office.HasExtraDecors = isChecked;
                Office.ExtraDecorsPrice = (Office.HasExtraDecors) ? 1650000 : 0;
                Game.FadeScreenOut(500);
                Script.Wait(500);
                UnloadAllInteriors();
                Game.Player.Character.Position = Office.SpawnPos;
                Game.Player.Character.Task.StandStill(-1);
                officeInteriorsMenu.CurrentSelection = Office.InteriorStyles.IndexOf(Office.InteriorStyle);
                Office.LoadInterior(Office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                Office.PurchaseCam = World.CreateCamera(Office.PurchaseCamPos, Office.PurchaseCamRot, Office.PurchaseCamFOV);
                World.RenderingCamera = Office.PurchaseCam;
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            baseMenu.OnMenuClose += (sender) => {
                Office.InteriorStyle = GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich"));
                Office.HasExtraDecors = data.GetValue("Interiors", "HasExtraOfficeDecors", false);
                GarageOne.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1"));
                GarageOne.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1"));
                GarageOne.NumberingStyle = GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1"));
                GarageTwo.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1"));
                GarageTwo.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1"));
                GarageTwo.NumberingStyle = GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1"));
                GarageThree.DecorationStyle = GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1"));
                GarageThree.LightingStyle = GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1"));
                GarageThree.NumberingStyle = GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1"));
                ModShop.FloorStyle = GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1"));
                extraDecorsOption.Checked = Office.HasExtraDecors;
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
                        Owner = (Owner)Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character);
                        try {
                            data.SetValue("Owner", "Owner", (int)Owner);
                            data.SetValue("Interiors", "OfficeInteriorStyle", Office.InteriorStyle.Name);
                            data.SetValue("Interiors", "HasExtraOfficeDecors", Office.HasExtraDecors);
                            data.SetValue("Interiors", "GarageOneDecorationStyle", GarageOne.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageOneLightingStyle", GarageOne.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageOneNumberingStyle", GarageOne.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoDecorationStyle", GarageTwo.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageTwoLightingStyle", GarageTwo.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoNumberingStyle", GarageTwo.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeDecorationStyle", GarageThree.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageThreeLightingStyle", GarageThree.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeNumberingStyle", GarageThree.NumberingStyle.Name);
                            data.SetValue("Interiors", "ModShopFloorStyle", ModShop.FloorStyle.Name);
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
                        Game.Player.Character.Position = Entrance.SpawnPos;
                        Game.Player.Character.Heading = Entrance.SpawnHeading;
                        Game.Player.Character.Task.ClearAll();
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                        Script.Wait(1000);
                        Game.PlaySound("PROPERTY_PURCHASE", "HUD_AWARDS");
                        Game.Player.Money -= price;
                        BigMessageThread.MessageInstance.ShowSimpleShard("Buiding Purchased", Name);
                        Utilities.DisplayNotification("Hi boss! I'm your new Personal Assistant, who will help you with businesses at ~b~" + Name + "~w~.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                        Utilities.DisplayNotification("Currently, your newly owned building is still undergoing final construction phase. It will take 2 more days before all the facilities become available.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                        Utilities.DisplayNotification("I'll give you further notice in the future.~n~Have a nice day!", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
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
                Game.Player.Character.Position = Entrance.SpawnPos;
                Game.Player.Character.Heading = Entrance.SpawnHeading;
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
                UnloadAllInteriors();
                UnloadAllExteriors();
                switch (item.Text) {
                    case "Office":
                        Game.Player.Character.Position = Office.SpawnPos;
                        Game.Player.Character.Heading = Office.SpawnHeading;
                        Office.LoadInterior();
                        Office.LoadExterior();
                        break;
                    case "Garage One":
                        Game.Player.Character.Position = GarageOne.SpawnPos;
                        Game.Player.Character.Heading = GarageOne.SpawnHeading;
                        GarageOne.LoadInterior();
                        GarageOne.LoadExterior();
                        break;
                    case "Garage Two":
                        Game.Player.Character.Position = GarageTwo.SpawnPos;
                        Game.Player.Character.Heading = GarageTwo.SpawnHeading;
                        GarageTwo.LoadInterior();
                        GarageTwo.LoadExterior();
                        break;
                    case "Garage Three":
                        Game.Player.Character.Position = GarageThree.SpawnPos;
                        Game.Player.Character.Heading = GarageThree.SpawnHeading;
                        GarageThree.LoadInterior();
                        GarageThree.LoadExterior();
                        break;
                    case "Mod Shop":
                        Game.Player.Character.Position = ModShop.SpawnPos;
                        Game.Player.Character.Heading = ModShop.SpawnHeading;
                        ModShop.LoadInterior();
                        ModShop.LoadExterior();
                        break;
                    case "Heli Pad":
                        Game.Player.Character.Position = HeliPad.SpawnPos;
                        Game.Player.Character.Heading = HeliPad.SpawnHeading;
                        break;
                    case "Exit the building":
                        Game.Player.Character.Position = Entrance.SpawnPos;
                        Game.Player.Character.Heading = Entrance.SpawnHeading;
                        break;
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
            if (CurrentLocation == Entrance) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
            }
            else if (CurrentLocation == Office) {
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (CurrentLocation == GarageOne) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (CurrentLocation == GarageTwo) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (CurrentLocation == GarageThree) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToModShopBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (CurrentLocation == ModShop) {
                TeleportMenu.AddItem(goToOfficeBtn);
                TeleportMenu.AddItem(goToGarageOneBtn);
                TeleportMenu.AddItem(goToGarageTwoBtn);
                TeleportMenu.AddItem(goToGarageThreeBtn);
                TeleportMenu.AddItem(goToHeliPadBtn);
                TeleportMenu.AddItem(exitBuildingBtn);
            }
            else if (CurrentLocation == HeliPad) {
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
                GarageEntrance.Interaction.State = 0;
                UnloadAllInteriors();
                UnloadAllExteriors();
                switch (item.Text) {
                    case "Garage One":
                        Game.Player.Character.CurrentVehicle.Position = GarageOne.VehicleElevatorInteraction.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = GarageOne.SpawnHeading + 30f;
                        GarageOne.LoadInterior();
                        GarageOne.LoadExterior();
                        GarageOne.VehicleElevatorInteraction.State = 1;
                        break;
                    case "Garage Two":
                        Game.Player.Character.CurrentVehicle.Position = GarageTwo.VehicleElevatorInteraction.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = GarageTwo.SpawnHeading + 30f;
                        GarageTwo.LoadInterior();
                        GarageTwo.LoadExterior();
                        GarageTwo.VehicleElevatorInteraction.State = 1;
                        break;
                    case "Garage Three":
                        Game.Player.Character.CurrentVehicle.Position = GarageThree.VehicleElevatorInteraction.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading = GarageThree.SpawnHeading + 30f;
                        GarageThree.LoadInterior();
                        GarageThree.LoadExterior();
                        GarageThree.VehicleElevatorInteraction.State = 1;
                        break;
                    case "Mod Shop":
                        break;
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
                GarageEntrance.Interaction.State = 0;
            };
            SinglePlayerOffice.MenuPool.Add(GarageEntranceMenu);
        }

        protected void CreateVehicleElevatorMenu() {
            VehicleElevatorMenu = new UIMenu("", "~b~Elevator Options", new Point(0, -107));
            VehicleElevatorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(), "SinglePlayerOffice.Resources.no_banner.png"));
            VehicleElevatorMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                if (item.Text == "Level A" || item.Text == "Level B" || item.Text == "Level C") {
                    var currentGarage = (Garage)CurrentLocation;
                    switch (item.Text) {
                        case "Level A":
                            currentGarage.VehicleElevatorInteraction.ElevatorPos = currentGarage.VehicleElevatorInteraction.LevelAPos;
                            break;
                        case "Level B":
                            currentGarage.VehicleElevatorInteraction.ElevatorPos = currentGarage.VehicleElevatorInteraction.LevelBPos;
                            break;
                        case "Level C":
                            currentGarage.VehicleElevatorInteraction.ElevatorPos = currentGarage.VehicleElevatorInteraction.LevelCPos;
                            break;
                    }

                    currentGarage.VehicleElevatorInteraction.State = 3;
                }
                else {
                    Game.Player.Character.Task.StandStill(-1);
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    UnloadAllInteriors();
                    UnloadAllExteriors();
                    switch (item.Text) {
                        case "Garage One":
                            Game.Player.Character.CurrentVehicle.Position = GarageOne.VehicleElevatorInteraction.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading = GarageOne.SpawnHeading + 30f;
                            GarageOne.LoadInterior();
                            GarageOne.LoadExterior();
                            GarageOne.VehicleElevatorInteraction.State = 1;
                            break;
                        case "Garage Two":
                            Game.Player.Character.CurrentVehicle.Position = GarageTwo.VehicleElevatorInteraction.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading = GarageTwo.SpawnHeading + 30f;
                            GarageTwo.LoadInterior();
                            GarageTwo.LoadExterior();
                            GarageTwo.VehicleElevatorInteraction.State = 1;
                            break;
                        case "Garage Three":
                            Game.Player.Character.CurrentVehicle.Position = GarageThree.VehicleElevatorInteraction.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading = GarageThree.SpawnHeading + 30f;
                            GarageThree.LoadInterior();
                            GarageThree.LoadExterior();
                            GarageThree.VehicleElevatorInteraction.State = 1;
                            break;
                        case "Mod Shop":
                            break;
                        case "Exit the building":
                            Game.Player.Character.CurrentVehicle.Position = GarageEntrance.SpawnPos;
                            Game.Player.Character.CurrentVehicle.Heading = GarageEntrance.SpawnHeading;
                            Game.Player.Character.Task.ClearAll();
                            break;
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
            if (CurrentLocation is Garage garage) {
                if (garage.VehicleElevatorInteraction.ElevatorPos == garage.VehicleElevatorInteraction.LevelAPos) {
                    VehicleElevatorMenu.AddItem(goToLevelBBtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.VehicleElevatorInteraction.ElevatorPos == garage.VehicleElevatorInteraction.LevelBPos) {
                    VehicleElevatorMenu.AddItem(goToLevelABtn);
                    VehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.VehicleElevatorInteraction.ElevatorPos == garage.VehicleElevatorInteraction.LevelCPos) {
                    VehicleElevatorMenu.AddItem(goToLevelABtn);
                    VehicleElevatorMenu.AddItem(goToLevelBBtn);
                }
            }
            if (Game.Player.Character.IsInVehicle()) {
                if (CurrentLocation == GarageOne) {
                    VehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    VehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (CurrentLocation == GarageTwo) {
                    VehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    VehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (CurrentLocation == GarageThree) {
                    VehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    VehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    VehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (CurrentLocation == ModShop) {
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
                            data.SetValue("Interiors", "OfficeInteriorStyle", Office.InteriorStyle.Name);
                            data.SetValue("Interiors", "HasExtraOfficeDecors", Office.HasExtraDecors);
                            data.SetValue("Interiors", "GarageOneDecorationStyle", GarageOne.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageOneLightingStyle", GarageOne.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageOneNumberingStyle", GarageOne.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoDecorationStyle", GarageTwo.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageTwoLightingStyle", GarageTwo.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageTwoNumberingStyle", GarageTwo.NumberingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeDecorationStyle", GarageThree.DecorationStyle.Name);
                            data.SetValue("Interiors", "GarageThreeLightingStyle", GarageThree.LightingStyle.Name);
                            data.SetValue("Interiors", "GarageThreeNumberingStyle", GarageThree.NumberingStyle.Name);
                            data.SetValue("Interiors", "ModShopFloorStyle", ModShop.FloorStyle.Name);
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
                        Game.Player.Character.Position = Entrance.SpawnPos;
                        Game.Player.Character.Heading = Entrance.SpawnHeading;
                        Game.Player.Character.Task.ClearAll();
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                        Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                        Script.Wait(1000);
                        Game.FadeScreenIn(1000);
                        Script.Wait(1000);
                        Game.Player.Money -= price;
                        Utilities.DisplayNotification("Boss, ~b~" + Name + "~w~ is undergoing interior refurbishment. Come back after a day to see the result!", "CHAR_PA_FEMALE", 1, "Personal Assistant", "");
                    }
                }
            };
            refurbishMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                SinglePlayerOffice.IsHudHidden = false;
                if (CurrentLocation != Office || (CurrentLocation == Office && World.RenderingCamera == Office.PurchaseCam)) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    World.RenderingCamera = null;
                    World.DestroyAllCameras();
                    UnloadAllInteriors();
                    UnloadAllExteriors();
                    Game.Player.Character.Position = Utilities.SavedPos;
                    Game.Player.Character.Heading = Utilities.SavedRot.Z;
                    Office.LoadInterior();
                    Office.LoadExterior();
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
            manageMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == refurbishMenu) {
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    UnloadAllInteriors();
                    UnloadAllExteriors();
                    Game.Player.Character.Position = Entrance.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    PurchaseCam = World.CreateCamera(PurchaseCamPos, PurchaseCamRot, PurchaseCamFOV);
                    World.RenderingCamera = PurchaseCam;
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            SinglePlayerOffice.MenuPool.Add(manageMenu);
            PAMenu.BindMenuToItem(manageMenu, manageMenuBtn);
            PAMenu.RefreshIndex();
            PAMenu.OnMenuClose += (sender) => {
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            SinglePlayerOffice.MenuPool.Add(PAMenu);
        }

        public int GetBuyingPrice() {
            return (
                Price +
                Office.InteriorStyle.Price +
                GarageOne.DecorationStyle.Price + GarageOne.LightingStyle.Price + GarageOne.NumberingStyle.Price +
                GarageTwo.DecorationStyle.Price + GarageTwo.LightingStyle.Price + GarageTwo.NumberingStyle.Price +
                GarageThree.DecorationStyle.Price + GarageThree.LightingStyle.Price + GarageThree.NumberingStyle.Price +
                ModShop.FloorStyle.Price +
                Office.ExtraDecorsPrice
            );
        }

        public int GetRefurbishingPrice() {
            int price = 0;
            if (Office.InteriorStyle != GetOfficeInteriorStyle(data.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich")))
                price += Office.InteriorStyle.Price;
            if (Office.HasExtraDecors != data.GetValue("Interiors", "HasExtraOfficeDecors", false)) price += Office.ExtraDecorsPrice;
            if (GarageOne.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1")))
                price += GarageOne.DecorationStyle.Price;
            if (GarageOne.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")))
                price += GarageOne.LightingStyle.Price;
            if (GarageOne.NumberingStyle != GetGarageOneNumberingStyle(data.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")))
                price += GarageOne.NumberingStyle.Price;
            if (GarageTwo.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1")))
                price += GarageTwo.DecorationStyle.Price;
            if (GarageTwo.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")))
                price += GarageTwo.LightingStyle.Price;
            if (GarageTwo.NumberingStyle != GetGarageTwoNumberingStyle(data.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")))
                price += GarageTwo.NumberingStyle.Price;
            if (GarageThree.DecorationStyle != GetGarageDecorationStyle(data.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1")))
                price += GarageThree.DecorationStyle.Price;
            if (GarageThree.LightingStyle != GetGarageLightingStyle(data.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")))
                price += GarageThree.LightingStyle.Price;
            if (GarageThree.NumberingStyle != GetGarageThreeNumberingStyle(data.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1")))
                price += GarageThree.NumberingStyle.Price;
            if (ModShop.FloorStyle != GetModShopFloorStyle(data.GetValue("Interiors", "ModShopFloorStyle", "Floor 1")))
                price += ModShop.FloorStyle.Price;
            return price;
        }

        public int GetSellingPrice() {
            return (GetBuyingPrice() * 75) / 100;
        }

        public bool IsOwnedBy(Ped ped) {
            return Function.Call<int>(Hash.GET_PED_TYPE, ped) == (int)Owner;
        }

        private Location GetCurrentLocation() {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);

            if (Game.Player.Character.Position.DistanceTo(Entrance.TriggerPos) < 10f)
                return Entrance;
            if (currentInteriorID == GarageEntrance.InteriorID)
                return GarageEntrance;
            if (Office.InteriorIDs.Contains(currentInteriorID))
                return Office;
            if (currentInteriorID == GarageOne.InteriorID)
                return GarageOne;
            if (currentInteriorID == GarageTwo.InteriorID)
                return GarageTwo;
            if (currentInteriorID == GarageThree.InteriorID)
                return GarageThree;
            if (currentInteriorID == ModShop.InteriorID)
                return ModShop;
            if (Game.Player.Character.Position.DistanceTo(HeliPad.TriggerPos) < 10f)
                return HeliPad;

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
            Office.UnloadInterior();
            GarageOne.UnloadInterior();
            GarageTwo.UnloadInterior();
            GarageThree.UnloadInterior();
            ModShop.UnloadInterior();
        }

        private void UnloadAllExteriors() {
            Office.UnloadExterior();
            GarageOne.UnloadExterior();
            GarageTwo.UnloadExterior();
            GarageThree.UnloadExterior();
            ModShop.UnloadExterior();
        }

        public void HandleConstructionNotification() {
            if (ConstructionTime != null && World.CurrentDate.CompareTo(ConstructionTime) > 0) {
                Utilities.DisplayNotification("Boss, ~b~" + Name + "~w~ is ready! Drop by sometime to check out the building.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "");
                ConstructionTime = null;
            }
        }

        private void HandleVisitingTime() {
            var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);

            if (!IsOwnedBy(Game.Player.Character) && (hours < 9 || hours > 16) && CurrentLocation is IInterior) {
                UpdateTeleportMenuButtons();
                TeleportMenu.GoUp();
                TeleportMenu.SelectItem();
            }
        }

        public void Update() {
            CurrentLocation = GetCurrentLocation();

            HandleVisitingTime();

            CurrentLocation.Update();
        }

        public void Dispose() {
            entranceBlip?.Remove();
            garageEntranceBlip?.Remove();
            Office.Dispose();
            GarageOne.Dispose();
            GarageTwo.Dispose();
            GarageThree.Dispose();
        }

    }
}
