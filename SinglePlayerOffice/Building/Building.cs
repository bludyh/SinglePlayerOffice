using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace SinglePlayerOffice.Buildings {

    internal abstract class Building {

        private Location currentLocation;

        public ScriptSettings SaveData { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Price { get; protected set; }
        public Owner Owner { get; set; }
        public bool IsOwned => Owner != Owner.None;
        public List<int> InteriorIDs { get; protected set; }
        public List<string> ExteriorMapObjects { get; protected set; }
        public Entrance Entrance { get; protected set; }
        public GarageEntrance GarageEntrance { get; protected set; }
        public Office Office { get; protected set; }
        public Garage GarageOne { get; protected set; }
        public Garage GarageTwo { get; protected set; }
        public Garage GarageThree { get; protected set; }
        public ModShop ModShop { get; protected set; }
        public HeliPad HeliPad { get; protected set; }
        public Blip EntranceBlip { get; private set; }
        public Blip GarageEntranceBlip { get; private set; }
        public DateTime? ConstructionTime { get; set; }

        public Location CurrentLocation {
            get => currentLocation;
            set {
                if (currentLocation == value) return;

                currentLocation?.OnLocationLeft();
                currentLocation = value;
                currentLocation.OnLocationArrived();
            }
        }

        public InteriorStyle GetOfficeInteriorStyle(string name) {
            foreach (var style in Office.InteriorStyles)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetGarageDecorationStyle(string name) {
            foreach (var style in Garage.DecorationStyles)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetGarageLightingStyle(string name) {
            foreach (var style in Garage.LightingStyles)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetGarageOneNumberingStyle(string name) {
            foreach (var style in Garage.NumberingStylesGarageOne)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetGarageTwoNumberingStyle(string name) {
            foreach (var style in Garage.NumberingStylesGarageTwo)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetGarageThreeNumberingStyle(string name) {
            foreach (var style in Garage.NumberingStylesGarageThree)
                if (style.Name == name)
                    return style;

            return null;
        }

        public InteriorStyle GetModShopFloorStyle(string name) {
            foreach (var style in ModShop.FloorStyles)
                if (style.Name == name)
                    return style;

            return null;
        }

        public void CreateEntranceBlip() {
            EntranceBlip = World.CreateBlip(Entrance.TriggerPos);
            if (IsOwned)
                EntranceBlip.Sprite = (BlipSprite) 475;
            else
                EntranceBlip.Sprite = (BlipSprite) 476;
            EntranceBlip.Name = Name;
            SetBlipColor(EntranceBlip);
        }

        public void CreateGarageEntranceBlip() {
            GarageEntranceBlip = World.CreateBlip(GarageEntrance.TriggerPos);
            GarageEntranceBlip.Sprite = (BlipSprite) 357;
            GarageEntranceBlip.Name = "Office Garage";
            SetBlipColor(GarageEntranceBlip);
        }

        public void SetBlipColor(Blip blip) {
            switch (Owner) {
                case Owner.Michael:
                    blip.Color = BlipColor.Blue;

                    break;
                case Owner.Franklin:
                    blip.Color = (BlipColor) 11;

                    break;
                case Owner.Trevor:
                    blip.Color = (BlipColor) 17;

                    break;
                default:
                    blip.Color = BlipColor.White;

                    break;
            }
        }

        public int GetBuyingPrice() {
            return Price +
                   Office.InteriorStyle.Price +
                   GarageOne.DecorationStyle.Price + GarageOne.LightingStyle.Price + GarageOne.NumberingStyle.Price +
                   GarageTwo.DecorationStyle.Price + GarageTwo.LightingStyle.Price + GarageTwo.NumberingStyle.Price +
                   GarageThree.DecorationStyle.Price + GarageThree.LightingStyle.Price +
                   GarageThree.NumberingStyle.Price +
                   ModShop.FloorStyle.Price +
                   Office.ExtraDecorsPrice;
        }

        public int GetRefurbishingPrice() {
            var price = 0;
            if (Office.InteriorStyle !=
                GetOfficeInteriorStyle(SaveData.GetValue("Interiors", "OfficeInteriorStyle", "Executive Rich")))
                price += Office.InteriorStyle.Price;
            if (Office.HasExtraDecors != SaveData.GetValue("Interiors", "HasExtraOfficeDecors", false))
                price += Office.ExtraDecorsPrice;
            if (GarageOne.DecorationStyle !=
                GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageOneDecorationStyle", "Decoration 1")))
                price += GarageOne.DecorationStyle.Price;
            if (GarageOne.LightingStyle !=
                GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageOneLightingStyle", "Lighting 1")))
                price += GarageOne.LightingStyle.Price;
            if (GarageOne.NumberingStyle !=
                GetGarageOneNumberingStyle(SaveData.GetValue("Interiors", "GarageOneNumberingStyle", "Signage 1")))
                price += GarageOne.NumberingStyle.Price;
            if (GarageTwo.DecorationStyle !=
                GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageTwoDecorationStyle", "Decoration 1")))
                price += GarageTwo.DecorationStyle.Price;
            if (GarageTwo.LightingStyle !=
                GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageTwoLightingStyle", "Lighting 1")))
                price += GarageTwo.LightingStyle.Price;
            if (GarageTwo.NumberingStyle !=
                GetGarageTwoNumberingStyle(SaveData.GetValue("Interiors", "GarageTwoNumberingStyle", "Signage 1")))
                price += GarageTwo.NumberingStyle.Price;
            if (GarageThree.DecorationStyle !=
                GetGarageDecorationStyle(SaveData.GetValue("Interiors", "GarageThreeDecorationStyle", "Decoration 1")))
                price += GarageThree.DecorationStyle.Price;
            if (GarageThree.LightingStyle !=
                GetGarageLightingStyle(SaveData.GetValue("Interiors", "GarageThreeLightingStyle", "Lighting 1")))
                price += GarageThree.LightingStyle.Price;
            if (GarageThree.NumberingStyle !=
                GetGarageThreeNumberingStyle(SaveData.GetValue("Interiors", "GarageThreeNumberingStyle", "Signage 1")))
                price += GarageThree.NumberingStyle.Price;
            if (ModShop.FloorStyle !=
                GetModShopFloorStyle(SaveData.GetValue("Interiors", "ModShopFloorStyle", "Floor 1")))
                price += ModShop.FloorStyle.Price;

            return price;
        }

        public int GetSellingPrice() {
            return GetBuyingPrice() * 75 / 100;
        }

        public bool IsOwnedBy(Ped ped) {
            return Function.Call<int>(Hash.GET_PED_TYPE, ped) == (int) Owner;
        }

        private Location GetCurrentLocation() {
            var currentInteriorId = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character);

            if (Game.Player.Character.Position.DistanceTo(Entrance.TriggerPos) < 10f)
                return Entrance;
            if (currentInteriorId == GarageEntrance.InteriorId)
                return GarageEntrance;
            if (Office.InteriorIDs.Contains(currentInteriorId))
                return Office;
            if (currentInteriorId == GarageOne.InteriorId)
                return GarageOne;
            if (currentInteriorId == GarageTwo.InteriorId)
                return GarageTwo;
            if (currentInteriorId == GarageThree.InteriorId)
                return GarageThree;
            if (currentInteriorId == ModShop.InteriorId)
                return ModShop;
            if (Game.Player.Character.Position.DistanceTo(HeliPad.TriggerPos) < 10f)
                return HeliPad;

            return null;
        }

        public void HideExteriorMapObjects() {
            Function.Call(Hash._0x4B5CFC83122DF602);

            foreach (var exterior in ExteriorMapObjects) {
                var exteriorHash = Function.Call<int>(Hash.GET_HASH_KEY, exterior);
                Function.Call(Hash._HIDE_MAP_OBJECT_THIS_FRAME, exteriorHash);
                Function.Call((Hash) 5819624144786551657, exteriorHash);
            }

            Function.Call(Hash._0x3669F1B198DCAA4F);
        }

        public void UnloadAllInteriors() {
            Office.UnloadInterior();
            GarageOne.UnloadInterior();
            GarageTwo.UnloadInterior();
            GarageThree.UnloadInterior();
            ModShop.UnloadInterior();
        }

        public void UnloadAllExteriors() {
            Office.UnloadExterior();
            GarageOne.UnloadExterior();
            GarageTwo.UnloadExterior();
            GarageThree.UnloadExterior();
            ModShop.UnloadExterior();
        }

        public void HandleConstructionNotification() {
            if (ConstructionTime == null || World.CurrentDate.CompareTo(ConstructionTime) <= 0) return;

            Utilities.DisplayNotification(
                "Boss, ~b~" + Name + "~w~ is ready! Drop by sometime to check out the building.", "CHAR_PA_FEMALE", 1,
                "Personal Assistant", "");
            ConstructionTime = null;
        }

        private void HandleVisitingTime() {
            var hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);

            if (IsOwnedBy(Game.Player.Character) || hours >= 9 && hours <= 16 ||
                !(CurrentLocation is IInterior)) return;

            UI.MenuPool.CloseAllMenus();
            UI.IsHudHidden = false;
            Game.FadeScreenOut(1000);
            Script.Wait(1000);
            UnloadAllInteriors();
            UnloadAllExteriors();
            Game.Player.Character.Position = Entrance.SpawnPos;
            Game.Player.Character.Heading = Entrance.SpawnHeading;
            Game.Player.Character.Task.ClearAll();
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_HEADING, 0f);
            Function.Call(Hash.SET_GAMEPLAY_CAM_RELATIVE_PITCH, 0f);
            Script.Wait(1000);
            Game.FadeScreenIn(1000);
        }

        public void Update() {
            CurrentLocation = GetCurrentLocation();

            HandleVisitingTime();

            CurrentLocation.Update();
        }

        public void Dispose() {
            EntranceBlip?.Remove();
            GarageEntranceBlip?.Remove();
            Entrance.Dispose();
            GarageEntrance.Dispose();
            Office.Dispose();
            GarageOne.Dispose();
            GarageTwo.Dispose();
            GarageThree.Dispose();
            ModShop.Dispose();
            HeliPad.Dispose();
        }

    }

}