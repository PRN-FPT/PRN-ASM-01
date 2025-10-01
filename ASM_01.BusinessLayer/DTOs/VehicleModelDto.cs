using ASM_01.DataAccessLayer.Enums;

namespace ASM_01.BusinessLayer.DTOs
{
    public class VehicleModelDto
    {
        public int EvModelId { get; set; }
        public string ModelName { get; set; } = null!;
        public string? Description { get; set; }
        public EvStatus Status { get; set; }
        public int? ModelYear { get; set; }
    }
}