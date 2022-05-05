using Factory.Core.Entities.Base;
using Factory.Core.Interfaces;
using Factory.Core.Warehouse;

namespace Factory.Core.Entities
{
    public class Dealer : BaseEntity, IDealer
    {
        private ICarWarehouse _carWarehouse;

        public IList<Car> Cars { get; set; }
        private Task _worker;
        private int _index;
        private CancellationToken _token;
        public Dealer(ICarWarehouse carWarehouse, int index, CancellationToken token = default) : base()
        {
            _token = token;
            _carWarehouse = carWarehouse;
            Cars = new List<Car>();
            _index=index;
            _worker = new Task(Start);
        }

        public void TakeCar(ICar car)
        {
            Cars.Add((Car)car);
            Console.WriteLine($"Dealer: {Id} Received Car {car.Id}");
            var str = "";
            foreach (ICar car2 in Cars)
            {
                str += $"Dealer-{Id} Have: {car2}\n";
            }
            Console.WriteLine(str);

        }

        public void Run()
        {
            try
            {
                _worker.Start();
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Start()
        {
            while (true)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"Dealer {Id} token.IsCancellationRequested");
                    _token.ThrowIfCancellationRequested();
                }
                else
                {
                    Thread.Sleep(Configure.DealersRequestTime[_index]);
                }
                _carWarehouse.HandleOrder(this);
            }
        }
    }
}
