using MonopolyWarehouse;
using MonopolyWarehouse.Models;

class Program {
    static void Main() {
        var warehouse = new Warehouse();

        GenerateSampleData(warehouse, palletCount: 10);

        Console.WriteLine("=== Паллеты по сроку годности ===");
        foreach (var group in warehouse.GetAllPallets()) {
            Console.WriteLine($"\nСрок годности: {group.Key:dd.MM.yyyy}");
            foreach (var pallet in group.OrderBy(p => p.Weight)) {
                Console.WriteLine($"  Палета {pallet.Id} | Вес: {pallet.Weight} кг | Объем: {pallet.Volume} см3");
            }
        }

        Console.WriteLine("\n=== 3 паллеты с наибольшим сроком годности коробоки ===");
        foreach (var pallet in warehouse.GetPalletsByBoxMaxExpiration(3)) {
            var maxDate = pallet.Boxes.Max(b => b.ExpirationDate);
            Console.WriteLine($"Палета {pallet.Id} | Макс. срок: {maxDate:dd.MM.yyyy} | Объем: {pallet.Volume} см3");
        }
    }

    static void GenerateSampleData(Warehouse warehouse, int palletCount) {
        var rand = new Random();

        var commonExpirations = Enumerable.Range(0, 5)
            .Select(i => DateOnly.FromDateTime(DateTime.Today.AddDays(20 + i * 10)))
            .ToList();

        for (int i = 0; i < palletCount; i++) {
            var pallet = new Pallet {
                Width = 120,
                Height = 100,
                Depth = 80
            };

            int boxCount = rand.Next(1, 6);
            for (int j = 0; j < boxCount; j++) {
                decimal width = rand.Next(30, 70);
                decimal height = rand.Next(20, 60);
                decimal depth = rand.Next(30, 70);
                decimal weight = (decimal)(rand.NextDouble() * 5 + 1);

                Box box;
                bool useProductionDate = rand.Next(2) == 0;
                if (useProductionDate) {
                    var prodDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-rand.Next(1, 60)));
                    box = new Box(width, height, depth, weight, productionDate: prodDate);
                } else {
                    var expDate = commonExpirations[rand.Next(commonExpirations.Count)];
                    box = new Box(width, height, depth, weight, expirationDate: expDate);
                }
                pallet.AddBox(box);
            }
            warehouse.Pallets.Add(pallet);
        }
    }


}
