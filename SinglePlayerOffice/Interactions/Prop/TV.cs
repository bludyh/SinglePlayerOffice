using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Tv : Interaction {

        private int tvRenderTargetHandle;

        public Prop Prop { get; set; }

        public override string HelpText =>
            !IsTvOn ? "Press ~INPUT_CONTEXT~ to turn on the TV" : "Press ~INPUT_CONTEXT~ to turn off the TV";

        public bool IsTvOn { get; private set; }

        public override void Update() {
            switch (State) {
                case 0:

                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle())
                        foreach (var prop in World.GetNearbyProps(Game.Player.Character.Position, 1.5f)) {
                            if (prop.Model.Hash != 608950395 && prop.Model.Hash != 1036195894) continue;

                            Utilities.DisplayHelpTextThisFrame(HelpText);

                            if (Game.IsControlJustPressed(2, Control.Context)) {
                                Prop = prop;
                                State = 1;
                            }

                            break;
                        }

                    break;
                case 1:

                    if (Prop.Model.Hash == 608950395) {
                        var oldTv = Prop;
                        var tvModel = new Model("prop_tv_flat_01");
                        tvModel.Request(250);

                        if (tvModel.IsInCdImage && tvModel.IsValid) {
                            while (!tvModel.IsLoaded)
                                Script.Wait(50);
                            Prop = World.CreateProp(tvModel, Vector3.Zero, false, false);
                        }

                        tvModel.MarkAsNoLongerNeeded();
                        Prop.Position = oldTv.Position;
                        Prop.Rotation = oldTv.Rotation;
                        oldTv.Delete();
                    }

                    State = 2;

                    break;
                case 2:

                    if (!IsTvOn) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen"))
                            Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "tvscreen", 0);
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, Prop.Model))
                            Function.Call(Hash.LINK_NAMED_RENDERTARGET, Prop.Model);
                        if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen"))
                            tvRenderTargetHandle =
                                Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "tvscreen");
                        Function.Call(Hash.REGISTER_SCRIPT_WITH_AUDIO, 0);
                        Function.Call(Hash.SET_TV_CHANNEL, Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 2));
                        Function.Call(Hash.SET_TV_VOLUME, 0);
                        Function.Call(Hash.ENABLE_MOVIE_SUBTITLES, 1);
                    }
                    else {
                        if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen")) {
                            Script.Wait(0);
                            Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
                        }
                    }

                    IsTvOn = !IsTvOn;
                    State = 0;

                    break;
            }

            if (!IsTvOn) return;

            Function.Call(Hash.SET_TV_AUDIO_FRONTEND, 0);
            Function.Call(Hash.ATTACH_TV_AUDIO_TO_ENTITY, Prop);
            Function.Call(Hash.SET_TEXT_RENDER_ID, tvRenderTargetHandle);
            Function.Call(Hash._0x61BB1D9B3A95D802, 4);
            Function.Call(Hash._0xC6372ECD45D73BCD, 1);
            Function.Call(Hash.DRAW_TV_CHANNEL, 0.5, 0.5, 1.0, 1.0, 0.0, 255, 255, 255, 255);
            Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
        }

        public override void Reset() {
            IsTvOn = false;
            Dispose();
        }

        public override void Dispose() {
            Prop?.Delete();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "tvscreen"))
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "tvscreen");
        }

    }

}