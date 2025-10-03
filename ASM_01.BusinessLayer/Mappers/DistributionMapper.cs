using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Mappers.Abstract;
using ASM_01.DataAccessLayer.Entities.Warehouse;
using ASM_01.DataAccessLayer.Enums;

namespace ASM_01.BusinessLayer.Mappers
{
    /// <summary>
    /// Mapper chuyển đổi dữ liệu phân phối
    /// </summary>
    public class DistributionMapper : IDistributionMapper
    {
        public DistributionRequestDto MapToDistributionRequestDto(DistributionRequest request)
        {
            return new DistributionRequestDto
            {
                RequestId = request.DistributionRequestId,
                DealerId = request.DealerId,
                ModelName = request.EvTrim?.EvModel?.ModelName ?? "N/A",
                TrimName = request.EvTrim?.TrimName ?? "N/A",
                RequestQuantity = request.RequestedQuantity,
                ApprovedQuantity = request.ApprovedQuantity ?? 0,
                ModelYear = request.EvTrim?.ModelYear ?? DateTime.MinValue.Year,
                ApprovalDate = request.ApprovedAt,
                RequestDate = request.RequestedAt,
                DealerName = request.Dealer?.Name ?? "N/A",
                Status = request.Status.ToString()
            };
        }

        public DistributionRequest MapToDistributionRequest(CreateDistributionRequestDto dto)
        {
            return new DistributionRequest
            {
                DealerId = dto.DealerId,
                EvTrimId = dto.EvTrimId,
                RequestedQuantity = dto.RequestedQuantity,
                Status = DistributionStatus.Pending,
                RequestedAt = DateTime.UtcNow
            };
        }
    }
}
