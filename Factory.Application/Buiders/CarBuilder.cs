using Factory.Core.Mediators;
using Factory.Core.Warehouse;

namespace Factory.Core.Buiders
{
    public class CarBuilder : ICarBuilder
    {
        private DetailsWarehouse<Body> _bodyWarehouse;
        private DetailsWarehouse<Engine> _engineWarehouse;
        private DetailsWarehouse<Accessories> _accessoriesWarehouse;
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

        public void PassEngine(Engine engine)
        {
            Console.WriteLine($"Pass Engine");

            _engine = engine;
            CheckCarStatus();
        }

        public void PassBody(Body body)
        {
            Console.WriteLine($"Pass Body");

            _body = body;
            CheckCarStatus();
        }

        public void PassAccessories(Accessories accessories)
        {
            Console.WriteLine($"Pass Accessories");

            _accessories = accessories;
            CheckCarStatus();
        }

        private void CheckCarStatus()
        {
            //TODO: ThreadPool

            if (_engine is not null
                && _body is not null
                && _accessories is not null
            )
            {
                var car = new Car(_engine, _body, _accessories);
                Reset();

                Thread.Sleep(1000);

                _carMediator.Notify(car, CarEventType.CarCreated);
            }
        }

        public void MakeOrder()
        {
            Console.WriteLine($"Car: make order");

            _engineWarehouse.MakeOrder(this);
            _bodyWarehouse.MakeOrder(this);
            _accessoriesWarehouse.MakeOrder(this);
        }
        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }
    }
}
