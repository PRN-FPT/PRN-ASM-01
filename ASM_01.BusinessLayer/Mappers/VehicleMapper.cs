using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Mappers.Abstract;
using ASM_01.DataAccessLayer.Entities.VehicleModels;

namespace ASM_01.BusinessLayer.Mappers
{
    /// <summary>
    /// Mapper chuyển đổi dữ liệu xe điện
    /// </summary>
    public class VehicleMapper : IVehicleMapper
    {
        public VehicleDto MapToVehicleDto(EvTrim trim, TrimPrice? latestPrice, Dictionary<string, string> specifications)
        {
            return new VehicleDto
            {
                ModelId = trim.EvModelId,
                VehicleId = trim.EvTrimId,
                ModelName = trim.EvModel.ModelName,
                TrimName = trim.TrimName,
                TrimId = trim.EvTrimId,
                ModelYear = trim.ModelYear,
                Description = trim.Description,
                Status = trim.EvModel.Status.ToString(),
                Price = latestPrice?.ListedPrice ?? 0,
                EffectiveDate = latestPrice?.EffectiveDate ?? DateTime.MinValue,
                Specifications = specifications
            };
        }

        public VehicleModelDto MapToVehicleModelDto(EvModel model)
        {
            return new VehicleModelDto
            {
                EvModelId = model.EvModelId,
                ModelName = model.ModelName,
                Description = model.Description,
                ModelYear = model.Trims.Max(t => t.ModelYear),
                Status = model.Status
            };
        }

        public VehicleComparisonDto MapToVehicleComparisonDto(EvTrim trim, TrimPrice? latestPrice, Dictionary<string, string> specifications)
        {
            return new VehicleComparisonDto
            {
                VehicleId = trim.EvTrimId,
                ModelName = trim.EvModel.ModelName,
                TrimName = trim.TrimName,
                Price = latestPrice?.ListedPrice ?? 0,
                EffectiveDate = latestPrice?.EffectiveDate ?? DateTime.MinValue,
                Specifications = specifications
            };
        }

        public EvModel MapToEvModel(CreateVehicleModelDto dto)
        {
            return new EvModel
            {
                ModelName = dto.ModelName,
                Description = dto.Description,
                Status = dto.Status
            };
        }

        public EvTrim MapToEvTrim(CreateVehicleTrimDto dto)
        {
            return new EvTrim
            {
                EvModelId = dto.EvModelId,
                TrimName = dto.TrimName,
                ModelYear = dto.ModelYear,
                Description = dto.Description
            };
        }

        public TrimPrice MapToTrimPrice(UpdateVehicleTrimPriceDto dto)
        {
            return new TrimPrice
            {
                EvTrimId = dto.EvTrimId,
                ListedPrice = dto.NewListedPrice,
                EffectiveDate = dto.EffectiveDate ?? DateTime.UtcNow
            };
        }
    }
}
