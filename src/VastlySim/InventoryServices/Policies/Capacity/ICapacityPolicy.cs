using VastlySim.InventoryServices.Data;
namespace VastlySim.InventoryServices.Policies.Capacity;

public interface ICapacityPolicy
{
    bool CanAdd(in InventoryServices.Data.Inventory inv, ItemId item, float quantity);
    bool CanRemove(in InventoryServices.Data.Inventory inv, ItemId item, float quantity);
}