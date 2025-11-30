using VastlySim.InventoryServices.Data;
namespace VastlySim.InventoryServices.Policies.Search;

public interface ISearchPolicy
{
    /// <summary>
    /// Returns index of the stack containing this item.
    /// Returns -1 if not found.
    /// Must not modify inventory.
    /// </summary>
    int FindIndex(ItemStack[] stacks, ItemId itemId);
}