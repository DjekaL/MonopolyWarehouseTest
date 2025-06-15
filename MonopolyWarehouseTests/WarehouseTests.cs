using MonopolyWarehouse.Models;
using MonopolyWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyWarehouseTests
{
    public class WarehouseTests {
        [Fact]
        public void GetAllPalletsWorksCorrectly() {
            var warehouse = new Warehouse();

            var p1 = new Pallet { Width = 100, Height = 100, Depth = 100 };
            var p2 = new Pallet { Width = 100, Height = 100, Depth = 100 };
            var p3 = new Pallet { Width = 100, Height = 100, Depth = 100 };
            var p4 = new Pallet { Width = 100, Height = 100, Depth = 100 };

            var firstDate = new DateOnly(2025, 9, 1);
            var secondDate = new DateOnly(2025, 10, 1);

            p1.AddBox(new Box(1, 1, 1, 10, expirationDate: firstDate));
            p2.AddBox(new Box(1, 1, 1, 4, expirationDate: firstDate));
            p1.AddBox(new Box(1, 1, 1, 7, expirationDate: firstDate));
            p3.AddBox(new Box(1, 1, 1, 1, expirationDate: secondDate));
            p2.AddBox(new Box(1, 1, 1, 6, expirationDate: secondDate));
            p4.AddBox(new Box(1, 1, 1, 2, expirationDate: secondDate));
            p3.AddBox(new Box(1, 1, 1, 8, expirationDate: secondDate));

            warehouse.Pallets.Add(p1);
            warehouse.Pallets.Add(p2);
            warehouse.Pallets.Add(p3);
            warehouse.Pallets.Add(p4);

            var grouped = warehouse.GetAllPallets();

            Assert.NotEmpty(grouped);
            Assert.Equal(firstDate, grouped.First().Key);
            Assert.True(grouped.First().Select(x => x.Weight).First() == grouped.First().Select(x => x.Weight).Min());
        }

        [Fact]
        public void GetPalletsByBoxMaxExpirationWorksCorrectly() {
            var warehouse = new Warehouse();

            var p1 = new Pallet { Width = 50, Height = 100, Depth = 100 };
            var p2 = new Pallet { Width = 70, Height = 100, Depth = 100 };
            var p3 = new Pallet { Width = 100, Height = 100, Depth = 100 };

            var firstDate = new DateOnly(2025, 10, 1);
            var secondDate = new DateOnly(2025, 9, 1);

            p1.AddBox(new Box(1, 1, 1, 1, expirationDate: firstDate));
            p2.AddBox(new Box(5, 1, 1, 1, expirationDate: firstDate));
            p3.AddBox(new Box(10, 1, 1, 1, expirationDate: secondDate));

            warehouse.Pallets.Add(p1);
            warehouse.Pallets.Add(p2);
            warehouse.Pallets.Add(p3);

            var grouped = warehouse.GetPalletsByBoxMaxExpiration(2);

            Assert.NotEmpty(grouped);
            Assert.Equal(2, grouped.Count);
            Assert.True(grouped.First().Volume == grouped.Select(x => x.Volume).Min());
            Assert.Equal(firstDate, grouped.First().ExpirationDate);
            Assert.True(grouped.First().ExpirationDate == grouped.Last().ExpirationDate);
        }
    }
}
