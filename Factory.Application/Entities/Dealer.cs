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
            Console.WriteLine($"Dealer: {Id} Received Car {car.Id}");
            Cars.Add(car);
            if(Cars.Count == 3)
            {
                foreach(ICar car2 in Cars)
                {
                    Console.WriteLine($"Dealer-{Id} Have: {car2}");
                }
            }
        }

        public void Run(int dealerRequestTime)
        {
            //Timer timer = new Timer(new TimerCallback(Start), null, 0, dealerRequestTime);

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(dealerRequestTime);
                Start(null);
            }
        }

        private void Start(object? obj)
        {
            _carWarehouse.HandleOrder(this);
        }
    }
}
