using Factory.Core.Entities;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface ICarWarehouse
    {
        void HandleOrder(IDealer dealer);
        void AddCar(ICar car, int createdId);
        void SetMediator(IMediator<ICar> carMediator);
        void Init();
        void Run();
    }
}
