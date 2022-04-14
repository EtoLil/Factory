using Factory.Core.Buiders;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using System.Collections.Concurrent;

namespace Factory.Core.Warehouse.Base
{
    public abstract class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : class, IDetails
    {
        protected readonly uint _capacity;
        protected int _notyfyCount;

        protected IList<IMediator<T>> _detailsMediator;
        protected ConcurrentQueue<T> _details;
        protected Queue<ICarDirector> _carDirectors;

        protected ManualResetEvent _event;
        protected Task _worker;

        protected static object _locker = new object();

        public DetailsWarehouse(uint capcity, IList<IMediator<T>> detailsMediators = null)
        {
            _notyfyCount = 0;
            _capacity = capcity;
            _detailsMediator = detailsMediators;
            _details = new ConcurrentQueue<T>();
            _carDirectors = new Queue<ICarDirector>();
            _event = new ManualResetEvent(true);
            _worker = new Task(Init);

            if(_detailsMediator is null)
            {
                _detailsMediator = new List<IMediator<T>>();
            }
        }

        public abstract void HandleOrder(ICarDirector carDirector);

        public abstract void AddDetail(T detail, int creatorId);

        public void SetMediator(IMediator<T> detailsMediator)
        {
            _detailsMediator.Add(detailsMediator);
        }
        public void Init()
        {
            foreach(IMediator<T> mediator in _detailsMediator)
            {
                if (_notyfyCount + _details.Count() < _capacity)
                {
                    _notyfyCount++;
                    Console.WriteLine($"{typeof(T).Name}Warehouse Notify Creator-{_detailsMediator.IndexOf(mediator)} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
                    Task.Run(()=>mediator.Notify(CreatingStatus.CanCreate)); 
                }
            }
        }

        public void Run()
        {
            _worker.Start();
        }
    }
}
