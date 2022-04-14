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
        public void Notify(CreatingStatus @event, T? input = null, int creatorId = default)
        {
            switch (@event)
            {
                case CreatingStatus.Created:
                    _detailsWarehouse.AddDetail(input, creatorId);
                    break;
                case CreatingStatus.CanCreate:
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
