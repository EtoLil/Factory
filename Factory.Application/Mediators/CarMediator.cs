using Factory.Core.Buiders;
using Factory.Core.Warehouse;

namespace Factory.Core.Mediators
{
    public class CarMediator : IMediator<Car, CarEventType>
    {
        private CarWarehouse _carWarehouse;

        private CarBuilder _carBuilder;

        public CarMediator(CarWarehouse carWarehouse, CarBuilder carBuilder)
        {
            _carWarehouse = carWarehouse;
            _carWarehouse.SetMediator(this);
            _carBuilder = carBuilder;
            _carBuilder.SetMediator(this);
        }

        //TODO: Refactore
        public void Notify(Car car, CarEventType @event)
        {
            if (@event == CarEventType.CarCreated)
            {
                Console.WriteLine($"Car Created");
                _carWarehouse.AddCar(car);
            }

            if (@event == CarEventType.WarehouseNotFull)
            {
                Console.WriteLine($"Car warehouse not full");

                _carBuilder.MakeOrder();
            }
        }
    }

    public enum CarEventType
    {
        CarCreated,
        WarehouseNotFull
    }
}
