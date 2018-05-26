using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Office {

        public List<int> InteriorIDs { get; set; }
        public Vector3 Trigger { get; set; }
        public Vector3 Spawn { get; set; }
        public float Heading { get; set; }
        public Vector3 CamPos { get; set; }
        public Vector3 CamRot { get; set; }
        public float CamFOV { get; set; }
        public Camera Cam { get; set; }
        public List<OfficeInteriorStyle> InteriorStyles { get; set; }
        public OfficeInteriorStyle InteriorStyle { get; set; }
        public bool HasExtraDecors { get; set; }
        public int ExtraDecorsPrice { get; set; }

    }
}
