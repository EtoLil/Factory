using Factory.Core.Entities;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface ICarWarehouse
    {
        void HandleOrder(Dealer dealer);
        void AddCar(Car car);
        void SetMediator(CarMediator carMediator);
        void Init();
        void Run();
    }
}
