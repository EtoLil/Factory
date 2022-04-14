using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;

namespace Factory.Core.Buiders
{
    public class CarDirector : ICarDirector
    {
        private IDetailsWarehouse<Body> _bodyWarehouse;
        private IDetailsWarehouse<Engine> _engineWarehouse;
        private IDetailsWarehouse<Accessories> _accessoriesWarehouse;
        private IMediator<ICar> _carMediator;

        private ManualResetEvent _event;

        private ICarBulder _carBulder;

        private bool _isEngineReceived;
        private bool _isBodyReceived;
        private bool _isAccessoriesReceived;

        private int _id;

        public CarDirector(
            int id,
            IDetailsWarehouse<Engine> engineWarehouse,
            IDetailsWarehouse<Body> bodyWarehouse,
            IDetailsWarehouse<Accessories> accessoriesWarehouse,
            IMediator<ICar> carMediator = null
            )
        {
            _carMediator = carMediator;

            _engineWarehouse = engineWarehouse;
            _bodyWarehouse = bodyWarehouse;
            _accessoriesWarehouse = accessoriesWarehouse;

            _event = new ManualResetEvent(true);
            _id=id;
            _carBulder = new CarBulder(id);
        }

        public void Reset()
        {
            Console.WriteLine($"Reset");

            _isEngineReceived = false;
            _isBodyReceived = false;
            _isAccessoriesReceived = false;
        }

        public void TakeEngine(Engine engine)
        {
            Console.WriteLine($"Bulder-{_id} Pass Engine-{engine.Id}");

            _carBulder.BuildEngine(engine);
            _isEngineReceived = true;
            Check();
        }

        public void TakeBody(Body body)
        {
            Console.WriteLine($"Bulder-{_id} Pass Body-{body.Id}");
            _carBulder.BuildBody(body);
            _isBodyReceived = true;
            Check();
        }

        public void TakeAccessories(Accessories accessories)
        {
            Console.WriteLine($"Bulder-{_id} Accessories-{accessories.Id}");
            _carBulder.BuildAccessories(accessories);
            _isAccessoriesReceived = true;
            Check();
        }

        public void HandleOrder()
        {
            Console.WriteLine($"Director-{_id} Make Order For Details");

            _engineWarehouse.HandleOrder(this);
            _bodyWarehouse.HandleOrder(this);
            _accessoriesWarehouse.HandleOrder(this);

            Check();
            _event.WaitOne();
            Send();
        }



        public void SetMediator(IMediator<ICar> carMediator)
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
            return _isEngineReceived
                && _isAccessoriesReceived
                && _isBodyReceived;
        }

        public void Send()
        {
            var car = _carBulder.GetResult();
            Reset();
            Console.WriteLine($"CarDirector {_id}: Send Car");
            _carMediator.Notify(CreatingStatus.Created, car, _id);
        }
    }
}
