using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Services.Abstract;
using ASM_01.BusinessLayer.Mappers.Abstract;
using ASM_01.DataAccessLayer.Repositories.Abstract;

namespace ASM_01.BusinessLayer.Services;

public class DealerInventoryService : IDealerInventoryService
{
    private readonly IDealerRepository _dealerRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IDealerMapper _dealerMapper;

    public DealerInventoryService(IDealerRepository dealerRepository, IStockRepository stockRepository, IDealerMapper dealerMapper)
    {
        _dealerRepository = dealerRepository;
        _stockRepository = stockRepository;
        _dealerMapper = dealerMapper;
    }
    public async Task<IReadOnlyList<DealerDto>> GetDealers(CancellationToken ct = default)
    {
        var dealers = await _dealerRepository.GetAllDealersAsync();
        return dealers.Select(d => _dealerMapper.MapToDealerDto(d))
        .OrderBy(d => d.Name)
        .ToList();
    }

    public async Task<int> GetStockByTrimAsync(int dealerId, int evTrimId, CancellationToken ct = default)
    {
        return await _stockRepository.GetStockQuantityAsync(dealerId, evTrimId);
    }

    public async Task<IReadOnlyList<DealerStockDto>> GetDealerInventoryAsync(int dealerId, CancellationToken ct = default)
    {
        var stocks = await _stockRepository.GetStocksByDealerIdAsync(dealerId);
        return stocks.Select(s => _dealerMapper.MapToDealerStockDto(s))
        .OrderBy(v => v.ModelName).ThenBy(v => v.TrimName)
        .ToList();
    }

    public async Task<IReadOnlyList<ModelStockDto>> GetInventoryByModelAsync(int dealerId, CancellationToken ct = default)
    {
        var stocks = await _stockRepository.GetStocksByDealerIdAsync(dealerId);
        return stocks
            .GroupBy(s => s.EvTrim.EvModel.ModelName)
            .Select(g => _dealerMapper.MapToModelStockDto(g.Key, g))
            .OrderBy(v => v.ModelName)
            .ToList();
    }
}