using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice.Interactions {

    internal class Boss : Interaction {

        private readonly Vector3 spawnPos;
        private Prop chair;
        private Ped ped;

        public Boss(Vector3 spawnPos) {
            this.spawnPos = spawnPos;
        }

        public bool IsGreeted { get; set; }
        public int ConversationState { get; set; }

        public override void Initialize() {
            if (ped != null) return;

            switch (SinglePlayerOffice.CurrentBuilding.Owner) {
                case Owner.Michael:
                    ped = World.CreatePed(PedHash.Michael, spawnPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 4, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 0, 7, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 0, 1, 2);

                    break;
                case Owner.Franklin:
                    ped = World.CreatePed(PedHash.Franklin, spawnPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 3, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 4, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 22, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 21, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 17, 9, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 14, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 7, 0, 2);

                    break;
                case Owner.Trevor:
                    ped = World.CreatePed(PedHash.Trevor, spawnPos);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 5, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 27, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 20, 1, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 19, 12, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 14, 0, 2);

                    break;
            }
        }

        public override void Update() {
            switch (State) {
                case 0:

                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY,
                            Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X,
                                Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) ||
                        !Function.Call<bool>(Hash.IS_PED_STILL, ped)) break;

                    chair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, ped.Position.X, ped.Position.Y,
                        ped.Position.Z, 1f, -1278649385, 0, 0, 0);
                    State = 1;

                    break;
                case 1:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@boss@male@");
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@boss@male@"))
                        State = 2;

                    break;
                case 2:

                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, chair, "anim@amb@office@boardroom@boss@male@",
                        "enter_b_chair", 3)) {
                        syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                            chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, ped, syncSceneHandle,
                            "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_b_chair",
                            "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    }
                    else {
                        State = 3;
                    }

                    break;
                case 3:

                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;

                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, ped, syncSceneHandle,
                        "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair",
                        "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    State = -1;

                    break;
            }

            switch (ConversationState) {
                case 1:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;

                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "HOWS_IT_GOING_MALE", "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;
                    IsGreeted = true;

                    break;
                case 2:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;

                    var rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);

                    switch (rnd) {
                        case 0:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_NO", "SPEECH_PARAMS_FORCE");

                            break;
                        case 1:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_WHATEVER", "SPEECH_PARAMS_FORCE");

                            break;
                        case 2:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE");

                            break;
                    }

                    ConversationState = 0;

                    break;
            }

            if (ped == null || !(Game.Player.Character.Position.DistanceTo(ped.Position) < 5f) ||
                ConversationState != 0 || IsGreeted) return;

            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_MALE", "SPEECH_PARAMS_FORCE");
            ConversationState = 1;
        }

        public override void Reset() {
            base.Reset();

            IsGreeted = false;
        }

        public override void Dispose() {
            ped?.Delete();
            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@boardroom@boss@male@");
        }

    }

}