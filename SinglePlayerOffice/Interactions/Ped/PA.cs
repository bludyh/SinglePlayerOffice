using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice.Interactions {
    class PAInteraction : Interaction, INPCInteraction {

        public Prop Chair { get; set; }
        public Ped Ped { get; set; }
        public bool IsGreeted { get; private set; }
        public int ConversationState { get; set; }

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            switch (State) {
                case 0:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Chair.Position.X, Chair.Position.Y, Chair.Position.Z, 0f, 0f, Chair.Rotation.Z, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Ped, syncSceneHandle, "anim@amb@office@pa@female@", "pa_base", 1000f, -2f, 260, 0, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, Chair, syncSceneHandle, "base_chair", "anim@amb@office@pa@female@", 1000f, -2f, 4 | 256, 1148846080);
                    State = -1;
                    break;
            }
            switch (ConversationState) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Ped)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GREET_ATTRACTIVE_F", "SPEECH_PARAMS_FORCE");
                    if (currentBuilding.IsOwnedBy(Game.Player.Character))
                        ConversationState = 2;
                    else
                        ConversationState = 0;
                    IsGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_STYLE", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_DECOR", "SPEECH_PARAMS_FORCE"); break;
                    }
                    ConversationState = 0;
                    break;
                case 3:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Ped)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;
                    IsGreeted = false;
                    break;
                case 4:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Ped)) break;
                    switch (Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2)) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_YES", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "STRIP_2ND_DANCE_ACCEPT", "SPEECH_PARAMS_FORCE"); break;
                    }
                    ConversationState = 0;
                    break;
            }
            if (Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 0.5f, 220394186, 0, 0, 0).Model == 220394186 && ConversationState == 0) {
                if (currentBuilding.IsOwnedBy(Game.Player.Character) && !IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_GREET", "SPEECH_PARAMS_FORCE");
                    ConversationState = 1;
                }
                else if (!currentBuilding.IsOwnedBy(Game.Player.Character) && !IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                    ConversationState = 1;
                }
                else if (currentBuilding.IsOwnedBy(Game.Player.Character) && IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_FAREWELL", "SPEECH_PARAMS_FORCE");
                    ConversationState = 3;
                }
                else if (!currentBuilding.IsOwnedBy(Game.Player.Character) && IsGreeted) {
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "GENERIC_BYE", "SPEECH_PARAMS_FORCE");
                    ConversationState = 3;
                }
            }
            if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && Ped != null && Game.Player.Character.Position.DistanceTo(Ped.Position) < 2f && !SinglePlayerOffice.MenuPool.IsAnyMenuOpen()) {
                if (currentBuilding.IsOwnedBy(Game.Player.Character)) {
                    Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to chat with your PA~n~Press ~INPUT_CONTEXT_SECONDARY~ for executive options");
                    if (Game.IsControlJustPressed(2, GTA.Control.ContextSecondary)) {
                        Game.Player.Character.Task.StandStill(-1);
                        Utilities.SavedPos = Game.Player.Character.Position;
                        Utilities.SavedRot = Game.Player.Character.Rotation;
                        SinglePlayerOffice.IsHudHidden = true;
                        currentBuilding.PAMenu.Visible = true;
                    }
                }
                if (Game.IsControlJustPressed(2, GTA.Control.Context) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character) && !Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Ped)) {
                    if (currentBuilding.IsOwnedBy(Game.Player.Character))
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_CHATVIP", "SPEECH_PARAMS_FORCE");
                    else
                        Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "EXECPA_CHATOTHERS", "SPEECH_PARAMS_FORCE");
                    ConversationState = 4;
                }
            }
        }

        public override void Reset() {
            IsGreeted = false;
            State = 0;
            ConversationState = 0;
        }

        public override void Dispose() {
            Ped?.Delete();
        }

    }
}
