using ASM_01.BusinessLayer.DTOs;
using ASM_01.DataAccessLayer.Entities.VehicleModels;

namespace ASM_01.BusinessLayer.Mappers.Abstract
{
    /// <summary>
    /// Interface cho mapper chuyển đổi dữ liệu xe điện
    /// </summary>
    public interface IVehicleMapper
    {
        /// <summary>
        /// Map từ EvTrim entity sang VehicleDto
        /// </summary>
        /// <param name="trim">Entity EvTrim</param>
        /// <param name="latestPrice">Giá mới nhất</param>
        /// <param name="specifications">Thông số kỹ thuật</param>
        /// <returns>VehicleDto</returns>
        VehicleDto MapToVehicleDto(EvTrim trim, TrimPrice? latestPrice, Dictionary<string, string> specifications);

        /// <summary>
        /// Map từ EvModel entity sang VehicleModelDto
        /// </summary>
        /// <param name="model">Entity EvModel</param>
        /// <returns>VehicleModelDto</returns>
        VehicleModelDto MapToVehicleModelDto(EvModel model);

        /// <summary>
        /// Map từ EvTrim entity sang VehicleComparisonDto
        /// </summary>
        /// <param name="trim">Entity EvTrim</param>
        /// <param name="latestPrice">Giá mới nhất</param>
        /// <param name="specifications">Thông số kỹ thuật</param>
        /// <returns>VehicleComparisonDto</returns>
        VehicleComparisonDto MapToVehicleComparisonDto(EvTrim trim, TrimPrice? latestPrice, Dictionary<string, string> specifications);

        /// <summary>
        /// Map từ CreateVehicleModelDto sang EvModel entity
        /// </summary>
        /// <param name="dto">CreateVehicleModelDto</param>
        /// <returns>EvModel entity</returns>
        EvModel MapToEvModel(CreateVehicleModelDto dto);

        /// <summary>
        /// Map từ CreateVehicleTrimDto sang EvTrim entity
        /// </summary>
        /// <param name="dto">CreateVehicleTrimDto</param>
        /// <returns>EvTrim entity</returns>
        EvTrim MapToEvTrim(CreateVehicleTrimDto dto);

        /// <summary>
        /// Map từ UpdateVehicleTrimPriceDto sang TrimPrice entity
        /// </summary>
        /// <param name="dto">UpdateVehicleTrimPriceDto</param>
        /// <returns>TrimPrice entity</returns>
        TrimPrice MapToTrimPrice(UpdateVehicleTrimPriceDto dto);
    }
}
