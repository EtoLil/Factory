﻿using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public class BodyWarehouse : DetailsWarehouse<Body>
    {
        public BodyWarehouse(
            uint capcity,
            DetailsMediator<Body> detailsMediator = null
            )
            : base(capcity, detailsMediator)
        {
        }

        public override void MakeOrder(CarBuilder carBuilder)
        {
            if (Details.Count > 0)
            {
                Console.WriteLine($"Warehouse has a body");

                carBuilder.PassBody(Details.Dequeue());
                return;
            }

            Console.WriteLine($"Warehouse has no body");
            CarBuilders.Enqueue(carBuilder);
        }

        public override void AddDetail(Body detail)
        {
            if (CarBuilders.Count != 0)
            {
                var carBuilder = CarBuilders.Dequeue();

                carBuilder.PassBody(detail);
            }
            else
            {
                Details.Enqueue(detail);
            }

            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(null, EventType.WarehouseNotFull);
            }
            else
            {
                Console.WriteLine($"BodyWarehouse Full");
            }
        }

        public void Start()
        {
            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(null, EventType.WarehouseNotFull);
            }
        }
    }
}
