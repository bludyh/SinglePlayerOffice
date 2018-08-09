using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    public class VehicleInfo {

        [XmlIgnore]
        public Vehicle Vehicle { get; private set; }
        public int VehicleHash { get; set; }
        public string VehicleName { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public int DashboardColor { get; set; }
        public int PearlescentColor { get; set; }
        public int RimColor { get; set; }
        public int TrimColor { get; set; }
        public int TireSmokeColorR { get; set; }
        public int TireSmokeColorG { get; set; }
        public int TireSmokeColorB { get; set; }
        public int TireSmokeColorA { get; set; }
        public int NeonLightsColorR { get; set; }
        public int NeonLightsColorG { get; set; }
        public int NeonLightsColorB { get; set; }
        public int NeonLightsColorA { get; set; }
        public int Livery { get; set; }
        public int WindowTint { get; set; }
        public int WheelType { get; set; }
        public int RoofState { get; set; }
        public int LockStatus { get; set; }
        public int NumberPlateType { get; set; }
        public string NumberPlate { get; set; }
        public bool RightHeadLightBroken { get; set; }
        public bool LeftHeadLightBroken { get; set; }
        public bool CanTiresBurst { get; set; }
        public float PetrolTankHealth { get; set; }
        public float BodyHealth { get; set; }
        public float DirtLevel { get; set; }
        public float EngineHealth { get; set; }
        public float FuelLevel { get; set; }
        public List<NeonLight> NeonLights { get; set; }
        public List<VehicleMod> VehicleMods { get; set; }
        public List<VehicleToggleMod> VehicleToggleMods { get; set; }

        public VehicleInfo() { }

        public VehicleInfo(Vehicle vehicle) {
            Vehicle = vehicle;
            VehicleHash = vehicle.Model.Hash;
            VehicleName = vehicle.DisplayName;
            PrimaryColor = (int)vehicle.PrimaryColor;
            SecondaryColor = (int)vehicle.SecondaryColor;
            DashboardColor = (int)vehicle.DashboardColor;
            PearlescentColor = (int)vehicle.PearlescentColor;
            RimColor = (int)vehicle.RimColor;
            TrimColor = (int)vehicle.TrimColor;
            TireSmokeColorR = vehicle.TireSmokeColor.R;
            TireSmokeColorG = vehicle.TireSmokeColor.G;
            TireSmokeColorB = vehicle.TireSmokeColor.B;
            TireSmokeColorA = vehicle.TireSmokeColor.A;
            NeonLightsColorR = vehicle.NeonLightsColor.R;
            NeonLightsColorG = vehicle.NeonLightsColor.G;
            NeonLightsColorB = vehicle.NeonLightsColor.B;
            NeonLightsColorA = vehicle.NeonLightsColor.A;
            Livery = vehicle.Livery;
            WindowTint = (int)vehicle.WindowTint;
            WheelType = (int)vehicle.WheelType;
            RoofState = (int)vehicle.RoofState;
            LockStatus = (int)vehicle.LockStatus;
            NumberPlateType = (int)vehicle.NumberPlateType;
            NumberPlate = vehicle.NumberPlate;
            RightHeadLightBroken = vehicle.RightHeadLightBroken;
            LeftHeadLightBroken = vehicle.LeftHeadLightBroken;
            CanTiresBurst = vehicle.CanTiresBurst;
            PetrolTankHealth = vehicle.PetrolTankHealth;
            BodyHealth = vehicle.BodyHealth;
            DirtLevel = vehicle.DirtLevel;
            EngineHealth = vehicle.EngineHealth;
            FuelLevel = vehicle.FuelLevel;
            NeonLights = GetNeonLights();
            VehicleMods = GetMods();
            VehicleToggleMods = GetToggleMods();
        }

        public string GetBrandName(bool p2) {
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "khamelion")) {
                return "HIJAK";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "issi2")) {
                return "WEENY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "elegy2")) {
                return "ANNIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "romero")) {
                return "CHARIOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller4") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller5") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller6")) {
                if (p2) {
                    return "GALLIVAN";
                }
                return "GALIVANTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "surfer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "surfer2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dune") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bfinjection")) {
                return "BF";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "feltzer2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dubsta") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "surano") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schwarzer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schafter2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "serrano") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dubsta2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "feltzer3")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sentinel") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sentinel2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "zion") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "zion2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "oracle") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "oracle2")) {
                if (p2) {
                    return "UBERMACH";
                }
                return "UBERMACHT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ztype") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "adder")) {
                return "TRUFFADE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jb700") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rapidgt") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rapidgt2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "exemplar") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "massacro") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "massacro2")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tailgater") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ninef") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ninef2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rocoto")) {
                return "OBEY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "picador") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "surge") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fugitive") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "marshall")) {
                return "CHEVAL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mower")) {
                if (p2) {
                    return "JACKSHP";
                }
                return "JACKSHEEPE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tornado") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tornado2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tornado3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "burrito") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "burrito2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "premier") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "voodoo2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sabregt") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rancherxl") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vigero") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "asea") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "asea2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "granger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pranger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sheriff") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sheriff2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gburrito") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gburrito2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stalion")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "buccaneer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cavalcade") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cavalcade2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "emperor") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "emperor2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "manana") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "primo") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "washington") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "virgo")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "banshee") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bison") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gresley") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "youga") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gauntlet") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "buffalo") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "buffalo2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ratloader") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dloader") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ratloader2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rumpo") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "banshee2")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stinger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stingergt") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cheetah") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "carbonizzare")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "coquette") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "coquette3")) {
                if (p2) {
                    return "INVERTO";
                }
                return "Invetero";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "radi") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sadler") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dominator") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sandking") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sandking2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "police") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "police3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "policet") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "benson") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bullet") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "minivan") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "speedo") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "speedo2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "peyote") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "towtruck") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "towtruck2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bobcatxl") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stanier") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hotknife") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "slamvan") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "guardian") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "chino")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tiptruck") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "taco") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "utillitruck") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "utillitruck2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "utillitruck3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "camper") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "riot") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tourbus") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ambulance") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stockade") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "boxville") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pony")) {
                return "BRUTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "prairie")) {
                return "BOLLOKAN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jackal") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "f620")) {
                return "OCELOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mesa") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mesa3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bodhi2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "seminole") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "crusader")) {
                return "CANIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "entityxf")) {
                return "OVERFLOD";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "monroe") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "infernus") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bati") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bati2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vacca") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ruffian") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faggio2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "osiris")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "phoenix") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ruiner") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dukes") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dukes2")) {
                return "IMPONTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bjxl") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rebel") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rebel2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "asterope") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "intruder") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "futo") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sultan") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dilettante") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dilettante2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "kuruma") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "kuruma2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sultanrs")) {
                return "KARIN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "penumbra") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sanchez") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sanchez2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mule")) {
                return "MAIBATSU";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blista") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blista2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blista3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "double") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jester") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jester2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "enduro") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vindicator") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "akuma")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fq2")) {
                return "FATHOM";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "voltic") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "brawler")) {
                return "COIL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "felon") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "felon2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "casco")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "comet2")) {
                return "PFISTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fusilade")) {
                return "SCHYSTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stretch") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "regina") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "landstalker")) {
                if (p2) {
                    return "DUNDREAR";
                }
                return "DUNDREARY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "handler") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bulldozer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "docktug") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cutter") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mixer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mixer2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "barracks") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "barracks2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "biff") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "forklift") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ripley") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "airtug") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dump") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "insurgent2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "insurgent")) {
                return "HVY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "packer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "flatbed") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tiptruck2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pounder") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "firetruk")) {
                return "MTL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tractor") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tractor2")) {
                return "STANLEY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hauler") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "phantom") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "trash")) {
                return "JOBUILT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "patriot")) {
                return "MAMMOTH";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "journey") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stratum")) {
                if (p2) {
                    return "ZIRCONIU";
                }
                return "ZIRCONIUM";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vader") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pcj")) {
                return "SHITZU";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bagger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "daemon") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sovereign")) {
                if (p2) {
                    return "WESTERN";
                }
                return "WESTERNMOTORCYCLE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blazer") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "caddy") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "carbonrs") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blazer3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blazer2")) {
                return "NAGASAKI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nemesis") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "lectro")) {
                if (p2) {
                    return "PRINCIPL";
                }
                return "PRINCIPE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hexer")) {
                return "LCC";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bmx") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cruiser") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "scorcher")) {
                if (!p2) {
                    return "Ped";
                }
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tribike") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tribike2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tribike3")) {
                if (!p2) {
                    return "TriCycles";
                }
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cogcabrio") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "superd") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "windsor")) {
                return "ENUS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "habanero")) {
                if (p2) {
                    return "EMPEROR";
                }
                return "EMPORER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ingot")) {
                return "VULCAR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "t20")) {
                return "PROGEN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bifta")) {
                return "BF";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "kalahari")) {
                return "CANIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "paradise")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "btype")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "zentorno")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jester")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "massacro")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "turismor")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "huntley")) {
                return "ENUS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "alpha")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "thrust")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sovereign")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "thrust")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blade") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "monster")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "warrener")) {
                return "VULCAR";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "glendale") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "panto")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dubsta3")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rhapsody")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pigalle")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "coquette2")) {
                if (p2) {
                    return "INVERTO";
                }
                return "Invetero";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "innovation")) {
                return "LCC";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hakuchou")) {
                return "SHITZU";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "furoregt")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ratloader2")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "slamvan")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jester2")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "massacro2")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "windsor")) {
                return "ENUS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "chino") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "chino2")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vindicator")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "virgo")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "swift2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "luxor2")) {
                return "BUCKING";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "feltzer3")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "t20")) {
                return "PROGEN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "osiris")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "coquette3")) {
                if (p2) {
                    return "INVERTO";
                }
                return "Invetero";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "toro")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "brawler")) {
                return "COIL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "primo2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "buccaneer2")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faction") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faction2")) {
                return "WILLARD";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "moonbeam2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "voodoo")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "moonbeam")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "chino2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dukes2")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faction3")) {
                return "WILLARD";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sabregt2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tornado5")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "virgo")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "virgo2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "virgo3")) {
                if (p2) {
                    return "DUNDREAR";
                }
                return "DUNDREARY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "slamvan3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "minivan2")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "lurcher") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "btype2")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "mamba") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tampa")) {
                return "DECLASSE";
            }
            if (((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cognoscenti") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cog55")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cog552")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cognoscenti2")) {
                return "ENUS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "verlierer2")) {
                return "BRAVADO";
            }
            if (((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schafter4") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schafter3")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schafter5")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "schafter6")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller4")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller5")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "baller6")) {
                if (p2) {
                    return "GALLIVAN";
                }
                return "GALIVANTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nightshade")) {
                return "IMPONTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "btype3")) {
                return "ALBANY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pfister811")) {
                return "PFISTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "seven70")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rumpo3")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bestiagts")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "prototipo")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "xls") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "xls2")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fmj")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "windsor2")) {
                return "ENUS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "reaper")) {
                return "PEGASSI";
            }
            if (((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "contender") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "trophytruck")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "trophytruck2")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dominator2")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "bf400")) {
                return "NAGASAKI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cliffhanger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gargoyle")) {
                if (p2) {
                    return "WESTERN";
                }
                return "WESTERNMOTORCYCLE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "buffalo3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gauntlet2")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "omnis")) {
                return "OBEY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "le7b")) {
                return "ANNIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tropos")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tampa2") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stalion2")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "brioso")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tyrus")) {
                return "PROGEN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "lynx")) {
                return "OCELOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sheava")) {
                if (p2) {
                    return "EMPEROR";
                }
                return "EMPORER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rallytruck")) {
                return "MTL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tornado6")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "avarus") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sanctus")) {
                return "LCC";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "chimera") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "shotaro")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blazer4")) {
                return "NAGASAKI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "defiler") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hakuchou2")) {
                return "SHITZU";
            }
            if (((((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nightblade") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "zombiea")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "zombieb")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "daemon2")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ratbike")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "wolfsbane")) {
                if (p2) {
                    return "WESTERN";
                }
                return "WESTERNMOTORCYCLE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "youga2")) {
                return "BRAVADO";
            }
            if (((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "esskey") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vortex")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faggio3")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "faggio")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "raptor")) {
                return "BF";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "manchez")) {
                return "MAIBATSU";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "blazer5")) {
                return "NAGASAKI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "comet3")) {
                return "PFISTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "diablous") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "diablous2")) {
                if (p2) {
                    return "PRINCIPL";
                }
                return "PRINCIPE";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fcr") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fcr2")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tempesta")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nero") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nero2")) {
                return "TRUFFADE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "penetrator")) {
                return "OCELOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ruiner2")) {
                return "IMPONTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "technical2")) {
                return "KARIN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "phantom2")) {
                return "JOBUILT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "voltic2")) {
                return "COIL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "wastelander")) {
                return "MTL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "italigtb") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "italigtb2")) {
                return "PROGEN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dune5") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dune4")) {
                return "BF";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "elegy") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "elegy2")) {
                return "ANNIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "specter") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "specter2")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gp1")) {
                return "PROGEN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "infernus2")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ruston")) {
                return "HIJAK";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "turismo2")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dukes2")) {
                return "IMPONTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ardent") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "xa21")) {
                return "OCELOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cheetah2")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "insurgent3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "nightshark")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "apc")) {
                return "HVY";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "technical3")) {
                return "KARIN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "halftrack")) {
                return "BRAVADO";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "torero") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "oppressor")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dune3")) {
                return "BF";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tampa3")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vagner") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "rapidgt3")) {
                if (p2) {
                    return "DEWBAUCH";
                }
                return "DEWBAUCHEE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cyclone")) {
                return "COIL";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "retinue") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hustler")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "riata")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "visione") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vigilante")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "z190")) {
                return "KARIN";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "avenger") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "thruster")) {
                return "MAMMOTH";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "deluxo")) {
                return "IMPONTE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "stromberg") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "pariah")) {
                return "OCELOT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hermes")) {
                return "ALBANY";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sentinel3") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "sc1")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "revolter")) {
                if (p2) {
                    return "UBERMACH";
                }
                return "UBERMACHT";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "savestra")) {
                return "ANNIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "yosemite")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "raiden")) {
                return "COIL";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "neon") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "comet4")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "comet5")) {
                return "PFISTER";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "streiter")) {
                if (p2) {
                    return "BENEFAC";
                }
                return "BENEFACTOR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "kamacho")) {
                return "CANIS";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gt500")) {
                if (p2) {
                    return "GROTTI";
                }
                return "Grotti_2";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "viseris")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "barrage")) {
                return "HVY";
            }
            if ((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "autarch") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tyrant")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "entity2")) {
                return "OVERFLOD";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "issi3")) {
                return "WEENY";
            }
            if ((((Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "gb200") || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "ellie")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "flashgt")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "caracara")) || Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "dominator3")) {
                return "VAPID";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "fagaloa")) {
                return "VULCAR";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "michelli")) {
                if (p2) {
                    return "LAMPADA";
                }
                return "LAMPADATI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "hotring")) {
                return "DECLASSE";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tezeract")) {
                return "PEGASSI";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "jester3")) {
                return "DINKA";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "taipan")) {
                return "CHEVAL";
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "cheburek")) {
                return "RUNE";
            }
            return "";
        }

        public string GetLogoTextureDict() {
            string brandName = GetBrandName(false);
            if (brandName == "LCC" || brandName == "Grotti_2" || brandName == "PROGEN" || brandName == "RUNE") {
                return "MPCarHUD2";
            }
            return "MPCarHUD";
        }

        public float GetMaxSpeedInPercentage() {
            float maxSpeed = Function.Call<float>(Hash._0x53AF99BAA671CA47, Vehicle);
            float percentage = (maxSpeed / 52.0588235294f) * 100f;
            if (percentage > 100f) return 100f;
            return percentage;
        }

        public float GetMaxAccelerationInPercentage() {
            float maxAcceleration = Function.Call<float>(Hash._0x5DD35C8D074E57AE, Vehicle);
            if (Vehicle.Model.IsBicycle) {
                maxAcceleration *= 0.5f;
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "voltic")) {
                maxAcceleration *= 2f;
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "tezeract")) {
                maxAcceleration *= 2.6753f;
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "t20")) {
                maxAcceleration -= 0.05f;
            }
            if (Vehicle.Model.Hash == Function.Call<int>(Hash.GET_HASH_KEY, "vindicator")) {
                maxAcceleration -= 0.02f;
            }
            float percentage = (maxAcceleration / 0.45625f) * 100f;
            if (percentage > 100f) return 100f;
            return percentage;
        }

        public float GetMaxBrakingInPercentage() {
            float maxBraking = Function.Call<float>(Hash._0xAD7E85FC227197C4, Vehicle);
            if (Vehicle.Model.IsBicycle) {
                maxBraking *= 0.5f;
            }
            float percentage = (maxBraking / 2.97297297297f) * 100f;
            if (percentage > 100f) return 100f;
            return percentage;
        }

        public float GetMaxTractionInPercentage() {
            float maxTraction = Function.Call<float>(Hash._0xA132FB5370554DB0, Vehicle);
            if (Vehicle.Model.IsBicycle) {
                maxTraction *= 0.5f;
            }
            float percentage = (maxTraction / 3.30864197531f) * 100f;
            if (percentage > 100f) return 100f;
            return percentage;
        }

        public List<NeonLight> GetNeonLights() {
            var neonLights = new List<NeonLight>();
            foreach (VehicleNeonLight light in Enum.GetValues(typeof(VehicleNeonLight))) {
                var neonLight = new NeonLight(light, Vehicle.IsNeonLightsOn(light));
                neonLights.Add(neonLight);
            }
            return neonLights;
        }

        public List<VehicleMod> GetMods() {
            var mods = new List<VehicleMod>();
            foreach (GTA.VehicleMod vehicleMod in Enum.GetValues(typeof(GTA.VehicleMod))) {
                var mod = new VehicleMod(vehicleMod, Vehicle.GetMod(vehicleMod));
                mods.Add(mod);
            }
            return mods;
        }

        public List<VehicleToggleMod> GetToggleMods() {
            var toggleMods = new List<VehicleToggleMod>();
            foreach (GTA.VehicleToggleMod vehicleToggleMod in Enum.GetValues(typeof(GTA.VehicleToggleMod))) {
                var toggleMod = new VehicleToggleMod(vehicleToggleMod, Vehicle.IsToggleModOn(vehicleToggleMod));
                toggleMods.Add(toggleMod);
            }
            return toggleMods;
        }

        public void CreateVehicle() {
            var vehicle = World.CreateVehicle((VehicleHash)VehicleHash, Position, Rotation.Z);
            vehicle.PrimaryColor = (VehicleColor)PrimaryColor;
            vehicle.SecondaryColor = (VehicleColor)SecondaryColor;
            vehicle.DashboardColor = (VehicleColor)DashboardColor;
            vehicle.PearlescentColor = (VehicleColor)PearlescentColor;
            vehicle.RimColor = (VehicleColor)RimColor;
            vehicle.TrimColor = (VehicleColor)TrimColor;
            vehicle.TireSmokeColor = Color.FromArgb(TireSmokeColorA, TireSmokeColorR, TireSmokeColorG, TireSmokeColorB);
            vehicle.NeonLightsColor = Color.FromArgb(NeonLightsColorA, NeonLightsColorR, NeonLightsColorG, NeonLightsColorB);
            vehicle.Livery = Livery;
            vehicle.WindowTint = (VehicleWindowTint)WindowTint;
            vehicle.WheelType = (VehicleWheelType)WheelType;
            vehicle.RoofState = (VehicleRoofState)RoofState;
            if (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character) != (int)SinglePlayerOffice.GetCurrentBuilding().Owner) vehicle.LockStatus = VehicleLockStatus.Locked;
            else vehicle.LockStatus = (VehicleLockStatus)LockStatus;
            vehicle.NumberPlateType = (NumberPlateType)NumberPlateType;
            vehicle.NumberPlate = NumberPlate;
            vehicle.RightHeadLightBroken = RightHeadLightBroken;
            vehicle.LeftHeadLightBroken = LeftHeadLightBroken;
            vehicle.CanTiresBurst = CanTiresBurst;
            vehicle.PetrolTankHealth = PetrolTankHealth;
            vehicle.BodyHealth = BodyHealth;
            vehicle.DirtLevel = DirtLevel;
            vehicle.EngineHealth = EngineHealth;
            vehicle.FuelLevel = FuelLevel;
            vehicle.InstallModKit();
            foreach (var neonLight in NeonLights) {
                vehicle.SetNeonLightsOn(neonLight.Light, neonLight.IsOn);
            }
            foreach (var vehicleMod in VehicleMods) {
                vehicle.SetMod(vehicleMod.Mod, vehicleMod.ModIndex, true);
            }
            foreach (var vehicleToggleMod in VehicleToggleMods) {
                vehicle.ToggleMod(vehicleToggleMod.Mod, vehicleToggleMod.IsOn);
            }
            Vehicle = vehicle;
        }

        public void DeleteVehicle() {
            if (Vehicle != null) Vehicle.Delete();
        }

    }
}
