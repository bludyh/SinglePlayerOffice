using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;
using NativeUI;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice {
    internal class SinglePlayerOffice : Script {
        public SinglePlayerOffice() {
            Tick += OnTick;
            Aborted += OnAborted;

            MenuPool = new MenuPool();
            Arcadius = new Arcadius();
            LomBank = new LomBank();
            MazeBank = new MazeBank();
            MazeBankWest = new MazeBankWest();
            Buildings = new List<Building> {Arcadius, LomBank, MazeBank, MazeBankWest};

            Utilities.LoadMpMap();
            Utilities.RequestGameResources();
        }

        public static bool IsHudHidden { get; set; }
        public static MenuPool MenuPool { get; private set; }
        public static Arcadius Arcadius { get; private set; }
        public static LomBank LomBank { get; private set; }
        public static MazeBank MazeBank { get; private set; }
        public static MazeBankWest MazeBankWest { get; private set; }
        public static List<Building> Buildings { get; private set; }

        private static void HandleHudVisibility() {
            if (IsHudHidden)
                Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME);
        }

        private static void HandleTimedNotifications() {
            foreach (var building in Buildings)
                building.HandleConstructionNotification();
        }

        private static void OnTick(object sender, EventArgs e) {
            HandleHudVisibility();
            HandleTimedNotifications();
            MenuPool.ProcessMenus();

            Utilities.CurrentBuilding?.Update();
        }

        private static void OnAborted(object sender, EventArgs e) {
            foreach (var building in Buildings)
                building.Dispose();
            World.RenderingCamera = null;
            World.DestroyAllCameras();
            Game.Player.Character.Task.ClearAll();
            Utilities.ReleaseGameResources();
        }
    }
}