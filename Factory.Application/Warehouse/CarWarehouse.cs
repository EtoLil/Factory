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

        protected static object _lockerWaitSapace = new object();
        protected static object _lockerGetNewCar = new object();
        protected static object _lockerGetNewOrder = new object();

        protected CancellationToken _token;

        public CarWarehouse(uint capcity, IList<IMediator<ICar>> carMediators = null, CancellationToken token = default)
        {
            _token = token;
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
            lock (_lockerGetNewOrder)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"CarWarehouse token.IsCancellationRequested from dealer {dealer.Id}");
                    _token.ThrowIfCancellationRequested();
                }
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
        }

        public void AddCar(ICar car, int creatorId)
        {
            lock (_lockerGetNewCar)
            {
                _notyfyCount--;
                Console.WriteLine($"CarWarehouse Got Car From Bilder-{creatorId} (notyfyCount = {_notyfyCount})");
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
            }
            lock (_lockerWaitSapace)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"CarWarehouse token.IsCancellationRequested from car factory {creatorId}");
                    _token.ThrowIfCancellationRequested();
                }
                if (_cars.Count() + _notyfyCount == _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"CarWarehouse Is Full: cars {_cars.Count} left");
                    Console.WriteLine($"CarWarehouse Bulder-{creatorId} Waiting For Start");
                }
                _event.WaitOne();
                _notyfyCount++;
            }

            Console.WriteLine($"CarWarehouse Notify Bilder-{creatorId} To Bild (notyfyCount = {_notyfyCount}): cars {_cars.Count} left");
            _carMediators[creatorId].Notify(CreatingStatus.CanCreate);
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

        public int GetCarsNumber()
        {
            return _cars.Count();
        }

        public List<Car> GetCarsList()
        {
            return _cars.Select(car => (Car)car).ToList();
        }

        public List<string> GetOrders()
        {
            return _dealers.Select(d => d.Id.ToString()).ToList();
        }

        public void ContinueManualResetEvent()
        {
            _event.Set();
        }
    }
}
