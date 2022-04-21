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
        public Dealer(ICarWarehouse carWarehouse, int index) : base()
        {
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
            _worker.Start();
        }

        private void Start()
        {
            while (true)
            {
                Thread.Sleep(Configure.DealersRequestTime[_index]);
                _carWarehouse.HandleOrder(this);
            }
        }
    }
}
