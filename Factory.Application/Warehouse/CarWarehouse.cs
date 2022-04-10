using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public class CarWarehouse
    {
        private readonly object locker = new object();

        private readonly uint _capacity;

        private Queue<Car> _cars;
        private CarMediator _carMediator;
        protected Queue<Dealer> _dealers;

        public CarWarehouse(uint capcity, CarMediator carMediator = null)
        {
            _capacity = capcity;
            _carMediator = carMediator;
            _cars = new Queue<Car>();
            _dealers = new Queue<Dealer>();
        }

        public void HandleOrder(Dealer dealer)
        {
            if (_cars.Count > 0)
            {
                lock (locker)
                {
                    if (_cars.Count > 0)
                    {
                        Console.WriteLine($"Warehouse has a car");

                        dealer.TakeCar(_cars.Dequeue());

                        _carMediator.Notify(CreatingStatus.CanCreate);
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Warehouse has no car");
                        _dealers.Enqueue(dealer);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Warehouse has no car");
                _dealers.Enqueue(dealer);
            }
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

            if (_cars.Count() < _capacity)
            {
                _carMediator.Notify(CreatingStatus.CanCreate);
            }
            else
            {
                Console.WriteLine($"CarWarehouse Full");
            }
        }

        public void Start()
        {
            if (_cars.Count() < _capacity)
            {
                _carMediator.Notify(CreatingStatus.CanCreate);
            }
        }

        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }
    }
}
