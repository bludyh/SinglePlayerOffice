using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    static class Interactions {

        private static Vector3 sofaInitialPos;
        private static Vector3 sofaInitialRot;
        private static List<Sofa> sofas;
        private static Sofa sofa;
        private static List<string> sofaPlayerIdleAnims;
        private static Prop tv;
        private static Prop remote;
        private static int tvRenderTargetHandle;
        private static bool isTVOn;
        private static Vector3 computerInitialPos;
        private static Vector3 computerInitialRot;
        private static Prop computerChair;
        private static Prop monitor;
        private static int computerRenderTargetHandle;
        private static List<string> computerPlayerIdleAnims;
        private static List<string> computerChairIdleAnims;
        private static Vector3 leftSafeDoorPos;
        private static Vector3 leftSafeDoorRot;
        private static Vector3 leftSafeInitialPos;
        private static Vector3 leftSafeInitialRot;
        private static Prop leftSafeDoor;
        private static bool isLeftSafeOpened;
        private static Vector3 rightSafeDoorPos;
        private static Vector3 rightSafeDoorRot;
        private static Vector3 rightSafeInitialPos;
        private static Vector3 rightSafeInitialRot;
        private static Prop rightSafeDoor;
        private static bool isRightSafeOpened;
        private static Vector3 radioInitialPos;
        private static Vector3 radioInitialRot;
        private static Prop radio;
        private static List<RadioStation> radioStations;
        private static RadioStation radioStation;
        private static string radioEmitter;
        private static bool isRadioOn;
        private static Vector3 bossChairInitialPos;
        private static Vector3 bossChairInitialRot;
        private static Prop bossChair;
        private static List<string> bossPlayerIdleAnims;
        private static List<string> bossChairIdleAnims;
        private static Vector3 staffChairInitialPos;
        private static Vector3 staffChairInitialRot;
        private static Prop staffChair;
        private static List<string> staffPlayerIdleAnims;
        private static List<string> staffChairIdleAnims;
        private static Vector3 laptopChairInitialPos;
        private static Vector3 laptopChairInitialRot;
        private static Prop laptopChair;
        private static List<string> laptopPlayerIdleAnims;
        private static List<string> laptopChairIdleAnims;
        private static List<Wardrobe> wardrobes;
        private static Wardrobe wardrobe;
        private static Vector3 wardrobeCamPos;
        private static Vector3 wardrobeCamRot;
        private static float wardrobeCamFOV;
        private static Camera wardrobeCam;
        private static UIMenu radioMenu;
        private static UIMenu wardrobeMenu;
        private static int syncSceneHandle;

        private static string SofaInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit on the couch"; } }
        public static int SofaInteractionStatus { get; set; }
        private static string TVInteractionHelpText { get { if (!isTVOn) return "Press ~INPUT_CONTEXT~ to turn on the TV"; return "Press ~INPUT_CONTEXT~ to turn off the TV"; } }
        public static int TvInteractionStatus { get; set; }
        private static string ComputerInteractionRejectHelpText { get { return "You do not have access to this computer"; } }
        public static int ComputerInteractionStatus { get; set; }
        private static string LeftSafeInteractionHelpText { get { if (!isLeftSafeOpened) return "Press ~INPUT_CONTEXT~ to open the safe"; return "Press ~INPUT_CONTEXT~ to close the safe"; } }
        public static int LeftSafeInteractionStatus { get; set; }
        private static string RightSafeInteractionHelpText { get { if (!isRightSafeOpened) return "Press ~INPUT_CONTEXT~ to open the safe"; return "Press ~INPUT_CONTEXT~ to close the safe"; } }
        public static int RightSafeInteractionStatus { get; set; }
        private static string SafeInteractionRejectHelpText { get { return "Only the owner can open the safe"; } }
        private static string RadioInteractionHelpText { get { if (!isRadioOn) return "Press ~INPUT_CONTEXT~ to turn on the radio"; return "Press ~INPUT_CONTEXT~ to turn off the radio"; } }
        public static int RadioInteractionStatus { get; set; }
        private static string ChairInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit down"; } }
        private static string BossChairInteractionRejectHelpText { get { return "Only boss can sit here"; } }
        public static int BossChairInteractionStatus { get; set; }
        public static int StaffChairInteractionStatus { get; set; }
        public static int LaptopChairInteractionStatus { get; set; }
        private static string WardrobeInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to change outfit"; } }
        private static string WardrobeInteractionRejectHelpText { get { return "You cannot change outfit here"; } }
        public static int WardrobeInteractionStatus { get; set; }

        static Interactions() {
            sofas = new List<Sofa>() {
                new Sofa(new Vector3(-137.806f, -644.631f, 167.820f), new Vector3(0f, 0f, -174f)),
                new Sofa(new Vector3(-1564.563f, -583.634f, 107.523f), new Vector3(0f, 0f, -144f)),
                new Sofa(new Vector3(-68.486f, -804.237f, 242.386f), new Vector3(0f, 0f, -20f)),
                new Sofa(new Vector3(-1369.017f, -476.016f, 71.05f), new Vector3(0f, 0f, -82f))
            };
            sofaPlayerIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c" };
            isTVOn = false;
            computerPlayerIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c", "idle_d", "idle_e" };
            computerChairIdleAnims = new List<string> { "idle_a_chair", "idle_b_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair" };
            isLeftSafeOpened = false;
            isRightSafeOpened = false;
            radioStations = new List<RadioStation>() {
                new RadioStation("Los Santos Rock Radio", "Get on the good ship power pop and rock.", "radio_01_class_rock"),
                new RadioStation("Non-Stop-Pop FM", "Pop hits from the 80s, 90s, noughties, and today.", "radio_02_pop"),
                new RadioStation("Radio Los Santos", "Contemporary Hip Hop blazing into your stereo.", "radio_03_hiphop_new"),
                new RadioStation("Channel X", "Real Punk. Real West Coast.", "radio_04_punk"),
                new RadioStation("West Coast Talk Radio", "Real Talk. Real Issues. Real Patronizing. Radio that’s all about you.", "radio_05_talk_01"),
                new RadioStation("Rebel Radio", "The true sound of Blaine County - drunk, armed, and ready to party.", "radio_06_country"),
                new RadioStation("Soulwax FM", "A continuous mix of dance and electronica.", "radio_07_dance_01"),
                new RadioStation("East Los FM", "Mexican electronica mixing corridos and traditional songs with hip hop, rock, and ska.", "radio_08_mexican"),
                new RadioStation("West Coast Classics", "Coming at you from the city of Davis - music from the days of the pager.", "radio_09_hiphop_old"),
                new RadioStation("Blue Ark", "The hottest reggae, dancehall and dub served up by The Upsetter.", "radio_12_reggae"),
                new RadioStation("WorldWide FM", "Join Gilles Peterson as he brings that perfect beat to Los Santos.", "radio_13_jazz"),
                new RadioStation("FlyLo FM", "A mix to carry you through the Los Santos freeways at top speed.", "radio_14_dance_02"),
                new RadioStation("The Lowdown 91.1", "The groove, the soul, the R&B. You dig?", "radio_15_motown"),
                new RadioStation("The Lab", "Dropping science with Dr. No and the Chemical Bro.", "radio_20_thelab"),
                new RadioStation("Radio Mirror Park", "Indie modern rock from the underground.", "radio_16_silverlake"),
                new RadioStation("Space 103.2", "Bringing the Funk to your Assdroid.", "radio_17_funk"),
                new RadioStation("Vinewood Boulevard Radio", "The soundtrack to your broken dreams and unspent potential.", "radio_18_90s_rock"),
                new RadioStation("Blaine County Radio", "The home of the patriot. Enough said.", "radio_11_talk_02")
            };
            isRadioOn = false;
            bossPlayerIdleAnims = new List<string>() { "idle_a", "idle_c", "idle_d", "idle_e" };
            bossChairIdleAnims = new List<string>() { "idle_a_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair" };
            staffPlayerIdleAnims = new List<string>() { "idle_a", "idle_d", "idle_e" };
            staffChairIdleAnims = new List<string>() { "idle_a_chair", "idle_d_chair", "idle_e_chair" };
            laptopPlayerIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c" };
            laptopChairIdleAnims = new List<string>() { "idle_a_chair", "idle_b_chair", "idle_c_chair" };
            wardrobes = new List<Wardrobe>() {
                new Wardrobe(new Vector3(-132.303f, -632.859f, 168.820f), new Vector3(0f, 0f, -84f)),
                new Wardrobe(new Vector3(-1565.723f, -570.756f, 108.523f), new Vector3(0f, 0f, -54f)),
                new Wardrobe(new Vector3(-78.625f, -812.353f, 243.386f), new Vector3(0f, 0f, 70f)),
                new Wardrobe(new Vector3(-1381.006f, -470.913f, 72.042f), new Vector3(0f, 0f, 8f))
            };
        }

        public static void CreateRadioMenu() {
            radioMenu = new UIMenu("Radio", "~b~Radio Stations");
            foreach (RadioStation station in radioStations) {
                UIMenuItem radioStationBtn = new UIMenuItem(station.Name, station.Description);
                radioMenu.AddItem(radioStationBtn);
            }
            radioMenu.RefreshIndex();
            radioMenu.OnItemSelect += (sender, item, index) => {
                SinglePlayerOffice.MenuPool.CloseAllMenus();
                radioStation = radioStations[index];
                RadioInteractionStatus = 3;
            };
            radioMenu.OnMenuClose += (sender) => {
                Game.Player.Character.Task.ClearAll();
            };
            SinglePlayerOffice.MenuPool.Add(radioMenu);
        }

        public static void CreateWardrobeMenu() {
            wardrobeMenu = new UIMenu("Wardrobe", "~b~Outfit Options") { MouseEdgeEnabled = false };

            UIMenu torsoMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Torso");
            torsoMenu.MouseEdgeEnabled = false;
            int currentTorsoType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 3);
            List<dynamic> torsoTypes = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 3); i++) torsoTypes.Add(i);
            torsoMenu.AddItem(new UIMenuListItem("Type", torsoTypes, currentTorsoType));
            int currentTorsoColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 3);
            List<dynamic> torsoColors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 3, currentTorsoType); i++) torsoColors.Add(i);
            torsoMenu.AddItem(new UIMenuListItem("Color", torsoColors, currentTorsoColor));
            torsoMenu.RefreshIndex();
            torsoMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    torsoMenu.RemoveItemAt(1);
                    torsoColors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 3, torsoTypes[newIndex]); i++) torsoColors.Add(i);
                    torsoMenu.AddItem(new UIMenuListItem("Color", torsoColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, torsoTypes[newIndex], 0, 2);
                    currentTorsoType = torsoTypes[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, currentTorsoType, torsoColors[newIndex], 2);
            };

            UIMenu torsoExtraMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Torso Extra");
            torsoExtraMenu.MouseEdgeEnabled = false;
            int currentTorsoExtraType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 11);
            List<dynamic> torsoExtraTypes = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 11); i++) torsoExtraTypes.Add(i);
            torsoExtraMenu.AddItem(new UIMenuListItem("Type", torsoExtraTypes, currentTorsoExtraType));
            int currentTorsoExtraColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 11);
            List<dynamic> torsoExtraColors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 11, currentTorsoExtraType); i++) torsoExtraColors.Add(i);
            torsoExtraMenu.AddItem(new UIMenuListItem("Color", torsoExtraColors, currentTorsoExtraColor));
            torsoExtraMenu.RefreshIndex();
            torsoExtraMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    torsoExtraMenu.RemoveItemAt(1);
                    torsoExtraColors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 11, torsoExtraTypes[newIndex]); i++) torsoExtraColors.Add(i);
                    torsoExtraMenu.AddItem(new UIMenuListItem("Color", torsoExtraColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, torsoExtraTypes[newIndex], 0, 2);
                    currentTorsoExtraType = torsoExtraTypes[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, currentTorsoExtraType, torsoExtraColors[newIndex], 2);
            };

            UIMenu legsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Legs");
            legsMenu.MouseEdgeEnabled = false;
            int currentLegsType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 4);
            List<dynamic> legsTypes = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 4); i++) legsTypes.Add(i);
            legsMenu.AddItem(new UIMenuListItem("Type", legsTypes, currentLegsType));
            int currentLegsColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 4);
            List<dynamic> legsColors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 4, currentLegsType); i++) legsColors.Add(i);
            legsMenu.AddItem(new UIMenuListItem("Color", legsColors, currentLegsColor));
            legsMenu.RefreshIndex();
            legsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    legsMenu.RemoveItemAt(1);
                    legsColors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 4, legsTypes[newIndex]); i++) legsColors.Add(i);
                    legsMenu.AddItem(new UIMenuListItem("Color", legsColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, legsTypes[newIndex], 0, 2);
                    currentLegsType = legsTypes[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, currentLegsType, legsColors[newIndex], 2);
            };

            UIMenu handsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Hands");
            handsMenu.MouseEdgeEnabled = false;
            int currentHandsType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 5);
            List<dynamic> handsTypes = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 5); i++) handsTypes.Add(i);
            handsMenu.AddItem(new UIMenuListItem("Type", handsTypes, currentHandsType));
            int currentHandsColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 5);
            List<dynamic> handsColors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 5, currentHandsType); i++) handsColors.Add(i);
            handsMenu.AddItem(new UIMenuListItem("Color", handsColors, currentHandsColor));
            handsMenu.RefreshIndex();
            handsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    handsMenu.RemoveItemAt(1);
                    handsColors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 5, handsTypes[newIndex]); i++) handsColors.Add(i);
                    handsMenu.AddItem(new UIMenuListItem("Color", handsColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, handsTypes[newIndex], 0, 2);
                    currentHandsType = handsTypes[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, currentHandsType, handsColors[newIndex], 2);
            };

            UIMenu feetMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Feet");
            feetMenu.MouseEdgeEnabled = false;
            int currentFeetType = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 6);
            List<dynamic> feetTypes = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 6); i++) feetTypes.Add(i);
            feetMenu.AddItem(new UIMenuListItem("Type", feetTypes, currentFeetType));
            int currentFeetColor = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 6);
            List<dynamic> feetColors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 6, currentFeetType); i++) feetColors.Add(i);
            feetMenu.AddItem(new UIMenuListItem("Color", feetColors, currentFeetColor));
            feetMenu.RefreshIndex();
            feetMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    feetMenu.RemoveItemAt(1);
                    feetColors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 6, feetTypes[newIndex]); i++) feetColors.Add(i);
                    feetMenu.AddItem(new UIMenuListItem("Color", feetColors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, feetTypes[newIndex], 0, 2);
                    currentFeetType = feetTypes[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, currentFeetType, feetColors[newIndex], 2);
            };

            UIMenu accessoriesMenu = SinglePlayerOffice.MenuPool.AddSubMenu(wardrobeMenu, "Accessories");
            accessoriesMenu.MouseEdgeEnabled = false;

            UIMenu hatsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Hats");
            hatsMenu.MouseEdgeEnabled = false;
            int currentHatsType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 0);
            List<dynamic> hatsTypes = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 0); i++) hatsTypes.Add(i);
            hatsMenu.AddItem(new UIMenuListItem("Type", hatsTypes, currentHatsType + 1));
            int currentHatsColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 0);
            List<dynamic> hatsColors = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 0, currentHatsType); i++) hatsColors.Add(i);
            hatsMenu.AddItem(new UIMenuListItem("Color", hatsColors, currentHatsColor + 1));
            hatsMenu.RefreshIndex();
            hatsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    hatsMenu.RemoveItemAt(1);
                    hatsColors.Clear();
                    for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 0, hatsTypes[newIndex]); i++) hatsColors.Add(i);
                    hatsMenu.AddItem(new UIMenuListItem("Color", hatsColors, 1));
                    if (hatsTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 0);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 2), 0, 2);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 0, hatsTypes[newIndex], 0, true);
                        currentHatsType = hatsTypes[newIndex];
                    }
                }
                else Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 0, currentHatsType, hatsColors[newIndex], true);
            };

            UIMenu glassesMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Glasses");
            glassesMenu.MouseEdgeEnabled = false;
            int currentGlassesType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 1);
            List<dynamic> glassesTypes = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 1); i++) glassesTypes.Add(i);
            glassesMenu.AddItem(new UIMenuListItem("Type", glassesTypes, currentGlassesType + 1));
            int currentGlassesColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 1);
            List<dynamic> glassesColors = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 1, currentGlassesType); i++) glassesColors.Add(i);
            glassesMenu.AddItem(new UIMenuListItem("Color", glassesColors, currentGlassesColor + 1));
            glassesMenu.RefreshIndex();
            glassesMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    glassesMenu.RemoveItemAt(1);
                    glassesColors.Clear();
                    for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 1, glassesTypes[newIndex]); i++) glassesColors.Add(i);
                    glassesMenu.AddItem(new UIMenuListItem("Color", glassesColors, 1));
                    if (glassesTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 1);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 1, glassesTypes[newIndex], 0, true);
                        currentGlassesType = glassesTypes[newIndex];
                    }
                }
                else Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 1, currentGlassesType, glassesColors[newIndex], true);
            };

            UIMenu earsMenu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Ears");
            earsMenu.MouseEdgeEnabled = false;
            int currentEarsType = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 2);
            List<dynamic> earsTypes = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, Game.Player.Character, 2); i++) earsTypes.Add(i);
            earsMenu.AddItem(new UIMenuListItem("Type", earsTypes, currentEarsType + 1));
            int currentEarsColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 2);
            List<dynamic> earsColors = new List<dynamic>();
            for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 2, currentEarsType); i++) earsColors.Add(i);
            earsMenu.AddItem(new UIMenuListItem("Color", earsColors, currentEarsColor + 1));
            earsMenu.RefreshIndex();
            earsMenu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    earsMenu.RemoveItemAt(1);
                    earsColors.Clear();
                    for (int i = -1; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_TEXTURE_VARIATIONS, Game.Player.Character, 2, earsTypes[newIndex]); i++) earsColors.Add(i);
                    earsMenu.AddItem(new UIMenuListItem("Color", earsColors, 1));
                    if (earsTypes[newIndex] == -1) {
                        Function.Call(Hash.CLEAR_PED_PROP, Game.Player.Character, 2);
                    }
                    else {
                        Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 2, earsTypes[newIndex], 0, true);
                        currentEarsType = earsTypes[newIndex];
                    }
                }
                else Function.Call(Hash.SET_PED_PROP_INDEX, Game.Player.Character, 2, currentEarsType, earsColors[newIndex], true);
            };

            UIMenu misc1Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 1");
            misc1Menu.MouseEdgeEnabled = false;
            int currentMisc1Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 8);
            List<dynamic> misc1Types = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 8); i++) misc1Types.Add(i);
            misc1Menu.AddItem(new UIMenuListItem("Type", misc1Types, currentMisc1Type));
            int currentMisc1Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 8);
            List<dynamic> misc1Colors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 8, currentMisc1Type); i++) misc1Colors.Add(i);
            misc1Menu.AddItem(new UIMenuListItem("Color", misc1Colors, currentMisc1Color));
            misc1Menu.RefreshIndex();
            misc1Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc1Menu.RemoveItemAt(1);
                    misc1Colors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 8, misc1Types[newIndex]); i++) misc1Colors.Add(i);
                    misc1Menu.AddItem(new UIMenuListItem("Color", misc1Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, misc1Types[newIndex], 0, 2);
                    currentMisc1Type = misc1Types[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, currentMisc1Type, misc1Colors[newIndex], 2);
            };

            UIMenu misc2Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 2");
            misc2Menu.MouseEdgeEnabled = false;
            int currentMisc2Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 9);
            List<dynamic> misc2Types = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 9); i++) misc2Types.Add(i);
            misc2Menu.AddItem(new UIMenuListItem("Type", misc2Types, currentMisc2Type));
            int currentMisc2Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 9);
            List<dynamic> misc2Colors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 9, currentMisc2Type); i++) misc2Colors.Add(i);
            misc2Menu.AddItem(new UIMenuListItem("Color", misc2Colors, currentMisc2Color));
            misc2Menu.RefreshIndex();
            misc2Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc2Menu.RemoveItemAt(1);
                    misc2Colors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 9, misc2Types[newIndex]); i++) misc2Colors.Add(i);
                    misc2Menu.AddItem(new UIMenuListItem("Color", misc2Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, misc2Types[newIndex], 0, 2);
                    currentMisc2Type = misc2Types[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, currentMisc2Type, misc2Colors[newIndex], 2);
            };

            UIMenu misc3Menu = SinglePlayerOffice.MenuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 3");
            misc3Menu.MouseEdgeEnabled = false;
            int currentMisc3Type = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, Game.Player.Character, 10);
            List<dynamic> misc3Types = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 10); i++) misc3Types.Add(i);
            misc3Menu.AddItem(new UIMenuListItem("Type", misc3Types, currentMisc3Type));
            int currentMisc3Color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, Game.Player.Character, 10);
            List<dynamic> misc3Colors = new List<dynamic>();
            for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 10, currentMisc3Type); i++) misc3Colors.Add(i);
            misc3Menu.AddItem(new UIMenuListItem("Color", misc3Colors, currentMisc3Color));
            misc3Menu.RefreshIndex();
            misc3Menu.OnListChange += (sender, list, newIndex) => {
                if (list.Text == "Type") {
                    misc3Menu.RemoveItemAt(1);
                    misc3Colors.Clear();
                    for (int i = 0; i < Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, 10, misc3Types[newIndex]); i++) misc3Colors.Add(i);
                    misc3Menu.AddItem(new UIMenuListItem("Color", misc3Colors, 0));
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, misc3Types[newIndex], 0, 2);
                    currentMisc3Type = misc3Types[newIndex];
                }
                else Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, currentMisc3Type, misc3Colors[newIndex], 2);
            };

            accessoriesMenu.RefreshIndex();

            wardrobeMenu.RefreshIndex();
            wardrobeMenu.OnMenuClose += (sender) => {
                World.RenderingCamera = null;
                World.DestroyAllCameras();
                SinglePlayerOffice.IsHudHidden = false;
                Game.Player.Character.Task.ClearAll();
                WardrobeInteractionStatus = 0;
            };
            SinglePlayerOffice.MenuPool.Add(wardrobeMenu);
        }

        public static void SofaOnTick() {
            switch (SofaInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Sofa sofa in sofas) {
                            if (Game.Player.Character.Position.DistanceTo(sofa.Position) < 1.5f) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(SofaInteractionHelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    Interactions.sofa = sofa;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    SofaInteractionStatus = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    sofaInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@seating@male@var_a@base@", "enter", sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 0, 2);
                    sofaInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@seating@male@var_a@base@", "enter", sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, sofaInitialPos.X, sofaInitialPos.Y, sofaInitialPos.Z, 1f, 1000, sofaInitialRot.Z, 0f);
                    SofaInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    SofaInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    SofaInteractionStatus = 4;
                    break;
                case 4:
                    SinglePlayerOffice.DisplayHelpTextThisFrame(TVInteractionHelpText + "~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        SofaInteractionStatus = 5;
                        break;
                    }
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        SofaInteractionStatus = 8;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", sofaPlayerIdleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3)], 4f, -1.5f, 13, 16, 1148846080, 0);
                    SofaInteractionStatus = 3;
                    break;
                case 5:
                    foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 10f)) {
                        if (prop.Model.Hash == 608950395 || prop.Model.Hash == 1036195894) {
                            tv = prop;
                            break;
                        }
                    }
                    Model remoteModel = new Model("ex_prop_tv_settop_remote");
                    remoteModel.Request(250);
                    if (remoteModel.IsInCdImage && remoteModel.IsValid) {
                        while (!remoteModel.IsLoaded) Script.Wait(50);
                        remote = World.CreateProp(remoteModel, Vector3.Zero, false, false);
                    }
                    remoteModel.MarkAsNoLongerNeeded();
                    remote.AttachTo(Game.Player.Character, Game.Player.Character.GetBoneIndex(Bone.SKEL_R_Hand), new Vector3(0.12f, 0.02f, -0.04f), new Vector3(-10f, 100f, 120f));
                    if (isTVOn && Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2) == 1) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "TV_BORED", "SPEECH_PARAMS_FORCE");
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@game@seated@male@var_c@base@", "enter_a", 4f, -4f, 13, 16, 1000f, 0);
                    SofaInteractionStatus = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    TvInteractionStatus = 1;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@game@seated@male@var_c@base@", "exit_a", 4f, -4f, 13, 16, 1000f, 0);
                    SofaInteractionStatus = 7;
                    break;
                case 7:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    remote.Delete();
                    SofaInteractionStatus = 3;
                    break;
                case 8:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    SofaInteractionStatus = 9;
                    break;
                case 9:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    SofaInteractionStatus = 0;
                    break;
            }
        }

        public static void TVOnTick() {
            switch (TvInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1.5f)) {
                            if (prop.Model.Hash == 608950395 || prop.Model.Hash == 1036195894) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(TVInteractionHelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    tv = prop;
                                    TvInteractionStatus = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    if (tv.Model.Hash == 608950395) {
                        Prop oldTV = tv;
                        Model tvModel = new Model("prop_tv_flat_01");
                        tvModel.Request(250);
                        if (tvModel.IsInCdImage && tvModel.IsValid) {
                            while (!tvModel.IsLoaded) Script.Wait(50);
                            tv = World.CreateProp(tvModel, Vector3.Zero, false, false);
                        }
                        tvModel.MarkAsNoLongerNeeded();
                        tv.Position = oldTV.Position;
                        tv.Rotation = oldTV.Rotation;
                        oldTV.Delete();
                    }
                    TvInteractionStatus = 2;
                    break;
                case 2:
                    if (!isTVOn) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "tvscreen", 0);
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, tv.Model)) Function.Call(Hash.LINK_NAMED_RENDERTARGET, tv.Model);
                        if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) tvRenderTargetHandle = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "tvscreen");
                        Function.Call(Hash.REGISTER_SCRIPT_WITH_AUDIO, 0);
                        Function.Call(Hash.SET_TV_CHANNEL, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2));
                        Function.Call(Hash.SET_TV_VOLUME, 0);
                        Function.Call(Hash.ENABLE_MOVIE_SUBTITLES, 1);
                    }
                    else {
                        if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                            Script.Wait(0);
                            Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
                        }
                    }
                    isTVOn = !isTVOn;
                    TvInteractionStatus = 0;
                    break;
            }
            if (isTVOn) {
                Function.Call(Hash.SET_TV_AUDIO_FRONTEND, 0);
                Function.Call(Hash.ATTACH_TV_AUDIO_TO_ENTITY, tv);
                Function.Call(Hash.SET_TEXT_RENDER_ID, tvRenderTargetHandle);
                Function.Call(Hash._0x61BB1D9B3A95D802, 4);
                Function.Call(Hash._0xC6372ECD45D73BCD, 1);
                Function.Call(Hash.DRAW_TV_CHANNEL, 0.5, 0.5, 1.0, 1.0, 0.0, 255, 255, 255, 255);
                Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
            }
        }

        public static void ComputerOnTick() {
            switch (ComputerInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1.2f)) {
                            if (prop.Model.Hash == -1626066319 || prop.Model.Hash == 1339364336) {
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)SinglePlayerOffice.GetCurrentBuilding().Owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(ChairInteractionHelpText);
                                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                        computerChair = prop;
                                        SinglePlayerOffice.IsHudHidden = true;
                                        ComputerInteractionStatus = 1;
                                    }
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(ComputerInteractionRejectHelpText);
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    computerInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@male@", "enter", computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, computerChair.Rotation.X, computerChair.Rotation.Y, computerChair.Rotation.Z, 0, 2);
                    computerInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@male@", "enter", computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, computerChair.Rotation.X, computerChair.Rotation.Y, computerChair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, computerInitialPos.X, computerInitialPos.Y, computerInitialPos.Z, 1f, 1000, computerInitialRot.Z, 0f);
                    ComputerInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "enter_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "base_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 4;
                    break;
                case 4:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the computer~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        ComputerInteractionStatus = 5;
                        break;
                    }
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        ComputerInteractionStatus = 9;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 5);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", computerPlayerIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, computerChairIdleAnims[rnd], "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 3;
                    break;
                case 5:
                    monitor = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 1f, 743064848, 0, 0, 0);
                    if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_computer_screen", 0);
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, 743064848)) {
                            Function.Call(Hash.LINK_NAMED_RENDERTARGET, 743064848);
                            computerRenderTargetHandle = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "prop_ex_computer_screen");
                        }
                    }
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "computer_enter", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "computer_enter_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 6;
                    break;
                case 6:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetHandle);
                    Function.Call((Hash)13305974099546635958, 73, 73);
                    Function.Call((Hash)3154009034243605640, "MPDesktop", "DesktopUI_Login", 0.5f, 0.5f, 1f, 1f, 0f, 255, 255, 255, 255);
                    Function.Call((Hash)16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, 1);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "computer_idle", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "computer_idle_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 7;
                    break;
                case 7:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetHandle);
                    Function.Call((Hash)13305974099546635958, 73, 73);
                    Function.Call((Hash)3154009034243605640, "MPDesktop", "DesktopUI_Map", 0.5f, 0.5f, 1f, 1f, 0f, 255, 255, 255, 255);
                    Function.Call((Hash)16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_AIM~ to stop using the computer");
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        ComputerInteractionStatus = 8;
                        break;
                    }
                    break;
                case 8:
                    if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Script.Wait(0);
                        Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_computer_screen");
                    }
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "computer_exit", 1000f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "computer_exit_chair", "anim@amb@office@boss@male@", 1000f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 3;
                    break;
                case 9:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@male@", "exit", 4f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncSceneHandle, "exit_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 10;
                    break;
                case 10:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    ComputerInteractionStatus = 0;
                    break;
            }
        }

        public static void LeftSafeOnTick() {
            switch (LeftSafeInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1.4f)) {
                            switch (prop.Model.Hash) {
                                case 646926492:
                                case 845785021:
                                case -1126494299:
                                case -524920966:
                                case -1842578680:
                                case -1387653807:
                                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)SinglePlayerOffice.GetCurrentBuilding().Owner) {
                                        SinglePlayerOffice.DisplayHelpTextThisFrame(LeftSafeInteractionHelpText);
                                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                            leftSafeDoor = prop;
                                            SinglePlayerOffice.IsHudHidden = true;
                                            LeftSafeInteractionStatus = 1;
                                        }
                                    }
                                    else SinglePlayerOffice.DisplayHelpTextThisFrame(SafeInteractionRejectHelpText);
                                    break;
                            }
                        }
                    }
                    break;
                case 1:
                    if (!isLeftSafeOpened) {
                        leftSafeInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@vault@left@male@", "open", leftSafeDoor.Position.X, leftSafeDoor.Position.Y, leftSafeDoor.Position.Z, leftSafeDoor.Rotation.X, leftSafeDoor.Rotation.Y, leftSafeDoor.Rotation.Z, 0, 2);
                        leftSafeInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@vault@left@male@", "open", leftSafeDoor.Position.X, leftSafeDoor.Position.Y, leftSafeDoor.Position.Z, leftSafeDoor.Rotation.X, leftSafeDoor.Rotation.Y, leftSafeDoor.Rotation.Z, 0, 2);
                    }
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, leftSafeInitialPos.X, leftSafeInitialPos.Y, leftSafeInitialPos.Z, 1f, -1, leftSafeInitialRot.Z, 0f);
                    LeftSafeInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    if (!isLeftSafeOpened) {
                        leftSafeDoorPos = leftSafeDoor.Position;
                        leftSafeDoorRot = leftSafeDoor.Rotation;
                    }
                    LeftSafeInteractionStatus = 3;
                    break;
                case 3:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, leftSafeDoorPos.X, leftSafeDoorPos.Y, leftSafeDoorPos.Z, 0f, 0f, leftSafeDoorRot.Z, 2);
                    if (!isLeftSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@left@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, leftSafeDoor, syncSceneHandle, "open_door", "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        isLeftSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@left@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, leftSafeDoor, syncSceneHandle, "close_door", "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        isLeftSafeOpened = false;
                    }
                    LeftSafeInteractionStatus = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    LeftSafeInteractionStatus = 0;
                    break;
            }
        }

        public static void RightSafeOnTick() {
            switch (RightSafeInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1.4f)) {
                            switch (prop.Model.Hash) {
                                case -1176373441:
                                case -1149617688:
                                case -548219756:
                                case 1854960432:
                                case 682108925:
                                case 1002451519:
                                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)SinglePlayerOffice.GetCurrentBuilding().Owner) {
                                        SinglePlayerOffice.DisplayHelpTextThisFrame(RightSafeInteractionHelpText);
                                        if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                            rightSafeDoor = prop;
                                            SinglePlayerOffice.IsHudHidden = true;
                                            RightSafeInteractionStatus = 1;
                                        }
                                    }
                                    else SinglePlayerOffice.DisplayHelpTextThisFrame(SafeInteractionRejectHelpText);
                                    break;
                            }
                        }
                    }
                    break;
                case 1:
                    if (!isRightSafeOpened) {
                        rightSafeInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@vault@right@male@", "open", rightSafeDoor.Position.X, rightSafeDoor.Position.Y, rightSafeDoor.Position.Z, rightSafeDoor.Rotation.X, rightSafeDoor.Rotation.Y, rightSafeDoor.Rotation.Z, 0, 2);
                        rightSafeInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@vault@right@male@", "open", rightSafeDoor.Position.X, rightSafeDoor.Position.Y, rightSafeDoor.Position.Z, rightSafeDoor.Rotation.X, rightSafeDoor.Rotation.Y, rightSafeDoor.Rotation.Z, 0, 2);
                    }
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, rightSafeInitialPos.X, rightSafeInitialPos.Y, rightSafeInitialPos.Z, 1f, -1, rightSafeInitialRot.Z, 0f);
                    RightSafeInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    if (!isRightSafeOpened) {
                        rightSafeDoorPos = rightSafeDoor.Position;
                        rightSafeDoorRot = rightSafeDoor.Rotation;
                    }
                    RightSafeInteractionStatus = 3;
                    break;
                case 3:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, rightSafeDoorPos.X, rightSafeDoorPos.Y, rightSafeDoorPos.Z, 0f, 0f, rightSafeDoorRot.Z, 2);
                    if (!isRightSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@right@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, rightSafeDoor, syncSceneHandle, "open_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        isRightSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boss@vault@right@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, rightSafeDoor, syncSceneHandle, "close_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        isRightSafeOpened = false;
                    }
                    RightSafeInteractionStatus = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    RightSafeInteractionStatus = 0;
                    break;
            }
        }

        public static void RadioOnTick() {
            switch (RadioInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            switch (prop.Model.Hash) {
                                case -364924791:
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(RadioInteractionHelpText);
                                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                        radio = prop;
                                        SinglePlayerOffice.IsHudHidden = true;
                                        RadioInteractionStatus = 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 1:
                    radioInitialPos = radio.GetOffsetInWorldCoords(new Vector3(0f, -0.65f, 0f));
                    radioInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@mp_radio@high_apment", "action_a_bedroom", radio.Position.X, radio.Position.Y, radio.Position.Z, radio.Rotation.X, radio.Rotation.Y, radio.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, radioInitialPos.X, radioInitialPos.Y, radioInitialPos.Z, 1f, -1, radioInitialRot.Z, 0f);
                    RadioInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    Game.Player.Character.Task.StandStill(-1);
                    if (!isRadioOn && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                        radioMenu.Visible = true;
                        RadioInteractionStatus = 0;
                    }
                    if (isRadioOn) {
                        RadioInteractionStatus = 3;
                    }
                    break;
                case 3:
                    Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom", 4f, -4f, -1, 0, 0, 0, 0, 0);
                    RadioInteractionStatus = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom") > 0.5f) {
                        radioEmitter = SinglePlayerOffice.GetCurrentBuilding().GetRadioEmitter();
                        if (!isRadioOn) {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitter, true);
                            Function.Call(Hash._0x0E0CD610D5EB6C85, radioEmitter, radio);
                            Function.Call(Hash.SET_EMITTER_RADIO_STATION, radioEmitter, radioStation.GameName);
                            isRadioOn = true;
                        }
                        else {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitter, false);
                            isRadioOn = false;
                        }
                        RadioInteractionStatus = 5;
                    }
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_apment", "action_a_bedroom") > 0.9f) {
                        if (isRadioOn && Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2) == 1) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "RADIO_LIKE", "SPEECH_PARAMS_FORCE");
                        SinglePlayerOffice.IsHudHidden = false;
                        Game.Player.Character.Task.ClearAll();
                        RadioInteractionStatus = 0;
                    }
                    break;
            }
        }

        public static void TalkShit() {
            switch (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character)) {
                case 0:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "CULT_TALK", "SPEECH_PARAMS_FORCE"); break;
                case 1:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE"); break;
                case 3:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_INSULT_OLD", "SPEECH_PARAMS_FORCE"); break;
            }
        }

        public static void BossChairOnTick() {
            switch (BossChairInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            if (prop.Model.Hash == -1278649385 && World.GetNearbyProps(prop.Position, 1.5f, -1278649385).Length == 1 && World.GetNearbyProps(prop.Position, 1.5f, 1385417869).Length == 0 && World.GetNearbyPeds(prop.Position, 0.5f).Length == 0) {
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)SinglePlayerOffice.GetCurrentBuilding().Owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(ChairInteractionHelpText);
                                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                        bossChair = prop;
                                        SinglePlayerOffice.IsHudHidden = true;
                                        BossChairInteractionStatus = 1;
                                    }
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(BossChairInteractionRejectHelpText);
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    bossChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@boss@male@", "enter_b", bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, bossChair.Rotation.X, bossChair.Rotation.Y, bossChair.Rotation.Z, 0, 2);
                    bossChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@boss@male@", "enter_b", bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, bossChair.Rotation.X, bossChair.Rotation.Y, bossChair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, bossChairInitialPos.X, bossChairInitialPos.Y, bossChairInitialPos.Z, 1f, -1, bossChairInitialRot.Z, 0f);
                    BossChairInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncSceneHandle, "enter_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossChairInteractionStatus = 3;
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        TalkShit();
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncSceneHandle, "base_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossChairInteractionStatus = 4;
                    goto case 4;
                case 4:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        TalkShit();
                    }
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        BossChairInteractionStatus = 5;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 4);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", bossPlayerIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncSceneHandle, bossChairIdleAnims[rnd], "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossChairInteractionStatus = 3;
                    break;
                case 5:
                    Ped[] nearbyPeds = World.GetNearbyPeds(Game.Player.Character, 5f);
                    if (nearbyPeds.Length != 0) {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GAME_QUIT", "SPEECH_PARAMS_FORCE");
                    } 
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", "exit_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncSceneHandle, "exit_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 13, 1000f);
                    BossChairInteractionStatus = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    BossChairInteractionStatus = 0;
                    break;
            }
        }

        public static void StaffChairOnTick() {
            switch (StaffChairInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            if (prop.Model.Hash == -1278649385 && World.GetNearbyProps(prop.Position, 1.5f, -1278649385).Length != 1 && World.GetNearbyProps(prop.Position, 1.5f, 1385417869).Length == 0 && World.GetNearbyPeds(prop.Position, 0.5f).Length == 0) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(ChairInteractionHelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    staffChair = prop;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    StaffChairInteractionStatus = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    staffChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, staffChair.Rotation.X, staffChair.Rotation.Y, staffChair.Rotation.Z, 0, 2);
                    staffChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, staffChair.Rotation.X, staffChair.Rotation.Y, staffChair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, staffChairInitialPos.X, staffChairInitialPos.Y, staffChairInitialPos.Z, 1f, -1, staffChairInitialRot.Z, 0f);
                    StaffChairInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncSceneHandle, "enter_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                    StaffChairInteractionStatus = 3;
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        TalkShit();
                        ((Office)SinglePlayerOffice.GetCurrentBuilding().GetCurrentLocation()).Scene.BossConversationStatus = 2;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_c@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncSceneHandle, "base_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                    StaffChairInteractionStatus = 4;
                    break;
                case 4:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        TalkShit();
                        ((Office)SinglePlayerOffice.GetCurrentBuilding().GetCurrentLocation()).Scene.BossConversationStatus = 2;
                    }
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        StaffChairInteractionStatus = 5;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_c@base@", staffPlayerIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncSceneHandle, staffChairIdleAnims[rnd], "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                    StaffChairInteractionStatus = 3;
                    break;
                case 5:
                    Ped[] nearbyPeds = World.GetNearbyPeds(Game.Player.Character, 5f);
                    if (nearbyPeds.Length != 0) {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GAME_QUIT", "SPEECH_PARAMS_FORCE");
                    } 
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_c@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncSceneHandle, "exit_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 13, 1000f);
                    StaffChairInteractionStatus = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    StaffChairInteractionStatus = 0;
                    break;
            }
        }

        public static void LaptopChairOnTick() {
            switch (LaptopChairInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            if (prop.Model.Hash == -1278649385 && World.GetNearbyProps(prop.Position, 1.5f, -1278649385).Length == 1 && World.GetNearbyProps(prop.Position, 1.5f, 1385417869).Length != 0) {
                                SinglePlayerOffice.DisplayHelpTextThisFrame(ChairInteractionHelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    laptopChair = prop;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    LaptopChairInteractionStatus = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    laptopChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, laptopChair.Rotation.X, laptopChair.Rotation.Y, laptopChair.Rotation.Z, 0, 2);
                    laptopChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, laptopChair.Rotation.X, laptopChair.Rotation.Y, laptopChair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, laptopChairInitialPos.X, laptopChairInitialPos.Y, laptopChairInitialPos.Z, 1f, -1, laptopChairInitialRot.Z, 0f);
                    LaptopChairInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, "enter_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "enter", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, "enter_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, "base_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 5;
                    break;
                case 5:
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        LaptopChairInteractionStatus = 6;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", laptopPlayerIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, laptopChairIdleAnims[rnd], "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 4;
                    break;
                case 6:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, "exit_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 13, 1000f);
                    LaptopChairInteractionStatus = 7;
                    break;
                case 7:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncSceneHandle, "exit_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 13, 1000f);
                    LaptopChairInteractionStatus = 8;
                    break;
                case 8:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    LaptopChairInteractionStatus = 0;
                    break;
            }
        }

        public static void WardrobeOnTick() {
            switch (WardrobeInteractionStatus) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Wardrobe wardrobe in wardrobes) {
                            if (Game.Player.Character.Position.DistanceTo(wardrobe.Position) < 1f) {
                                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)SinglePlayerOffice.GetCurrentBuilding().Owner) {
                                    SinglePlayerOffice.DisplayHelpTextThisFrame(WardrobeInteractionHelpText);
                                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                        Interactions.wardrobe = wardrobe;
                                        SinglePlayerOffice.IsHudHidden = true;
                                        WardrobeInteractionStatus = 1;
                                    }
                                }
                                else SinglePlayerOffice.DisplayHelpTextThisFrame(WardrobeInteractionRejectHelpText);
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, wardrobe.Position.X, wardrobe.Position.Y, wardrobe.Position.Z, 1f, -1, wardrobe.Rotation.Z, 0f);
                    WardrobeInteractionStatus = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    Game.Player.Character.Task.StandStill(-1);
                    Game.Player.Character.ClearBloodDamage();
                    wardrobeMenu.Visible = true;
                    wardrobeCamPos = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0f, 1.5f, 0f));
                    wardrobeCamRot = new Vector3(0f, 0f, Game.Player.Character.Rotation.Z + 180f);
                    wardrobeCamFOV = 75f;
                    wardrobeCam = World.CreateCamera(wardrobeCamPos, wardrobeCamRot, wardrobeCamFOV);
                    World.RenderingCamera = wardrobeCam;
                    WardrobeInteractionStatus = -1;
                    break;
            }
        }

        public static void Reset() {
            isTVOn = false;
            if (tv != null) tv.Delete();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                Script.Wait(0);
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
            }
            isLeftSafeOpened = false;
            isRightSafeOpened = false;
            isRadioOn = false;
            if (radioEmitter != null) Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitter, false);
        }

        public static void Dispose() {
            if (tv != null) tv.Delete();
            if (remote != null) remote.Delete();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                Script.Wait(0);
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
            }
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                Script.Wait(0);
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_computer_screen");
            }
            if (radioEmitter != null) Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitter, false);
        }
    }
}
