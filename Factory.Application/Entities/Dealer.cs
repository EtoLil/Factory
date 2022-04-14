using Factory.Core.Entities.Base;
using Factory.Core.Interfaces;
using Factory.Core.Warehouse;

namespace Factory.Core.Entities
{
    public class Dealer : BaseEntity, IDealer
    {
        private ICarWarehouse _carWarehouse;

        public IList<ICar> Cars { get; set; }

        public Dealer(ICarWarehouse carWarehouse) : base()
        {
            _carWarehouse = carWarehouse;
            Cars = new List<ICar>();
        }

        public void TakeCar(ICar car)
        {
            Cars.Add(car);
            Console.WriteLine($"Dealer: {Id} Received Car {car.Id}");
            var str = "";
            foreach (ICar car2 in Cars)
            {
                str += $"Dealer-{Id} Have: {car2}\n";
            }
            Console.WriteLine(str);

        }

        public void Run(int dealerRequestTime)
        {
            Timer timer = new Timer(new TimerCallback(Start), null, 0, dealerRequestTime);
        }

        private void Start(object? obj)
        {
            _carWarehouse.HandleOrder(this);
        }
    }
}
