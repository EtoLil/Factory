using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public interface IDetailsWarehouse<T>
        where T : IDetails
    {
        void AddDetail(T detail);
        void MakeOrder(CarBuilder carBuilder);
        void SetMediator(DetailsMediator<T> detailsMediator);
    }
}
