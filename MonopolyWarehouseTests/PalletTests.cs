using MonopolyWarehouse.Models;

namespace MonopolyWarehouseTests {
    public class PalletTests {
        [Fact]
        public void WeightIsSumOfBoxesPlus30() {
            var pallet = new Pallet { Width = 100, Height = 100, Depth = 100 };
            pallet.AddBox(new Box(1, 1, 1, 10, expirationDate: new DateOnly(2025, 12, 1)));
            pallet.AddBox(new Box(1, 1, 1, 5, expirationDate: new DateOnly(2025, 12, 2)));

            Assert.Equal(45, pallet.Weight);
        }

        [Fact]
        public void ExpirationDateIsEarliestAmongBoxes() {
            var pallet = new Pallet { Width = 100, Height = 100, Depth = 100 };
            pallet.AddBox(new Box(1, 1, 1, 1, expirationDate: new DateOnly(2025, 10, 1)));
            pallet.AddBox(new Box(1, 1, 1, 1, expirationDate: new DateOnly(2025, 8, 1)));

            Assert.Equal(new DateOnly(2025, 8, 1), pallet.ExpirationDate);
        }

        [Fact]
        public void CannotAddOversizedBox() {
            var pallet = new Pallet { Width = 100, Height = 100, Depth = 100 };
            var box = new Box(120, 20, 120, 1, expirationDate: new DateOnly(2025, 12, 1));

            pallet.AddBox(box);
            Assert.Empty(pallet.Boxes);
        }
    }
}