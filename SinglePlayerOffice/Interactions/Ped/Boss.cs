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
    class BossInteraction : Interaction, INPCInteraction {

        private Prop chair;

        public Ped Ped { get; set; }
        public bool IsGreeted { get; private set; }
        public int ConversationState { get; set; }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Function.Call<bool>(Hash.IS_INTERIOR_READY, Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z)) || !Function.Call<bool>(Hash.IS_PED_STILL, Ped)) break;
                    chair = Function.Call<Prop>(Hash.GET_CLOSEST_OBJECT_OF_TYPE, Ped.Position.X, Ped.Position.Y, Ped.Position.Z, 1f, -1278649385, 0, 0, 0);
                    State = 1;
                    break;
                case 1:
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, chair, "anim@amb@office@boardroom@boss@male@", "enter_b_chair", 2)) {
                        syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Ped, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", "enter_b", 1.5f, -1.5f, 13, 16, 1.5f, 4);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "enter_b_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    }
                    else State = 2;
                    break;
                case 2:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) != 1f) break;
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, chair.Position.X, chair.Position.Y, chair.Position.Z, 0f, 0f, chair.Heading, 2);
                    Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, syncSceneHandle, true);
                    Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Ped, syncSceneHandle, "anim@amb@office@boardroom@boss@male@", "base", 4f, -1.5f, 13, 16, 1148846080, 0);
                    Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, chair, syncSceneHandle, "base_chair", "anim@amb@office@boardroom@boss@male@", 4f, -4f, 32781, 1000f);
                    State = -1;
                    break;
            }
            switch (ConversationState) {
                case 1:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "HOWS_IT_GOING_MALE", "SPEECH_PARAMS_FORCE");
                    ConversationState = 0;
                    IsGreeted = true;
                    break;
                case 2:
                    if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, Game.Player.Character)) break;
                    int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 3);
                    switch (rnd) {
                        case 0: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "GENERIC_NO", "SPEECH_PARAMS_FORCE"); break;
                        case 1: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "GENERIC_WHATEVER", "SPEECH_PARAMS_FORCE"); break;
                        case 2: Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Ped, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE"); break;
                    }
                    ConversationState = 0;
                    break;
            }
            if (Ped != null && Game.Player.Character.Position.DistanceTo(Ped.Position) < 5f && ConversationState == 0 && !IsGreeted) {
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_HI_MALE", "SPEECH_PARAMS_FORCE");
                ConversationState = 1;
            }
        }

        public override void Reset() {
            Ped?.Delete();
            IsGreeted = false;
            State = 0;
            ConversationState = 0;
        }

        public override void Dispose() {
            Ped?.Delete();
        }

    }
}
