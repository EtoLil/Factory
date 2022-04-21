using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Interfaces
{
    public interface ICarDirector
    {
        int Id { get; }
        WorkState State { get; }

        void TakeEngine(Engine engine);
        void TakeBody(Body body);
        void TakeAccessories(Accessories accessories);
        void HandleOrder();
        void Send();
        void SetMediator(IMediator<ICar> carMediator);
        int GetBuiltCarNumber();
    }
}
