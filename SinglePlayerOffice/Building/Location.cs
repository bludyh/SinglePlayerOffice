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
        public List<Scene> ActiveScenes { get; private set; }
        public List<Action> ActiveInteractions { get; private set; }

        public Location() {
            ActiveScenes = new List<Scene>();
            ActiveInteractions = new List<Action>();
        }

        protected abstract void TeleportOnTick();
        public virtual void OnTick() {
            foreach (var scene in ActiveScenes) {
                scene.OnTick();
            }
            foreach (var interaction in ActiveInteractions) {
                interaction();
            }
        }

    }
}
