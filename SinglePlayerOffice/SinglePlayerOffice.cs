using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class SinglePlayerOffice : Script {

        public static ScriptSettings Configs { get; private set; }
        public static MenuPool MenuPool { get; private set; }
        public static bool IsHudHidden { get; set; }
        public static Arcadius Arcadius { get; private set; }
        public static LomBank LomBank { get; private set; }
        public static MazeBank MazeBank { get; private set; }
        public static MazeBankWest MazeBankWest { get; private set; }
        public static List<Building> Buildings { get; private set; }

        public SinglePlayerOffice() {
            Tick += OnTick;
            Aborted += OnAborted;
            Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "DLC_IMPORTEXPORT/GARAGE_ELEVATOR", false, -1);
            LoadConfigs();
            LoadMPMap();
            MenuPool = new MenuPool();
            IsHudHidden = false;
            Arcadius = new Arcadius();
            LomBank = new LomBank();
            MazeBank = new MazeBank();
            MazeBankWest = new MazeBankWest();
            Buildings = new List<Building> { Arcadius, LomBank, MazeBank, MazeBankWest };
        }

        private static void LoadConfigs() {
            try { Configs = ScriptSettings.Load(@"scripts\SinglePlayerOffice.ini"); }
            catch (Exception ex) { Logger.Log(ex.Message); }
        }

        private static void LoadMPMap() {
            Function.Call(Hash._LOAD_MP_DLC_MAPS);
            Function.Call(Hash._ENABLE_MP_DLC_MAPS, 1);
        }

        public static void DisplayHelpTextThisFrame(string text) {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
        }

        public static Building GetCurrentBuilding() {
            var currentInteriorID = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);
            foreach (Building building in Buildings) if (Game.Player.Character.Position.DistanceTo(building.Entrance.TriggerPos) < 10f || building.InteriorIDs.Contains(currentInteriorID) || Game.Player.Character.Position.DistanceTo(building.HeliPad.TriggerPos) < 10f) return building;
            return null;
        }

        private void OnTick(object sender, EventArgs e) {
            MenuPool.ProcessMenus();
            if (IsHudHidden) Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME);
            var currentBuilding = GetCurrentBuilding();
            if (currentBuilding != null) currentBuilding.OnTick();
        }

        private void OnAborted(object sender, EventArgs e) {
            foreach (Building building in Buildings) building.Dispose();
            World.RenderingCamera = null;
            World.DestroyAllCameras();
            Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_IMPORTEXPORT/GARAGE_ELEVATOR");
        }

    }
}
