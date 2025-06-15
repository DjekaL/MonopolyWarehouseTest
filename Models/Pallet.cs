namespace MonopolyWarehouse.Models {
    public class Pallet : Item {

        int _palletWeight = 30;

        static int _id = 0;

        protected override int GetNextId() => _id++;

        public List<Box>? Boxes { get; set; } = new List<Box>();

        public override decimal Weight => Boxes.Sum(b => b.Weight) + _palletWeight;

        public override DateOnly ExpirationDate => Boxes.Min(b => b.ExpirationDate);

        public override decimal Volume => base.Volume + Boxes.Sum(b => b.Volume);

        public void AddBox(Box box) {
            if (box.Width <= Width && box.Depth <= Depth) {
                Boxes.Add(box);
            }
        }
    }
}
