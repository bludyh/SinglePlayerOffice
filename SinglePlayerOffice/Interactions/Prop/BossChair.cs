using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {
    internal class BossChair : Interaction {
        private readonly List<string> chairIdleAnims;
        private readonly List<string> idleAnims;

        private Prop chair;

        public BossChair() {
            idleAnims = new List<string> {"idle_a", "idle_c", "idle_d", "idle_e"};
            chairIdleAnims = new List<string> {"idle_a_chair", "idle_c_chair", "idle_d_chair", "idle_e_chair"};
        }

        public override string HelpText => "Press ~INPUT_CONTEXT~ to sit down";

        public override string RejectHelpText => "Only boss can sit here";

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle())
                        foreach (var prop in World.GetNearbyProps(Game.Player.Character.Position, 1f)) {
                            if (prop.Model.Hash != -1278649385 ||
                                World.GetNearbyProps(prop.Position, 1.5f, -1278649385).Length != 1 ||
                                World.GetNearbyProps(prop.Position, 1.5f, 1385417869).Length != 0 ||
                                World.GetNearbyPeds(prop.Position, 0.5f).Length != 0) continue;
                            if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                                Utilities.DisplayHelpTextThisFrame(HelpText);
                                if (Game.IsControlJustPressed(2, Control.Context)) {
                                    chair = prop;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    State = 1;
                                }
                            }
                            else {
                                Utilities.DisplayHelpTextThisFrame(RejectHelpText);
                            }

                            break;
                        }

                    break;
                case 1:
                    initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION,
                        "anim@amb@office@boardroom@boss@male@", "enter_b", chair.Position.X, chair.Position.Y,
                        chair.Position.Z, chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION,
                        "anim@amb@office@boardroom@boss@male@", "enter_b", chair.Position.X, chair.Position.Y,
                        chair.Position.Z, chair.Rotation.X, chair.Rotation.Y, chair.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y,
                        initialPos.Z, 1f, -1, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_b_chair",
                        "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 3;
                    break;
                case 3:
                    Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to talk shit");
                    if (Game.IsControlJustPressed(2, Control.Context)) Utilities.TalkShit();
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair",
                        "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 4;
                    //goto case 4;
                    break;
                case 4:
                    Utilities.DisplayHelpTextThisFrame(
                        "Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, Control.Context)) Utilities.TalkShit();
                    if (Game.IsControlJustPressed(2, Control.Aim)) {
                        State = 5;
                        break;
                    }

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    var rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 4);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boardroom@boss@male@", idleAnims[rnd], 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, chairIdleAnims[rnd],
                        "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    State = 3;
                    break;
                case 5:
                    var nearbyPeds = World.GetNearbyPeds(Game.Player.Character, 5f);
                    if (nearbyPeds.Length != 0)
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GAME_QUIT",
                            "SPEECH_PARAMS_FORCE");
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                        "anim@amb@office@boardroom@boss@male@", "exit_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "exit_b_chair",
                        "anim@amb@office@boardroom@boss@male@", 4f, -4f, 13, 1000f);
                    State = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    State = 0;
                    break;
            }
        }
    }
}