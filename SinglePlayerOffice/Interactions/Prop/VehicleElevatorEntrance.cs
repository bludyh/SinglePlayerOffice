using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice.Interactions {
    internal class VehicleElevatorEntrance : Interaction {
        private readonly float cameraFov;

        private readonly Vector3 cameraPos;
        private readonly Vector3 cameraRot;

        private Camera camera;

        public VehicleElevatorEntrance(Vector3 camPos, Vector3 camRot, float camFov) {
            cameraPos = camPos;
            cameraRot = camRot;
            cameraFov = camFov;
        }

        public override void Update() {
            var currentLocation = (GarageEntrance) Utilities.CurrentBuilding.CurrentLocation;
            switch (State) {
                case 1:
                    camera = World.CreateCamera(cameraPos, cameraRot, cameraFov);
                    World.RenderingCamera = camera;
                    Game.Player.Character.Task.DriveTo(Game.Player.Character.CurrentVehicle, currentLocation.TriggerPos,
                        1f, 10f);
                    State = 2;
                    break;
                case 2:
                    World.RenderingCamera.PointAt(Game.Player.Character.CurrentVehicle);
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x21d33957) != 1) {
                        Function.Call(Hash._0x260BE8F09E326A20, Game.Player.Character.CurrentVehicle, 1f, 1, 0);
                        if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0xc572e06a) != 1)
                            Game.Player.Character.Task.StandStill(-1);
                    }

                    break;
            }
        }
    }
}