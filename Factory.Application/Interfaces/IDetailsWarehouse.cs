using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsWarehouse<T>
        where T : class, IDetail
    {
        void AddDetail(T detail, int creatorId);
        void HandleOrder(ICarDirector carBuilder);
        void SetMediator(IMediator<T> detailsMediator);
        void Init();
        void Run();
        int GetDetailsNumber();
        List<T> GetDetailsList();
        List<int> GetOrders();
        void ContinueManualResetEvent();

    }
}
