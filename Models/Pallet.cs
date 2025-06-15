namespace MonopolyWarehouse.Models {
    public class Pallet : Item {

        int _palletWeight = 30;

        static int _id = 0;

        protected override int GetNextId() => _id++;

        private List<Box> _boxes = new List<Box>();

        public List<Box> Boxes {
            get { return _boxes; }
            private set { _boxes = value; }
        }

        public override decimal Weight => PalletsIsEmpty(Boxes) ? Boxes.Sum(b => b.Weight) + _palletWeight : _palletWeight;

        public override DateOnly ExpirationDate => PalletsIsEmpty(Boxes) ? Boxes.Min(b => b.ExpirationDate) : DateOnly.MinValue;

        public override decimal Volume => PalletsIsEmpty(Boxes) ? base.Volume + Boxes.Sum(b => b.Volume) : base.Volume;

        private bool PalletsIsEmpty(List<Box> boxes) {
            return boxes.Any();
        }

        private bool CanAddBox(Box box) {
            return box.Width <= Width && box.Depth <= Depth;
        }

        public void AddBox(Box box) {
            try {
                if (!CanAddBox(box)) {
                    throw new InvalidOperationException("Box exceeds pallet dimensions.");
                }
                Boxes.Add(box);
            }
            catch(InvalidOperationException ex) {
                Console.WriteLine($"{ex}");
            }
        }
    }
}
