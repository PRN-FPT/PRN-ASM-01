using ASM_01.BusinessLayer.DTOs;
using ASM_01.DataAccessLayer.Entities.Warehouse;

namespace ASM_01.BusinessLayer.Mappers.Abstract
{
    /// <summary>
    /// Interface cho mapper chuyển đổi dữ liệu phân phối
    /// </summary>
    public interface IDistributionMapper
    {
        /// <summary>
        /// Map từ DistributionRequest entity sang DistributionRequestDto
        /// </summary>
        /// <param name="request">Entity DistributionRequest</param>
        /// <returns>DistributionRequestDto</returns>
        DistributionRequestDto MapToDistributionRequestDto(DistributionRequest request);

        /// <summary>
        /// Map từ CreateDistributionRequestDto sang DistributionRequest entity
        /// </summary>
        /// <param name="dto">CreateDistributionRequestDto</param>
        /// <returns>DistributionRequest entity</returns>
        DistributionRequest MapToDistributionRequest(CreateDistributionRequestDto dto);
    }
}
