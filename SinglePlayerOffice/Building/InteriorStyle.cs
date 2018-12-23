using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Buildings {
    class InteriorStyle {

        public string Name { get; private set; }
        public int Price { get; private set; }
        public string Style { get; private set; }

        public InteriorStyle(string name, int price, string style) {
            Name = name;
            Price = price;
            Style = style;
        }

    }
}
