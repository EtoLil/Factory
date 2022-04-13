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
            Console.WriteLine($"Dealer: {Id} received car {car.Id}");
            Cars.Add(car);
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
