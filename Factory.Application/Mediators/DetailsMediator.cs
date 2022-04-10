using Factory.Core.Creators;
using Factory.Core.Warehouse;

namespace Factory.Core.Mediators
{
    public class DetailsMediator<T> : IMediator<T>
        where T : IDetails
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
        public void Notify(T input, CreatingStatus @event)
        {
            if (@event == CreatingStatus.Created)
            {
                Console.WriteLine($"{typeof(T).Name} Created");
                _detailsWarehouse.AddDetail(input);
            }

            if (@event == CreatingStatus.CanCreate)
            {
                Console.WriteLine($"{typeof(T).Name} warehouse not full");

                _detailsCreator.Create();
            }
        }
    }
}
