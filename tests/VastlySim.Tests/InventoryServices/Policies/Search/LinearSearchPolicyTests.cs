using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Search;
namespace VastlySimTests.InventoryServices.Policies.Search
{
    public class LinearSearchPolicyTests
    {
        private readonly ItemId Apple = new ItemId(1);
        private readonly ItemId Bread = new ItemId(2);

        [Test]
        public void FindIndex_ReturnsCorrectIndex()
        {
            var stacks = new[]
            {
                new ItemStack { ItemId = Apple, Quantity = 5 },
                new ItemStack { ItemId = Bread, Quantity = 10 }
            };

            var policy = new LinearSearchPolicy();

            int index = policy.FindIndex(stacks, Bread);

            Assert.That(index, Is.EqualTo(1));
        }

        [Test]
        public void FindIndex_ReturnsMinusOne_IfNotFound()
        {
            var stacks = new[]
            {
                new ItemStack { ItemId = Apple, Quantity = 5 }
            };

            var policy = new LinearSearchPolicy();

            int index = policy.FindIndex(stacks, Bread);

            Assert.That(index, Is.EqualTo(-1));
        }
    }
}