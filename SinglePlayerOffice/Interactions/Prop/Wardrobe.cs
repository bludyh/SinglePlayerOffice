using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    internal class Wardrobe : Interaction {
        private Camera camera;
        private float cameraFov;

        private Vector3 cameraPos;
        private Vector3 cameraRot;
        private UIMenu wardrobeMenu;

        public Wardrobe(Vector3 pos, Vector3 rot) {
            Position = pos;
            Rotation = rot;
            CreateWardrobeMenu();
        }

        public override string HelpText => "Press ~INPUT_CONTEXT~ to change outfit";

        public override string RejectHelpText => "You cannot change outfit here";

        public Vector3 Position { get; }
        public Vector3 Rotation { get; }

        public void CreateWardrobeMenu() {
            wardrobeMenu = new UIMenu("", "~b~Outfit Options") { MouseEdgeEnabled = false };
            wardrobeMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));

            var torsoMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Torso");
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

            var torsoExtraMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Torso Extra");
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

            var legsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Legs");
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

            var handsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Hands");
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

            var feetMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Feet");
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

            var accessoriesMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Accessories");
            accessoriesMenu.SetBannerType(new Sprite("shopui_title_highendfashion", "shopui_title_highendfashion",
                new Point(0, 0), new Size(0, 0)));
            accessoriesMenu.MouseEdgeEnabled = false;

            var hatsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Hats");
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

            var glassesMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Glasses");
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

            var earsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Ears");
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

            var misc1Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 1");
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

            var misc2Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 2");
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

            var misc3Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 3");
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
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
                State = 0;
            };
            SinglePlayerOffice.MenuPool.Add(wardrobeMenu);
        }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() &&
                        !SinglePlayerOffice.MenuPool.IsAnyMenuOpen() &&
                        Game.Player.Character.Position.DistanceTo(Position) < 1f) {
                        if (Utilities.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                            Utilities.DisplayHelpTextThisFrame(HelpText);
                            if (Game.IsControlJustPressed(2, Control.Context)) {
                                Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                                SinglePlayerOffice.IsHudHidden = true;
                                State = 1;
                            }
                        }
                        else {
                            Utilities.DisplayHelpTextThisFrame(RejectHelpText);
                        }
                    }

                    break;
                case 1:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, Position.X, Position.Y,
                        Position.Z, 1f, -1, Rotation.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    Game.Player.Character.Task.StandStill(-1);
                    Game.Player.Character.ClearBloodDamage();
                    wardrobeMenu.Visible = true;
                    cameraPos = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0f, 1.5f, 0f));
                    cameraRot = new Vector3(0f, 0f, Game.Player.Character.Rotation.Z + 180f);
                    cameraFov = 75f;
                    camera = World.CreateCamera(cameraPos, cameraRot, cameraFov);
                    World.RenderingCamera = camera;
                    State = -1;
                    break;
            }
        }
    }
}