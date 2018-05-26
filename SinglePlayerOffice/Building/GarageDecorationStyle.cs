using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    class GarageDecorationStyle {

        public string Name { get; private set; }
        public int Price { get; private set; }
        public string PropName { get; private set; }

        public GarageDecorationStyle(string name, int price, string propName) {
            Name = name;
            Price = price;
            PropName = propName;
        }

    }
}
