using System.Collections.Generic;
using GTA;
using GTA.Math;
using SinglePlayerOffice.Interactions;

namespace SinglePlayerOffice.Buildings {

    internal abstract class Location {

        public Vector3 TriggerPos { get; set; }
        public Vector3 SpawnPos { get; set; }
        public float SpawnHeading { get; set; }
        public virtual string RadioEmitter => "SE_Script_Placed_Prop_Emitter_Boombox";
        public List<Interaction> Interactions => GetInteractions();

        protected virtual List<Interaction> GetInteractions() {
            return new List<Interaction>();
        }

        public virtual void OnLocationArrived() { }

        public virtual void OnLocationLeft() {
            foreach (var interaction in Interactions)
                interaction.Reset();
        }

        protected abstract void HandleTrigger();

        public virtual void Update() {
            if (this is IInterior) {
                Game.DisableControlThisFrame(2, Control.CharacterWheel);
                SinglePlayerOffice.CurrentBuilding.HideExteriorMapObjects();
            }

            HandleTrigger();

            foreach (var interaction in Interactions)
                interaction.Update();
        }

        public virtual void Dispose() {
            foreach (var interaction in Interactions)
                interaction.Dispose();
        }

    }

}