using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class ScenesController : Script {

        private static int officeInteriorID;
        private static Owner owner;
        private static Vector3 bossChairPos;
        private static int bossSceneStatus;
        private static Ped boss;
        private static Prop bossChair;
        private static int bossScene;
        public static int BossConversationStatus { get; set; }
        private static bool isBossGreeted;
        private static List<Vector3> staffChairsPos;
        private static List<int> seatedStaffChairs;
        private static int maleStaffSceneStatus;
        private static Ped maleStaff;
        private static Prop maleStaffChair;
        private static int maleStaffScene;
        public static int MaleStaffConversationStatus { get; set; }
        private static bool isMaleStaffGreeted;
        private static int femaleStaffSceneStatus;
        private static Ped femaleStaff;
        private static Prop femaleStaffChair;
        private static int femaleStaffScene;
        public static int FemaleStaffConversationStatus { get; set; }
        private static bool isFemaleStaffGreeted;
        private static Vector3 paChairPos;
        private static Vector3 paChairRot;
        private static int paSceneStatus;
        private static Ped pa;
        private static Prop paChair;
        private static int paScene;
        public static int PaConversationStatus { get; set; }
        private static bool isPaGreeted;

        public ScenesController() {
            Tick += OnTick;
            Aborted += OnAborted;
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@boss@male@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@boss@male@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@crew@male@var_b@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@male@var_b@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@crew@female@var_c@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@female@var_c@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@pa@female@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@pa@female@");
        }

        static ScenesController() {
            bossSceneStatus = -1;
            BossConversationStatus = -1;
            isBossGreeted = false;
            seatedStaffChairs = new List<int>();
            maleStaffSceneStatus = -1;
            MaleStaffConversationStatus = -1;
            isMaleStaffGreeted = false;
            femaleStaffSceneStatus = -1;
            FemaleStaffConversationStatus = -1;
            isFemaleStaffGreeted = false;
            paSceneStatus = -1;
            PaConversationStatus = -1;
            isPaGreeted = false;
        }

        public static void StartOfficeScenes(int officeInteriorID, Owner owner, Vector3 bossChairPos, List<Vector3> staffChairsPos, Vector3 paChairPos, Vector3 paChairRot) {
            ScenesController.officeInteriorID = officeInteriorID;
            ScenesController.owner = owner;
            ScenesController.bossChairPos = bossChairPos;
            ScenesController.staffChairsPos = staffChairsPos;
            ScenesController.paChairPos = paChairPos;
            ScenesController.paChairRot = paChairRot;
            int hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);
            if (hours > 8 && hours < 17) {
                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)owner) bossSceneStatus = 0;
                maleStaffSceneStatus = 0;
                femaleStaffSceneStatus = 0;
            }
            paSceneStatus = 0;
        }

        private static void BossSceneOnTick() {
            switch (bossSceneStatus) {
                case 0:
                    switch (owner) {
                        case Owner.Michael:
                            boss = World.CreatePed(PedHash.Michael, bossChairPos);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 4, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 4, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 2, 4, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 0, 7, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 0, 7, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 0, 1, 2);
                            break;
                        case Owner.Franklin:
                            boss = World.CreatePed(PedHash.Franklin, bossChairPos);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 3, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 4, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 2, 0, 1, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 22, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 21, 1, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 17, 9, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 8, 14, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 11, 7, 0, 2);
                            break;
                        case Owner.Trevor:
                            boss = World.CreatePed(PedHash.Trevor, bossChairPos);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 1, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 5, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 27, 1, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 20, 1, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 19, 12, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 8, 14, 0, 2);
                            break;
                    }
                    bossSceneStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, officeInteriorID) || !Function.Call<bool>(Hash.IS_PED_STILL, boss)) break;
                        bossChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, boss.Position.X, boss.Position.Y, boss.Position.Z, 1f, -1278649385, 0, 0, 0);
                        bossSceneStatus = 2;
                    break;
                case 2:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, bossChair, "anim@amb@office@boardroom@boss@male@", "enter_b_chair", 2)) {
                        bossScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, boss, bossScene, "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, bossScene, "enter_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    }
                    else bossSceneStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, bossScene) != 1f) break;
                    bossScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, bossScene, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, boss, bossScene, "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, bossScene, "base_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    bossSceneStatus = -1;
                    break;
            }
            switch (BossConversationStatus) {
                case 0:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "HOWS_IT_GOING_MALE", "SPEECH_PARAMS_FORCE");
                    BossConversationStatus = -1;
                    isBossGreeted = true;
                    break;
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    switch (rnd) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_NO", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_WHATEVER", "SPEECH_PARAMS_FORCE"); break;
                        case 2: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE"); break;
                    }
                    BossConversationStatus = -1;
                    break;
            }
            if (boss != null && Game.Player.Character.Position.DistanceTo(boss.Position) < 5f && BossConversationStatus == -1 && !isBossGreeted) {
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_MALE", "SPEECH_PARAMS_FORCE");
                BossConversationStatus = 0;
            }
        }

        private static int GetRandomStaffChairIndex() {
            int index;
            do {
                index = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, staffChairsPos.Count);
            } while (seatedStaffChairs.Contains(index));
            seatedStaffChairs.Add(index);
            return index;
        }

        private static void MaleStaffSceneOnTick() {
            switch (maleStaffSceneStatus) {
                case 0:
                    maleStaff = World.CreatePed(PedHash.Business01AMM, staffChairsPos[GetRandomStaffChairIndex()]);
                    maleStaff.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
                    Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
                    maleStaffSceneStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, officeInteriorID) || !Function.Call<bool>(Hash.IS_PED_STILL, maleStaff)) break;
                    maleStaffChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, maleStaff.Position.X, maleStaff.Position.Y, maleStaff.Position.Z, 1f, -1278649385, 0, 0, 0);
                    maleStaffSceneStatus = 2;
                    break;
                case 2:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, maleStaffChair, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter_chair", 2)) {
                        maleStaffScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, maleStaffChair.Position.X, maleStaffChair.Position.Y, maleStaffChair.Position.Z, 0f, 0f, maleStaffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, maleStaff, maleStaffScene, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, maleStaffChair, maleStaffScene, "enter_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    }
                    else maleStaffSceneStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, maleStaffScene) != 1f) break;
                    maleStaffScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, maleStaffChair.Position.X, maleStaffChair.Position.Y, maleStaffChair.Position.Z, 0f, 0f, maleStaffChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, maleStaffScene, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, maleStaff, maleStaffScene, "anim@amb@office@boardroom@crew@male@var_b@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, maleStaffChair, maleStaffScene, "base_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    maleStaffSceneStatus = -1;
                    break;
            }
            switch (MaleStaffConversationStatus) {
                case 0:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, maleStaff, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = -1;
                    isMaleStaffGreeted = true;
                    break;
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, maleStaff)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = -1;
                    break;
            }
            if (maleStaff != null && Game.Player.Character.Position.DistanceTo(maleStaff.Position) < 1f && MaleStaffConversationStatus == -1) {
                if (!isMaleStaffGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 0;
                }
                else if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, maleStaff)) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, maleStaff, "PED_RANT_01", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 1;
                }
            }
        }

        private static void FemaleStaffSceneOnTick() {
            switch (femaleStaffSceneStatus) {
                case 0:
                    femaleStaff = World.CreatePed(PedHash.Business04AFY, staffChairsPos[GetRandomStaffChairIndex()]);
                    femaleStaff.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
                    Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
                    femaleStaffSceneStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, officeInteriorID) || !Function.Call<bool>(Hash.IS_PED_STILL, femaleStaff)) break;
                    femaleStaffChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, femaleStaff.Position.X, femaleStaff.Position.Y, femaleStaff.Position.Z, 1f, -1278649385, 0, 0, 0);
                    femaleStaffSceneStatus = 2;
                    break;
                case 2:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, femaleStaffChair, "anim@amb@office@boardroom@crew@female@var_c@base@", "enter_chair", 2)) {
                        femaleStaffScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, femaleStaffChair.Position.X, femaleStaffChair.Position.Y, femaleStaffChair.Position.Z, 0f, 0f, femaleStaffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, femaleStaff, femaleStaffScene, "anim@amb@office@boardroom@crew@female@var_c@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, femaleStaffChair, femaleStaffScene, "enter_chair", "anim@amb@office@boardroom@crew@female@var_c@base@", 4f, -4f, 32781, 1000f);
                    }
                    else femaleStaffSceneStatus = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, femaleStaffScene) != 1f) break;
                    femaleStaffScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, femaleStaffChair.Position.X, femaleStaffChair.Position.Y, femaleStaffChair.Position.Z, 0f, 0f, femaleStaffChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, femaleStaffScene, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, femaleStaff, femaleStaffScene, "anim@amb@office@boardroom@crew@female@var_c@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, femaleStaffChair, femaleStaffScene, "base_chair", "anim@amb@office@boardroom@crew@female@var_c@base@", 4f, -4f, 32781, 1000f);
                    femaleStaffSceneStatus = -1;
                    break;
            }
            switch (FemaleStaffConversationStatus) {
                case 0:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, femaleStaff, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = -1;
                    isFemaleStaffGreeted = true;
                    break;
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, femaleStaff)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = -1;
                    break;
            }
            if (femaleStaff != null && Game.Player.Character.Position.DistanceTo(femaleStaff.Position) < 1f && FemaleStaffConversationStatus == -1) {
                if (!isFemaleStaffGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_FEMALE", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 0;
                }
                else if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, femaleStaff)) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, femaleStaff, "PED_RANT_01", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 1;
                }
            }
        }

        private static void PaSceneOnTick() {
            switch (paSceneStatus) {
                case 0:
                    pa = World.CreatePed(PedHash.ExecutivePAFemale01, paChairPos);
                    pa.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 0, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 2, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 3, 1, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 4, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 6, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 7, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 2), Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 8, 3, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 11, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
                    paSceneStatus = 1;
                    break;
                case 1:
                    Model model = new Model("ex_prop_offchair_exec_03");
                    model.Request(250);
                    if (model.IsInCdImage && model.IsValid) {
                        while (!model.IsLoaded) Wait(50);
                        paChair = World.CreateProp(model, Vector3.Zero, false, false);
                        paChair.Position = paChairPos;
                        paChair.Rotation = paChairRot;
                    }
                    model.MarkAsNoLongerNeeded();
                    paSceneStatus = 2;
                    break;
                case 2:
                    paScene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, paChair.Position.X, paChair.Position.Y, paChair.Position.Z, 0f, 0f, paChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, paScene, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, pa, paScene, "anim@amb@office@pa@female@", "pa_base", 1000f, -2f, 260, 0, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, paChair, paScene, "base_chair", "anim@amb@office@pa@female@", 1000f, -2f, 4 | 256, 1148846080);
                    paSceneStatus = -1;
                    break;
            }
            switch (PaConversationStatus) {
                case 0:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GREET_ATTRACTIVE_F", "SPEECH_PARAMS_FORCE");
                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) PaConversationStatus = 1;
                    else PaConversationStatus = -1;
                    isPaGreeted = true;
                    break;
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_STYLE", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = -1;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = -1;
                    isPaGreeted = false;
                    break;
                case 3:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2);
                    switch (rnd) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_YES", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "STRIP_2ND_DANCE_ACCEPT", "SPEECH_PARAMS_FORCE"); break;
                    }
                    PaConversationStatus = -1;
                    break;
            }
            if (pa != null && Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 0.5f, 220394186, 0, 0, 0).Model == 220394186 && PaConversationStatus == -1) {
                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner && !isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_GREET", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 0;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)owner && !isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 0;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner && isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_FAREWELL", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 2;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)owner && isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 2;
                }
            }
            if (pa != null && Game.Player.Character.Position.DistanceTo(pa.Position) < 2f && Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) {
                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)owner) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_CHATVIP", "SPEECH_PARAMS_FORCE");
                else Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_CHATOTHERS", "SPEECH_PARAMS_FORCE");
                PaConversationStatus = 3;
            }
        }

        public static void ResetScenes() {
            if (boss != null) boss.Delete();
            isBossGreeted = false;
            //if (maleStaff != null) maleStaff.Delete();
            //isMaleStaffGreeted = false;
            //if (femaleStaff != null) femaleStaff.Delete();
            //isFemaleStaffGreeted = false;
            if (pa != null) pa.Delete();
            if (paChair != null) paChair.Delete();
            isPaGreeted = false;
            seatedStaffChairs.Clear();
        }

        private void OnTick(object sender, EventArgs e) {
            BossSceneOnTick();
            MaleStaffSceneOnTick();
            FemaleStaffSceneOnTick();
            PaSceneOnTick();
        }

        private void OnAborted(object sender, EventArgs e) {
            if (boss != null) boss.Delete();
            if (maleStaff != null) maleStaff.Delete();
            if (femaleStaff != null) femaleStaff.Delete();
            if (pa != null) pa.Delete();
            if (paChair != null) paChair.Delete();
        }

    }
}
