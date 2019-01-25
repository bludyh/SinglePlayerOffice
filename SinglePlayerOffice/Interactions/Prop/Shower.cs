using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Shower : Interaction {

        private readonly List<string> idleAnims;

        private Prop door;
        private int ptfxHandle1;
        private int ptfxHandle2;
        private int soundHandle;

        public Shower() {
            idleAnims = new List<string>
                { "male_shower_idle_a", "male_shower_idle_b", "male_shower_idle_c", "male_shower_idle_d" };
        }

        public override void Update() {
            switch (State) {
                case 0:

                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle())
                        foreach (var prop in World.GetNearbyProps(Game.Player.Character.Position, 2f)) {
                            if (prop.Model.Hash != 879181614) continue;

                            Utilities.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to take a shower");

                            if (Game.IsControlJustPressed(2, Control.Context)) {
                                door = prop;
                                Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                                State = 1;
                            }

                            break;
                        }

                    break;
                case 1:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "mp_safehouseshower@male@");
                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_mp_house");
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_safehouseshower@male@") &&
                        Function.Call<bool>(Hash.HAS_NAMED_PTFX_ASSET_LOADED, "scr_mp_house") &&
                        Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "dlc_EXEC1/MP_APARTMENT_SHOWER_01", 0,
                            -1))
                        State = 2;

                    break;
                case 2:
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    UI.IsHudHidden = true;
                    initialPos = door.GetOffsetInWorldCoords(new Vector3(-0.4663941f, -1.10257f, -0.2125397f));
                    Game.Player.Character.Position = initialPos;
                    Game.Player.Character.Heading = door.Heading;

                    switch (Function.Call<int>(Hash.GET_PED_TYPE, Game.Player.Character)) {
                        case 0:
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 26, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 18, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 1, 0, 2);

                            break;
                        case 1:
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 26, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 18, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 5, 0, 2);

                            break;
                        case 3:
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 16, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 25, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 2);
                            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 17, 0, 2);

                            break;
                    }

                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 0, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 2);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 0, 0, 2);
                    Function.Call(Hash.CLEAR_ALL_PED_PROPS, Game.Player.Character);
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                    State = 3;

                    break;
                case 3:
                    Game.Player.Character.Task.PlayAnimation("mp_safehouseshower@male@", "male_shower_enter_into_idle");
                    State = 4;

                    break;
                case 4:
                    var pos = door.GetOffsetInWorldCoords(new Vector3(-0.13f, -0.4f, 1.2f));
                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_mp_house");
                    ptfxHandle1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_AT_COORD, "ent_amb_shower", pos.X,
                        pos.Y, pos.Z, -45f, 0f, 0f, 1f, 0, 0, 0, 1);
                    pos = door.GetOffsetInWorldCoords(new Vector3(0.3f, -1.8f, 1f));
                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_mp_house");
                    ptfxHandle2 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_AT_COORD, "ent_amb_shower_steam",
                        pos.X, pos.Y, pos.Z, 0f, 0f, door.Rotation.Z, 1f, 0, 0, 0, 1);
                    soundHandle = Audio.PlaySoundFromEntity(Game.Player.Character,
                        "GTAO_MP_APARTMENT_SHOWER_PLASTIC_MASTER");
                    State = 5;

                    break;
                case 5:

                    if (Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.Player.Character,
                        "mp_safehouseshower@male@", "male_shower_enter_into_idle", 3)) break;

                    Game.Player.Character.Task.PlayAnimation("mp_safehouseshower@male@",
                        idleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 4)]);
                    State = 6;

                    break;
                case 6:
                    Utilities.DisplayHelpTextThisFrame(
                        "Press ~INPUT_CONTEXT~ to talk shit~n~Press ~INPUT_AIM~ to finish");

                    if (Game.IsControlJustPressed(2, Control.Context))
                        TalkShit();

                    if (Game.IsControlJustPressed(2, Control.Aim)) {
                        State = 7;

                        break;
                    }

                    if (idleAnims.Any(anim => Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.Player.Character,
                        "mp_safehouseshower@male@", anim, 3)))
                        break;

                    Game.Player.Character.Task.PlayAnimation("mp_safehouseshower@male@",
                        idleAnims[Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 4)]);

                    break;
                case 7:
                    Game.FadeScreenOut(1000);
                    Script.Wait(1000);
                    UI.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    Game.Player.Character.ClearBloodDamage();
                    Script.Wait(1000);
                    Game.FadeScreenIn(1000);
                    Function.Call(Hash.REMOVE_ANIM_DICT, "mp_safehouseshower@male@");
                    if (Function.Call<bool>(Hash.DOES_PARTICLE_FX_LOOPED_EXIST, ptfxHandle1))
                        Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, ptfxHandle1, 0);
                    if (Function.Call<bool>(Hash.DOES_PARTICLE_FX_LOOPED_EXIST, ptfxHandle2))
                        Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, ptfxHandle2, 0);
                    Function.Call(Hash._REMOVE_NAMED_PTFX_ASSET, "scr_mp_house");
                    Audio.StopSound(soundHandle);
                    Audio.ReleaseSound(soundHandle);
                    Function.Call(Hash.RELEASE_AMBIENT_AUDIO_BANK);
                    State = 0;

                    break;
            }
        }

    }

}