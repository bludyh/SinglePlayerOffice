using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class LaptopInteraction : Interaction {

        private Prop chair;
        private readonly List<string> idleAnims;
        private readonly List<string> chairIdleAnims;

        public override string HelpText {
            get {
                return "Press ~INPUT_CONTEXT~ to sit down";
            }
        }

        public LaptopInteraction() {
            idleAnims = new List<string> { "idle_a", "idle_b", "idle_c" };
            chairIdleAnims = new List<string> { "idle_a_chair", "idle_b_chair", "idle_c_chair" };
        }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            if (prop.Model.Hash == -1278649385 && World.GetNearbyProps(prop.Position, 1.5f, -1278649385).Length == 1 && World.GetNearbyProps(prop.Position, 1.5f, 1385417869).Length != 0 && World.GetNearbyPeds(prop.Position, 0.5f).Length == 0) {
                                Utilities.DisplayHelpTextThisFrame(HelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    chair = prop;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    State = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", chair.Position.X, chair.Position.Y, chair.Position.Z, chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", chair.Position.X, chair.Position.Y, chair.Position.Z, chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y, initialPos.Z, 1f, -1, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    State = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "enter", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    State = 4;
                    break;
                case 4:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    State = 5;
                    break;
                case 5:
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        State = 6;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", idleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, chairIdleAnims[rnd], "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 32781, 1000f);
                    State = 4;
                    break;
                case 6:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@laptops@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "exit_chair", "anim@amb@office@laptops@male@var_b@base@", 4f, -4f, 13, 1000f);
                    State = 7;
                    break;
                case 7:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@boardroom@crew@male@var_b@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "exit_chair", "anim@amb@office@boardroom@crew@male@var_b@base@", 4f, -4f, 13, 1000f);
                    State = 8;
                    break;
                case 8:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    State = 0;
                    break;
            }
        }

    }
}
