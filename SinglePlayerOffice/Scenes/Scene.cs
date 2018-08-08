using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    abstract class Scene {

        public Location Location { get; set; }
        public List<Action> ActiveInteractions { get; private set; }

        public Scene() {
            ActiveInteractions = new List<Action>();
        }

        public virtual void OnTick() {
            foreach (var interaction in ActiveInteractions) {
                interaction();
            }
        }
        public abstract void Dispose();

    }
}
