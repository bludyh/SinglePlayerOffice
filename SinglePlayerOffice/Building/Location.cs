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

        protected Location() {
            ActiveScenes = new List<Scene>();
            ActiveInteractions = new List<Action>();
        }

        protected abstract void TeleportOnTick();

        protected virtual void OnArrival() {
            if (Building == null)
                Building = SinglePlayerOffice.GetCurrentBuilding();

            foreach (var scene in ActiveScenes)
                scene.OnSceneStarted();
        }

        public void OnTick() {
            if (Utilities.LastLocation != this) {
                Utilities.LastLocation = this;
                OnArrival();
            }

            if (this is IInterior) {
                Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                Building.HideExteriorMapObjects();
            }

            foreach (var scene in ActiveScenes)
                scene.OnTick();

            foreach (var interaction in ActiveInteractions)
                interaction();
        }

        public virtual void Dispose() { }

    }
}
