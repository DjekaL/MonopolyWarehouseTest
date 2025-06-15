namespace MonopolyWarehouse.Models {
    public abstract class Item {

        public int Id { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Depth { get; set; }

        public virtual decimal Weight { get; set; }

        public virtual decimal Volume => Width * Height * Depth;

        public virtual DateOnly ExpirationDate { get; set; }

        protected virtual int GetNextId() => 0;

        protected Item() {
            Id = GetNextId();
        }
    }
}
