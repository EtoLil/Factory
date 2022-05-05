using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Mediators
{
    public class DetailsMediator<T> : IMediator<T>
        where T : class, IDetail
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
                    try
                    {
                        _detailsWarehouse.AddDetail(input, creatorId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CreatingStatus.CanCreate:
                    try
                    {
                        _detailsCreator.Send();
                    }
                    catch (OperationCanceledException ex)
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
