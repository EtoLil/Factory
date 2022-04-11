using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Warehouse;

namespace Factory.Core.Mediators
{
    public class CarMediator : IMediator<Car>
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
        public void Notify(CreatingStatus @event, Car? car = null)
        {
            switch (@event)
            {
                case CreatingStatus.Created:
                    Console.WriteLine($"Car Created");
                    _carWarehouse.AddCar(car);
                    break;
                case CreatingStatus.CanCreate:
                    Console.WriteLine($"Car warehouse not full");
                    _carBuilder.HandleOrder();
                    break;
                case CreatingStatus.CanNotCreate:
                    break;
                default:
                    break;
            }
        }
    }
}
