using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Garage {

        public string IPL { get; set; }
        public int InteriorID { get; set; }
        public Vector3 Trigger { get; set; }
        public Vector3 Spawn { get; set; }
        public float Heading { get; set; }
        public Vector3 CamPos { get; set; }
        public Vector3 CamRot { get; set; }
        public float CamFOV { get; set; }
        public Camera Cam { get; set; }
        public Vector3 DecorationCamPos { get; set; }
        public Vector3 DecorationCamRot { get; set; }
        public float DecorationCamFOV { get; set; }
        public Camera DecorationCam { get; set; }
        public GarageDecorationStyle DecorationStyle { get; set; }
        public Vector3 LightingCamPos { get; set; }
        public Vector3 LightingCamRot { get; set; }
        public float LightingCamFOV { get; set; }
        public Camera LightingCam { get; set; }
        public GarageLightingStyle LightingStyle { get; set; }
        public Vector3 NumberingCamPos { get; set; }
        public Vector3 NumberingCamRot { get; set; }
        public float NumberingCamFOV { get; set; }
        public Camera NumberingCam { get; set; }
        public GarageNumberingStyle NumberingStyle { get; set; }

    }
}
