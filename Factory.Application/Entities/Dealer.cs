using Factory.Core.Entities.Base;
using Factory.Core.Interfaces;
using Factory.Core.Warehouse;

namespace Factory.Core.Entities
{
    public class Dealer : BaseEntity, IDealer
    {
        private CarWarehouse _carWarehouse;

        public IList<ICar> Cars { get; set; }

        public Dealer(CarWarehouse carWarehouse) : base()
        {
            _carWarehouse = carWarehouse;
            Cars = new List<ICar>();
        }

        public void TakeCar(ICar car)
        {
            Console.WriteLine($"Dealer: {Id} received car {car.Id}");
            Cars.Add(car);
        }

        public void Start()
        {
            _carWarehouse.HandleOrder(this);
        }
    }
}
