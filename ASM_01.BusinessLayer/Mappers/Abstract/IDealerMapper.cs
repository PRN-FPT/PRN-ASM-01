using ASM_01.BusinessLayer.DTOs;
using ASM_01.DataAccessLayer.Entities.Warehouse;

namespace ASM_01.BusinessLayer.Mappers.Abstract
{
    /// <summary>
    /// Interface cho mapper chuyển đổi dữ liệu đại lý
    /// </summary>
    public interface IDealerMapper
    {
        /// <summary>
        /// Map từ Dealer entity sang DealerDto
        /// </summary>
        /// <param name="dealer">Entity Dealer</param>
        /// <returns>DealerDto</returns>
        DealerDto MapToDealerDto(Dealer dealer);

        /// <summary>
        /// Map từ VehicleStock entity sang DealerStockDto
        /// </summary>
        /// <param name="stock">Entity VehicleStock</param>
        /// <returns>DealerStockDto</returns>
        DealerStockDto MapToDealerStockDto(VehicleStock stock);

        /// <summary>
        /// Map từ nhóm VehicleStock sang ModelStockDto
        /// </summary>
        /// <param name="modelName">Tên model</param>
        /// <param name="stocks">Danh sách VehicleStock</param>
        /// <returns>ModelStockDto</returns>
        ModelStockDto MapToModelStockDto(string modelName, IEnumerable<VehicleStock> stocks);
    }
}
