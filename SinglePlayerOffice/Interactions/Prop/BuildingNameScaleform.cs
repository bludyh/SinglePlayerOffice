using GTA;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class BuildingNameScaleform : Interaction {

        private static int _buildingNameRenderTargetHandle;
        private static Scaleform _buildingNameScaleform;

        public override void Update() {
            switch (State) {
                case 0:

                    if (_buildingNameScaleform == null) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text")) {
                            Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_office_text", 0);

                            if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, -2082168399)) {
                                Function.Call(Hash.LINK_NAMED_RENDERTARGET, -2082168399);
                                _buildingNameRenderTargetHandle =
                                    Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "prop_ex_office_text");
                            }
                        }

                        State = 1;
                    }
                    else {
                        Function.Call(Hash.SET_TEXT_RENDER_ID, _buildingNameRenderTargetHandle);
                        Function.Call(Hash._SET_2D_LAYER, 4);
                        Function.Call(Hash._0xC6372ECD45D73BCD, 1);
                        Function.Call(Hash._0xB8A850F20A067EB6, 73, 73);
                        Function.Call(Hash.DRAW_SCALEFORM_MOVIE, _buildingNameScaleform.Handle, 0.196f, 0.245f, 0.46f,
                            0.66f, 255, 255, 255, 255, 0);
                        Function.Call(Hash.SET_TEXT_RENDER_ID,
                            Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                        Function.Call(Hash._0xE3A3DB414A373DAB);
                    }

                    break;
                case 1:
                    _buildingNameScaleform = new Scaleform("ORGANISATION_NAME");

                    if (_buildingNameScaleform.IsLoaded) {
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, _buildingNameScaleform.Handle,
                            "SET_ORGANISATION_NAME");
                        Function.Call((Hash) 8646405517797544368, SinglePlayerOffice.CurrentBuilding.Name);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                        Function.Call((Hash) 3671366047641330747, _buildingNameScaleform.Handle, 1);
                        State = 0;
                    }

                    break;
            }
        }

        public override void Reset() {
            base.Reset();

            _buildingNameScaleform?.Dispose();
            _buildingNameScaleform = null;
        }

        public override void Dispose() {
            _buildingNameScaleform?.Dispose();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text"))
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_office_text");
        }

    }

}