using GTA;

namespace SinglePlayerOffice.Vehicles {

    public class NeonLight {

        public NeonLight() { }

        public NeonLight(VehicleNeonLight light, bool isOn) {
            Light = light;
            IsOn = isOn;
        }

        public VehicleNeonLight Light { get; set; }
        public bool IsOn { get; set; }

    }

}