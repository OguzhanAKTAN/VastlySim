using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Capacity;
namespace VastlySimTests.InventoryServices.Policies.Capacity
{
    public class DefaultCapacityPolicyTests
    {
        [Test]
        public void DefaultPolicy_AlwaysAllowsAdd()
        {
            var policy = new DefaultCapacityPolicy();
            var inv = new Inventory { Stacks = new ItemStack[1] };

            bool ok = policy.CanAdd(inv, new ItemId(1), 10);

            Assert.That(ok);
        }

        [Test]
        public void DefaultPolicy_AlwaysAllowsRemove()
        {
            var policy = new DefaultCapacityPolicy();
            var inv = new Inventory { Stacks = new ItemStack[1] };

            bool ok = policy.CanRemove(inv, new ItemId(1), 5);

            Assert.That(ok);
        }
    }
}