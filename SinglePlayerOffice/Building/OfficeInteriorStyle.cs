using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePlayerOffice {
    class OfficeInteriorStyle {

        public string Name { get; private set; }
        public int Price { get; private set; }
        public string IPL { get; private set; }

        public OfficeInteriorStyle(string name, int price, string ipl) {
            Name = name;
            Price = price;
            IPL = ipl;
        }

    }
}
