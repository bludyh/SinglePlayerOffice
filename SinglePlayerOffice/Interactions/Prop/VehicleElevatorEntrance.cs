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
    class GarageEntranceInteraction : Interaction {

        private Camera camera;

        public Vector3 CameraPos { get; set; }
        public Vector3 CameraRot { get; set; }
        public float CameraFOV { get; set; }

        public override void Update() {
            var currentLocation = (GarageEntrance)Utilities.CurrentBuilding.CurrentLocation;
            switch (State) {
                case 1:
                    camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV);
                    World.RenderingCamera = camera;
                    Game.Player.Character.Task.DriveTo(Game.Player.Character.CurrentVehicle, currentLocation.TriggerPos, 1f, 10f);
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
