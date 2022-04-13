using Factory.Core.Buiders;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse.Base
{
    public abstract class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : class, IDetails
    {
        protected readonly uint _capacity;

        protected DetailsMediator<T> _detailsMediator;
        protected Queue<T> _details;
        protected Queue<CarBuilder> _carBuilders;

        protected ManualResetEvent _event;
        protected Task _worker;
        public DetailsWarehouse(uint capcity, DetailsMediator<T> detailsMediator = null)
        {
            _capacity = capcity;
            _detailsMediator = detailsMediator;
            _details = new Queue<T>();
            _carBuilders = new Queue<CarBuilder>();
            _event = new ManualResetEvent(true);
            _worker = new Task(Init);
        }

        public abstract void HandleOrder(CarBuilder carBuilder);

        public abstract void AddDetail(T detail);

        public void SetMediator(DetailsMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
        public void Init()
        {
            if (_details.Count() < _capacity)
            {
                _detailsMediator.Notify(CreatingStatus.CanCreate);
            }
        }

        public void Run()
        {
            _worker.Start();
        }
    }
}
