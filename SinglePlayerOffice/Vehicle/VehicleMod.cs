using System.Xml.Serialization;

namespace SinglePlayerOffice.Vehicles {
    public class VehicleMod {
        public VehicleMod() { }

        public VehicleMod(GTA.VehicleMod mod, int modIndex) {
            Mod = mod;
            ModIndex = modIndex;
        }

        [XmlElement(Namespace = "VehicleMod")] public GTA.VehicleMod Mod { get; set; }

        public int ModIndex { get; set; }
    }
}