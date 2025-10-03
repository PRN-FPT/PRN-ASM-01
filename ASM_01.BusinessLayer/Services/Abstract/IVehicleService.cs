using ASM_01.BusinessLayer.DTOs;
using ASM_01.DataAccessLayer.Entities.VehicleModels;

namespace ASM_01.BusinessLayer.Services.Abstract
{
    /// <summary>
    /// Interface cho service quản lý xe điện, bao gồm model và trim
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Lấy danh sách tất cả xe điện (trims) bao gồm giá mới nhất và thông số kỹ thuật
        /// </summary>
        /// <returns>Danh sách xe điện với thông tin đầy đủ</returns>
        public Task<IEnumerable<VehicleDto>> GetAllVehicles();
        
        /// <summary>
        /// Lấy danh sách tất cả model xe điện
        /// </summary>
        /// <returns>Danh sách tất cả model xe điện</returns>
        public Task<IEnumerable<VehicleModelDto>> GetAllModel();
        
        /// <summary>
        /// Lấy thông tin chi tiết của một xe điện theo ID
        /// </summary>
        /// <param name="id">ID của xe điện cần lấy thông tin</param>
        /// <returns>Thông tin xe điện hoặc null nếu không tìm thấy</returns>
        public Task<VehicleDto?> GetVehicleById(int id);
        
        /// <summary>
        /// Tìm kiếm xe điện theo từ khóa
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách xe điện khớp với từ khóa</returns>
        public Task<IEnumerable<VehicleDto>> SearchVehicles(string keyword);
        
        /// <summary>
        /// So sánh nhiều xe điện với nhau
        /// </summary>
        /// <param name="vehicleIds">Mảng ID của các xe điện cần so sánh</param>
        /// <returns>Danh sách thông tin so sánh của các xe điện</returns>
        public Task<IEnumerable<VehicleComparisonDto>> CompareVehicles(int[] vehicleIds);
        
        /// <summary>
        /// Tạo mới một model xe điện
        /// </summary>
        /// <param name="dto">Thông tin model xe điện cần tạo</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Model xe điện đã được tạo</returns>
        /// <exception cref="ArgumentException">Khi tên model không hợp lệ</exception>
        /// <exception cref="InvalidOperationException">Khi model đã tồn tại</exception>
        public Task<EvModel> CreateVehicleModelAsync(CreateVehicleModelDto dto, CancellationToken ct = default);
        
        /// <summary>
        /// Tạo mới một trim (phiên bản) cho model xe điện
        /// </summary>
        /// <param name="dto">Thông tin trim cần tạo</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trim đã được tạo</returns>
        /// <exception cref="InvalidOperationException">Khi model không tồn tại</exception>
        public Task<EvTrim> CreateVehicleTrimAsync(CreateVehicleTrimDto dto, CancellationToken ct = default);
        
        /// <summary>
        /// Cập nhật trạng thái và mô tả của model xe điện
        /// </summary>
        /// <param name="dto">Thông tin cập nhật model</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Model đã được cập nhật</returns>
        /// <exception cref="InvalidOperationException">Khi model không tồn tại</exception>
        public Task<EvModel> UpdateVehicleModelStatusAsync(UpdateVehicleModelStatusDto dto, CancellationToken ct = default);
        
        /// <summary>
        /// Cập nhật giá của trim xe điện (tạo bản ghi giá mới để lưu lịch sử)
        /// </summary>
        /// <param name="dto">Thông tin giá cập nhật</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Bản ghi giá mới đã được tạo</returns>
        /// <exception cref="ArgumentOutOfRangeException">Khi giá không hợp lệ</exception>
        /// <exception cref="InvalidOperationException">Khi trim không tồn tại</exception>
        public Task<TrimPrice> UpdateVehicleTrimPriceAsync(UpdateVehicleTrimPriceDto dto, CancellationToken ct = default);
        
        /// <summary>
        /// Lấy thông tin chi tiết của một model xe điện theo ID
        /// </summary>
        /// <param name="id">ID của model cần lấy thông tin</param>
        /// <returns>Thông tin model hoặc null nếu không tìm thấy</returns>
        public Task<VehicleModelDto?> GetModelAsync(int id);
    }
}