using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class OfficeScene : Scene {

        private static int buildingNameRenderTargetHandle;
        private static Scaleform buildingNameScaleform;

        private Ped boss;
        private Prop bossChair;
        private int bossSceneHandle;
        private bool isBossGreeted;
        private List<int> seatedStaffChairs;
        private Ped maleStaff;
        private Prop maleStaffChair;
        private int maleStaffSceneHandle;
        private bool isMaleStaffGreeted;
        private Ped femaleStaff;
        private Prop femaleStaffChair;
        private int femaleStaffSceneHandle;
        private bool isFemaleStaffGreeted;
        private Ped pa;
        private Prop paChair;
        private int paSceneHandle;
        private bool isPaGreeted;

        public int BuildingNameStatus { get; set; }
        public Vector3 BossChairPos { get; set; }
        public int BossStatus { get; set; }
        public int BossConversationStatus { get; set; }
        public List<Vector3> StaffChairPosList { get; set; }
        public int MaleStaffStatus { get; set; }
        public int MaleStaffConversationStatus { get; set; }
        public int FemaleStaffStatus { get; set; }
        public int FemaleStaffConversationStatus { get; set; }
        public Vector3 PaChairPos { get; set; }
        public Vector3 PaChairRot { get; set; }
        public int PaStatus { get; set; }
        public int PaConversationStatus { get; set; }

        public OfficeScene() {
            seatedStaffChairs = new List<int>();
            ActiveInteractions.AddRange(new List<Action> { BuildingNameOnTick, BossOnTick, MaleStaffOnTick, FemaleStaffOnTick, PaOnTick });
        }

        private void CreatePaChair() {
            Model model = new Model("ex_prop_offchair_exec_03");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded) Script.Wait(50);
                paChair = World.CreateProp(model, Vector3.Zero, false, false);
                paChair.Position = PaChairPos;
                paChair.Rotation = PaChairRot;
            }
            model.MarkAsNoLongerNeeded();
        }

        private void CreateBossPed() {
            switch (Location.Building.Owner) {
                case Owner.Michael:
                    boss = World.CreatePed(PedHash.Michael, BossChairPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 4, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 2, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 0, 1, 2);
                    break;
                case Owner.Franklin:
                    boss = World.CreatePed(PedHash.Franklin, BossChairPos);
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
                    boss = World.CreatePed(PedHash.Trevor, BossChairPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 0, 0, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 1, 5, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 3, 27, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 4, 20, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 6, 19, 12, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, boss, 8, 14, 0, 2);
                    break;
            }
        }

        private void CreateMaleStaffPed() {
            maleStaff = World.CreatePed(PedHash.Business01AMM, StaffChairPosList[GetRandomStaffChairIndex()]);
            Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
        }

        private void CreateFemaleStaffPed() {
            femaleStaff = World.CreatePed(PedHash.Business04AFY, StaffChairPosList[GetRandomStaffChairIndex()]);
            Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION);
        }

        private void CreatePaPed() {
            pa = World.CreatePed(PedHash.ExecutivePAFemale01, PaChairPos);
            pa.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 0, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 2, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 3, 1, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 4, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 6, 0, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 7, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 2), Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 8, 3, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, pa, 11, 3, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
        }

        private int GetRandomStaffChairIndex() {
            int index;
            do {
                index = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, StaffChairPosList.Count);
            } while (seatedStaffChairs.Contains(index));
            seatedStaffChairs.Add(index);
            return index;
        }

        private void BuildingNameOnTick() {
            switch (BuildingNameStatus) {
                case 0:
                    if (buildingNameScaleform == null) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text")) {
                            Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_office_text", 0);
                            if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, -2082168399)) {
                                Function.Call(Hash.LINK_NAMED_RENDERTARGET, -2082168399);
                                buildingNameRenderTargetHandle = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "prop_ex_office_text");
                            }
                        }
                        BuildingNameStatus = 1;
                    }
                    else {
                        Function.Call(Hash.SET_TEXT_RENDER_ID, buildingNameRenderTargetHandle);
                        Function.Call(Hash._SET_2D_LAYER, 4);
                        Function.Call(Hash._0xC6372ECD45D73BCD, 1);
                        Function.Call(Hash._0xB8A850F20A067EB6, 73, 73);
                        Function.Call(Hash.DRAW_SCALEFORM_MOVIE, buildingNameScaleform.Handle, 0.196f, 0.245f, 0.46f, 0.66f, 255, 255, 255, 255, 0);
                        Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                        Function.Call(Hash._0xE3A3DB414A373DAB);
                    }
                    break;
                case 1:
                    buildingNameScaleform = new Scaleform("ORGANISATION_NAME");
                    if (buildingNameScaleform.IsLoaded) {
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, buildingNameScaleform.Handle, "SET_ORGANISATION_NAME");
                        Function.Call((Hash)8646405517797544368, Location.Building.Name);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                        Function.Call((Hash)3671366047641330747, buildingNameScaleform.Handle, 1);
                        BuildingNameStatus = 0;
                    }
                    break;
            }
        }

        private void BossOnTick() {
            switch (BossStatus) {
                case 0:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) || !Function.Call<bool>(Hash.IS_PED_STILL, boss)) break;
                    bossChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, boss.Position.X, boss.Position.Y, boss.Position.Z, 1f, -1278649385, 0, 0, 0);
                    BossStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, bossChair, "anim@amb@office@boardroom@boss@male@", "enter_b_chair", 2)) {
                        bossSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, boss, bossSceneHandle, "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, bossSceneHandle, "enter_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    }
                    else BossStatus = 2;
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, bossSceneHandle) != 1f) break;
                    bossSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, bossChair.Position.X, bossChair.Position.Y, bossChair.Position.Z, 0f, 0f, bossChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, bossSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, boss, bossSceneHandle, "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, bossChair, bossSceneHandle, "base_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    BossStatus = -1;
                    break;
            }
            switch (BossConversationStatus) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "HOWS_IT_GOING_MALE", "SPEECH_PARAMS_FORCE");
                    BossConversationStatus = 0;
                    isBossGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    switch (rnd) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_NO", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_WHATEVER", "SPEECH_PARAMS_FORCE"); break;
                        case 2: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, boss, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE"); break;
                    }
                    BossConversationStatus = 0;
                    break;
            }
            if (boss != null && Game.Player.Character.Position.DistanceTo(boss.Position) < 5f && BossConversationStatus == 0 && !isBossGreeted) {
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_MALE", "SPEECH_PARAMS_FORCE");
                BossConversationStatus = 1;
            }
        }

        private void MaleStaffOnTick() {
            switch (MaleStaffStatus) {
                case 0:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) || !Function.Call<bool>(Hash.IS_PED_STILL, maleStaff)) break;
                    maleStaffChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, maleStaff.Position.X, maleStaff.Position.Y, maleStaff.Position.Z, 1f, -1278649385, 0, 0, 0);
                    MaleStaffStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, maleStaffChair, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter_chair", 2)) {
                        maleStaffSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, maleStaffChair.Position.X, maleStaffChair.Position.Y, maleStaffChair.Position.Z, 0f, 0f, maleStaffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, maleStaff, maleStaffSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, maleStaffChair, maleStaffSceneHandle, "enter_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    }
                    else MaleStaffStatus = 2;
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, maleStaffSceneHandle) != 1f) break;
                    maleStaffSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, maleStaffChair.Position.X, maleStaffChair.Position.Y, maleStaffChair.Position.Z, 0f, 0f, maleStaffChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, maleStaffSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, maleStaff, maleStaffSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, maleStaffChair, maleStaffSceneHandle, "base_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    MaleStaffStatus = -1;
                    break;
            }
            switch (MaleStaffConversationStatus) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, maleStaff, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 0;
                    isMaleStaffGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, maleStaff)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 0;
                    break;
            }
            if (maleStaff != null && Game.Player.Character.Position.DistanceTo(maleStaff.Position) < 1f && MaleStaffConversationStatus == 0) {
                if (!isMaleStaffGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 1;
                }
                else if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, maleStaff)) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, maleStaff, "PED_RANT_01", "SPEECH_PARAMS_FORCE");
                    MaleStaffConversationStatus = 2;
                }
            }
        }

        private void FemaleStaffOnTick() {
            switch (FemaleStaffStatus) {
                case 0:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) || !Function.Call<bool>(Hash.IS_PED_STILL, femaleStaff)) break;
                    femaleStaffChair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, femaleStaff.Position.X, femaleStaff.Position.Y, femaleStaff.Position.Z, 1f, -1278649385, 0, 0, 0);
                    FemaleStaffStatus = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, femaleStaffChair, "anim@amb@office@boardroom@crew@female@var_c@base@", "enter_chair", 2)) {
                        femaleStaffSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, femaleStaffChair.Position.X, femaleStaffChair.Position.Y, femaleStaffChair.Position.Z, 0f, 0f, femaleStaffChair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, femaleStaff, femaleStaffSceneHandle, "anim@amb@office@boardroom@crew@female@var_c@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, femaleStaffChair, femaleStaffSceneHandle, "enter_chair", "anim@amb@office@boardroom@crew@female@var_c@base@", 4f, -4f, 32781, 1000f);
                    }
                    else FemaleStaffStatus = 2;
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, femaleStaffSceneHandle) != 1f) break;
                    femaleStaffSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, femaleStaffChair.Position.X, femaleStaffChair.Position.Y, femaleStaffChair.Position.Z, 0f, 0f, femaleStaffChair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, femaleStaffSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, femaleStaff, femaleStaffSceneHandle, "anim@amb@office@boardroom@crew@female@var_c@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, femaleStaffChair, femaleStaffSceneHandle, "base_chair", "anim@amb@office@boardroom@crew@female@var_c@base@", 4f, -4f, 32781, 1000f);
                    FemaleStaffStatus = -1;
                    break;
            }
            switch (FemaleStaffConversationStatus) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, femaleStaff, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 0;
                    isFemaleStaffGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, femaleStaff)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 0;
                    break;
            }
            if (femaleStaff != null && Game.Player.Character.Position.DistanceTo(femaleStaff.Position) < 1f && FemaleStaffConversationStatus == 0) {
                if (!isFemaleStaffGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_FEMALE", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 1;
                }
                else if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, femaleStaff)) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, femaleStaff, "PED_RANT_01", "SPEECH_PARAMS_FORCE");
                    FemaleStaffConversationStatus = 2;
                }
            }
        }

        private void PaOnTick() {
            switch (PaStatus) {
                case 0:
                    paSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, PaChairPos.X, PaChairPos.Y, PaChairPos.Z, 0f, 0f, PaChairRot.Z, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, paSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, pa, paSceneHandle, "anim@amb@office@pa@female@", "pa_base", 1000f, -2f, 260, 0, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, paChair, paSceneHandle, "base_chair", "anim@amb@office@pa@female@", 1000f, -2f, 4 | 256, 1148846080);
                    PaStatus = -1;
                    break;
            }
            switch (PaConversationStatus) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GREET_ATTRACTIVE_F", "SPEECH_PARAMS_FORCE");
                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Location.Building.Owner) PaConversationStatus = 2;
                    else PaConversationStatus = 0;
                    isPaGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_STYLE", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_DECOR", "SPEECH_PARAMS_FORCE"); break;
                    }
                    PaConversationStatus = 0;
                    break;
                case 3:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 0;
                    isPaGreeted = false;
                    break;
                case 4:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) break;
                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_YES", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "STRIP_2ND_DANCE_ACCEPT", "SPEECH_PARAMS_FORCE"); break;
                    }
                    PaConversationStatus = 0;
                    break;
            }
            if (pa != null && Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 0.5f, 220394186, 0, 0, 0).Model == 220394186 && PaConversationStatus == 0) {
                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Location.Building.Owner && !isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_GREET", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 1;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)Location.Building.Owner && !isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 1;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Location.Building.Owner && isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_FAREWELL", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 3;
                }
                else if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)Location.Building.Owner && isPaGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 3;
                }
            }
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && pa != null && Game.Player.Character.Position.DistanceTo(pa.Position) < 2f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Location.Building.Owner) {
                    Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to chat with your PA~n~Press ~INPUT_CONTEXT_SECONDARY~ for executive options");
                    if (Game.IsControlJustPressed(2, GTA.Control.ContextSecondary)) {
                        Game.Player.Character.Task.StandStill(-1);
                        Utilities.SavedPos = Game.Player.Character.Position;
                        Utilities.SavedRot = Game.Player.Character.Rotation;
                        SinglePlayerOffice.IsHudHidden = true;
                        Location.Building.PAMenu.Visible = true;
                    }
                }
                if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pa)) {
                    if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) == (int)Location.Building.Owner) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_CHATVIP", "SPEECH_PARAMS_FORCE");
                    else Function.Call(Hash._PLAY_AMBIENT_SPEECH1, pa, "EXECPA_CHATOTHERS", "SPEECH_PARAMS_FORCE");
                    PaConversationStatus = 4;
                }
            }
        }

        public override void OnSceneStarted() {
            base.OnSceneStarted();

            if (buildingNameScaleform != null) {
                buildingNameScaleform.Dispose();
                buildingNameScaleform = null;
            }
            if (boss != null) {
                boss.Delete();
                boss = null;
            }
            if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)Location.Building.Owner && boss == null) CreateBossPed();
            if (paChair == null) CreatePaChair();
            if (pa == null) CreatePaPed();
            isBossGreeted = false;
            isMaleStaffGreeted = false;
            isFemaleStaffGreeted = false;
            isPaGreeted = false;
            BuildingNameStatus = 0;
            BossStatus = 0;
            MaleStaffStatus = 0;
            FemaleStaffStatus = 0;
            PaStatus = 0;
        }

        protected override void HandleSceneBehaviors() {
            var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);
            if ((hours > 8 && hours < 17) && (maleStaff == null && femaleStaff == null)) {
                CreateMaleStaffPed();
                CreateFemaleStaffPed();
            }
            else if ((hours < 9 || hours > 16) && (maleStaff != null && femaleStaff != null)) {
                maleStaff.Delete();
                femaleStaff.Delete();
            }
        }

        public override void Dispose() {
            if (buildingNameScaleform != null) buildingNameScaleform.Dispose();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text")) Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_office_text");
            if (boss != null) boss.Delete();
            if (maleStaff != null) maleStaff.Delete();
            if (femaleStaff != null) femaleStaff.Delete();
            if (paChair != null) paChair.Delete();
            if (pa != null) pa.Delete();
        }

    }
}
