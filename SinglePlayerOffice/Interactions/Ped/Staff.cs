using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Staff : Interaction {

        private readonly Model model;
        private readonly Vector3 spawnPos;
        private Prop chair;
        private Ped ped;

        public Staff(Model model, Vector3 spawnPos) {
            this.model = model;
            this.spawnPos = spawnPos;
        }

        public bool IsGreeted { get; set; }
        public int ConversationState { get; set; }

        public override void Update() {
            switch (State) {
                case 0:
                    var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);

                    if (hours > 8 && hours < 17) {
                        if (ped == null) {
                            ped = World.CreatePed(model, spawnPos);
                            Function.Call(Hash.SET_PED_RANDOM_COMPONENT_VARIATION, ped, 0);
                            State = 1;
                        }
                    }
                    else {
                        if (ped != null) {
                            ped.Delete();
                            ped = null;
                        }
                    }

                    break;
                case 1:

                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY,
                            Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) ||
                        !Function.Call<bool>(Hash.IS_PED_STILL, ped)) break;

                    chair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, ped.Position.X, ped.Position.Y,
                        ped.Position.Z, 1f, -1278649385, 0, 0, 0);
                    State = 2;

                    break;
                case 2:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@male@var_b@base@");
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@female@var_c@base@");
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED,
                            "anim@amb@office@boardroom@crew@male@var_b@base@") &&
                        Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED,
                            "anim@amb@office@boardroom@crew@female@var_c@base@"))
                        State = 3;

                    break;
                case 3:

                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, chair,
                        $"anim@amb@office@boardroom@crew@{(ped.Gender == Gender.Male ? "male@var_b" : "female@var_c")}@base@",
                        "enter_chair", 3)) {
                        syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                            chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, ped, syncSceneHandle,
                            $"anim@amb@office@boardroom@crew@{(ped.Gender == Gender.Male ? "male@var_b" : "female@var_c")}@base@",
                            "enter", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_chair",
                            $"anim@amb@office@boardroom@crew@{(ped.Gender == Gender.Male ? "male@var_b" : "female@var_c")}@base@",
                            4f, -4f, 32781, 1000f);
                    }
                    else {
                        State = 4;
                    }

                    break;
                case 4:

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, ped, syncSceneHandle,
                        $"anim@amb@office@boardroom@crew@{(ped.Gender == Gender.Male ? "male@var_b" : "female@var_c")}@base@",
                        "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair",
                        $"anim@amb@office@boardroom@crew@{(ped.Gender == Gender.Male ? "male@var_b" : "female@var_c")}@base@",
                        4f, -4f, 32781, 1000f);
                    State = 0;

                    break;
            }

            switch (ConversationState) {
                case 1:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;

                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;
                    IsGreeted = true;

                    break;
                case 2:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) break;

                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP",
                        "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;

                    break;
            }

            if (ped == null || !(Game.Player.Character.Position.DistanceTo(ped.Position) < 1f) ||
                ConversationState != 0) return;

            if (!IsGreeted) {
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character,
                    $"GENERIC_HI{(ped.Gender == Gender.Male ? string.Empty : "_FEMALE")}", "SPEECH_PARAMS_FORCE");
                ConversationState = 1;
            }
            else if (Game.IsControlJustPressed(2, Control.Context) &&
                     !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) &&
                     !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) {
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "PED_RANT_01", "SPEECH_PARAMS_FORCE");
                ConversationState = 2;
            }
        }

        public override void Reset() {
            State = ped == null ? 0 : 1;
            IsGreeted = false;
        }

        public override void Dispose() {
            ped?.Delete();
            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@boardroom@crew@male@var_b@base@");
            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@boardroom@crew@female@var_c@base@");
        }

    }

}