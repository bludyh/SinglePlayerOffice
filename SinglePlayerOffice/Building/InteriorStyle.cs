namespace SinglePlayerOffice.Buildings {

    internal class InteriorStyle {

        public InteriorStyle(string name, int price, string style) {
            Name = name;
            Price = price;
            Style = style;
        }

        public string Name { get; }
        public int Price { get; }
        public string Style { get; }

    }

}