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

        protected IMediator<T> _detailsMediator;
        protected Queue<T> _details;
        protected Queue<ICarDirector> _carDirectors;

        protected ManualResetEvent _event;
        protected Task _worker;
        public DetailsWarehouse(uint capcity, IMediator<T> detailsMediator = null)
        {
            _capacity = capcity;
            _detailsMediator = detailsMediator;
            _details = new Queue<T>();
            _carDirectors = new Queue<ICarDirector>();
            _event = new ManualResetEvent(true);
            _worker = new Task(Init);
        }

        public abstract void HandleOrder(ICarDirector carDirector);

        public abstract void AddDetail(T detail);

        public void SetMediator(IMediator<T> detailsMediator)
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
