using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Capacity;
using VastlySim.InventoryServices.Policies.Search;
namespace VastlySim.InventoryServices;

public class InventoryService
{
    private readonly ICapacityPolicy _capacity;
    private readonly ISearchPolicy _search;

    public InventoryService()
    {
        _capacity = new DefaultCapacityPolicy();
        _search = new LinearSearchPolicy();
    }
    
    public InventoryService(ICapacityPolicy capacityPolicy, ISearchPolicy searchPolicy)
    {
        _capacity = capacityPolicy;
        _search = searchPolicy;
    }

    /// <summary>
    /// Attempts to add quantity of an item to the inventory.
    /// Does not resize or create new stacks (policy-controlled).
    /// </summary>
    public virtual bool Add(ref Inventory inv, ItemId id, float quantity)
    {
        if (!_capacity.CanAdd(inv, id, quantity))
            return false;

        int index = _search.FindIndex(inv.Stacks, id);

        if (index >= 0)
        {
            // Found existing stack → mutate
            inv.Stacks[index].Quantity += quantity;
            return true;
        }

        // No stack found → not this service's job to create one
        return false;
    }

    /// <summary>
    /// Attempts to remove quantity from existing stack.
    /// </summary>
    public virtual bool Remove(ref Inventory inv, ItemId id, float quantity)
    {
        if (!_capacity.CanRemove(inv, id, quantity))
            return false;

        int index = _search.FindIndex(inv.Stacks, id);
        if (index < 0)
            return false;

        ref ItemStack stack = ref inv.Stacks[index];

        if (stack.Quantity < quantity)
            return false;

        stack.Quantity -= quantity;
        return true;
    }

    /// <summary>
    /// Retrieves quantity without modifying anything.
    /// </summary>
    public virtual float GetQuantity(in Inventory inv, ItemId id)
    {
        int index = _search.FindIndex(inv.Stacks, id);
        return index >= 0 ? inv.Stacks[index].Quantity : 0f;
    }
}