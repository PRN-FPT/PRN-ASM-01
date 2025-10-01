using ASM_01.DataAccessLayer.Enums;

namespace ASM_01.DataAccessLayer.Entities.VehicleModels;

public class EvModel
{
    public int EvModelId { get; set; }
    public string ModelName { get; set; } = null!;
    public string? Description { get; set; }
    public EvStatus Status { get; set; } = EvStatus.Unavailable;

    public virtual ICollection<EvTrim> Trims { get; set; } = new List<EvTrim>();
}
