namespace Benchmarks.InventoryServices.Capacity;
using BenchmarkDotNet.Attributes;
using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Capacity;

[MemoryDiagnoser]
public class DefaultCapacityPolicyBench
{
    private DefaultCapacityPolicy _policy;
    private Inventory _inventory;
    private ItemId _itemId;

    [GlobalSetup]
    public void Setup()
    {
        _policy = new DefaultCapacityPolicy();
        _inventory = new Inventory(new ItemStack[1]);
        _itemId = new ItemId(1);
    }

    [Benchmark(Baseline = true)]
    public bool CanAdd()
    {
        return _policy.CanAdd(_inventory, _itemId, 10f);
    }

    [Benchmark]
    public bool CanRemove()
    {
        return _policy.CanRemove(_inventory, _itemId, 10f);
    }
}