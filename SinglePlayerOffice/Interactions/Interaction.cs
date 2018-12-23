using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice.Interactions {
    class Elevator {

        public Prop Base { get; set; }
        public Prop Platform { get; set; }

    }
    class ElevatorGate {

        public Prop Prop { get; set; }
        public Vector3 InitialPos { get; set; }

        public ElevatorGate(Prop prop, Vector3 pos) {
            Prop = prop;
            InitialPos = pos;
        }

    }
    class Sofa {

        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }

        public Sofa(Vector3 pos, Vector3 rot) {
            Position = pos;
            Rotation = rot;
        }

    }
    class RadioStation {

        public static List<RadioStation> Stations { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string GameName { get; private set; }

        static RadioStation() {
            Stations = new List<RadioStation> {
                new RadioStation("Los Santos Rock Radio", "Get on the good ship power pop and rock.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.LosSantosRockRadio)),
                new RadioStation("Non-Stop-Pop FM", "Pop hits from the 80s, 90s, noughties, and today.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.NonStopPopFM)),
                new RadioStation("Radio Los Santos", "Contemporary Hip Hop blazing into your stereo.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.RadioLosSantos)),
                new RadioStation("Channel X", "Real Punk. Real West Coast.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.ChannelX)),
                new RadioStation("West Coast Talk Radio", "Real Talk. Real Issues. Real Patronizing. Radio that’s all about you.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.WestCoastTalkRadio)),
                new RadioStation("Rebel Radio", "The true sound of Blaine County - drunk, armed, and ready to party.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.RebelRadio)),
                new RadioStation("Soulwax FM", "A continuous mix of dance and electronica.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.SoulwaxFM)),
                new RadioStation("East Los FM", "Mexican electronica mixing corridos and traditional songs with hip hop, rock, and ska.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.EastLosFM)),
                new RadioStation("West Coast Classics", "Coming at you from the city of Davis - music from the days of the pager.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.WestCoastClassics)),
                new RadioStation("Blue Ark", "The hottest reggae, dancehall and dub served up by The Upsetter.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.TheBlueArk)),
                new RadioStation("WorldWide FM", "Join Gilles Peterson as he brings that perfect beat to Los Santos.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.WorldWideFM)),
                new RadioStation("FlyLo FM", "A mix to carry you through the Los Santos freeways at top speed.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.FlyloFM)),
                new RadioStation("The Lowdown 91.1", "The groove, the soul, the R&B. You dig?", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.TheLowdown)),
                new RadioStation("The Lab", "Dropping science with Dr. No and the Chemical Bro.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.TheLab)),
                new RadioStation("Radio Mirror Park", "Indie modern rock from the underground.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.RadioMirrorPark)),
                new RadioStation("Space 103.2", "Bringing the Funk to your Assdroid.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.Space)),
                new RadioStation("Vinewood Boulevard Radio", "The soundtrack to your broken dreams and unspent potential.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.VinewoodBoulevardRadio)),
                new RadioStation("blonded Los Santos 97.8 FM", "An eclectic mix of savory soul, hip hop and deep cuts.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.BlondedLosSantos)),
                new RadioStation("Blaine County Radio", "The home of the patriot. Enough said.", Function.Call<string>(Hash.GET_RADIO_STATION_NAME, (int)GTA.RadioStation.BlaineCountyRadio))
            };
        }

        public RadioStation(string name, string des, string gameName) {
            Name = name;
            Description = des;
            GameName = gameName;
        }

    }
    class Wardrobe {

        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }

        public Wardrobe(Vector3 pos, Vector3 rot) {
            Position = pos;
            Rotation = rot;
        }

    }
    abstract class Interaction {

        protected Vector3 initialPos;
        protected Vector3 initialRot;
        protected int syncSceneHandle;

        public virtual string HelpText { get; }
        public virtual string RejectHelpText { get; }
        public int State { get; set; }

        public abstract void Update();

        public virtual void Reset() { }

        public virtual void Dispose() { }

    }
}
