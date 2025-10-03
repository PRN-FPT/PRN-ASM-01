namespace ASM_01.DataAccessLayer.Entities.Warehouse;

public class Dealer
{
    public int DealerId { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VehicleStock> VehicleStocks { get; set; } = new List<VehicleStock>();
    public virtual ICollection<DistributionRequest> DistributionRequests { get; set; } = new List<DistributionRequest>();
}

