using VastlySim.InventoryServices.Data;
namespace VastlySim.InventoryServices.Policies.Search;

public sealed class LinearSearchPolicy : ISearchPolicy
{
    public int FindIndex(ItemStack[] stacks, ItemId itemId)
    {
        var id = itemId.Value;

        for (var i = 0; i < stacks.Length; i++)
        {
            if (stacks[i].ItemId.Value == id)
                return i;
        }

        return -1;
    }
}