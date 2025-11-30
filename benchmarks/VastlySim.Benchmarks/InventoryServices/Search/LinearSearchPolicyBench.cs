namespace Benchmarks.InventoryServices.Search;
using BenchmarkDotNet.Attributes;
using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Search;

[MemoryDiagnoser]
public class LinearSearchPolicyBench
{
    private LinearSearchPolicy _policy;

    private ItemStack[] _smallStacks;
    private ItemStack[] _mediumStacks;
    private ItemStack[] _largeStacks;

    private ItemId _target;

    [Params(10, 100, 1000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _policy = new LinearSearchPolicy();
        _target = new ItemId(9999);

        _smallStacks = Build(10);
        _mediumStacks = Build(100);
        _largeStacks = Build(1000);
    }

    private ItemStack[] Build(int count)
    {
        var arr = new ItemStack[count];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = new ItemStack
            {
                ItemId = new ItemId((uint)i),
                Quantity = 1f
            };
        }

        // Make sure the target exists at the end (worst case)
        arr[^1] = new ItemStack
        {
            ItemId = _target,
            Quantity = 999f
        };

        return arr;
    }

    [Benchmark(Baseline = true, Description = "Linear search (10 items)")]
    public int SearchSmall()
    {
        return _policy.FindIndex(_smallStacks, _target);
    }

    [Benchmark(Description = "Linear search (100 items)")]
    public int SearchMedium()
    {
        return _policy.FindIndex(_mediumStacks, _target);
    }

    [Benchmark(Description = "Linear search (1000 items)")]
    public int SearchLarge()
    {
        return _policy.FindIndex(_largeStacks, _target);
    }
}