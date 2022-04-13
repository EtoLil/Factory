using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Mediators
{
    public class DetailsMediator<T> : IMediator<T>
        where T : class, IDetails
    {
        private IDetailsWarehouse<T> _detailsWarehouse;

        private IDetailsCreator<T> _detailsCreator;

        public DetailsMediator(IDetailsWarehouse<T> detailsWarehouse, IDetailsCreator<T> detailsCreator)
        {
            _detailsWarehouse = detailsWarehouse;
            _detailsWarehouse.SetMediator(this);
            _detailsCreator = detailsCreator;
            _detailsCreator.SetMediator(this);
        }

        //TODO: Refactore
        public void Notify(CreatingStatus @event, T? input = null)
        {
            switch (@event)
            {
                case CreatingStatus.Created:
                    Console.WriteLine($"{typeof(T).Name} Created");
                    _detailsWarehouse.AddDetail(input);
                    break;
                case CreatingStatus.CanCreate:
                    Console.WriteLine($"{typeof(T).Name} warehouse not full");
                    _detailsCreator.Send();
                    break;
                case CreatingStatus.CanNotCreate:
                    break;
                default:
                    break;
            }
        }
    }
}
