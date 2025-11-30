using VastlySim.InventoryServices.Data;
namespace VastlySim.InventoryServices.Policies.Capacity;

public class DefaultCapacityPolicy : ICapacityPolicy 
{
    public bool CanAdd(in Inventory inv, ItemId item, float quantity) => true;
    public bool CanRemove(in Inventory inv, ItemId item, float quantity) => true;
}