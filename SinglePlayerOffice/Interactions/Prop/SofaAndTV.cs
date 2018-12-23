using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class SofaWithTVInteraction : Interaction {

        private Sofa sofa;
        private Prop tv;
        private Prop remote;
        private TVInteration tvInteraction;
        private readonly List<string> idleAnims;

        public override string HelpText {
            get {
                return "Press ~INPUT_CONTEXT~ to sit on the couch";
            }
        }

        public List<Sofa> Sofas { get; set; }

        public SofaWithTVInteraction() {
            idleAnims = new List<string> { "idle_a", "idle_b", "idle_c" };
        }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle()) {
                        foreach (var sofa in Sofas) {
                            if (Game.Player.Character.Position.DistanceTo(sofa.Position) < 1.5f && World.GetNearbyPeds(sofa.Position, 0.5f).Length == 0) {
                                Utilities.DisplayHelpTextThisFrame(HelpText);
                                if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                                    this.sofa = sofa;
                                    SinglePlayerOffice.IsHudHidden = true;
                                    State = 1;
                                }
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, "anim@amb@office@seating@male@var_a@base@", "enter", sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 0, 2);
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, "anim@amb@office@seating@male@var_a@base@", "enter", sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y, initialPos.Z, 1f, 1000, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    State = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    State = 4;
                    break;
                case 4:
                    Utilities.DisplayHelpTextThisFrame(HelpText + "~n~Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, GTA.Control.Context)) {
                        State = 5;
                        break;
                    }
                    if (Game.IsControlJustPressed(2, GTA.Control.Aim)) {
                        State = 8;
                        break;
                    }
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", idleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3)], 4f, -1.5f, 13, 16, 1148846080, 0);
                    State = 3;
                    break;
                case 5:
                    foreach (Prop prop in World.GetNearbyProps(Game.Player.Character.Position, 10f)) {
                        if (prop.Model.Hash == 608950395 || prop.Model.Hash == 1036195894) {
                            tv = prop;
                            tvInteraction = new TVInteration(tv);
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
                    if (tvInteraction.IsTVOn && Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2) == 1) Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "TV_BORED", "SPEECH_PARAMS_FORCE");
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@game@seated@male@var_c@base@", "enter_a", 4f, -4f, 13, 16, 1000f, 0);
                    State = 6;
                    break;
                case 6:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    tvInteraction.State = 1;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@game@seated@male@var_c@base@", "exit_a", 4f, -4f, 13, 16, 1000f, 0);
                    State = 7;
                    break;
                case 7:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    remote.Delete();
                    State = 3;
                    break;
                case 8:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, sofa.Position.X, sofa.Position.Y, sofa.Position.Z, sofa.Rotation.X, sofa.Rotation.Y, sofa.Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, "anim@amb@office@seating@male@var_a@base@", "exit", 4f, -4f, 13, 16, 1000f, 0);
                    State = 9;
                    break;
                case 9:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    State = 0;
                    break;
            }
            tvInteraction.Update();
        }

        public override void Reset() {
            tvInteraction.Reset();
        }

        public override void Dispose() {
            tvInteraction.Dispose();
            if (remote != null) remote.Delete();
        }

    }
}
