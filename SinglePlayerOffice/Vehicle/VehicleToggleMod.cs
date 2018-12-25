using System.Xml.Serialization;

namespace SinglePlayerOffice.Vehicles {
    public class VehicleToggleMod {
        public VehicleToggleMod() { }

        public VehicleToggleMod(GTA.VehicleToggleMod mod, bool isOn) {
            Mod = mod;
            IsOn = isOn;
        }

        [XmlElement(Namespace = "VehicleToggleMod")]
        public GTA.VehicleToggleMod Mod { get; set; }

        public bool IsOn { get; set; }
    }
}