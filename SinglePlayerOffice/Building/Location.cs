using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Scenes;

namespace SinglePlayerOffice.Buildings {
    abstract class Location {

        public Vector3 TriggerPos { get; set; }
        public Vector3 SpawnPos { get; set; }
        public float SpawnHeading { get; set; }
        public virtual string RadioEmitter {
            get {
                return "SE_Script_Placed_Prop_Emitter_Boombox";
            }
        }

        public virtual void OnLocationArrived() { }

        public virtual void OnLocationLeft() { }

        protected abstract void HandleTrigger();

        public virtual void Update() {
            if (this is IInterior) {
                Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                Utilities.CurrentBuilding.HideExteriorMapObjects();
            }

            HandleTrigger();
        }

        public virtual void Dispose() { }

    }
}
