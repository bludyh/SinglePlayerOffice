using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace SinglePlayerOffice {
    class InteractionsThread : Script {

        public InteractionsThread() {
            Tick += OnTick;
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@seating@male@var_d@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@seating@male@var_d@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@game@seated@male@var_c@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@game@seated@male@var_c@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boss@male@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boss@male@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boss@vault@left@male@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boss@vault@left@male@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boss@vault@right@male@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boss@vault@right@male@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@mp_radio@high_life_apment")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@mp_radio@high_life_apment");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@boss@male@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@boss@male@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@crew@male@var_c@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@male@var_c@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boardroom@crew@male@var_b@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boardroom@crew@male@var_b@base@");
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@laptops@male@var_b@base@")) Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@laptops@male@var_b@base@");
            if (!Function.Call<bool>(Hash._0x0145F696AAAAD2E4, "MPDesktop")) Function.Call(Hash._0xDFA2EF8E04127DD5, "MPDesktop", false);
        }

        private void OnTick(object sender, EventArgs e) {
            foreach (Building buiding in SinglePlayerOffice.Buildings) {
                buiding.InteractionsController.OnTick();
            }
        }

        protected override void Dispose(bool A_0) {
            if (A_0) {
                foreach (Building building in SinglePlayerOffice.Buildings) {
                    building.InteractionsController.Dispose();
                }
                Game.Player.Character.Task.ClearAll();
            }
        }

    }
}
