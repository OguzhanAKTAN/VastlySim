namespace Benchmarks.InventoryServices;
using BenchmarkDotNet.Attributes;
using VastlySim.InventoryServices;
using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Capacity;
using VastlySim.InventoryServices.Policies.Search;

[MemoryDiagnoser]
public class InventoryServiceBench
{
    private InventoryService _svc;
    private Inventory _inventory;
    private ItemId _itemId;

    [GlobalSetup]
    public void Setup()
    {
        _svc = new InventoryService(
            new DefaultCapacityPolicy(),
            new LinearSearchPolicy());

        _itemId = new ItemId(1);

        _inventory = new Inventory(new[]
        {
            new ItemStack { ItemId = _itemId, Quantity = 10 }
        });
    }

    [Benchmark(Baseline = true)]
    public bool Add()
    {
        return _svc.Add(ref _inventory, _itemId, 1);
    }

    [Benchmark]
    public bool Remove()
    {
        return _svc.Remove(ref _inventory, _itemId, 1);
    }
}