using MonopolyWarehouse.Models;
namespace MonopolyWarehouseTests;
public class BoxTests {

    [Fact]
    public void VolumeIsCalculatedCorrectly() {
        var box = new Box(2, 3, 4, 1, expirationDate: new DateOnly(2025, 12, 1));
        Assert.Equal(24, box.Volume);
    }

    [Fact]
    public void ExpirationIsCalculatedFromProductionDate() {
        var prodDate = new DateOnly(2025, 6, 1);
        var box = new Box(1, 1, 1, 1, productionDate: prodDate);
        Assert.Equal(prodDate.AddDays(100), box.ExpirationDate);
    }

    [Fact]
    public void ThrowsIfNoDatesProvided() {
        Assert.Throws<ArgumentException>(() =>
            new Box(1, 1, 1, 1)
        );
    }
}
