using Factory.Core.Entities;
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

        private ManualResetEvent _event;

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

            _event = new ManualResetEvent(true);
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
            Check();
        }

        public void TakeBody(Body body)
        {
            Console.WriteLine($"Pass Body");

            _body = body;
            Check();
        }

        public void TakeAccessories(Accessories accessories)
        {
            Console.WriteLine($"Pass Accessories");

            _accessories = accessories;
            Check();
        }

        public void HandleOrder()
        {
            Console.WriteLine($"Car: make order");

            _engineWarehouse.HandleOrder(this);
            _bodyWarehouse.HandleOrder(this);
            _accessoriesWarehouse.HandleOrder(this);

            Check();
            _event.WaitOne();
            Send();
        }



        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }

        private void Check()
        {
            if (AreAllPartsReady())
            {
                _event.Set();
            }
            else
            {
                _event.Reset();
            }
        }


        private bool AreAllPartsReady()
        {
            return _engine is not null
                && _body is not null
                && _accessories is not null;
        }

        public void Send()
        {
            Car car = Create();
            _carMediator.Notify(CreatingStatus.Created, car);

        }
        public Car Create()
        {
            Thread.Sleep(Configure.CarCreateTime);
            var car = new Car(_engine, _body, _accessories);
            Reset();
            return car;
        }
    }
}
