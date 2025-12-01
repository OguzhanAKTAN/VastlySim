using VastlySim.InventoryServices;
using VastlySim.InventoryServices.Data;
using VastlySim.InventoryServices.Policies.Capacity;
using VastlySim.InventoryServices.Policies.Search;
namespace VastlySimTests.InventoryServices
{
    public class InventoryServiceTests
    {
        private InventoryService _service;
        private readonly ItemId Apple = new ItemId(1);
        private readonly ItemId Bread = new ItemId(2);

        [SetUp]
        public void Setup()
        {
            _service = new InventoryService(
                new DefaultCapacityPolicy(),
                new LinearSearchPolicy());
        }

        private Inventory Make(params (ItemId id, float qty)[] stacks)
        {
            var items = new ItemStack[stacks.Length];
            for (int i = 0; i < stacks.Length; i++)
            {
                items[i] = new ItemStack
                {
                    ItemId = stacks[i].id,
                    Quantity = stacks[i].qty
                };
            }

            return new Inventory { Stacks = items };
        }

        // --------------------------
        // ADD TESTS
        // --------------------------

        [Test]
        public void Add_IncreasesQuantity_IfItemExists()
        {
            var inv = Make((Apple, 10));

            bool ok = _service.Add(ref inv, Apple, 5);

            Assert.That(ok);
            Assert.That(inv.Stacks[0].Quantity, Is.EqualTo(15));
        }

        [Test]
        public void Add_Fails_IfItemDoesNotExist()
        {
            var inv = Make((Apple, 10));

            bool ok = _service.Add(ref inv, Bread, 5);

            Assert.That(!ok);
        }

        [Test]
        public void Add_RespectsCapacityPolicy()
        {
            var inv = Make((Apple, 10));
            var svc = new InventoryService(
                new DenyAddPolicy(),
                new LinearSearchPolicy());

            bool ok = svc.Add(ref inv, Apple, 3);

            Assert.That(!ok);
            Assert.That(inv.Stacks[0].Quantity, Is.EqualTo(10));
        }

        private class DenyAddPolicy : ICapacityPolicy
        {
            public bool CanAdd(in Inventory inv, ItemId id, float q) => false;
            public bool CanRemove(in Inventory inv, ItemId id, float q) => true;
        }

        // --------------------------
        // REMOVE TESTS
        // --------------------------

        [Test]
        public void Remove_DecreasesQuantity_IfEnoughExists()
        {
            var inv = Make((Apple, 10));

            bool ok = _service.Remove(ref inv, Apple, 4);

            Assert.That(ok);
            Assert.That(inv.Stacks[0].Quantity, Is.EqualTo(6));
        }

        [Test]
        public void Remove_Fails_IfNotEnoughQuantity()
        {
            var inv = Make((Apple, 3));

            bool ok = _service.Remove(ref inv, Apple, 5);

            Assert.That(!ok);
        }

        [Test]
        public void Remove_Fails_IfItemNotFound()
        {
            var inv = Make((Apple, 3));

            bool ok = _service.Remove(ref inv, Bread, 1);

            Assert.That(ok, Is.False);
        }

        [Test]
        public void Remove_RespectsCapacityPolicy()
        {
            var inv = Make((Apple, 10));
            var svc = new InventoryService(
                new DenyRemovePolicy(),
                new LinearSearchPolicy());

            bool ok = svc.Remove(ref inv, Apple, 3);

            Assert.That(ok, Is.False);
            Assert.That(inv.Stacks[0].Quantity, Is.EqualTo(10));
        }

        private class DenyRemovePolicy : ICapacityPolicy
        {
            public bool CanAdd(in Inventory inv, ItemId id, float q) => true;
            public bool CanRemove(in Inventory inv, ItemId id, float q) => false;
        }

        // --------------------------
        // GET QUANTITY
        // --------------------------

        [Test]
        public void GetQuantity_ReturnsCorrectAmount()
        {
            var inv = Make((Apple, 12), (Bread, 7));

            float qty = _service.GetQuantity(inv, Bread);

            Assert.That(qty, Is.EqualTo(7));
        }

        [Test]
        public void GetQuantity_ReturnsZero_IfMissing()
        {
            var inv = Make((Apple, 12));

            float qty = _service.GetQuantity(inv, Bread);

            Assert.That(qty, Is.EqualTo(0));
        }
    }
}
