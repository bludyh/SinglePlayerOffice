using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Pa : Interaction {

        private readonly Vector3 chairSpawnPos;
        private readonly Vector3 chairSpawnRot;
        private Prop chair;

        private Ped ped;

        public Pa(Vector3 chairSpawnPos, Vector3 chairSpawnRot) {
            this.chairSpawnPos = chairSpawnPos;
            this.chairSpawnRot = chairSpawnRot;
        }

        public bool IsGreeted { get; set; }
        public int ConversationState { get; set; }
        public override bool IsCreated => ped != null && chair != null;

        public override void Create() {
            var model = new Model("ex_prop_offchair_exec_03");
            model.Request(250);

            if (model.IsInCdImage && model.IsValid) {
                while (!model.IsLoaded)
                    Script.Wait(50);
                chair = World.CreateProp(model, Vector3.Zero, false, false);
                chair.Position = chairSpawnPos;
                chair.Rotation = chairSpawnRot;
            }

            model.MarkAsNoLongerNeeded();

            ped = World.CreatePed(PedHash.ExecutivePAFemale01, chairSpawnPos);
            ped.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER");
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 1, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 3,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 0,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 2),
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 3, 0, 2);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 3,
                Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3), 2);
        }

        public override void Update() {
            var currentBuilding = SinglePlayerOffice.CurrentBuilding;

            switch (State) {
                case 0:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@pa@female@");
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@pa@female@"))
                        State = 1;

                    break;
                case 1:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X,
                        chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Rotation.Z, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, ped, syncSceneHandle, "anim@amb@office@pa@female@",
                        "pa_base", 1000f, -2f, 260, 0, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair",
                        "anim@amb@office@pa@female@", 1000f, -2f, 4 | 256, 1148846080);
                    State = -1;

                    break;
            }

            switch (ConversationState) {
                case 1:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) break;

                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GREET_ATTRACTIVE_F",
                        "SPEECH_PARAMS_FORCE");
                    ConversationState = currentBuilding.IsOwnedBy(Game.Player.Character) ? 2 : 0;
                    IsGreeted = true;

                    break;
                case 2:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;

                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "EXECPA_STYLE", "SPEECH_PARAMS_FORCE");

                            break;
                        case 1:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "EXECPA_DECOR", "SPEECH_PARAMS_FORCE");

                            break;
                    }

                    ConversationState = 0;

                    break;
                case 3:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) break;

                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_BYE",
                        "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;
                    IsGreeted = false;

                    break;
                case 4:

                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) break;

                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_YES",
                                "SPEECH_PARAMS_FORCE");

                            break;
                        case 1:
                            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "STRIP_2ND_DANCE_ACCEPT",
                                "SPEECH_PARAMS_FORCE");

                            break;
                    }

                    ConversationState = 0;

                    break;
            }

            if (Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X,
                        Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 0.5f, 220394186, 0, 0, 0)
                    .Model == 220394186 && ConversationState == 0) {
                if (currentBuilding.IsOwnedBy(Game.Player.Character) && !IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "EXECPA_GREET", "SPEECH_PARAMS_FORCE");
                    ConversationState = 1;
                }
                else if (!currentBuilding.IsOwnedBy(Game.Player.Character) && !IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    ConversationState = 1;
                }
                else if (currentBuilding.IsOwnedBy(Game.Player.Character) && IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "EXECPA_FAREWELL", "SPEECH_PARAMS_FORCE");
                    ConversationState = 3;
                }
                else if (!currentBuilding.IsOwnedBy(Game.Player.Character) && IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    ConversationState = 3;
                }
            }

            if (Game.Player.Character.IsDead || Game.Player.Character.IsInVehicle() || ped == null ||
                !(Game.Player.Character.Position.DistanceTo(ped.Position) < 2f) ||
                UI.MenuPool.IsAnyMenuOpen()) return;

            if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                Utilities.DisplayHelpTextThisFrame(
                    "Press ~INPUT_CONTEXT~ to chat with your PA~n~Press ~INPUT_CONTEXT_SECONDARY~ for executive options");

                if (Game.IsControlJustPressed(2, Control.ContextSecondary)) {
                    Game.Player.Character.Task.StandStill(-1);
                    Utilities.SavedPos = Game.Player.Character.Position;
                    Utilities.SavedRot = Game.Player.Character.Rotation;
                    UI.IsHudHidden = true;
                    UI.PaMenu.Visible = true;
                }
            }

            if (!Game.IsControlJustPressed(2, Control.Context) ||
                Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) ||
                Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped)) return;

            Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped,
                currentBuilding.IsOwnedBy(Game.Player.Character)
                    ? "EXECPA_CHATVIP"
                    : "EXECPA_CHATOTHERS", "SPEECH_PARAMS_FORCE");
            ConversationState = 4;
        }

        public override void Reset() {
            IsGreeted = false;
        }

        public override void Dispose() {
            ped?.Delete();
            chair?.Delete();
            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@pa@female@");
        }

    }

}