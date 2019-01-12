using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {
    internal class LeftSafe : Interaction {
        private Prop door;

        public override string HelpText => !IsSafeOpened
            ? "Press ~INPUT_CONTEXT~ to open the safe"
            : "Press ~INPUT_CONTEXT~ to close the safe";

        public override string RejectHelpText => "Only the owner can open the safe";
        public bool IsSafeOpened { get; private set; }

        public override void Update() {
            switch (State) {
                case 0:
                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle())
                        foreach (var prop in World.GetNearbyProps(Game.Player.Character.Position, 1.4f))
                            switch (prop.Model.Hash) {
                                case 646926492:
                                case 845785021:
                                case -1126494299:
                                case -524920966:
                                case -1842578680:
                                case -1387653807:
                                    if (Utilities.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                                        Utilities.DisplayHelpTextThisFrame(HelpText);
                                        if (Game.IsControlJustPressed(2, Control.Context)) {
                                            door = prop;
                                            Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                                            SinglePlayerOffice.IsHudHidden = true;
                                            State = 1;
                                        }
                                    }
                                    else {
                                        Utilities.DisplayHelpTextThisFrame(RejectHelpText);
                                    }

                                    break;
                            }
                    break;
                case 1:
                    Function.Call(Hash.REQUEST_ANIM_DICT, "anim@amb@office@boss@vault@left@male@");
                    if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@amb@office@boss@vault@left@male@"))
                        State = 2;
                    break;
                case 2:
                    if (!IsSafeOpened) {
                        initialPos = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_POSITION,
                            "anim@amb@office@boss@vault@left@male@", "open", door.Position.X, door.Position.Y,
                            door.Position.Z, door.Rotation.X, door.Rotation.Y, door.Rotation.Z, 0, 2);
                        initialRot = Function.Call<Vector3>(Hash.GET_ANIM_INITIAL_OFFSET_ROTATION,
                            "anim@amb@office@boss@vault@left@male@", "open", door.Position.X, door.Position.Y,
                            door.Position.Z, door.Rotation.X, door.Rotation.Y, door.Rotation.Z, 0, 2);
                    }

                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, initialPos.X, initialPos.Y,
                        initialPos.Z, 1f, -1, initialRot.Z, 0f);
                    State = 3;
                    break;
                case 3:
                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;
                    if (!IsSafeOpened) {
                        Utilities.SavedPos = door.Position;
                        Utilities.SavedRot = door.Rotation;
                    }

                    State = 4;
                    break;
                case 4:
                    syncSceneHandle = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, Utilities.SavedPos.X,
                        Utilities.SavedPos.Y, Utilities.SavedPos.Z, 0f, 0f, Utilities.SavedRot.Z, 2);
                    if (!IsSafeOpened) {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                            "anim@amb@office@boss@vault@left@male@", "open", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, door, syncSceneHandle, "open_door",
                            "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        IsSafeOpened = true;
                    }
                    else {
                        Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, Game.Player.Character, syncSceneHandle,
                            "anim@amb@office@boss@vault@left@male@", "close", 1.5f, -1.5f, 13, 16, 1.5f, 0);
                        Function.Call(Hash.PLAY_SYNCHRONIZED_ENTITY_ANIM, door, syncSceneHandle, "close_door",
                            "anim@amb@office@boss@vault@left@male@", 4f, -4f, 32781, 1000f);
                        IsSafeOpened = false;
                    }

                    State = 5;
                    break;
                case 5:
                    if (Function.Call<float>(Hash.GET_SYNCHRONIZED_SCENE_PHASE, syncSceneHandle) < 1f) break;
                    SinglePlayerOffice.IsHudHidden = false;
                    Game.Player.Character.Task.ClearAll();
                    Function.Call(Hash.REMOVE_ANIM_DICT, "anim@amb@office@boss@vault@left@male@");
                    State = 0;
                    break;
            }
        }

        public override void Reset() {
            IsSafeOpened = false;
        }
    }
}