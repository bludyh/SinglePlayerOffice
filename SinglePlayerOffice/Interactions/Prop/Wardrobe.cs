using GTA;
using GTA.Math;
using GTA.Native;

namespace SinglePlayerOffice.Interactions {

    internal class Wardrobe : Interaction {

        private Camera camera;
        private float cameraFov;

        private Vector3 cameraPos;
        private Vector3 cameraRot;

        public Wardrobe(Vector3 pos, Vector3 rot) {
            Position = pos;
            Rotation = rot;
        }

        public override string HelpText => "Press ~INPUT_CONTEXT~ to change outfit";

        public override string RejectHelpText => "You cannot change outfit here";

        public Vector3 Position { get; }
        public Vector3 Rotation { get; }

        public override void Update() {
            switch (State) {
                case 0:

                    if (!Game.Player.Character.IsDead && !Game.Player.Character.IsInVehicle() &&
                        !UI.MenuPool.IsAnyMenuOpen() &&
                        Game.Player.Character.Position.DistanceTo(Position) < 1f) {
                        if (SinglePlayerOffice.CurrentBuilding.IsOwnedBy(Game.Player.Character)) {
                            Utilities.DisplayHelpTextThisFrame(HelpText);

                            if (Game.IsControlJustPressed(2, Control.Context)) {
                                Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                                UI.IsHudHidden = true;
                                State = 1;
                            }
                        }
                        else {
                            Utilities.DisplayHelpTextThisFrame(RejectHelpText);
                        }
                    }

                    break;
                case 1:
                    Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, Game.Player.Character, Position.X, Position.Y,
                        Position.Z, 1f, -1, Rotation.Z, 0f);
                    State = 2;

                    break;
                case 2:

                    if (Function.Call<int>(Hash.GET_SCRIPT_TASK_STATUS, Game.Player.Character, 0x7d8f4411) == 1) break;

                    Game.Player.Character.Task.StandStill(-1);
                    Game.Player.Character.ClearBloodDamage();
                    UI.WardrobeMenu.Visible = true;
                    cameraPos = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0f, 1.5f, 0f));
                    cameraRot = new Vector3(0f, 0f, Game.Player.Character.Rotation.Z + 180f);
                    cameraFov = 75f;
                    camera = World.CreateCamera(cameraPos, cameraRot, cameraFov);
                    World.RenderingCamera = camera;
                    State = -1;

                    break;
            }
        }

    }

}