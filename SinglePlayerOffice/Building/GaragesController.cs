using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class GaragesController : Script {

        public GaragesController() {
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e) {
            Building currentBuilding = SinglePlayerOffice.GetCurrentBuilding();
            if (currentBuilding == null) return;
            switch (currentBuilding.GetCurrentLocation()) {
                case Location.GarageEntrance:
                    currentBuilding.GarageEntrance.OnTick();
                    break;
                case Location.GarageOne:
                    //currentBuilding.GarageOne.OnTick();
                    break;
                case Location.GarageTwo:
                    //currentBuilding.GarageTwo.OnTick();
                    break;
                case Location.GarageThree:
                    //currentBuilding.GarageThree.OnTick();
                    break;
            }
        }

    }
}
