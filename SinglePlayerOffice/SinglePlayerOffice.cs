using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice {

    internal class SinglePlayerOffice : Script {

        public SinglePlayerOffice() {
            Tick += OnTick;
            KeyUp += OnKeyUp;
            Aborted += OnAborted;

            Arcadius = new Arcadius();
            LomBank = new LomBank();
            MazeBank = new MazeBank();
            MazeBankWest = new MazeBankWest();
            Buildings = new List<Building> { Arcadius, LomBank, MazeBank, MazeBankWest };

            Utilities.LoadMpMap();
        }

        public static Arcadius Arcadius { get; private set; }
        public static LomBank LomBank { get; private set; }
        public static MazeBank MazeBank { get; private set; }
        public static MazeBankWest MazeBankWest { get; private set; }
        public static List<Building> Buildings { get; private set; }
        public static Building CurrentBuilding => GetCurrentBuilding();

        private static Building GetCurrentBuilding() {
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);

            foreach (var building in Buildings)
                if (Game.Player.Character.Position.DistanceTo(building.Entrance.TriggerPos) < 10f
                    || building.InteriorIDs.Contains(currentInteriorId)
                    || Game.Player.Character.Position.DistanceTo(building.HeliPad.TriggerPos) < 10f)
                    return building;

            return null;
        }

        private static void HandleTimedNotifications() {
            foreach (var building in Buildings)
                building.HandleConstructionNotification();
        }

        private static void OnTick(object sender, EventArgs e) {
            HandleTimedNotifications();

            UI.HandleHudVisibility();
            UI.HandleMenus();

            CurrentBuilding?.Update();
        }

        //Debug begin
        private static void OnKeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F5) {
            }
        }

        //Debug end

        private static void OnAborted(object sender, EventArgs e) {
            foreach (var building in Buildings)
                building.Dispose();

            World.RenderingCamera = null;
            World.DestroyAllCameras();
            Game.Player.Character.Task.ClearAll();

            if (Game.IsScreenFadedOut)
                Game.FadeScreenIn(1000);
        }

    }

}