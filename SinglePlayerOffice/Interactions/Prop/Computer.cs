using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Computer : Interaction {

        private readonly List<string> chairIdleAnims;
        private readonly List<string> idleAnims;

        private Prop chair;
        private Prop computer;
        private int computerRenderTargetHandle;

        public Computer() {
            idleAnims = new List<string> { "idle_a", "idle_b", "idle_c", "idle_d", "idle_e" };
            chairIdleAnims = new List<string>
                { "idle_a_chair", "idle_b_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair" };
        }

        public override void Update() {
            switch (State) {
                case 0:

                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle())
                        foreach (var prop in World.GetNearbyProps(Game.Player.Character.Position, 1.2f)) {
                            if (prop.Model.Hash != -1626066319 && prop.Model.Hash != 1339364336 ||
                                World.GetNearbyPeds(prop.Position, 0.5f).Length != 0) continue;

                            if (SinglePlayerOffice.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                                Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to sit down");

                                if (Game.IsControlJustPressed(2, Control.Context)) {
                                    chair = prop;
                                    Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                                    UI.IsHudHidden = true;
                                    State = 1;
                                }
                            }
                            else {
                                Utilities.DisplayHelpTextThisFrame("You do not have access to this computer");
                            }

                            break;
                        }

                    break;
                case 1:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boss@male@");
                    Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, "MPDesktop", false);
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boss@male@") &&
                        Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, "MPDesktop"))
                        State = 2;

                    break;
                case 2:
                    initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION,
                        "anim@amb@office@boss@male@", "enter", chair.Position.X, chair.Position.Y, chair.Position.Z,
                        chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION,
                        "anim@amb@office@boss@male@", "enter", chair.Position.X, chair.Position.Y, chair.Position.Z,
                        chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y,
                        initialPos.Z, 1f, 1, initialRot.Z, 0f);
                    State = 3;

                    break;
                case 3:

                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_chair",
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 4;

                    break;
                case 4:

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair",
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 5;

                    break;
                case 5:
                    Utilities.DisplayHelpTextThisFrame(
                        "Press ~INPUT_CONTEXT~ to use the computer~n~Press ~INPUT_AIM~ to stand up");

                    if (Game.IsControlJustPressed(2, Control.Context)) {
                        State = 6;

                        break;
                    }

                    if (Game.IsControlJustPressed(2, Control.Aim)) {
                        State = 10;

                        break;
                    }

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    var rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 5);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", idleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, chairIdleAnims[rnd],
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 4;

                    break;
                case 6:
                    computer = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X,
                        Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 1f, 743064848, 0, 0, 0);

                    if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_computer_screen", 0);

                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, computer.Model)) {
                            Function.Call(Hash.LINK_NAMED_RENDERTARGET, computer.Model);
                            computerRenderTargetHandle = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID,
                                "prop_ex_computer_screen");
                        }
                    }

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "computer_enter", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "computer_enter_chair",
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 7;

                    break;
                case 7:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetHandle);
                    Function.Call((Hash) 13305974099546635958, 73, 73);
                    Function.Call((Hash) 3154009034243605640, "MPDesktop", "DesktopUI_Login", 0.5f, 0.5f, 1f, 1f, 0f,
                        255, 255, 255, 255);
                    Function.Call((Hash) 16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID,
                        Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, 1);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "computer_idle", 4f, -1.5f, 12, 16, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "computer_idle_chair",
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 8;

                    break;
                case 8:
                    Function.Call(Hash.SET_TEXT_RENDER_ID, computerRenderTargetHandle);
                    Function.Call((Hash) 13305974099546635958, 73, 73);
                    Function.Call((Hash) 3154009034243605640, "MPDesktop", "DesktopUI_Map", 0.5f, 0.5f, 1f, 1f, 0f, 255,
                        255, 255, 255);
                    Function.Call((Hash) 16403195341277969835);
                    Function.Call(Hash.SET_TEXT_RENDER_ID,
                        Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                    Utilities.DisplayHelpTextThisFrame("Press ~INPUT_AIM~ to stop using the computer");
                    if (Game.IsControlJustPressed(2, Control.Aim))
                        State = 9;

                    break;
                case 9:

                    if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen")) {
                        Script.Wait(0);
                        Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_computer_screen");
                    }

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "computer_exit", 1000f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "computer_exit_chair",
                        "anim@amb@office@boss@male@", 1000f, -4f, 32781, 1000f);
                    State = 4;

                    break;
                case 10:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boss@male@", "exit", 4f, -1.5f, 12, 0, 1148846080, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "exit_chair",
                        "anim@amb@office@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 11;

                    break;
                case 11:

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    UI.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@boss@male@");
                    Function.Call(Hash.SET_STREAMED_TEXTURE_DICT_AS_NO_LONGER_NEEDED, "MPDesktop");
                    State = 0;

                    break;
            }
        }

        public override void Dispose() {
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_computer_screen"))
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_computer_screen");
        }

    }

}