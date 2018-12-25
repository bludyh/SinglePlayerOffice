using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {
    internal class Sofa : Interaction {
        private readonly List<string> animDicts;
        private readonly List<string> idleAnims;
        private string animDict;

        public Sofa(Vector3 pos, Vector3 rot) {
            animDicts = new List<string> {
                "anim@amb@office@seating@male@var_a@base@", "anim@amb@office@seating@male@var_d@base@",
                "anim@amb@office@seating@male@var_e@base@"
            };
            idleAnims = new List<string> {"idle_a", "idle_b", "idle_c"};
            Position = pos;
            Rotation = rot;
        }

        public override string HelpText => "Press ~INPUT_CONTEXT~ to sit on the couch";
        public Vector3 Position { get; }
        public Vector3 Rotation { get; }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() &&
                        Game.Player.Character.Position.DistanceTo(Position) < 1.5f &&
                        World.GetNearbyPeds(Position, 0.5f).Length == 0) {
                        Utilities.DisplayHelpTextThisFrame(HelpText);
                        if (Game.IsControlJustPressed(2, Control.Context)) {
                            SinglePlayerOffice.IsHudHidden = true;
                            State = 1;
                        }
                    }

                    break;
                case 1:
                    animDict = animDicts[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3)];
                    initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION, animDict, "enter",
                        Position.X, Position.Y, Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 0, 2);
                    initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION, animDict, "enter",
                        Position.X, Position.Y, Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 0, 2);
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y,
                        initialPos.Z, 1f, 1000, initialRot.Z, 0f);
                    State = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Position.X, Position.Y,
                        Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, animDict,
                        "enter", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                    State = 3;
                    break;
                case 3:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Position.X, Position.Y,
                        Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, animDict,
                        "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    State = 4;
                    break;
                case 4:
                    Utilities.DisplayHelpTextThisFrame("Press ~INPUT_AIM~ to stand up");
                    if (Game.IsControlJustPressed(2, Control.Aim)) {
                        State = 5;
                        break;
                    }

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Position.X, Position.Y,
                        Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, animDict,
                        idleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3)], 4f, -1.5f, 13, 16,
                        1148846080, 0);
                    State = 3;
                    break;
                case 5:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Position.X, Position.Y,
                        Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle, animDict,
                        "exit", 4f, -4f, 13, 16, 1000f, 0);
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