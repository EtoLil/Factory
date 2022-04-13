using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using System.Collections.Concurrent;

namespace Factory.Core.Warehouse
{
    public class CarWarehouse : ICarWarehouse
    {
        private readonly uint _capacity;

        private ConcurrentQueue<Car> _cars;
        private CarMediator _carMediator;
        protected Queue<Dealer> _dealers;

        protected ManualResetEvent _event = new ManualResetEvent(true);
        protected Task _worker;
        public CarWarehouse(uint capcity, CarMediator carMediator = null)
        {
            _capacity = capcity;
            _carMediator = carMediator;
            _cars = new ConcurrentQueue<Car>();
            _dealers = new Queue<Dealer>();
            _worker = new Task(Init);
        }

        public void HandleOrder(Dealer dealer)
        {
            if (_cars.Count > 0 && _cars.TryDequeue(out Car? car))
            {
                Console.WriteLine($"Warehouse has a car");

                dealer.TakeCar(car);

                _event.Set();
                return;
            }

            Console.WriteLine($"Warehouse has no car");
            _dealers.Enqueue(dealer);
        }

        public void AddCar(Car car)
        {
            if (_dealers.Count != 0)
            {
                var dealer = _dealers.Dequeue();

                dealer.TakeCar(car);
            }
            else
            {
                _cars.Enqueue(car);
            }

            if (_cars.Count() == _capacity)
            {
                _event.Reset();
                Console.WriteLine($"CarWarehouse Full");
            }
            _event.WaitOne();
            _carMediator.Notify(CreatingStatus.CanCreate);
        }

        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }

        public void Init()
        {
            if (_cars.Count() < _capacity)
            {
                _carMediator.Notify(CreatingStatus.CanCreate);
            }
        }

        public void Run()
        {
            _worker.Start();
        }
    }
}
