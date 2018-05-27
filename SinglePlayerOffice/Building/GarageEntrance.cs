using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class GarageEntrance {

        public Vector3 Trigger { get; set; }
        public Vector3 Spawn { get; set; }
        public float Heading { get; set; }
        public Vector3 CamPos { get; set; }
        public Vector3 CamRot { get; set; }
        public float CamFOV { get; set; }
        public Camera Cam { get; set; }
        public int Status { get; set; }

        public void OnTick() {
            switch (Status) {
                case 1:
                    Cam = World.CreateCamera(CamPos, CamRot, CamFOV);
                    World.RenderingCamera = Cam;
                    Game.Player.Character.Task.DriveTo(Game.Player.Character.CurrentVehicle, Trigger, 1f, 5f);
                    Status = 2;
                    break;
                case 2:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x21d33957) == 1) break;
                    Function.Call(Hash._0x260BE8F09E326A20, Game.Player.Character.CurrentVehicle, 3f, 1, 0);
                    Game.Player.Character.Task.StandStill(-1);
                    break;
            }
        }

    }
}
