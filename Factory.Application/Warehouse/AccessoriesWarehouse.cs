using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

namespace Factory.Core.Warehouse
{
    public class AccessoriesWarehouse : DetailsWarehouse<Accessories>
    {
        public AccessoriesWarehouse(uint capcity, DetailsMediator<Accessories> detailsMediator = null)
            : base(capcity, detailsMediator)
        {
        }

        public override void HandleOrder(CarBuilder carBuilder)
        {
            if (Details.Count > 0)
            {
                Console.WriteLine($"Warehouse has accessories");

                carBuilder.TakeAccessories(Details.Dequeue());
                _detailsMediator.Notify(CreatingStatus.CanCreate);
                return;
            }

            CarBuilders.Enqueue(carBuilder);
            Console.WriteLine($"Warehouse has no accessories");
        }

        public override void AddDetail(Accessories detail)
        {
            if (CarBuilders.Count != 0)
            {
                var carBuilder = CarBuilders.Dequeue();

                carBuilder.TakeAccessories(detail);
            }
            else
            {
                Details.Enqueue(detail);
            }

            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(CreatingStatus.CanCreate);
            }
            else
            {
                Console.WriteLine($"AccessoriesWarehouse Full");
            }
        }

        public void Start()
        {
            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(CreatingStatus.CanCreate);
            }
        }
    }
}
