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
                ExpirationDate = expirationDate.Value;
            } else if (productionDate is not null) {
                ExpirationDate = productionDate.Value.AddDays(100);
            } else {
                throw new ArgumentException($"Production date or expiration date must be provided.");
            }
        }
    }
}