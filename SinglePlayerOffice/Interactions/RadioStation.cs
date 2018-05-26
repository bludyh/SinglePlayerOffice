using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    class RadioStation {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string GameName { get; private set; }

        public RadioStation(string name, string des, string gameName) {
            Name = name;
            Description = des;
            GameName = gameName;
        }

    }
}
