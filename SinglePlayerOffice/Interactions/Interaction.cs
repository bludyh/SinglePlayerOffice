using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal abstract class Interaction {

        protected Vector3 initialPos;
        protected Vector3 initialRot;
        protected int syncSceneHandle;

        public virtual string HelpText { get; }
        public virtual string RejectHelpText { get; }
        public virtual bool IsCreated { get; }
        public int State { get; set; }

        public static void TalkShit() {
            switch (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character)) {
                case 0:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "CULT_TALK",
                        "SPEECH_PARAMS_FORCE");

                    break;
                case 1:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP",
                        "SPEECH_PARAMS_FORCE");

                    break;
                case 3:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_INSULT_OLD",
                        "SPEECH_PARAMS_FORCE");

                    break;
            }
        }

        public virtual void Create() { }

        public abstract void Update();

        public virtual void Reset() { }

        public virtual void Dispose() { }

    }

}