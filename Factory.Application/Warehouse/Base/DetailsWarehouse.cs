using Factory.Core.Buiders;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse.Base
{
    public abstract class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : class, IDetails
    {
        protected readonly uint _capacity;

        protected DetailsMediator<T> _detailsMediator;
        protected Queue<T> Details { get; set; }
        protected Queue<CarBuilder> CarBuilders { get; set; }

        public DetailsWarehouse(uint capcity, DetailsMediator<T> detailsMediator = null)
        {
            _capacity = capcity;
            _detailsMediator = detailsMediator;
            Details = new Queue<T>();
            CarBuilders = new Queue<CarBuilder>();
        }

        public abstract void MakeOrder(CarBuilder carBuilder);

        public abstract void AddDetail(T detail);

        public void SetMediator(DetailsMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
    }
}
