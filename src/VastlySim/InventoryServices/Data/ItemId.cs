namespace VastlySim.InventoryServices.Data;

public readonly struct ItemId(uint value)
{
    public readonly uint Value = value;
}