using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using GTA;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Buildings;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice {

    internal static class UI {

        static UI() {
            MenuPool = new MenuPool();
        }

        public static bool IsHudHidden { get; set; }
        public static MenuPool MenuPool { get; private set; }
        public static UIMenu PurchaseMenu => CreatePurchaseMenu();
        public static UIMenu TeleportMenu => CreateTeleportMenu();
        public static UIMenu GarageEntranceMenu => CreateGarageEntranceMenu();
        public static UIMenu VehicleElevatorMenu => CreateVehicleElevatorMenu();
        public static UIMenu PaMenu => CreatePaMenu();
        public static UIMenu RadioMenu => CreateRadioMenu();
        public static UIMenu WardrobeMenu => CreateWardrobeMenu();

        public static void HandleHudVisibility() {
            if (IsHudHidden)
                Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME);
        }

        public static void HandleMenus() {
            MenuPool.ProcessMenus();
        }

        private static UIMenu CreateBaseMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var baseMenu = new UIMenu("", "~b~Purchase Options");
            baseMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            var officeInteriorsMenu = MenuPool.AddSubMenu(baseMenu, "Office Interiors",
                currentBuilding.Description);
            officeInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in currentBuilding.Office.InteriorStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                officeInteriorsMenu.AddItem(item);
            }

            officeInteriorsMenu.RefreshIndex();
            officeInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.Office.ChangeInteriorStyle(
                    currentBuilding.Office.InteriorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            officeInteriorsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.Office.InteriorStyle =
                    currentBuilding.Office.InteriorStyles[index];
                foreach (var i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var extraDecorsOption = new UIMenuCheckboxItem("Extra Office Decorations",
                currentBuilding.Office.HasExtraDecors,
                $"Price: ~g~${1650000:n0}");
            baseMenu.AddItem(extraDecorsOption);

            var garageOneInteriorsMenu =
                MenuPool.AddSubMenu(baseMenu, "Garage One Interiors", currentBuilding.Description);
            garageOneInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            var garageOneDecorationsMenu =
                MenuPool.AddSubMenu(garageOneInteriorsMenu, "Decorations");
            garageOneDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageOneDecorationsMenu.AddItem(item);
            }

            garageOneDecorationsMenu.RefreshIndex();
            garageOneDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageOne.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneDecorationsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageOne.DecorationStyle = Garage.DecorationStyles[index];
                foreach (var i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageOneLightingsMenu = MenuPool.AddSubMenu(garageOneInteriorsMenu, "Lightings");
            garageOneLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageOneLightingsMenu.AddItem(item);
            }

            garageOneLightingsMenu.RefreshIndex();
            garageOneLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageOne.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneLightingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageOne.LightingStyle = Garage.LightingStyles[index];
                foreach (var i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageOneNumberingsMenu = MenuPool.AddSubMenu(garageOneInteriorsMenu, "Numberings");
            garageOneNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.NumberingStylesGarageOne) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageOneNumberingsMenu.AddItem(item);
            }

            garageOneNumberingsMenu.RefreshIndex();
            garageOneNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageOne.ChangeNumberingStyle(
                    Garage.NumberingStylesGarageOne[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageOneNumberingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageOne.NumberingStyle = Garage.NumberingStylesGarageOne[index];
                foreach (var i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            garageOneInteriorsMenu.RefreshIndex();
            garageOneInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageOneDecorationsMenu && World.RenderingCamera !=
                    currentBuilding.GarageOne.DecorationCam) {
                    currentBuilding.GarageOne.DecorationCam = World.CreateCamera(
                        currentBuilding.GarageOne.DecorationCamPos,
                        currentBuilding.GarageOne.DecorationCamRot,
                        currentBuilding.GarageOne.DecorationCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageOne.DecorationCam, 1000,
                        true, true);
                }
                else if (nextMenu == garageOneLightingsMenu &&
                         World.RenderingCamera != currentBuilding.GarageOne.LightingCam) {
                    currentBuilding.GarageOne.LightingCam = World.CreateCamera(
                        currentBuilding.GarageOne.LightingCamPos,
                        currentBuilding.GarageOne.LightingCamRot,
                        currentBuilding.GarageOne.LightingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageOne.LightingCam, 1000, true,
                        true);
                }
                else if (nextMenu == garageOneNumberingsMenu && World.RenderingCamera !=
                         currentBuilding.GarageOne.NumberingCam) {
                    currentBuilding.GarageOne.NumberingCam = World.CreateCamera(
                        currentBuilding.GarageOne.NumberingCamPos,
                        currentBuilding.GarageOne.NumberingCamRot,
                        currentBuilding.GarageOne.NumberingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageOne.NumberingCam, 1000,
                        true, true);
                }
            };

            var garageTwoInteriorsMenu =
                MenuPool.AddSubMenu(baseMenu, "Garage Two Interiors", currentBuilding.Description);
            garageTwoInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            var garageTwoDecorationsMenu =
                MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Decorations");
            garageTwoDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageTwoDecorationsMenu.AddItem(item);
            }

            garageTwoDecorationsMenu.RefreshIndex();
            garageTwoDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageTwo.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoDecorationsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageTwo.DecorationStyle = Garage.DecorationStyles[index];
                foreach (var i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageTwoLightingsMenu = MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Lightings");
            garageTwoLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageTwoLightingsMenu.AddItem(item);
            }

            garageTwoLightingsMenu.RefreshIndex();
            garageTwoLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageTwo.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoLightingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageTwo.LightingStyle = Garage.LightingStyles[index];
                foreach (var i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageTwoNumberingsMenu = MenuPool.AddSubMenu(garageTwoInteriorsMenu, "Numberings");
            garageTwoNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.NumberingStylesGarageTwo) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageTwoNumberingsMenu.AddItem(item);
            }

            garageTwoNumberingsMenu.RefreshIndex();
            garageTwoNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageTwo.ChangeNumberingStyle(
                    Garage.NumberingStylesGarageTwo[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageTwoNumberingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageTwo.NumberingStyle = Garage.NumberingStylesGarageTwo[index];
                foreach (var i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            garageTwoInteriorsMenu.RefreshIndex();
            garageTwoInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageTwoDecorationsMenu && World.RenderingCamera !=
                    currentBuilding.GarageTwo.DecorationCam) {
                    currentBuilding.GarageTwo.DecorationCam = World.CreateCamera(
                        currentBuilding.GarageTwo.DecorationCamPos,
                        currentBuilding.GarageTwo.DecorationCamRot,
                        currentBuilding.GarageTwo.DecorationCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageTwo.DecorationCam, 1000,
                        true, true);
                }
                else if (nextMenu == garageTwoLightingsMenu &&
                         World.RenderingCamera != currentBuilding.GarageTwo.LightingCam) {
                    currentBuilding.GarageTwo.LightingCam = World.CreateCamera(
                        currentBuilding.GarageTwo.LightingCamPos,
                        currentBuilding.GarageTwo.LightingCamRot,
                        currentBuilding.GarageTwo.LightingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageTwo.LightingCam, 1000, true,
                        true);
                }
                else if (nextMenu == garageTwoNumberingsMenu && World.RenderingCamera !=
                         currentBuilding.GarageTwo.NumberingCam) {
                    currentBuilding.GarageTwo.NumberingCam = World.CreateCamera(
                        currentBuilding.GarageTwo.NumberingCamPos,
                        currentBuilding.GarageTwo.NumberingCamRot,
                        currentBuilding.GarageTwo.NumberingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageTwo.NumberingCam, 1000,
                        true, true);
                }
            };

            var garageThreeInteriorsMenu =
                MenuPool.AddSubMenu(baseMenu, "Garage Three Interiors", currentBuilding.Description);
            garageThreeInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            var garageThreeDecorationsMenu =
                MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Decorations");
            garageThreeDecorationsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.DecorationStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageThreeDecorationsMenu.AddItem(item);
            }

            garageThreeDecorationsMenu.RefreshIndex();
            garageThreeDecorationsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageThree.ChangeDecorationStyle(Garage.DecorationStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeDecorationsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageThree.DecorationStyle = Garage.DecorationStyles[index];
                foreach (var i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageThreeLightingsMenu =
                MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Lightings");
            garageThreeLightingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.LightingStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageThreeLightingsMenu.AddItem(item);
            }

            garageThreeLightingsMenu.RefreshIndex();
            garageThreeLightingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageThree.ChangeLightingStyle(Garage.LightingStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeLightingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageThree.LightingStyle = Garage.LightingStyles[index];
                foreach (var i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            var garageThreeNumberingsMenu =
                MenuPool.AddSubMenu(garageThreeInteriorsMenu, "Numberings");
            garageThreeNumberingsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in Garage.NumberingStylesGarageThree) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                garageThreeNumberingsMenu.AddItem(item);
            }

            garageThreeNumberingsMenu.RefreshIndex();
            garageThreeNumberingsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.GarageThree.ChangeNumberingStyle(
                    Garage.NumberingStylesGarageThree[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            garageThreeNumberingsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.GarageThree.NumberingStyle =
                    Garage.NumberingStylesGarageThree[index];
                foreach (var i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };
            garageThreeInteriorsMenu.RefreshIndex();
            garageThreeInteriorsMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu == garageThreeDecorationsMenu && World.RenderingCamera !=
                    currentBuilding.GarageThree.DecorationCam) {
                    currentBuilding.GarageThree.DecorationCam = World.CreateCamera(
                        currentBuilding.GarageThree.DecorationCamPos,
                        currentBuilding.GarageThree.DecorationCamRot,
                        currentBuilding.GarageThree.DecorationCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageThree.DecorationCam, 1000,
                        true, true);
                }
                else if (nextMenu == garageThreeLightingsMenu && World.RenderingCamera !=
                         currentBuilding.GarageThree.LightingCam) {
                    currentBuilding.GarageThree.LightingCam = World.CreateCamera(
                        currentBuilding.GarageThree.LightingCamPos,
                        currentBuilding.GarageThree.LightingCamRot,
                        currentBuilding.GarageThree.LightingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageThree.LightingCam, 1000,
                        true, true);
                }
                else if (nextMenu == garageThreeNumberingsMenu && World.RenderingCamera !=
                         currentBuilding.GarageThree.NumberingCam) {
                    currentBuilding.GarageThree.NumberingCam = World.CreateCamera(
                        currentBuilding.GarageThree.NumberingCamPos,
                        currentBuilding.GarageThree.NumberingCamRot,
                        currentBuilding.GarageThree.NumberingCamFov);
                    World.RenderingCamera.InterpTo(currentBuilding.GarageThree.NumberingCam, 1000,
                        true, true);
                }
            };

            var modShopInteriorsMenu =
                MenuPool.AddSubMenu(baseMenu, "Mod Shop Interiors", currentBuilding.Description);
            modShopInteriorsMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.dynasty8_executive.png"));

            foreach (var style in ModShop.FloorStyles) {
                var item = new UIMenuItem(style.Name, $"Price: ~g~${style.Price:n0}");
                modShopInteriorsMenu.AddItem(item);
            }

            modShopInteriorsMenu.RefreshIndex();
            modShopInteriorsMenu.OnIndexChange += (sender, index) => {
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.ModShop.ChangeFloorStyle(ModShop.FloorStyles[index]);
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            modShopInteriorsMenu.OnItemSelect += (sender, item, index) => {
                currentBuilding.ModShop.FloorStyle = ModShop.FloorStyles[index];
                foreach (var i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                item.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            };

            baseMenu.RefreshIndex();
            baseMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (!forward) return;

                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();

                if (nextMenu == officeInteriorsMenu) {
                    foreach (var i in officeInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    officeInteriorsMenu.CurrentSelection =
                        currentBuilding.Office.InteriorStyles.IndexOf(currentBuilding.Office.InteriorStyle);
                    officeInteriorsMenu.MenuItems[officeInteriorsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = currentBuilding.Office.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.Office.LoadInterior(
                        currentBuilding.Office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                    currentBuilding.Office.LoadExterior();
                    currentBuilding.Office.PurchaseCam = World.CreateCamera(
                        currentBuilding.Office.PurchaseCamPos,
                        currentBuilding.Office.PurchaseCamRot,
                        currentBuilding.Office.PurchaseCamFov);
                    World.RenderingCamera = currentBuilding.Office.PurchaseCam;
                }
                else if (nextMenu == garageOneInteriorsMenu) {
                    foreach (var i in garageOneDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneDecorationsMenu.CurrentSelection =
                        Garage.DecorationStyles.IndexOf(currentBuilding.GarageOne.DecorationStyle);
                    garageOneDecorationsMenu.MenuItems[garageOneDecorationsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageOneLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneLightingsMenu.CurrentSelection =
                        Garage.LightingStyles.IndexOf(currentBuilding.GarageOne.LightingStyle);
                    garageOneLightingsMenu.MenuItems[garageOneLightingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageOneNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageOneNumberingsMenu.CurrentSelection =
                        Garage.NumberingStylesGarageOne.IndexOf(currentBuilding.GarageOne
                            .NumberingStyle);
                    garageOneNumberingsMenu.MenuItems[garageOneNumberingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = currentBuilding.GarageOne.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.GarageOne.LoadInterior(
                        Garage.DecorationStyles[garageOneDecorationsMenu.CurrentSelection],
                        Garage.LightingStyles[garageOneLightingsMenu.CurrentSelection],
                        Garage.NumberingStylesGarageOne[garageOneNumberingsMenu.CurrentSelection]);
                    currentBuilding.GarageOne.LoadExterior();
                    currentBuilding.GarageOne.PurchaseCam = World.CreateCamera(
                        currentBuilding.GarageOne.PurchaseCamPos,
                        currentBuilding.GarageOne.PurchaseCamRot,
                        currentBuilding.GarageOne.PurchaseCamFov);
                    World.RenderingCamera = currentBuilding.GarageOne.PurchaseCam;
                }
                else if (nextMenu == garageTwoInteriorsMenu) {
                    foreach (var i in garageTwoDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoDecorationsMenu.CurrentSelection =
                        Garage.DecorationStyles.IndexOf(currentBuilding.GarageTwo.DecorationStyle);
                    garageTwoDecorationsMenu.MenuItems[garageTwoDecorationsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageTwoLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoLightingsMenu.CurrentSelection =
                        Garage.LightingStyles.IndexOf(currentBuilding.GarageTwo.LightingStyle);
                    garageTwoLightingsMenu.MenuItems[garageTwoLightingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageTwoNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageTwoNumberingsMenu.CurrentSelection =
                        Garage.NumberingStylesGarageTwo.IndexOf(currentBuilding.GarageTwo
                            .NumberingStyle);
                    garageTwoNumberingsMenu.MenuItems[garageTwoNumberingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = currentBuilding.GarageTwo.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.GarageTwo.LoadInterior(
                        Garage.DecorationStyles[garageTwoDecorationsMenu.CurrentSelection],
                        Garage.LightingStyles[garageTwoLightingsMenu.CurrentSelection],
                        Garage.NumberingStylesGarageTwo[garageTwoNumberingsMenu.CurrentSelection]);
                    currentBuilding.GarageTwo.LoadExterior();
                    currentBuilding.GarageTwo.PurchaseCam = World.CreateCamera(
                        currentBuilding.GarageTwo.PurchaseCamPos,
                        currentBuilding.GarageTwo.PurchaseCamRot,
                        currentBuilding.GarageTwo.PurchaseCamFov);
                    World.RenderingCamera = currentBuilding.GarageTwo.PurchaseCam;
                }
                else if (nextMenu == garageThreeInteriorsMenu) {
                    foreach (var i in garageThreeDecorationsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeDecorationsMenu.CurrentSelection =
                        Garage.DecorationStyles.IndexOf(currentBuilding.GarageThree.DecorationStyle);
                    garageThreeDecorationsMenu.MenuItems[garageThreeDecorationsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageThreeLightingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeLightingsMenu.CurrentSelection =
                        Garage.LightingStyles.IndexOf(currentBuilding.GarageThree.LightingStyle);
                    garageThreeLightingsMenu.MenuItems[garageThreeLightingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    foreach (var i in garageThreeNumberingsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    garageThreeNumberingsMenu.CurrentSelection =
                        Garage.NumberingStylesGarageThree.IndexOf(currentBuilding.GarageThree
                            .NumberingStyle);
                    garageThreeNumberingsMenu.MenuItems[garageThreeNumberingsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = currentBuilding.GarageThree.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.GarageThree.LoadInterior(
                        Garage.DecorationStyles[garageThreeDecorationsMenu.CurrentSelection],
                        Garage.LightingStyles[garageThreeLightingsMenu.CurrentSelection],
                        Garage.NumberingStylesGarageThree[garageThreeNumberingsMenu.CurrentSelection]);
                    currentBuilding.GarageThree.LoadExterior();
                    currentBuilding.GarageThree.PurchaseCam = World.CreateCamera(
                        currentBuilding.GarageThree.PurchaseCamPos,
                        currentBuilding.GarageThree.PurchaseCamRot,
                        currentBuilding.GarageThree.PurchaseCamFov);
                    World.RenderingCamera = currentBuilding.GarageThree.PurchaseCam;
                }
                else if (nextMenu == modShopInteriorsMenu) {
                    foreach (var i in modShopInteriorsMenu.MenuItems) i.SetRightBadge(UIMenuItem.BadgeStyle.None);
                    modShopInteriorsMenu.CurrentSelection =
                        ModShop.FloorStyles.IndexOf(currentBuilding.ModShop.FloorStyle);
                    modShopInteriorsMenu.MenuItems[modShopInteriorsMenu.CurrentSelection]
                        .SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                    Game.Player.Character.Position = currentBuilding.ModShop.SpawnPos;
                    Game.Player.Character.Task.StandStill(-1);
                    currentBuilding.ModShop.LoadInterior(
                        ModShop.FloorStyles[modShopInteriorsMenu.CurrentSelection]);
                    currentBuilding.ModShop.LoadExterior();
                    currentBuilding.ModShop.PurchaseCam = World.CreateCamera(
                        currentBuilding.ModShop.PurchaseCamPos,
                        currentBuilding.ModShop.PurchaseCamRot,
                        currentBuilding.ModShop.PurchaseCamFov);
                    World.RenderingCamera = currentBuilding.ModShop.PurchaseCam;
                }

                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            baseMenu.OnCheckboxChange += (sender, item, isChecked) => {
                currentBuilding.Office.HasExtraDecors = isChecked;
                currentBuilding.Office.ExtraDecorsPrice =
                    currentBuilding.Office.HasExtraDecors ? 1650000 : 0;
                Game.FadeScreenOut(500);
                Script.Wait(500);
                currentBuilding.UnloadAllInteriors();
                Game.Player.Character.Position = currentBuilding.Office.SpawnPos;
                Game.Player.Character.Task.StandStill(-1);
                officeInteriorsMenu.CurrentSelection =
                    currentBuilding.Office.InteriorStyles.IndexOf(currentBuilding
                        .Office.InteriorStyle);
                currentBuilding.Office.LoadInterior(
                    currentBuilding.Office.InteriorStyles[officeInteriorsMenu.CurrentSelection]);
                currentBuilding.Office.PurchaseCam =
                    World.CreateCamera(currentBuilding.Office.PurchaseCamPos,
                        currentBuilding.Office.PurchaseCamRot,
                        currentBuilding.Office.PurchaseCamFov);
                World.RenderingCamera = currentBuilding.Office.PurchaseCam;
                Script.Wait(500);
                Game.FadeScreenIn(500);
            };
            baseMenu.OnMenuClose += sender => {
                currentBuilding.Office.InteriorStyle =
                    currentBuilding.GetOfficeInteriorStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "OfficeInteriorStyle",
                            "Executive Rich"));
                currentBuilding.Office.HasExtraDecors =
                    currentBuilding.SaveData.GetValue("Interiors", "HasExtraOfficeDecors", false);
                currentBuilding.GarageOne.DecorationStyle =
                    currentBuilding.GetGarageDecorationStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageOneDecorationStyle",
                            "Decoration 1"));
                currentBuilding.GarageOne.LightingStyle =
                    currentBuilding.GetGarageLightingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageOneLightingStyle",
                            "Lighting 1"));
                currentBuilding.GarageOne.NumberingStyle =
                    currentBuilding.GetGarageOneNumberingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageOneNumberingStyle",
                            "Signage 1"));
                currentBuilding.GarageTwo.DecorationStyle =
                    currentBuilding.GetGarageDecorationStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageTwoDecorationStyle",
                            "Decoration 1"));
                currentBuilding.GarageTwo.LightingStyle =
                    currentBuilding.GetGarageLightingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageTwoLightingStyle",
                            "Lighting 1"));
                currentBuilding.GarageTwo.NumberingStyle =
                    currentBuilding.GetGarageTwoNumberingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageTwoNumberingStyle",
                            "Signage 1"));
                currentBuilding.GarageThree.DecorationStyle =
                    currentBuilding.GetGarageDecorationStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageThreeDecorationStyle",
                            "Decoration 1"));
                currentBuilding.GarageThree.LightingStyle =
                    currentBuilding.GetGarageLightingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageThreeLightingStyle",
                            "Lighting 1"));
                currentBuilding.GarageThree.NumberingStyle =
                    currentBuilding.GetGarageThreeNumberingStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "GarageThreeNumberingStyle",
                            "Signage 1"));
                currentBuilding.ModShop.FloorStyle =
                    currentBuilding.GetModShopFloorStyle(
                        currentBuilding.SaveData.GetValue("Interiors", "ModShopFloorStyle",
                            "Floor 1"));
                extraDecorsOption.Checked = currentBuilding.Office.HasExtraDecors;
            };

            return baseMenu;
        }

        private static UIMenu CreatePurchaseMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var purchaseMenu = CreateBaseMenu();
            purchaseMenu.MouseEdgeEnabled = false;

            var purchaseBtn = new UIMenuItem("Purchase");
            purchaseMenu.AddItem(purchaseBtn);
            purchaseMenu.RefreshIndex();
            purchaseMenu.OnIndexChange += (sender, index) => {
                if (sender.MenuItems[index] == purchaseBtn)
                    purchaseBtn.Description =
                        $"Total Price: ~g~${currentBuilding.GetBuyingPrice():n0}";
            };
            purchaseMenu.OnItemSelect += (sender, item, index) => {
                if (item != purchaseBtn) return;

                var price = currentBuilding.GetBuyingPrice();

                if (Game.Player.Money < price) {
                    GTA.UI.ShowSubtitle("~r~Not enough money!");
                }
                else {
                    currentBuilding.Owner =
                        (Owner) Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character);

                    try {
                        currentBuilding.SaveData.SetValue("Owner", "Owner",
                            (int) currentBuilding.Owner);
                        currentBuilding.SaveData.SetValue("Interiors", "OfficeInteriorStyle",
                            currentBuilding.Office.InteriorStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "HasExtraOfficeDecors",
                            currentBuilding.Office.HasExtraDecors);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneDecorationStyle",
                            currentBuilding.GarageOne.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneLightingStyle",
                            currentBuilding.GarageOne.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneNumberingStyle",
                            currentBuilding.GarageOne.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoDecorationStyle",
                            currentBuilding.GarageTwo.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoLightingStyle",
                            currentBuilding.GarageTwo.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoNumberingStyle",
                            currentBuilding.GarageTwo.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeDecorationStyle",
                            currentBuilding.GarageThree.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeLightingStyle",
                            currentBuilding.GarageThree.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeNumberingStyle",
                            currentBuilding.GarageThree.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "ModShopFloorStyle",
                            currentBuilding.ModShop.FloorStyle.Name);
                        currentBuilding.SaveData.Save();
                    }
                    catch (Exception ex) {
                        Logger.Log(ex.ToString());
                    }

                    currentBuilding.EntranceBlip.Sprite = (BlipSprite) 475;
                    currentBuilding.SetBlipColor(currentBuilding.EntranceBlip);
                    currentBuilding.CreateGarageEntranceBlip();
                    currentBuilding.ConstructionTime = World.CurrentDate.AddDays(2);
                    MenuPool.CloseAllMenus();
                    IsHudHidden = false;
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    World.RenderingCamera = null;
                    World.DestroyAllCameras();
                    currentBuilding.UnloadAllInteriors();
                    currentBuilding.UnloadAllExteriors();
                    Game.Player.Character.Position = currentBuilding.Entrance.SpawnPos;
                    Game.Player.Character.Heading = currentBuilding.Entrance.SpawnHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                    Script.Wait(1000);
                    Game.PlaySound("PROPERTY_PURCHASE", "HUD_AWARDS");
                    Game.Player.Money -= price;
                    BigMessageThread.MessageInstance.ShowSimpleShard("Buiding Purchased",
                        currentBuilding.Name);
                    Utilities.DisplayNotification(
                        "Hi boss! I'm your new Personal Assistant, who will help you with businesses at ~b~" +
                        currentBuilding.Name +
                        "~w~.", "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                    Utilities.DisplayNotification(
                        "Currently, your newly owned building is still undergoing final construction phase. It will take 2 more days before all the facilities become available.",
                        "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                    Utilities.DisplayNotification("I'll give you further notice in the future.~n~Have a nice day!",
                        "CHAR_PA_FEMALE", 1, "Personal Assistant", "Greetings");
                }
            };
            purchaseMenu.OnMenuClose += sender => {
                IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();
                Game.Player.Character.Position = currentBuilding.Entrance.SpawnPos;
                Game.Player.Character.Heading = currentBuilding.Entrance.SpawnHeading;
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            MenuPool.Add(purchaseMenu);

            return purchaseMenu;
        }

        private static UIMenu CreateTeleportMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var teleportMenu = new UIMenu("", "~b~Floor Options", new Point(0, -107));
            teleportMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));
            teleportMenu.OnItemSelect += (sender, item, index) => {
                MenuPool.CloseAllMenus();
                IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();

                switch (item.Text) {
                    case "Office":
                        Game.Player.Character.Position = currentBuilding.Office.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.Office.SpawnHeading;
                        currentBuilding.Office.LoadInterior();
                        currentBuilding.Office.LoadExterior();

                        break;
                    case "Garage One":
                        Game.Player.Character.Position = currentBuilding.GarageOne.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.GarageOne.SpawnHeading;
                        currentBuilding.GarageOne.LoadInterior();
                        currentBuilding.GarageOne.LoadExterior();

                        break;
                    case "Garage Two":
                        Game.Player.Character.Position = currentBuilding.GarageTwo.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.GarageTwo.SpawnHeading;
                        currentBuilding.GarageTwo.LoadInterior();
                        currentBuilding.GarageTwo.LoadExterior();

                        break;
                    case "Garage Three":
                        Game.Player.Character.Position = currentBuilding.GarageThree.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.GarageThree.SpawnHeading;
                        currentBuilding.GarageThree.LoadInterior();
                        currentBuilding.GarageThree.LoadExterior();

                        break;
                    case "Mod Shop":
                        Game.Player.Character.Position = currentBuilding.ModShop.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.ModShop.SpawnHeading;
                        currentBuilding.ModShop.LoadInterior();
                        currentBuilding.ModShop.LoadExterior();

                        break;
                    case "Heli Pad":
                        Game.Player.Character.Position = currentBuilding.HeliPad.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.HeliPad.SpawnHeading;

                        break;
                    case "Exit the building":
                        Game.Player.Character.Position = currentBuilding.Entrance.SpawnPos;
                        Game.Player.Character.Heading = currentBuilding.Entrance.SpawnHeading;

                        break;
                }

                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            teleportMenu.OnMenuClose += sender => {
                IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            UpdateTeleportMenuButtons(teleportMenu);
            MenuPool.Add(teleportMenu);

            return teleportMenu;
        }

        private static void UpdateTeleportMenuButtons(UIMenu teleportMenu) {
            var currentBuilding = SinglePlayerOffice.CurrentBuilding;
            var goToOfficeBtn = new UIMenuItem("Office");
            var goToGarageOneBtn = new UIMenuItem("Garage One");
            var goToGarageTwoBtn = new UIMenuItem("Garage Two");
            var goToGarageThreeBtn = new UIMenuItem("Garage Three");
            var goToModShopBtn = new UIMenuItem("Mod Shop");
            var goToHeliPadBtn = new UIMenuItem("Heli Pad");
            var exitBuildingBtn = new UIMenuItem("Exit the building");

            teleportMenu.Clear();

            if (currentBuilding.CurrentLocation == currentBuilding.Entrance) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.Office) {
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.GarageOne) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.GarageTwo) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.GarageThree) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.ModShop) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToHeliPadBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }
            else if (currentBuilding.CurrentLocation == currentBuilding.HeliPad) {
                teleportMenu.AddItem(goToOfficeBtn);
                teleportMenu.AddItem(goToGarageOneBtn);
                teleportMenu.AddItem(goToGarageTwoBtn);
                teleportMenu.AddItem(goToGarageThreeBtn);
                teleportMenu.AddItem(goToModShopBtn);
                teleportMenu.AddItem(exitBuildingBtn);
            }

            teleportMenu.RefreshIndex();
        }

        private static UIMenu CreateGarageEntranceMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var garageEntranceMenu = new UIMenu("", "~b~Garage Options", new Point(0, -107))
                { MouseEdgeEnabled = false };
            garageEntranceMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));
            garageEntranceMenu.AddItem(new UIMenuItem("Garage One"));
            garageEntranceMenu.AddItem(new UIMenuItem("Garage Two"));
            garageEntranceMenu.AddItem(new UIMenuItem("Garage Three"));
            garageEntranceMenu.AddItem(new UIMenuItem("Mod Shop"));
            garageEntranceMenu.RefreshIndex();
            garageEntranceMenu.OnItemSelect += (sender, item, index) => {
                MenuPool.CloseAllMenus();
                IsHudHidden = false;
                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                currentBuilding.GarageEntrance.VehicleElevatorEntrance.State = 0;
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();

                switch (item.Text) {
                    case "Garage One":
                        Game.Player.Character.CurrentVehicle.Position =
                            currentBuilding.GarageOne.VehicleElevator.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading =
                            currentBuilding.GarageOne.SpawnHeading + 30f;
                        currentBuilding.GarageOne.LoadInterior();
                        currentBuilding.GarageOne.LoadExterior();
                        currentBuilding.GarageOne.VehicleElevator.State = 1;

                        break;
                    case "Garage Two":
                        Game.Player.Character.CurrentVehicle.Position =
                            currentBuilding.GarageTwo.VehicleElevator.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading =
                            currentBuilding.GarageTwo.SpawnHeading + 30f;
                        currentBuilding.GarageTwo.LoadInterior();
                        currentBuilding.GarageTwo.LoadExterior();
                        currentBuilding.GarageTwo.VehicleElevator.State = 1;

                        break;
                    case "Garage Three":
                        Game.Player.Character.CurrentVehicle.Position = currentBuilding.GarageThree
                            .VehicleElevator.LevelAPos;
                        Game.Player.Character.CurrentVehicle.Heading =
                            currentBuilding.GarageThree.SpawnHeading + 30f;
                        currentBuilding.GarageThree.LoadInterior();
                        currentBuilding.GarageThree.LoadExterior();
                        currentBuilding.GarageThree.VehicleElevator.State = 1;

                        break;
                    case "Mod Shop":

                        // TODO
                        break;
                }

                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            garageEntranceMenu.OnMenuClose += sender => {
                IsHudHidden = false;
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                Game.Player.Character.Task.ClearAll();
                currentBuilding.GarageEntrance.VehicleElevatorEntrance.State = 0;
            };
            MenuPool.Add(garageEntranceMenu);

            return garageEntranceMenu;
        }

        private static UIMenu CreateVehicleElevatorMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var vehicleElevatorMenu = new UIMenu("", "~b~Elevator Options", new Point(0, -107));
            vehicleElevatorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));
            vehicleElevatorMenu.OnItemSelect += (sender, item, index) => {
                MenuPool.CloseAllMenus();
                IsHudHidden = false;

                if (item.Text == "Level A" || item.Text == "Level B" || item.Text == "Level C") {
                    var currentGarage = (Garage) currentBuilding.CurrentLocation;

                    switch (item.Text) {
                        case "Level A":
                            currentGarage.VehicleElevator.Position = currentGarage.VehicleElevator.LevelAPos;

                            break;
                        case "Level B":
                            currentGarage.VehicleElevator.Position = currentGarage.VehicleElevator.LevelBPos;

                            break;
                        case "Level C":
                            currentGarage.VehicleElevator.Position = currentGarage.VehicleElevator.LevelCPos;

                            break;
                    }

                    currentGarage.VehicleElevator.State = 4;
                }
                else {
                    Game.Player.Character.Task.StandStill(-1);
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    currentBuilding.UnloadAllInteriors();
                    currentBuilding.UnloadAllExteriors();

                    switch (item.Text) {
                        case "Garage One":
                            Game.Player.Character.CurrentVehicle.Position = currentBuilding.GarageOne
                                .VehicleElevator.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading =
                                currentBuilding.GarageOne.SpawnHeading + 30f;
                            currentBuilding.GarageOne.LoadInterior();
                            currentBuilding.GarageOne.LoadExterior();
                            currentBuilding.GarageOne.VehicleElevator.State = 1;

                            break;
                        case "Garage Two":
                            Game.Player.Character.CurrentVehicle.Position = currentBuilding.GarageTwo
                                .VehicleElevator.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading =
                                currentBuilding.GarageTwo.SpawnHeading + 30f;
                            currentBuilding.GarageTwo.LoadInterior();
                            currentBuilding.GarageTwo.LoadExterior();
                            currentBuilding.GarageTwo.VehicleElevator.State = 1;

                            break;
                        case "Garage Three":
                            Game.Player.Character.CurrentVehicle.Position = currentBuilding
                                .GarageThree.VehicleElevator.LevelAPos;
                            Game.Player.Character.CurrentVehicle.Heading =
                                currentBuilding.GarageThree.SpawnHeading + 30f;
                            currentBuilding.GarageThree.LoadInterior();
                            currentBuilding.GarageThree.LoadExterior();
                            currentBuilding.GarageThree.VehicleElevator.State = 1;

                            break;
                        case "Mod Shop":

                            // TODO
                            break;
                        case "Exit the building":
                            Game.Player.Character.CurrentVehicle.Position =
                                currentBuilding.GarageEntrance.SpawnPos;
                            Game.Player.Character.CurrentVehicle.Heading =
                                currentBuilding.GarageEntrance.SpawnHeading;
                            Game.Player.Character.Task.ClearAll();

                            break;
                    }

                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                }
            };
            vehicleElevatorMenu.OnMenuClose += sender => IsHudHidden = false;
            UpdateVehicleElevatorMenuButtons(vehicleElevatorMenu);
            MenuPool.Add(vehicleElevatorMenu);

            return vehicleElevatorMenu;
        }

        private static void UpdateVehicleElevatorMenuButtons(UIMenu vehicleElevatorMenu) {
            var currentBuilding = SinglePlayerOffice.CurrentBuilding;
            var goToLevelABtn = new UIMenuItem("Level A");
            var goToLevelBBtn = new UIMenuItem("Level B");
            var goToLevelCBtn = new UIMenuItem("Level C");
            var goToGarageOneBtn = new UIMenuItem("Garage One");
            var goToGarageTwoBtn = new UIMenuItem("Garage Two");
            var goToGarageThreeBtn = new UIMenuItem("Garage Three");
            var goToModShopBtn = new UIMenuItem("Mod Shop");
            var exitBuildingBtn = new UIMenuItem("Exit the building");

            vehicleElevatorMenu.Clear();

            if (currentBuilding.CurrentLocation is Garage garage) {
                if (garage.VehicleElevator.Position == garage.VehicleElevator.LevelAPos) {
                    vehicleElevatorMenu.AddItem(goToLevelBBtn);
                    vehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.VehicleElevator.Position == garage.VehicleElevator.LevelBPos) {
                    vehicleElevatorMenu.AddItem(goToLevelABtn);
                    vehicleElevatorMenu.AddItem(goToLevelCBtn);
                }
                else if (garage.VehicleElevator.Position == garage.VehicleElevator.LevelCPos) {
                    vehicleElevatorMenu.AddItem(goToLevelABtn);
                    vehicleElevatorMenu.AddItem(goToLevelBBtn);
                }
            }

            if (Game.Player.Character.IsInVehicle()) {
                if (currentBuilding.CurrentLocation ==
                    currentBuilding.GarageOne) {
                    vehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    vehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    vehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentBuilding.CurrentLocation ==
                         currentBuilding.GarageTwo) {
                    vehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    vehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                    vehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentBuilding.CurrentLocation ==
                         currentBuilding.GarageThree) {
                    vehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    vehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    vehicleElevatorMenu.AddItem(goToModShopBtn);
                }
                else if (currentBuilding.CurrentLocation ==
                         currentBuilding.ModShop) {
                    vehicleElevatorMenu.AddItem(goToGarageOneBtn);
                    vehicleElevatorMenu.AddItem(goToGarageTwoBtn);
                    vehicleElevatorMenu.AddItem(goToGarageThreeBtn);
                }

                vehicleElevatorMenu.AddItem(exitBuildingBtn);
            }

            vehicleElevatorMenu.RefreshIndex();
        }

        private static UIMenu CreatePaMenu() {
            MenuPool = new MenuPool();

            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            var paMenu = new UIMenu("", "~b~Executive Options", new Point(0, -107));
            paMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));
            var manageMenuBtn = new UIMenuItem("Manage Building");
            var requestVehicleMenuBtn = new UIMenuItem("Request A Special Vehicle");
            paMenu.AddItem(manageMenuBtn);
            paMenu.AddItem(requestVehicleMenuBtn);

            var manageMenu = new UIMenu("", "~b~Management Options", new Point(0, -107));
            manageMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));
            var refurbishMenuBtn = new UIMenuItem("Refurbish Building");
            var sellBtn = new UIMenuItem("Sell Building");
            manageMenu.AddItem(refurbishMenuBtn);
            manageMenu.AddItem(sellBtn);

            var refurbishMenu = CreateBaseMenu();
            var refurbishBtn = new UIMenuItem("Refurbish");
            refurbishMenu.AddItem(refurbishBtn);
            refurbishMenu.RefreshIndex();
            refurbishMenu.OnIndexChange += (sender, index) => {
                if (refurbishMenu.MenuItems[index] == refurbishBtn)
                    refurbishBtn.Description =
                        $"Total Price: ~g~${currentBuilding.GetRefurbishingPrice():n0}";
            };
            refurbishMenu.OnItemSelect += (sender, item, index) => {
                if (item != refurbishBtn) return;

                var price = currentBuilding.GetRefurbishingPrice();

                if (Game.Player.Money < price) {
                    GTA.UI.ShowSubtitle("~r~Not enough money!");
                }
                else {
                    try {
                        currentBuilding.SaveData.SetValue("Interiors", "OfficeInteriorStyle",
                            currentBuilding.Office.InteriorStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "HasExtraOfficeDecors",
                            currentBuilding.Office.HasExtraDecors);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneDecorationStyle",
                            currentBuilding.GarageOne.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneLightingStyle",
                            currentBuilding.GarageOne.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageOneNumberingStyle",
                            currentBuilding.GarageOne.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoDecorationStyle",
                            currentBuilding.GarageTwo.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoLightingStyle",
                            currentBuilding.GarageTwo.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageTwoNumberingStyle",
                            currentBuilding.GarageTwo.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeDecorationStyle",
                            currentBuilding.GarageThree.DecorationStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeLightingStyle",
                            currentBuilding.GarageThree.LightingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "GarageThreeNumberingStyle",
                            currentBuilding.GarageThree.NumberingStyle.Name);
                        currentBuilding.SaveData.SetValue("Interiors", "ModShopFloorStyle",
                            currentBuilding.ModShop.FloorStyle.Name);
                        currentBuilding.SaveData.Save();
                    }
                    catch (Exception ex) {
                        Logger.Log(ex.ToString());
                    }

                    currentBuilding.ConstructionTime = World.CurrentDate.AddDays(1);
                    MenuPool.CloseAllMenus();
                    IsHudHidden = false;
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    World.RenderingCamera = null;
                    World.DestroyAllCameras();
                    currentBuilding.UnloadAllInteriors();
                    currentBuilding.UnloadAllExteriors();
                    Game.Player.Character.Position = currentBuilding.Entrance.SpawnPos;
                    Game.Player.Character.Heading = currentBuilding.Entrance.SpawnHeading;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                    Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                    Script.Wait(1000);
                    Game.Player.Money -= price;
                    Utilities.DisplayNotification(
                        "Boss, ~b~" + currentBuilding.Name +
                        "~w~ is undergoing interior refurbishment. Come back after a day to see the result!",
                        "CHAR_PA_FEMALE", 1, "Personal Assistant", "");
                }
            };
            refurbishMenu.OnMenuClose += sender => {
                MenuPool.CloseAllMenus();
                IsHudHidden = false;

                if (currentBuilding.CurrentLocation == currentBuilding.Office &&
                    (currentBuilding.CurrentLocation != currentBuilding.Office ||
                     World.RenderingCamera != currentBuilding.Office.PurchaseCam)) return;

                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();
                Game.Player.Character.Position = Utilities.SavedPos;
                Game.Player.Character.Heading = Utilities.SavedRot.Z;
                currentBuilding.Office.LoadInterior();
                currentBuilding.Office.LoadExterior();
                currentBuilding.Office.Pa.IsGreeted = true;
                currentBuilding.Office.Staffs.ForEach(s => {
                    s.IsGreeted = true;
                    s.State = 0;
                });
                Game.Player.Character.Task.ClearAll();
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
                Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            MenuPool.Add(refurbishMenu);

            manageMenu.BindMenuToItem(refurbishMenu, refurbishMenuBtn);
            manageMenu.RefreshIndex();
            manageMenu.OnMenuChange += (sender, nextMenu, forward) => {
                if (nextMenu != refurbishMenu) return;

                Game.FadeScreenOut(1000);
                Script.Wait(1000);
                currentBuilding.UnloadAllInteriors();
                currentBuilding.UnloadAllExteriors();
                Game.Player.Character.Position = currentBuilding.Entrance.SpawnPos;
                Game.Player.Character.Task.StandStill(-1);
                currentBuilding.Entrance.PurchaseCam = World.CreateCamera(
                    currentBuilding.Entrance.PurchaseCamPos,
                    currentBuilding.Entrance.PurchaseCamRot,
                    currentBuilding.Entrance.PurchaseCamFov);
                World.RenderingCamera = currentBuilding.Entrance.PurchaseCam;
                Script.Wait(1000);
                Game.FadeScreenIn(1000);
            };
            MenuPool.Add(manageMenu);

            paMenu.BindMenuToItem(manageMenu, manageMenuBtn);
            paMenu.RefreshIndex();
            paMenu.OnMenuClose += sender => {
                IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            MenuPool.Add(paMenu);

            return paMenu;
        }

        private static UIMenu CreateRadioMenu() {
            MenuPool = new MenuPool();

            var radioMenu = new UIMenu("", "~b~Radio Stations", new Point(0, -107));
            radioMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly(),
                "SinglePlayerOffice.Resources.no_banner.png"));

            foreach (var station in Radio.Stations) {
                var radioStationBtn = new UIMenuItem(station.Name, station.Description);
                radioMenu.AddItem(radioStationBtn);
            }

            radioMenu.RefreshIndex();
            radioMenu.OnItemSelect += (sender, item, index) => {
                MenuPool.CloseAllMenus();
                var radio = SinglePlayerOffice.CurrentBuilding.CurrentLocation.Interactions.OfType<Radio>().First();
                radio.Station = Radio.Stations[index];
                radio.State = 4;
            };
            radioMenu.OnMenuClose += sender => {
                IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
            };
            MenuPool.Add(radioMenu);

            return radioMenu;
        }

        private static UIMenu CreateWardrobeMenu() {
            MenuPool = new MenuPool();

            var wardrobeMenu = new UIMenu("", "~b~Outfit Options") { MouseEdgeEnabled = false };
            wardrobeMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));

            var torsoMenu = MenuPool.AddSubMenu(wardrobeMenu, "Torso");
            torsoMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            torsoMenu.MouseEdgeEnabled = false;
            var currentTorsoType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 3);
            var torsoTypes = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 3);
                i++) torsoTypes.Add(i);
            torsoMenu.AddItem(new UIMenuListItem("Type", torsoTypes, currentTorsoType));
            var currentTorsoColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 3);
            var torsoColors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 3,
                    currentTorsoType);
                i++) torsoColors.Add(i);
            torsoMenu.AddItem(new UIMenuListItem("Color", torsoColors, currentTorsoColor));
            torsoMenu.RefreshIndex();
            torsoMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    torsoMenu.RemoveItemAt(1);
                    torsoColors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 3,
                            torsoTypes[newIndex]);
                        i++) torsoColors.Add(i);
                    torsoMenu.AddItem(new UIMenuListItem("Color", torsoColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, torsoTypes[newIndex], 0,
                        2);
                    currentTorsoType = torsoTypes[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, currentTorsoType,
                        torsoColors[newIndex], 2);
                }
            };

            var torsoExtraMenu = MenuPool.AddSubMenu(wardrobeMenu, "Torso Extra");
            torsoExtraMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            torsoExtraMenu.MouseEdgeEnabled = false;
            var currentTorsoExtraType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 11);
            var torsoExtraTypes = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 11);
                i++) torsoExtraTypes.Add(i);
            torsoExtraMenu.AddItem(new UIMenuListItem("Type", torsoExtraTypes, currentTorsoExtraType));
            var currentTorsoExtraColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 11);
            var torsoExtraColors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 11,
                    currentTorsoExtraType);
                i++) torsoExtraColors.Add(i);
            torsoExtraMenu.AddItem(new UIMenuListItem("Color", torsoExtraColors, currentTorsoExtraColor));
            torsoExtraMenu.RefreshIndex();
            torsoExtraMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    torsoExtraMenu.RemoveItemAt(1);
                    torsoExtraColors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 11,
                            torsoExtraTypes[newIndex]);
                        i++) torsoExtraColors.Add(i);
                    torsoExtraMenu.AddItem(new UIMenuListItem("Color", torsoExtraColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11,
                        torsoExtraTypes[newIndex], 0, 2);
                    currentTorsoExtraType = torsoExtraTypes[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, currentTorsoExtraType,
                        torsoExtraColors[newIndex], 2);
                }
            };

            var legsMenu = MenuPool.AddSubMenu(wardrobeMenu, "Legs");
            legsMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            legsMenu.MouseEdgeEnabled = false;
            var currentLegsType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 4);
            var legsTypes = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 4);
                i++) legsTypes.Add(i);
            legsMenu.AddItem(new UIMenuListItem("Type", legsTypes, currentLegsType));
            var currentLegsColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 4);
            var legsColors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 4,
                    currentLegsType);
                i++) legsColors.Add(i);
            legsMenu.AddItem(new UIMenuListItem("Color", legsColors, currentLegsColor));
            legsMenu.RefreshIndex();
            legsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    legsMenu.RemoveItemAt(1);
                    legsColors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 4,
                            legsTypes[newIndex]);
                        i++) legsColors.Add(i);
                    legsMenu.AddItem(new UIMenuListItem("Color", legsColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, legsTypes[newIndex], 0,
                        2);
                    currentLegsType = legsTypes[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, currentLegsType,
                        legsColors[newIndex], 2);
                }
            };

            var handsMenu = MenuPool.AddSubMenu(wardrobeMenu, "Hands");
            handsMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            handsMenu.MouseEdgeEnabled = false;
            var currentHandsType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 5);
            var handsTypes = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 5);
                i++) handsTypes.Add(i);
            handsMenu.AddItem(new UIMenuListItem("Type", handsTypes, currentHandsType));
            var currentHandsColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 5);
            var handsColors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 5,
                    currentHandsType);
                i++) handsColors.Add(i);
            handsMenu.AddItem(new UIMenuListItem("Color", handsColors, currentHandsColor));
            handsMenu.RefreshIndex();
            handsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    handsMenu.RemoveItemAt(1);
                    handsColors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 5,
                            handsTypes[newIndex]);
                        i++) handsColors.Add(i);
                    handsMenu.AddItem(new UIMenuListItem("Color", handsColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, handsTypes[newIndex], 0,
                        2);
                    currentHandsType = handsTypes[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, currentHandsType,
                        handsColors[newIndex], 2);
                }
            };

            var feetMenu = MenuPool.AddSubMenu(wardrobeMenu, "Feet");
            feetMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            feetMenu.MouseEdgeEnabled = false;
            var currentFeetType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 6);
            var feetTypes = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 6);
                i++) feetTypes.Add(i);
            feetMenu.AddItem(new UIMenuListItem("Type", feetTypes, currentFeetType));
            var currentFeetColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 6);
            var feetColors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 6,
                    currentFeetType);
                i++) feetColors.Add(i);
            feetMenu.AddItem(new UIMenuListItem("Color", feetColors, currentFeetColor));
            feetMenu.RefreshIndex();
            feetMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    feetMenu.RemoveItemAt(1);
                    feetColors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 6,
                            feetTypes[newIndex]);
                        i++) feetColors.Add(i);
                    feetMenu.AddItem(new UIMenuListItem("Color", feetColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, feetTypes[newIndex], 0,
                        2);
                    currentFeetType = feetTypes[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, currentFeetType,
                        feetColors[newIndex], 2);
                }
            };

            var accessoriesMenu = MenuPool.AddSubMenu(wardrobeMenu, "Accessories");
            accessoriesMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            accessoriesMenu.MouseEdgeEnabled = false;

            var hatsMenu = MenuPool.AddSubMenu(accessoriesMenu, "Hats");
            hatsMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            hatsMenu.MouseEdgeEnabled = false;
            var currentHatsType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 0);
            var hatsTypes = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 0);
                i++) hatsTypes.Add(i);
            hatsMenu.AddItem(new UIMenuListItem("Type", hatsTypes, currentHatsType + 1));
            var currentHatsColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 0);
            var hatsColors = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 0,
                    currentHatsType);
                i++) hatsColors.Add(i);
            hatsMenu.AddItem(new UIMenuListItem("Color", hatsColors, currentHatsColor + 1));
            hatsMenu.RefreshIndex();
            hatsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    hatsMenu.RemoveItemAt(1);
                    hatsColors.Clear();
                    for (var i = -1;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 0,
                            hatsTypes[newIndex]);
                        i++) hatsColors.Add(i);
                    hatsMenu.AddItem(new UIMenuListItem("Color", hatsColors, 1));

                    if (hatsTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 0);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2,
                            Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 2), 0, 2);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 0, hatsTypes[newIndex], 0, true);
                        currentHatsType = hatsTypes[newIndex];
                    }
                }
                else {
                    Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 0, currentHatsType,
                        hatsColors[newIndex], true);
                }
            };

            var glassesMenu = MenuPool.AddSubMenu(accessoriesMenu, "Glasses");
            glassesMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            glassesMenu.MouseEdgeEnabled = false;
            var currentGlassesType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 1);
            var glassesTypes = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 1);
                i++) glassesTypes.Add(i);
            glassesMenu.AddItem(new UIMenuListItem("Type", glassesTypes, currentGlassesType + 1));
            var currentGlassesColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 1);
            var glassesColors = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 1,
                    currentGlassesType);
                i++) glassesColors.Add(i);
            glassesMenu.AddItem(new UIMenuListItem("Color", glassesColors, currentGlassesColor + 1));
            glassesMenu.RefreshIndex();
            glassesMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    glassesMenu.RemoveItemAt(1);
                    glassesColors.Clear();
                    for (var i = -1;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 1,
                            glassesTypes[newIndex]);
                        i++) glassesColors.Add(i);
                    glassesMenu.AddItem(new UIMenuListItem("Color", glassesColors, 1));

                    if (glassesTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 1);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 1, glassesTypes[newIndex], 0,
                            true);
                        currentGlassesType = glassesTypes[newIndex];
                    }
                }
                else {
                    Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 1, currentGlassesType,
                        glassesColors[newIndex], true);
                }
            };

            var earsMenu = MenuPool.AddSubMenu(accessoriesMenu, "Ears");
            earsMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            earsMenu.MouseEdgeEnabled = false;
            var currentEarsType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 2);
            var earsTypes = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 2);
                i++) earsTypes.Add(i);
            earsMenu.AddItem(new UIMenuListItem("Type", earsTypes, currentEarsType + 1));
            var currentEarsColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 2);
            var earsColors = new List<dynamic>();
            for (var i = -1;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 2,
                    currentEarsType);
                i++) earsColors.Add(i);
            earsMenu.AddItem(new UIMenuListItem("Color", earsColors, currentEarsColor + 1));
            earsMenu.RefreshIndex();
            earsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    earsMenu.RemoveItemAt(1);
                    earsColors.Clear();
                    for (var i = -1;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 2,
                            earsTypes[newIndex]);
                        i++) earsColors.Add(i);
                    earsMenu.AddItem(new UIMenuListItem("Color", earsColors, 1));

                    if (earsTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 2);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 2, earsTypes[newIndex], 0, true);
                        currentEarsType = earsTypes[newIndex];
                    }
                }
                else {
                    Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 2, currentEarsType,
                        earsColors[newIndex], true);
                }
            };

            var misc1Menu = MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 1");
            misc1Menu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            misc1Menu.MouseEdgeEnabled = false;
            var currentMisc1Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 8);
            var misc1Types = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 8);
                i++) misc1Types.Add(i);
            misc1Menu.AddItem(new UIMenuListItem("Type", misc1Types, currentMisc1Type));
            var currentMisc1Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 8);
            var misc1Colors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 8,
                    currentMisc1Type);
                i++) misc1Colors.Add(i);
            misc1Menu.AddItem(new UIMenuListItem("Color", misc1Colors, currentMisc1Color));
            misc1Menu.RefreshIndex();
            misc1Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc1Menu.RemoveItemAt(1);
                    misc1Colors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 8,
                            misc1Types[newIndex]);
                        i++) misc1Colors.Add(i);
                    misc1Menu.AddItem(new UIMenuListItem("Color", misc1Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, misc1Types[newIndex], 0,
                        2);
                    currentMisc1Type = misc1Types[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, currentMisc1Type,
                        misc1Colors[newIndex], 2);
                }
            };

            var misc2Menu = MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 2");
            misc2Menu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            misc2Menu.MouseEdgeEnabled = false;
            var currentMisc2Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 9);
            var misc2Types = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 9);
                i++) misc2Types.Add(i);
            misc2Menu.AddItem(new UIMenuListItem("Type", misc2Types, currentMisc2Type));
            var currentMisc2Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 9);
            var misc2Colors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 9,
                    currentMisc2Type);
                i++) misc2Colors.Add(i);
            misc2Menu.AddItem(new UIMenuListItem("Color", misc2Colors, currentMisc2Color));
            misc2Menu.RefreshIndex();
            misc2Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc2Menu.RemoveItemAt(1);
                    misc2Colors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 9,
                            misc2Types[newIndex]);
                        i++) misc2Colors.Add(i);
                    misc2Menu.AddItem(new UIMenuListItem("Color", misc2Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, misc2Types[newIndex], 0,
                        2);
                    currentMisc2Type = misc2Types[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, currentMisc2Type,
                        misc2Colors[newIndex], 2);
                }
            };

            var misc3Menu = MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 3");
            misc3Menu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            misc3Menu.MouseEdgeEnabled = false;
            var currentMisc3Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 10);
            var misc3Types = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 10);
                i++) misc3Types.Add(i);
            misc3Menu.AddItem(new UIMenuListItem("Type", misc3Types, currentMisc3Type));
            var currentMisc3Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 10);
            var misc3Colors = new List<dynamic>();
            for (var i = 0;
                i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 10,
                    currentMisc3Type);
                i++) misc3Colors.Add(i);
            misc3Menu.AddItem(new UIMenuListItem("Color", misc3Colors, currentMisc3Color));
            misc3Menu.RefreshIndex();
            misc3Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc3Menu.RemoveItemAt(1);
                    misc3Colors.Clear();
                    for (var i = 0;
                        i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 10,
                            misc3Types[newIndex]);
                        i++) misc3Colors.Add(i);
                    misc3Menu.AddItem(new UIMenuListItem("Color", misc3Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, misc3Types[newIndex], 0,
                        2);
                    currentMisc3Type = misc3Types[newIndex];
                }
                else {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, currentMisc3Type,
                        misc3Colors[newIndex], 2);
                }
            };

            accessoriesMenu.RefreshIndex();

            wardrobeMenu.RefreshIndex();
            wardrobeMenu.OnMenuClose += sender => {
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
                var wardrobe = SinglePlayerOffice.CurrentBuilding.CurrentLocation.Interactions.OfType<Wardrobe>()
                    .First();
                wardrobe.State = 0;
            };
            MenuPool.Add(wardrobeMenu);

            return wardrobeMenu;
        }

    }

}