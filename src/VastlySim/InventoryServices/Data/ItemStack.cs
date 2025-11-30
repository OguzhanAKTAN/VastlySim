namespace VastlySim.InventoryServices.Data;

public struct ItemStack(ItemId itemId)
{
    public ItemId ItemId = itemId;
    public float Quantity;
}