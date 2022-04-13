using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Warehouse;

namespace Factory.Core.Mediators
{
    public class CarMediator : IMediator<ICar>
    {
        private ICarWarehouse _carWarehouse;

        private ICarDirector _carDirector;

        public CarMediator(ICarWarehouse carWarehouse, ICarDirector carDirector)
        {
            _carWarehouse = carWarehouse;
            _carWarehouse.SetMediator(this);
            _carDirector = carDirector;
            _carDirector.SetMediator(this);
        }

        //TODO: Refactore
        public void Notify(CreatingStatus @event, ICar? car = null)
        {
            switch (@event)
            {
                case CreatingStatus.Created:
                    Console.WriteLine($"Car Created");
                    _carWarehouse.AddCar(car);
                    break;
                case CreatingStatus.CanCreate:
                    Console.WriteLine($"Car warehouse not full");
                    _carDirector.HandleOrder();
                    break;
                case CreatingStatus.CanNotCreate:
                    break;
                default:
                    break;
            }
        }
    }
}
