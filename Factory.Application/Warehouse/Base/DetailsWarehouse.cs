using Factory.Core.Buiders;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using System.Collections.Concurrent;

namespace Factory.Core.Warehouse.Base
{
    public abstract class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : class, IDetail
    {
        protected readonly uint _capacity;
        protected int _notyfyCount;

        protected IList<IMediator<T>> _detailsMediator;
        protected ConcurrentQueue<T> _details;
        protected Queue<ICarDirector> _carDirectors;

        protected ManualResetEvent _event;
        protected Task _worker;
        protected IList<Thread> _threads = new List<Thread>();

        protected static object _lockerWaitSapace = new object();
        protected static object _lockerGetNewDetail = new object();
        protected static object _lockerGetNewOrder = new object();

        protected CancellationToken _token;

        public DetailsWarehouse(uint capcity, IList<IMediator<T>> detailsMediators = null, CancellationToken token = default)
        {
            _token = token;
            _notyfyCount = 0;
            _capacity = capcity;
            _detailsMediator = detailsMediators;
            _details = new ConcurrentQueue<T>();
            _carDirectors = new Queue<ICarDirector>();
            _event = new ManualResetEvent(true);
            _worker = new Task(Init);

            if (_detailsMediator is null)
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
            foreach (IMediator<T> mediator in _detailsMediator)
            {
                if (_notyfyCount + _details.Count() < _capacity)
                {
                    _notyfyCount++;
                    Console.WriteLine($"{typeof(T).Name}Warehouse Notify Creator-{_detailsMediator.IndexOf(mediator)} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
                    Task.Run(() => mediator.Notify(CreatingStatus.CanCreate));
                }
            }
        }

        public void Run()
        {
            _worker.Start();
        }

        public int GetDetailsNumber()
        {
            return _details.Count();
        }
        public List<T> GetDetailsList()
        {
            return _details.ToList();
        }
        public List<int> GetOrders()
        {
            return _carDirectors.Select(d => d.Id).ToList();
        }

        public void ContinueManualResetEvent()
        {
            _event.Set();
        }
    }
}
