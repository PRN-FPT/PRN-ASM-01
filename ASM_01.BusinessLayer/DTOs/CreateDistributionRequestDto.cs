namespace ASM_01.BusinessLayer.DTOs
{
    public class CreateDistributionRequestDto
    {
        public int DealerId { get; set; }
        public int EvTrimId { get; set; }
        public int RequestedQuantity { get; set; }
    }
}
