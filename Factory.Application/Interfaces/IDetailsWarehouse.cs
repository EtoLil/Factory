using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsWarehouse<T>
        where T : class, IDetails
    {
        void AddDetail(T detail, int creatorId);
        void HandleOrder(ICarDirector carBuilder);
        void SetMediator(IMediator<T> detailsMediator);
        void Init();
        void Run();
        int GetDetailsNumber();
        public List<T> GetDetailsList();
        public List<int> GetOrders();
    }
}
