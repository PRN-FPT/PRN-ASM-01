using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Services.Abstract;
using ASM_01.BusinessLayer.Mappers.Abstract;
using ASM_01.DataAccessLayer.Entities.VehicleModels;
using ASM_01.DataAccessLayer.Repositories.Abstract;

namespace ASM_01.BusinessLayer.Services;

public class VehicleService(IVehicleRepository _vehicleRepository, IUnitOfWork _unitOfWork, IVehicleMapper _vehicleMapper) : IVehicleService
{
    public async Task<IEnumerable<VehicleDto>> GetAllVehicles()
    {
        var trims = await _vehicleRepository.GetAllTrimsAsync();

        var vehicles = trims.Select(t =>
        {
            var latestPrice = t.Prices
                .OrderByDescending(p => p.EffectiveDate)
                .FirstOrDefault();

            return _vehicleMapper.MapToVehicleDto(t, latestPrice, new Dictionary<string, string>());
        }).ToList();

        var specs = await _vehicleRepository.GetAllSpecsAsync();

        foreach (var v in vehicles)
        {
            var trimSpecs = await _vehicleRepository.GetSpecsByTrimIdAsync(v.VehicleId);
            var vehicleSpecs = trimSpecs.ToDictionary(
                ts => ts.Spec.SpecName,
                ts => ts.Value + (ts.Spec.Unit != null ? $" {ts.Spec.Unit}" : "")
            );

            v.Specifications = vehicleSpecs;
        }
        return vehicles;
    }

    public async Task<VehicleDto?> GetVehicleById(int id)
    {
        var trim = await _vehicleRepository.GetTrimByIdAsync(id);

        if (trim == null)
            return null;

        var latestPrice = await _vehicleRepository.GetLatestPriceByTrimIdAsync(id);
        var trimSpecs = await _vehicleRepository.GetSpecsByTrimIdAsync(id);
        var specs = trimSpecs.ToDictionary(
            ts => ts.Spec.SpecName,
            ts => ts.Value + (ts.Spec.Unit != null ? $" {ts.Spec.Unit}" : "")
        );

        return _vehicleMapper.MapToVehicleDto(trim, latestPrice, specs);
    }

    public async Task<IEnumerable<VehicleDto>> SearchVehicles(string keyword)
    {
        var trims = await _vehicleRepository.SearchTrimsAsync(keyword);

        var result = new List<VehicleDto>();

        foreach (var trim in trims)
        {
            var latestPrice = await _vehicleRepository.GetLatestPriceByTrimIdAsync(trim.EvTrimId);
            var trimSpecs = await _vehicleRepository.GetSpecsByTrimIdAsync(trim.EvTrimId);
            var specs = trimSpecs.ToDictionary(
                ts => ts.Spec.SpecName,
                ts => ts.Value + (ts.Spec.Unit != null ? $" {ts.Spec.Unit}" : "")
            );

            result.Add(_vehicleMapper.MapToVehicleDto(trim, latestPrice, specs));
        }

        return result;
    }

    public async Task<IEnumerable<VehicleComparisonDto>> CompareVehicles(int[] vehicleIds)
    {
        var result = new List<VehicleComparisonDto>();

        foreach (var vehicleId in vehicleIds)
        {
            var trim = await _vehicleRepository.GetTrimByIdAsync(vehicleId);
            if (trim == null) continue;

            var latestPrice = await _vehicleRepository.GetLatestPriceByTrimIdAsync(vehicleId);
            var trimSpecs = await _vehicleRepository.GetSpecsByTrimIdAsync(vehicleId);
            var specs = trimSpecs.ToDictionary(
                ts => ts.Spec.SpecName,
                ts => ts.Value + (ts.Spec.Unit != null ? $" {ts.Spec.Unit}" : "")
            );

            result.Add(_vehicleMapper.MapToVehicleComparisonDto(trim, latestPrice, specs));
        }

        return result;
    }

    public async Task<EvModel> CreateVehicleModelAsync(CreateVehicleModelDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.ModelName))
            throw new ArgumentException("Model name is required.");

        var exists = await _vehicleRepository.ModelNameExistsAsync(dto.ModelName);
        if (exists)
            throw new InvalidOperationException("A model with this name already exists.");

        var model = _vehicleMapper.MapToEvModel(dto);

        return await _vehicleRepository.CreateModelAsync(model);
    }

    public async Task<EvTrim> CreateVehicleTrimAsync(CreateVehicleTrimDto dto, CancellationToken ct = default)
    {
        // Validate model exists
        var model = await _vehicleRepository.GetModelByIdAsync(dto.EvModelId);
        if (model == null)
            throw new InvalidOperationException("Vehicle model not found.");

        var trim = _vehicleMapper.MapToEvTrim(dto);

        var createdTrim = await _vehicleRepository.CreateTrimAsync(trim);

        if (dto.ListedPrice.HasValue)
        {
            var priceDto = new UpdateVehicleTrimPriceDto
            {
                EvTrimId = createdTrim.EvTrimId,
                NewListedPrice = dto.ListedPrice.Value,
                EffectiveDate = DateTime.UtcNow
            };
            var price = _vehicleMapper.MapToTrimPrice(priceDto);
            await _vehicleRepository.CreatePriceAsync(price);
        }

        return createdTrim;
    }

    public async Task<EvModel> UpdateVehicleModelStatusAsync(UpdateVehicleModelStatusDto dto, CancellationToken ct = default)
    {
        // Find the model
        var model = await _vehicleRepository.GetModelByIdAsync(dto.EvModelId);
        if (model == null)
            throw new InvalidOperationException("Vehicle model not found.");

        // Update status
        model.Status = dto.Status;
        model.Description = dto.Description;

        return await _vehicleRepository.UpdateModelAsync(model);
    }
    
    public async Task<TrimPrice> UpdateVehicleTrimPriceAsync(UpdateVehicleTrimPriceDto dto, CancellationToken ct = default)
    {
        if (dto.NewListedPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(dto.NewListedPrice), "Price must be greater than zero.");

        // Validate trim exists
        var trim = await _vehicleRepository.GetTrimByIdAsync(dto.EvTrimId);
        if (trim == null)
            throw new InvalidOperationException("EV Trim not found.");

        // Create new price record (we keep history)
        var newPrice = _vehicleMapper.MapToTrimPrice(dto);

        return await _vehicleRepository.CreatePriceAsync(newPrice);
    }

    public async Task<VehicleModelDto?> GetModelAsync(int id)
    {
        var model = await _vehicleRepository.GetModelByIdAsync(id);

        if (model == null) return null;

        return _vehicleMapper.MapToVehicleModelDto(model);
    }
    
    public async Task<IEnumerable<VehicleModelDto>> GetAllModel()
    {
        var models = await _vehicleRepository.GetAllModelsAsync();
        return models.Select(model => _vehicleMapper.MapToVehicleModelDto(model));
    }
}
