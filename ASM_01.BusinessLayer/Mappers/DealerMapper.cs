using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Mappers.Abstract;
using ASM_01.DataAccessLayer.Entities.Warehouse;

namespace ASM_01.BusinessLayer.Mappers
{
    /// <summary>
    /// Mapper chuyển đổi dữ liệu đại lý
    /// </summary>
    public class DealerMapper : IDealerMapper
    {
        public DealerDto MapToDealerDto(Dealer dealer)
        {
            return new DealerDto
            {
                DealerId = dealer.DealerId,
                Name = dealer.Name,
                Address = dealer.Address
            };
        }

        public DealerStockDto MapToDealerStockDto(VehicleStock stock)
        {
            return new DealerStockDto
            {
                EvTrimId = stock.EvTrimId,
                TrimName = stock.EvTrim.TrimName,
                ModelName = stock.EvTrim.EvModel.ModelName,
                ModelYear = stock.EvTrim.ModelYear,
                Quantity = stock.Quantity
            };
        }

        public ModelStockDto MapToModelStockDto(string modelName, IEnumerable<VehicleStock> stocks)
        {
            var stockList = stocks.ToList();
            return new ModelStockDto
            {
                ModelName = modelName,
                TotalQuantity = stockList.Sum(x => x.Quantity),
                Trims = stockList.Select(x => new TrimQty
                {
                    EvTrimId = x.EvTrimId,
                    TrimName = x.EvTrim.TrimName,
                    Quantity = x.Quantity
                }).ToList()
            };
        }
    }
}
