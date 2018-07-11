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
    public class VehicleMod {

        [XmlElement(Namespace = "VehicleMod")]
        public GTA.VehicleMod Mod { get; set; }
        public int ModIndex { get; set; }

        public VehicleMod() { }
        public VehicleMod(GTA.VehicleMod mod, int modIndex) {
            Mod = mod;
            ModIndex = modIndex;
        }

    }
}
