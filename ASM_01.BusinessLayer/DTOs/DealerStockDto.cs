namespace ASM_01.BusinessLayer.DTOs
{
    public class DealerStockDto
    {
        public int EvTrimId { get; set; }
        public string TrimName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public int? ModelYear { get; set; }
        public int Quantity { get; set; }
    }
}