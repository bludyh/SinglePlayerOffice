using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class BuildingNameScaleform : Interaction {

        private static int buildingNameRenderTargetHandle;
        private static Scaleform buildingNameScaleform;

        public override void Update() {
            var currentBuilding = Utilities.CurrentBuilding;
            switch (State) {
                case 0:
                    if (buildingNameScaleform == null) {
                        if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text")) {
                            Function.Call(Hash.REGISTER_NAMED_RENDERTARGET, "prop_ex_office_text", 0);
                            if (!Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_LINKED, -2082168399)) {
                                Function.Call(Hash.LINK_NAMED_RENDERTARGET, -2082168399);
                                buildingNameRenderTargetHandle = Function.Call<int>(Hash.GET_NAMED_RENDERTARGET_RENDER_ID, "prop_ex_office_text");
                            }
                        }
                        State = 1;
                    }
                    else {
                        Function.Call(Hash.SET_TEXT_RENDER_ID, buildingNameRenderTargetHandle);
                        Function.Call(Hash._SET_2D_LAYER, 4);
                        Function.Call(Hash._0xC6372ECD45D73BCD, 1);
                        Function.Call(Hash._0xB8A850F20A067EB6, 73, 73);
                        Function.Call(Hash.DRAW_SCALEFORM_MOVIE, buildingNameScaleform.Handle, 0.196f, 0.245f, 0.46f, 0.66f, 255, 255, 255, 255, 0);
                        Function.Call(Hash.SET_TEXT_RENDER_ID, Function.Call<int>(Hash.GET_DEFAULT_SCRIPT_RENDERTARGET_RENDER_ID));
                        Function.Call(Hash._0xE3A3DB414A373DAB);
                    }
                    break;
                case 1:
                    buildingNameScaleform = new Scaleform("ORGANISATION_NAME");
                    if (buildingNameScaleform.IsLoaded) {
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, buildingNameScaleform.Handle, "SET_ORGANISATION_NAME");
                        Function.Call((Hash)8646405517797544368, currentBuilding.Name);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 0);
                        Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                        Function.Call((Hash)3671366047641330747, buildingNameScaleform.Handle, 1);
                        State = 0;
                    }
                    break;
            }
        }

        public override void Dispose() {
            buildingNameScaleform?.Dispose();
            if (Function.Call<bool>(Hash.IS_NAMED_RENDERTARGET_REGISTERED, "prop_ex_office_text"))
                Function.Call(Hash.RELEASE_NAMED_RENDERTARGET, "prop_ex_office_text");
        }

    }
}
