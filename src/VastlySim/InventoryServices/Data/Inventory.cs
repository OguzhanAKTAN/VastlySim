namespace VastlySim.InventoryServices.Data;

public struct Inventory(ItemStack[] stacks)
{
    public ItemStack[] Stacks = stacks;
}