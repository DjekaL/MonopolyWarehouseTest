namespace MonopolyWarehouse.Models {
    public class Box : Item {

        static int _id = 0;

        public override decimal Weight => Math.Round(base.Weight, 2);

        public DateOnly? ProductionDate { get; set; }

        protected override int GetNextId() => _id++;

        public Box(decimal width, decimal height, decimal depth, decimal weight, DateOnly? productionDate = null, DateOnly? expirationDate = null) {
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            ProductionDate = productionDate;
            if (expirationDate is not null) {
                ExpirationDate = (DateOnly)expirationDate;
            } else {
                ExpirationDate = (DateOnly)(productionDate?.AddDays(100));
            }
        }
    }
}
