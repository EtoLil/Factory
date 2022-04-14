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
        protected int _notyfyCount;

        private ConcurrentQueue<ICar> _cars;
        private IList<IMediator<ICar>> _carMediators;
        protected Queue<IDealer> _dealers;

        protected ManualResetEvent _event = new ManualResetEvent(true);
        protected Task _worker;
        protected static object _locker = new object();
        public CarWarehouse(uint capcity, IList<IMediator<ICar>> carMediators = null)
        {
            _capacity = capcity;
            _carMediators = carMediators;
            _cars = new ConcurrentQueue<ICar>();
            _dealers = new Queue<IDealer>();
            _worker = new Task(Init);
            _notyfyCount = 0;
            if (_carMediators is null)
            {
                _carMediators = new List<IMediator<ICar>>();
            }
        }

        public void HandleOrder(IDealer dealer)
        {
            if (_cars.Count > 0 && _cars.TryDequeue(out ICar? car))
            {
                Console.WriteLine($"CarWarehouse Can Give Car: cars {_cars.Count} left");
                dealer.TakeCar(car);
                Console.WriteLine($"CarWarehouse Gave Car: cars {_cars.Count} left");
                _event.Set();
                return;
            }

            Console.WriteLine($"CarWarehouse Empty");
            _dealers.Enqueue(dealer);
            Console.WriteLine($"CarWarehouse Queue Add 1: {_dealers.Count} dealers are waiting Car");
        }

        public void AddCar(ICar car, int creatorId)
        {
            lock (_locker)
            {
                _notyfyCount--;
                Console.WriteLine($"CarWarehouse Got Car From Creator-{creatorId} (notyfyCount = {_notyfyCount})");
                if (_dealers.Count != 0)
                {
                    var dealer = _dealers.Dequeue();
                    dealer.TakeCar(car);
                    Console.WriteLine($"CarWarehouse Queue Minus 1: {_dealers.Count} dealers are waiting Car");
                }
                else
                {
                    _cars.Enqueue(car);
                    Console.WriteLine($"CarWarehouse Get Car: cars {_cars.Count} left");
                }


                if (_cars.Count() == _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"CarWarehouse Is Full: cars {_cars.Count} left");
                }
                _event.WaitOne();
                _notyfyCount++;
                Console.WriteLine($"CarWarehouse Notify Bilder-{creatorId} To Bild (notyfyCount = {_notyfyCount}): cars {_cars.Count} left");
                _carMediators[creatorId].Notify(CreatingStatus.CanCreate);
            }
        }

        public void SetMediator(IMediator<ICar> carMediator)
        {
            _carMediators.Add(carMediator);
        }

        public void Init()
        {
            foreach (var carMediator in _carMediators)
            {
                if (_notyfyCount + _cars.Count() < _capacity)
                {
                    _notyfyCount++;
                    Console.WriteLine($"CarWarehouse Notify Bilder-{_carMediators.IndexOf(carMediator)} To Bild (notyfyCount = {_notyfyCount}): cars {_cars.Count} left");
                    Task.Run(() => carMediator.Notify(CreatingStatus.CanCreate));
                }
            }
        }

        public void Run()
        {
            _worker.Start();
        }
    }
}
