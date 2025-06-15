using MonopolyWarehouse.Models;

namespace MonopolyWarehouse {
    public class Warehouse {

        public List<Pallet> Pallets { get; private set; } = new();

        public IEnumerable<IGrouping<DateOnly, Pallet>> GetAllPallets() {
            return Pallets.OrderBy(p => p.Weight)
                            .GroupBy(p => p.ExpirationDate)
                            .OrderBy(g => g.Key);
        }

        public List<Pallet> GetPalletsByBoxMaxExpiration(int amount) {
            return Pallets.Where(p => p.Boxes.Any())
                            .OrderByDescending(p => p.Boxes.Max(b => b.ExpirationDate))
                            .Take(amount)
                            .OrderBy(p => p.Volume)
                            .ToList();
        }
    }
}
