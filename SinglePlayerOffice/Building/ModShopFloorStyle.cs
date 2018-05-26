using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    class ModShopFloorStyle {

        public string Name { get; private set; }
        public int Price { get; private set; }
        public string PropName { get; private set; }

        public ModShopFloorStyle(string name, int price, string propName) {
            Name = name;
            Price = price;
            PropName = propName;
        }

    }
}
