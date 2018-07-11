using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    public class NeonLight {

        public VehicleNeonLight Light { get; set; }
        public bool IsOn { get; set; }

        public NeonLight() { }
        public NeonLight(VehicleNeonLight light, bool isOn) {
            Light = light;
            IsOn = isOn;
        }

    }
}
