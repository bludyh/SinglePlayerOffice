using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class Wardrobe {

        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }

        public Wardrobe(Vector3 pos, Vector3 rot) {
            Position = pos;
            Rotation = rot;
        }

    }
}
