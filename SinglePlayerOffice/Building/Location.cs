using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    abstract class Location {

        public Building Building { get; set; }
        public Vector3 TriggerPos { get; set; }
        public Vector3 SpawnPos { get; set; }
        public float SpawnHeading { get; set; }

        protected abstract void TeleportOnTick();
        public abstract void OnTick();

    }
}
