using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    public class VehicleToggleMod {

        [XmlElement(Namespace = "VehicleToggleMod")]
        public GTA.VehicleToggleMod Mod { get; set; }
        public bool IsOn { get; set; }

        public VehicleToggleMod() { }
        public VehicleToggleMod(GTA.VehicleToggleMod mod, bool isOn) {
            Mod = mod;
            IsOn = isOn;
        }

    }
}
