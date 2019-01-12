using System;
using GTA;
using GTA.Math;
using GTA.Native;
using SinglePlayerOffice.Buildings;

namespace SinglePlayerOffice {
    internal static class Utilities {
        public static Vector3 SavedPos { get; set; }
        public static Vector3 SavedRot { get; set; }
        public static Building CurrentBuilding => GetCurrentBuilding();

        public static void LoadMpMap() {
            Function.Call(Hash._LOAD_MP_DLC_MAPS);
            Function.Call(Hash._ENABLE_MP_DLC_MAPS, 1);
            SpawnMissingProps();
        }

        private static void SpawnMissingProps() {
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_M_items"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_M_items");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_D_items"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_D_items");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_S_items"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_S_items");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_L_items"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_L_items");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_FameShame"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_FameShame");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_JewelHeist"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_JewelHeist");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "Michael_premier"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "Michael_premier");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_plane_ticket"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_plane_ticket");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "burgershot_yoga"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "burgershot_yoga");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166657, "V_Michael_bed_Messy"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166657, "V_Michael_bed_Messy");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 166401, "V_Michael_Scuba"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 166401, "V_Michael_Scuba");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "franklin_unpacking"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "franklin_unpacking");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "franklin_settled"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "franklin_settled");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "progress_tshirt"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "progress_tshirt");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "bong_and_wine"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "bong_and_wine");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "progress_flyer"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "progress_flyer");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "progress_tux"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "progress_tux");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 206849, "unlocked"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 206849, "unlocked");
            if (!Function.Call<bool>(Hash._IS_INTERIOR_PROP_ENABLED, 197121, "V_19_Trevor_Mess"))
                Function.Call(Hash._ENABLE_INTERIOR_PROP, 197121, "V_19_Trevor_Mess");
            Function.Call(Hash.REFRESH_INTERIOR, 166657);
            Function.Call(Hash.REFRESH_INTERIOR, 166401);
            Function.Call(Hash.REFRESH_INTERIOR, 206849);
            Function.Call(Hash.REFRESH_INTERIOR, 197121);
        }

        public static void DisplayHelpTextThisFrame(string text) {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON");
            for (var i = 0; i < text.Length; i += 99)
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text.Substring(i, Math.Min(99, text.Length - i)));
            Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
        }

        public static int DisplayNotification(string message, string picName, int iconType, string sender,
            string subject) {
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "CELL_EMAIL_BCON");
            for (var i = 0; i < message.Length; i += 99)
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message.Substring(i, Math.Min(99, message.Length - i)));
            Function.Call(Hash._0x1E6611149DB3DB6B, picName, picName, 1, iconType, sender, subject, 1f);
            return Function.Call<int>(Hash._DRAW_NOTIFICATION, 1, 1);
        }

        private static Building GetCurrentBuilding() {
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);

            foreach (var building in SinglePlayerOffice.Buildings)
                if (Game.Player.Character.Position.DistanceTo(building.Entrance.TriggerPos) < 10f
                    || building.InteriorIDs.Contains(currentInteriorId)
                    || Game.Player.Character.Position.DistanceTo(building.HeliPad.TriggerPos) < 10f)
                    return building;

            return null;
        }

        public static void TalkShit() {
            switch (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character)) {
                case 0:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "CULT_TALK",
                        "SPEECH_PARAMS_FORCE");
                    break;
                case 1:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "PED_RANT_RESP",
                        "SPEECH_PARAMS_FORCE");
                    break;
                case 3:
                    Function.Call(Hash._PLAY_AMBIENT_SPEECH1, Game.Player.Character, "GENERIC_INSULT_OLD",
                        "SPEECH_PARAMS_FORCE");
                    break;
            }
        }
    }
}