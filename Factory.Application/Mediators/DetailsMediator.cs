using Factory.Core.Creators;
using Factory.Core.Warehouse;

namespace Factory.Core.Mediators
{
    public class DetailsMediator<T> : IMediator<T, EventType>
        where T : IDetails
    {
        private IDetailsWarehouse<T> _detailsWarehouse;

        private IDetailsCreator _detailsCreator;

        public DetailsMediator(IDetailsWarehouse<T> detailsWarehouse, IDetailsCreator detailsCreator)
        {
            _detailsWarehouse = detailsWarehouse;
            _detailsCreator = detailsCreator;
        }

        public void Notify(T input, EventType @event)
        {
            if (@event == EventType.DetailCreated)
            {
                _detailsWarehouse.AddDetail(input);
            }

            if (@event == EventType.WarehouseNotFull)
            {
                _detailsCreator.Create();
            }
        }
    }

    public enum EventType
    {
        DetailCreated,
        WarehouseNotFull
    }
}
