using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Vehicles;

namespace SinglePlayerOffice.Interactions {

    internal class VehicleInfoScaleform : Interaction {

        private static VehicleInfo _vehicleInfo;
        private static Scaleform _scaleform;

        public override void Update() {
            switch (State) {
                case 0:
                    var vehicle = World.GetClosestVehicle(Game.Player.Character.Position, 3f);

                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() && vehicle != null) {
                        if (_vehicleInfo == null || vehicle != _vehicleInfo.Vehicle) {
                            _vehicleInfo = new VehicleInfo(vehicle);
                            _scaleform?.Dispose();
                            State = 1;
                        }
                        else {
                            var pos = vehicle.Position + new Vector3(0f, 0f, vehicle.Model.GetDimensions().Z + 2f);
                            var camCoord = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);
                            var rot = new Vector3(0f, 0f,
                                180f - Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, camCoord.X - pos.X,
                                    camCoord.Y - pos.Y));
                            var scale = new Vector3(8f, 4.5f, 1f);
                            _scaleform.Render3D(pos, rot, scale);
                        }
                    }
                    else {
                        _vehicleInfo = null;
                    }

                    break;
                case 1:
                    _scaleform = new Scaleform("MP_CAR_STATS_01");

                    if (_scaleform.IsLoaded) {
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, _scaleform.Handle,
                            "SET_VEHICLE_INFOR_AND_STATS");
                        Function.Call(Hash._0x80338406F3475E55, "FM_TWO_STRINGS");
                        Function.Call(Hash._0xC63CD5D2920ACBE7, _vehicleInfo.GetBrandName(true));
                        Function.Call(Hash._0xC63CD5D2920ACBE7, _vehicleInfo.Vehicle.DisplayName);
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._0x80338406F3475E55, "MP_PROP_CAR0");
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._0xE83A3E3557A56640, _vehicleInfo.GetLogoTextureDict());
                        Function.Call(Hash._0xE83A3E3557A56640, _vehicleInfo.GetBrandName(false));
                        Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_0");
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_1");
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_2");
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._0x80338406F3475E55, "FMMC_VEHST_3");
                        Function.Call(Hash._0x362E2D3FE93A9959);
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT,
                            Function.Call<int>(Hash.ROUND, _vehicleInfo.GetMaxSpeedInPercentage()));
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT,
                            Function.Call<int>(Hash.ROUND, _vehicleInfo.GetMaxAccelerationInPercentage()));
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT,
                            Function.Call<int>(Hash.ROUND, _vehicleInfo.GetMaxBrakingInPercentage()));
                        Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT,
                            Function.Call<int>(Hash.ROUND, _vehicleInfo.GetMaxTractionInPercentage()));
                        Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                        State = 0;
                    }

                    break;
            }
        }

        public override void Dispose() {
            _scaleform?.Dispose();
        }

    }

}