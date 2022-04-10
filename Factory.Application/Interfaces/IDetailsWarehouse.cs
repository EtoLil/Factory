using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsWarehouse<T>
        where T : class, IDetails
    {
        void AddDetail(T detail);
        void MakeOrder(CarBuilder carBuilder);
        void SetMediator(DetailsMediator<T> detailsMediator);
    }
}
