﻿using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;

namespace Factory.Core.Buiders
{
    public class CarBuilder : ICarBuilder
    {
        private BodyWarehouse _bodyWarehouse;
        private EngineWarehouse _engineWarehouse;
        private AccessoriesWarehouse _accessoriesWarehouse;
        private CarMediator _carMediator;

        private Engine _engine;
        private Body _body;
        private Accessories _accessories;

        public CarBuilder(
            EngineWarehouse engineWarehouse,
            BodyWarehouse bodyWarehouse,
            AccessoriesWarehouse accessoriesWarehouse,
            CarMediator carMediator = null
            )
        {
            _carMediator = carMediator;

            _engineWarehouse = engineWarehouse;
            _bodyWarehouse = bodyWarehouse;
            _accessoriesWarehouse = accessoriesWarehouse;
        }

        public void Reset()
        {
            Console.WriteLine($"Reset");

            _accessories = null;
            _engine = null;
            _body = null;
        }

        public void TakeEngine(Engine engine)
        {
            Console.WriteLine($"Pass Engine");

            _engine = engine;
            TryToCreate();
        }

        public void TakeBody(Body body)
        {
            Console.WriteLine($"Pass Body");

            _body = body;
            TryToCreate();
        }

        public void TakeAccessories(Accessories accessories)
        {
            Console.WriteLine($"Pass Accessories");

            _accessories = accessories;
            TryToCreate();
        }

        public void HandleOrder()
        {
            Console.WriteLine($"Car: make order");

            _engineWarehouse.HandleOrder(this);
            _bodyWarehouse.HandleOrder(this);
            _accessoriesWarehouse.HandleOrder(this);
        }

        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }

        private void TryToCreate()
        {
            if (AreAllPartsReady())
            {
                CreateCar();
            }
        }

        private bool AreAllPartsReady()
        {
            return _engine is not null
                && _body is not null
                && _accessories is not null;
        }

        private void CreateCar()
        {
            Thread.Sleep(Configure.CarCreateTime);
            var car = new Car(_engine, _body, _accessories);

            Reset();

            _carMediator.Notify(CreatingStatus.Created, car);
        }
    }
}
