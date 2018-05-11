﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class InteractionsController {

        public string SofaInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit on the couch"; } }
        public int SofaInteractionStatus { get; private set; }
        private Vector3 sofaInitialPos;
        private Vector3 sofaInitialRot;
        private Vector3 sofaPos;
        private Vector3 sofaRot;
        private List<string> sofaIdleAnims;
        public string TVInteractionHelpText {
            get {
                if (!isTVOn) return "Press ~INPUT_CONTEXT~ to turn on the TV";
                return "Press ~INPUT_CONTEXT~ to turn off the TV";
            }
        }
        public int TVInteractionStatus { get; private set; } 
        private Prop tv;
        private Prop remote;
        private int tvRenderTargetID;
        private bool isTVOn;
        public string ComputerInterationHelpText { get { return "Press ~INPUT_CONTEXT~ to sit down"; } }
        public string ComputerInteractionRejectHelpText { get { return "You do not have access to this computer"; } }
        public int ComputerInteractionStatus { get; private set; }
        //private Vector3 computerChairPos;
        //private Vector3 computerChairRot;
        private Vector3 computerInitialPos;
        private Vector3 computerInitialRot;
        private Prop computerChair;
        //private List<Model> officeChairModels;
        private Prop monitor;
        private int computerRenderTargetID;
        private List<string> computerIdleAnims;
        private List<string> computerChairIdleAnims;
        public string LeftSafeInteractionHelpText {
            get {
                if (!isLeftSafeOpened) return "Press ~INPUT_CONTEXT~ to open the safe";
                return "Press ~INPUT_CONTEXT~ to close the safe";
            }
        }
        public string SafeInteractionRejectHelpText { get { return "Only the owner can open the safe"; } }
        public int LeftSafeInteractionStatus { get; private set; }
        private Vector3 leftSafeInitialPos;
        private Vector3 leftSafeInitialRot;
        //private List<Model> leftSafeDoorModels;
        private Prop leftSafeDoor;
        private Vector3 leftSafeDoorPos;
        private Vector3 leftSafeDoorRot;
        private bool isLeftSafeOpened;
        public string RightSafeInteractionHelpText {
            get {
                if (!isRightSafeOpened) return "Press ~INPUT_CONTEXT~ to open the safe";
                return "Press ~INPUT_CONTEXT~ to close the safe";
            }
        }
        public int RightSafeInteractionStatus { get; private set; }
        private Vector3 rightSafeInitialPos;
        private Vector3 rightSafeInitialRot;
        //private List<Model> rightSafeDoorModels;
        private Prop rightSafeDoor;
        private Vector3 rightSafeDoorPos;
        private Vector3 rightSafeDoorRot;
        private bool isRightSafeOpened;
        public string RadioInteractionHelpText {
            get {
                if (!isRadioOn) return "Press ~INPUT_CONTEXT~ to turn on the radio";
                return "Press ~INPUT_CONTEXT~ to turn off the radio";
            }
        }
        public int RadioInteractionStatus { get; private set; }
        private Vector3 radioStartPos;
        private float radioStartHeading;
        private OfficeInteriorStyle officeInteriorStyle;
        private List<Model> radioModels;
        private Prop radio;
        private List<RadioStation> radioStations;
        private RadioStation radioStation;
        private Dictionary<string, string> radioEmitters;
        private bool isRadioOn;
        public string BossChairInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit down"; } }
        public string BossChairInteractionRejectHelpText { get { return "Only boss can sit here"; } }
        public int BossChairInteractionStatus { get; private set; }
        private Vector3 bossChairInitialPos;
        private Vector3 bossChairInitialRot;
        private Prop bossChair;
        private List<string> bossIdleAnims;
        private List<string> bossChairIdleAnims;
        public string StaffChairInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit down"; } }
        public int StaffChairInteractionStatus { get; private set; }
        private Vector3 staffChairInitialPos;
        private Vector3 staffChairInitialRot;
        private Prop staffChair;
        private List<string> staffIdleAnims;
        private List<string> staffChairIdleAnims;
        public string LaptopChairInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to sit down"; } }
        public int LaptopChairInteractionStatus { get; private set; }
        private Vector3 laptopChairInitialPos;
        private Vector3 laptopChairInitialRot;
        private Prop laptopChair;
        private List<string> laptopIdleAnims;
        private List<string> laptopChairIdleAnims;
        public string WardrobeInteractionHelpText { get { return "Press ~INPUT_CONTEXT~ to change outfit"; } }
        public string WardrobeInteractionRejectHelpText { get { return "You cannot change outfit here"; } }
        public int WardrobeInteractionStatus { get; private set; }
        private Vector3 wardrobeStartPos;
        private float wardrobeStartHeading;
        private Vector3 wardrobeCamPos;
        private Vector3 wardrobeCamRot;
        private float wardrobeCamFOV;
        private Camera wardrobeCam;
        private MenuPool menuPool;
        private UIMenu radioMenu;
        private UIMenu wardrobeMenu;
        private int syncScene;

        public InteractionsController() {
            SofaInteractionStatus = -1;
            sofaIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c" };
            TVInteractionStatus = -1;
            isTVOn = false;
            ComputerInteractionStatus = -1;
            //officeChairModels = new List<Model>() { -1626066319, -1278649385, 1580642483, 1339364336 };
            computerIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c", "idle_d", "idle_e" };
            computerChairIdleAnims = new List<string> { "idle_a_chair", "idle_b_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair" };
            LeftSafeInteractionStatus = -1;
            //leftSafeDoorModels = new List<Model>() { 646926492, 845785021, -1126494299, -524920966, -1842578680, -1387653807 };
            isLeftSafeOpened = false;
            RightSafeInteractionStatus = -1;
            //rightSafeDoorModels = new List<Model>() { -1176373441, -1149617688, -548219756, 1854960432, 682108925, 1002451519 };
            isRightSafeOpened = false;
            RadioInteractionStatus = -1;
            radioModels = new List<Model>() { -364924791, 2079380440 };
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
            radioEmitters = new Dictionary<string, string>() {
                { "Old Spice Warms", "SE_ex_int_office_01a_Radio_01" },
                { "Old Spice Classical", "SE_ex_int_office_01b_Radio_01" },
                { "Old Spice Vintage", "SE_ex_int_office_01c_Radio_01" },
                { "Executive Contrast", "SE_ex_int_office_02a_Radio_01" },
                { "Executive Rich", "SE_ex_int_office_02b_Radio_01" },
                { "Executive Cool", "SE_ex_int_office_02c_Radio_01" },
                { "Power Broker Ice", "SE_ex_int_office_03a_Radio_01" },
                { "Power Broker Conservative", "SE_ex_int_office_03b_Radio_01" },
                { "Power Broker Polished", "SE_ex_int_office_03c_Radio_01" }
            };
            isRadioOn = false;
            BossChairInteractionStatus = -1;
            bossIdleAnims = new List<string>() { "idle_a", "idle_c", "idle_d", "idle_e" };
            bossChairIdleAnims = new List<string>() { "idle_a_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair" };
            StaffChairInteractionStatus = -1;
            staffIdleAnims = new List<string>() { "idle_a", "idle_d", "idle_e" };
            staffChairIdleAnims = new List<string>() { "idle_a_chair", "idle_d_chair", "idle_e_chair" };
            LaptopChairInteractionStatus = -1;
            laptopIdleAnims = new List<string>() { "idle_a", "idle_b", "idle_c" };
            laptopChairIdleAnims = new List<string>() { "idle_a_chair", "idle_b_chair", "idle_c_chair" };
            WardrobeInteractionStatus = -1;
            menuPool = new MenuPool();
            CreateRadioMenu();
            CreateWardrobeMenu();
        }

        public void StartSofaInteraction(Vector3 sofaPos, Vector3 sofaRot) {
            this.sofaPos = sofaPos;
            this.sofaRot = sofaRot;
            sofaInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@seating@male@var_d@base@", "enter", this.sofaPos.X, this.sofaPos.Y, this.sofaPos.Z, this.sofaRot.X, this.sofaRot.Y, this.sofaRot.Z, 0, 2);
            sofaInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@seating@male@var_d@base@", "enter", this.sofaPos.X, this.sofaPos.Y, this.sofaPos.Z, this.sofaRot.X, this.sofaRot.Y, this.sofaRot.Z, 0, 2);
            SofaInteractionStatus = 0;
        }

        private void SofaOnTick() {
            switch (SofaInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, sofaInitialPos.X, sofaInitialPos.Y, sofaInitialPos.Z, 1f, 1000, sofaInitialRot.Z, 0f);
                    SofaInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@seating@male@var_d@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        SofaInteractionStatus = 2;
                    }
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@seating@male@var_d@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                        SofaInteractionStatus = 3;
                    }
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame(TVInteractionHelpText + "~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        SofaInteractionStatus = 4;
                        break;
                    }
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        SofaInteractionStatus = 7;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@seating@male@var_d@base@", sofaIdleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3)], 4f, -1.5f, 13, 16, 1148846080, 0);
                        SofaInteractionStatus = 2;
                    }
                    break;
                case 4:
                    if (tv == null) {
                        Model tvModel = new Model("prop_tv_flat_01");
                        tvModel.Request(250);
                        if (tvModel.IsInCdImage && tvModel.IsValid) {
                            while (!tvModel.IsLoaded) Script.Wait(50);
                            tv = World.CreateProp(tvModel, Vector3.Zero, false, false);
                        }
                        tvModel.MarkAsNoLongerNeeded();
                    }
                    Prop nearbyTV = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 5f, 608950395, 0, 0, 0);
                    if (nearbyTV.Model == 608950395) {
                        tv.Position = nearbyTV.Position;
                        tv.Rotation = nearbyTV.Rotation;
                        nearbyTV.Delete();
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
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@game@seated@male@var_c@base@", "enter_d", 4f, -4f, 13, 16, 1000f, 0);
                    SofaInteractionStatus = 5;
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        TVInteractionStatus = 0;
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@game@seated@male@var_c@base@", "exit_d", 4f, -4f, 13, 16, 1000f, 0);
                        SofaInteractionStatus = 6;
                    }
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        remote.Delete();
                        SofaInteractionStatus = 2;
                    }
                    break;
                case 7:
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofaPos.X, sofaPos.Y, sofaPos.Z, sofaRot.X, sofaRot.Y, sofaRot.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@seating@male@var_d@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    SofaInteractionStatus = 8;
                    break;
                case 8:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        Game.Player.Character.Task.ClearAll();
                        SofaInteractionStatus = -1;
                    }
                    break;
            }
        }

        public void StartTVInteraction() {
            if (tv == null) {
                Model tvModel = new Model("prop_tv_flat_01");
                tvModel.Request(250);
                if (tvModel.IsInCdImage && tvModel.IsValid) {
                    while (!tvModel.IsLoaded) Script.Wait(50);
                    tv = World.CreateProp(tvModel, Vector3.Zero, false, false);
                }
                tvModel.MarkAsNoLongerNeeded();
            }
            Prop nearbyTV = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 3f, 608950395, 0, 0, 0);
            if (nearbyTV.Model == 608950395) {
                tv.Position = nearbyTV.Position;
                tv.Rotation = nearbyTV.Rotation;
                nearbyTV.Delete();
            }
            TVInteractionStatus = 0;
        }

        private void TVOnTick() {
            switch (TVInteractionStatus) {
                case 0:
                    if (!isTVOn) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                            Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "tvscreen", 0);
                        }
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, tv.Model)) {
                            Function.Call(Hash.LINK_NAMED_RENDERTARGET, tv.Model);
                            tvRenderTargetID = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "tvscreen");
                        }
                        Function.Call(Hash.ATTACH_TV_AUDIO_TO_ENTITY, tv);
                        Function.Call(Hash.SET_TV_CHANNEL, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2));
                        Function.Call(Hash.SET_TV_VOLUME, 0);
                    }
                    else {
                        Script.Wait(0);
                        if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                            Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
                        }
                    }
                    isTVOn = !isTVOn;
                    TVInteractionStatus = -1;
                    break;
            }
            if (isTVOn) {
                Function.Call(Hash.SET_TEXT_RENDER_ID, tvRenderTargetID);
                Function.Call(Hash._0x61BB1D9B3A95D802, 4);
                Function.Call(Hash._0xC6372ECD45D73BCD, 1);
                Function.Call(Hash.DRAW_TV_CHANNEL, 0.5, 0.5, 1.0, 1.0, 0.0, 255, 255, 255, 255);
                Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                Function.Call(Hash.ENABLE_MOVIE_SUBTITLES, true);
            }
        }

        public void StartComputerInteraction(Prop computerChair) {
            this.computerChair = computerChair;
            computerInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@male@", "enter", computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, computerChair.Rotation.X, computerChair.Rotation.Y, computerChair.Rotation.Z, 0, 2);
            computerInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@male@", "enter", computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, computerChair.Rotation.X, computerChair.Rotation.Y, computerChair.Rotation.Z, 0, 2);
            ComputerInteractionStatus = 0;
        }

        private void ComputerOnTick() {
            switch (ComputerInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, computerInitialPos.X, computerInitialPos.Y, computerInitialPos.Z, 1f, 0, computerInitialRot.Z, 0f);
                    ComputerInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "enter_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                        ComputerInteractionStatus = 2;
                    }
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "base_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 3;
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to use the computer~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        ComputerInteractionStatus = 4;
                        break;
                    }
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        ComputerInteractionStatus = 8;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 5);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", computerIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, computerChairIdleAnims[rnd], "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 2;
                    break;
                case 4:
                    monitor = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 1f, 743064848, 0, 0, 0);
                    if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_computer_screen", 0);
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, 743064848)) {
                            Function.Call(Hash.LINK_NAMED_RENDERTARGET, 743064848);
                            computerRenderTargetID = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "prop_ex_computer_screen");
                        }
                    }
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "computer_enter", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "computer_enter_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 5;
                    break;
                case 5:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetID);
                    Function.Call((Hash)13305974099546635958, 73, 73);
                    Function.Call((Hash)3154009034243605640, "MPDesktop", "DesktopUI_Login", 0.5f, 0.5f, 1f, 1f, 0f, 255, 255, 255, 255);
                    Function.Call((Hash)16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncScene, 1);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "computer_idle", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "computer_idle_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 6;
                    break;
                case 6:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetID);
                    Function.Call((Hash)13305974099546635958, 73, 73);
                    Function.Call((Hash)3154009034243605640, "MPDesktop", "DesktopUI_Map", 0.5f, 0.5f, 1f, 1f, 0f, 255, 255, 255, 255);
                    Function.Call((Hash)16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_AIM~ to stop using the computer");
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        ComputerInteractionStatus = 7;
                        break;
                    }
                    break;
                case 7:
                    if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Script.Wait(0);
                        Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_computer_screen");
                    }
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "computer_exit", 1000f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "computer_exit_chair", "anim@amb@office@boss@male@", 1000f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 2;
                    break;
                case 8:
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, computerChair.Position.X, computerChair.Position.Y, computerChair.Position.Z, 0f, 0f, computerChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@male@", "exit", 4f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, computerChair, syncScene, "exit_chair", "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    ComputerInteractionStatus = 9;
                    break;
                case 9:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    Game.Player.Character.Task.ClearAll();
                    ComputerInteractionStatus = -1;
                    break;
            }
        }

        public void StartLeftSafeInteraction(Prop leftSafeDoor) {
            this.leftSafeDoor = leftSafeDoor;
            if (!isLeftSafeOpened) {
                leftSafeInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@vault@left@male@", "open", leftSafeDoor.Position.X, leftSafeDoor.Position.Y, leftSafeDoor.Position.Z, leftSafeDoor.Rotation.X, leftSafeDoor.Rotation.Y, leftSafeDoor.Rotation.Z, 0, 2);
                leftSafeInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@vault@left@male@", "open", leftSafeDoor.Position.X, leftSafeDoor.Position.Y, leftSafeDoor.Position.Z, leftSafeDoor.Rotation.X, leftSafeDoor.Rotation.Y, leftSafeDoor.Rotation.Z, 0, 2);
            }
            LeftSafeInteractionStatus = 0;
        }

        private void LeftSafeOnTick() {
            switch (LeftSafeInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, leftSafeInitialPos.X, leftSafeInitialPos.Y, leftSafeInitialPos.Z, 1f, -1, leftSafeInitialRot.Z, 0f);
                    LeftSafeInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        if (!isLeftSafeOpened) {
                            leftSafeDoorPos = leftSafeDoor.Position;
                            leftSafeDoorRot = leftSafeDoor.Rotation;
                        }
                        LeftSafeInteractionStatus = 2;
                    }
                    break;
                case 2:
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, leftSafeDoorPos.X, leftSafeDoorPos.Y, leftSafeDoorPos.Z, 0f, 0f, leftSafeDoorRot.Z, 2);
                    if (!isLeftSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@vault@left@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, leftSafeDoor, syncScene, "open_door", "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        isLeftSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@vault@left@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, leftSafeDoor, syncScene, "close_door", "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        isLeftSafeOpened = false;
                    }
                    LeftSafeInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        Game.Player.Character.Task.ClearAll();
                        LeftSafeInteractionStatus = -1;
                    }
                    break;
            }
        }

        public void StartRightSafeInteraction(Prop rightSafeDoor) {
            this.rightSafeDoor = rightSafeDoor;
            if (!isRightSafeOpened) {
                rightSafeInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boss@vault@right@male@", "open", rightSafeDoor.Position.X, rightSafeDoor.Position.Y, rightSafeDoor.Position.Z, rightSafeDoor.Rotation.X, rightSafeDoor.Rotation.Y, rightSafeDoor.Rotation.Z, 0, 2);
                rightSafeInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boss@vault@right@male@", "open", rightSafeDoor.Position.X, rightSafeDoor.Position.Y, rightSafeDoor.Position.Z, rightSafeDoor.Rotation.X, rightSafeDoor.Rotation.Y, rightSafeDoor.Rotation.Z, 0, 2);
            }
            RightSafeInteractionStatus = 0;
        }

        private void RightSafeOnTick() {
            switch (RightSafeInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, rightSafeInitialPos.X, rightSafeInitialPos.Y, rightSafeInitialPos.Z, 1f, -1, rightSafeInitialRot.Z, 0f);
                    RightSafeInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        if (!isRightSafeOpened) {
                            rightSafeDoorPos = rightSafeDoor.Position;
                            rightSafeDoorRot = rightSafeDoor.Rotation;
                        }
                        RightSafeInteractionStatus = 2;
                    }
                    break;
                case 2:
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, rightSafeDoorPos.X, rightSafeDoorPos.Y, rightSafeDoorPos.Z, 0f, 0f, rightSafeDoorRot.Z, 2);
                    if (!isRightSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@vault@right@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, rightSafeDoor, syncScene, "open_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        isRightSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boss@vault@right@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, rightSafeDoor, syncScene, "close_door", "anim@amb@office@boss@vault@right@male@", 4f, -4f, 32781, 1000f);
                        isRightSafeOpened = false;
                    }
                    RightSafeInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        Game.Player.Character.Task.ClearAll();
                        RightSafeInteractionStatus = -1;
                    }
                    break;
            }
        }

        public void StartRadioInteraction(Vector3 start, float heading, OfficeInteriorStyle style) {
            radioStartPos = start;
            radioStartHeading = heading;
            officeInteriorStyle = style;
            RadioInteractionStatus = 0;
        }

        private void CreateRadioMenu() {
            radioMenu = new UIMenu("Radio Stations", "");
            foreach (RadioStation station in radioStations) {
                UIMenuItem radioStationBtn = new UIMenuItem(station.Name, station.Description);
                radioMenu.AddItem(radioStationBtn);
            }
            radioMenu.RefreshIndex();
            radioMenu.OnItemSelect += (sender, item, index) => {
                menuPool.CloseAllMenus();
                radioStation = radioStations[index];
                RadioInteractionStatus = 2;
            };
            radioMenu.OnMenuClose += (sender) => {
                Game.Player.Character.Task.ClearAll();
            };
            menuPool.Add(radioMenu);
        }

        private void RadioOnTick() {
            switch (RadioInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, radioStartPos.X, radioStartPos.Y, radioStartPos.Z, 1f, -1, radioStartHeading, 0f);
                    RadioInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        Game.Player.Character.Task.StandStill(-1);
                        foreach (Model model in radioModels) {
                            radio = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 3f, model, 0, 0, 0);
                            if (radioModels.Contains(radio.Model)) break;
                        }
                        if (!isRadioOn && !menuPool.IsAnyMenuOpen()) {
                            radioMenu.Visible = true;
                            RadioInteractionStatus = -1;
                        }
                        if (isRadioOn) {
                            RadioInteractionStatus = 2;
                        }
                    }
                    break;
                case 2:
                    Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@mp_radio@high_life_apment", "enter_bedroom", 4f, -4f, -1, 0, 0, 0, 0, 0);
                    RadioInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_life_apment", "enter_bedroom") > 0.9f) {
                        if (!isRadioOn) {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitters[officeInteriorStyle.Name], true);
                            Function.Call(Hash._0x0E0CD610D5EB6C85, radioEmitters[officeInteriorStyle.Name], radio);
                            Function.Call(Hash.SET_EMITTER_RADIO_STATION, radioEmitters[officeInteriorStyle.Name], radioStation.GameName);
                            isRadioOn = true;
                        }
                        else {
                            Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitters[officeInteriorStyle.Name], false);
                            isRadioOn = false;
                        }
                        Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@mp_radio@high_life_apment", "exit_bedroom", 4f, -4f, -1, 0, 0, 0, 0, 0);
                        RadioInteractionStatus = 4;
                    }
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "anim@mp_radio@high_life_apment", "exit_bedroom") > 0.9f) {
                        int rand = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2);
                        if (isRadioOn && rand == 1) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "RADIO_LIKE", "SPEECH_PARAMS_FORCE");
                        Game.Player.Character.Task.ClearAll();
                        RadioInteractionStatus = -1;
                    }
                    break;
            }
        }

        public void StartBossChairInteraction(Prop bossChair) {
            this.bossChair = bossChair;
            bossChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@boss@male@", "enter_b", bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, bossChair.Rotation.X, bossChair.Rotation.Y, bossChair.Rotation.Z, 0, 2);
            bossChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@boss@male@", "enter_b", bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, bossChair.Rotation.X, bossChair.Rotation.Y, bossChair.Rotation.Z, 0, 2);
            BossChairInteractionStatus = 0;
        }

        private void TalkShit() {
            switch (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character)) {
                case 0:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "CULT_TALK", "SPEECH_PARAMS_FORCE"); break;
                case 1:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE"); break;
                case 3:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_INSULT_OLD", "SPEECH_PARAMS_FORCE"); break;
            }
        }

        private void BossChairOnTick() {
            switch (BossChairInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, bossChairInitialPos.X, bossChairInitialPos.Y, bossChairInitialPos.Z, 1f, -1, bossChairInitialRot.Z, 0f);
                    BossChairInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncScene, "enter_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                        BossChairInteractionStatus = 2;
                    }
                    break;
                case 2:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        TalkShit();
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncScene, "base_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossChairInteractionStatus = 3;
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        TalkShit();
                    }
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        BossChairInteractionStatus = 4;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 4);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@boss@male@", bossIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncScene, bossChairIdleAnims[rnd], "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossChairInteractionStatus = 2;
                    break;
                case 4:
                    Ped[] nearbyPeds = World.GetNearbyPeds(Game.Player.Character, 5f);
                    if (nearbyPeds.Length != 0) {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GAME_QUIT", "SPEECH_PARAMS_FORCE");
                    } 
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@boss@male@", "exit_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, syncScene, "exit_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 13, 1000f);
                    BossChairInteractionStatus = 5;
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    Game.Player.Character.Task.ClearAll();
                    BossChairInteractionStatus = -1;
                    break;
            }
        }

        public void StartStaffChairInteraction(Prop staffChair) {
            this.staffChair = staffChair;
            staffChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, staffChair.Rotation.X, staffChair.Rotation.Y, staffChair.Rotation.Z, 0, 2);
            staffChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, staffChair.Rotation.X, staffChair.Rotation.Y, staffChair.Rotation.Z, 0, 2);
            StaffChairInteractionStatus = 0;
        }

        private void StaffChairOnTick() {
            switch (StaffChairInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, staffChairInitialPos.X, staffChairInitialPos.Y, staffChairInitialPos.Z, 1f, -1, staffChairInitialRot.Z, 0f);
                    StaffChairInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_c@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncScene, "enter_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                        StaffChairInteractionStatus = 2;
                    }
                    break;
                case 2:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        TalkShit();
                        ScenesController.BossConversationStatus = 1;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) == 1f) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_c@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncScene, "base_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                        StaffChairInteractionStatus = 3;
                    }
                    break;
                case 3:
                    SinglePlayerOffice.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlPressed(2, GTA.Control.Context)) {
                        TalkShit();
                        ScenesController.BossConversationStatus = 1;
                    }
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        StaffChairInteractionStatus = 4;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_c@base@", staffIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncScene, staffChairIdleAnims[rnd], "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 32781, 1000f);
                    StaffChairInteractionStatus = 2;
                    break;
                case 4:
                    Ped[] nearbyPeds = World.GetNearbyPeds(Game.Player.Character, 5f);
                    if (nearbyPeds.Length != 0) {
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GAME_QUIT", "SPEECH_PARAMS_FORCE");
                    } 
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, staffChair.Position.X, staffChair.Position.Y, staffChair.Position.Z, 0f, 0f, staffChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_c@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, staffChair, syncScene, "exit_chair", "anim@amb@office@boardroom@crew@male@var_c@base@", 4f, -4f, 13, 1000f);
                    StaffChairInteractionStatus = 5;
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    Game.Player.Character.Task.ClearAll();
                    StaffChairInteractionStatus = -1;
                    break;
            }
        }

        public void StartLaptopChairInteraction(Prop laptopChair) {
            this.laptopChair = laptopChair;
            laptopChairInitialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, laptopChair.Rotation.X, laptopChair.Rotation.Y, laptopChair.Rotation.Z, 0, 2);
            laptopChairInitialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, laptopChair.Rotation.X, laptopChair.Rotation.Y, laptopChair.Rotation.Z, 0, 2);
            LaptopChairInteractionStatus = 0;
        }

        private void LaptopChairOnTick() {
            switch (LaptopChairInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, laptopChairInitialPos.X, laptopChairInitialPos.Y, laptopChairInitialPos.Z, 1f, -1, laptopChairInitialRot.Z, 0f);
                    LaptopChairInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, "enter_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                        LaptopChairInteractionStatus = 2;
                    }
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@laptops@male@var_b@base@", "enter", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, "enter_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@laptops@male@var_b@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, "base_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 4;
                    break;
                case 4:
                    if (Game.IsControlPressed(2, GTA.Control.Aim)) {
                        LaptopChairInteractionStatus = 5;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@laptops@male@var_b@base@", laptopIdleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, laptopChairIdleAnims[rnd], "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    LaptopChairInteractionStatus = 3;
                    break;
                case 5:
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@laptops@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, "exit_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 13, 1000f);
                    LaptopChairInteractionStatus = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    syncScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, laptopChair.Position.X, laptopChair.Position.Y, laptopChair.Position.Z, 0f, 0f, laptopChair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncScene, "anim@amb@office@boardroom@crew@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, laptopChair, syncScene, "exit_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 13, 1000f);
                    LaptopChairInteractionStatus = 7;
                    break;
                case 7:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncScene) != 1f) break;
                    Game.Player.Character.Task.ClearAll();
                    LaptopChairInteractionStatus = -1;
                    break;
            }
        }

        public void StartWardrobeInteraction(Vector3 start, float heading, Vector3 camPos, Vector3 camRot, float camFOV) {
            wardrobeStartPos = start;
            wardrobeStartHeading = heading;
            wardrobeCamPos = camPos;
            wardrobeCamRot = camRot;
            wardrobeCamFOV = camFOV;
            WardrobeInteractionStatus = 0;
        }

        private void CreateWardrobeMenu() {
            wardrobeMenu = new UIMenu("Wardrobe", "~b~Outfit Options") {
                MouseEdgeEnabled = false
            };

            UIMenu torsoMenu = menuPool.AddSubMenu(wardrobeMenu, "Torso");
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

            UIMenu torsoExtraMenu = menuPool.AddSubMenu(wardrobeMenu, "Torso Extra");
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

            UIMenu legsMenu = menuPool.AddSubMenu(wardrobeMenu, "Legs");
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

            UIMenu handsMenu = menuPool.AddSubMenu(wardrobeMenu, "Hands");
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

            UIMenu feetMenu = menuPool.AddSubMenu(wardrobeMenu, "Feet");
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

            UIMenu accessoriesMenu = menuPool.AddSubMenu(wardrobeMenu, "Accessories");
            accessoriesMenu.MouseEdgeEnabled = false;

            UIMenu hatsMenu = menuPool.AddSubMenu(accessoriesMenu, "Hats");
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

            UIMenu glassesMenu = menuPool.AddSubMenu(accessoriesMenu, "Glasses");
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

            UIMenu earsMenu = menuPool.AddSubMenu(accessoriesMenu, "Ears");
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

            UIMenu misc1Menu = menuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 1");
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

            UIMenu misc2Menu = menuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 2");
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

            UIMenu misc3Menu = menuPool.AddSubMenu(accessoriesMenu, "Miscellaneous 3");
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
                Game.Player.Character.Task.ClearAll();
                WardrobeInteractionStatus = -1;
            };
            menuPool.Add(wardrobeMenu);
        }

        private void WardrobeOnTick() {
            switch (WardrobeInteractionStatus) {
                case 0:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, wardrobeStartPos.X, wardrobeStartPos.Y, wardrobeStartPos.Z, 1f, -1, wardrobeStartHeading, 0f);
                    WardrobeInteractionStatus = 1;
                    break;
                case 1:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) != 1) {
                        Game.Player.Character.Task.StandStill(-1);
                        Game.Player.Character.ClearBloodDamage();
                        wardrobeMenu.Visible = true;
                        wardrobeCam = World.CreateCamera(wardrobeCamPos, wardrobeCamRot, wardrobeCamFOV);
                        World.RenderingCamera = wardrobeCam;
                        WardrobeInteractionStatus = 2;
                    }
                    break;
            }
        }

        public void ResetInterations() {
            isTVOn = false;
            isLeftSafeOpened = false;
            isRightSafeOpened = false;
            isRadioOn = false;
            if (officeInteriorStyle != null) Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitters[officeInteriorStyle.Name], false);
        }

        public void OnTick() {
            menuPool.ProcessMenus();
            SofaOnTick();
            TVOnTick();
            ComputerOnTick();
            LeftSafeOnTick();
            RightSafeOnTick();
            RadioOnTick();
            BossChairOnTick();
            StaffChairOnTick();
            LaptopChairOnTick();
            WardrobeOnTick();
        }

        public void Dispose() {
            if (tv != null) tv.Delete();
            if (remote != null) remote.Delete();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
            if (officeInteriorStyle != null) Function.Call(Hash.SET_STATIC_EMITTER_ENABLED, radioEmitters[officeInteriorStyle.Name], false);
        }

    }
}