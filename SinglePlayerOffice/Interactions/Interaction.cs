using GTA.Math;

namespace SinglePlayerOffice.Interactions {
    internal abstract class Interaction {
        protected Vector3 initialPos;
        protected Vector3 initialRot;
        protected int syncSceneHandle;

        public virtual string HelpText { get; }
        public virtual string RejectHelpText { get; }
        public virtual bool IsCreated { get; }
        public int State { get; set; }

        public virtual void Create() { }

        public abstract void Update();

        public virtual void Reset() { }

        public virtual void Dispose() { }
    }
}