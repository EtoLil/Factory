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
        public void Notify(CreatingStatus @event, ICar? car = null, int creatorId = default)
        {
            switch (@event)
            {
                case CreatingStatus.Created:
                    try
                    {
                        _carWarehouse.AddCar(car, creatorId);
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CreatingStatus.CanCreate:
                    try
                    {
                        _carDirector.HandleOrder();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CreatingStatus.CanNotCreate:
                    break;
                default:
                    break;
            }
        }
    }
}
