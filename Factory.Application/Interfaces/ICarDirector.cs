using Factory.Core.Entities;

namespace Factory.Core.Interfaces
{
    public interface ICarDirector
    {
        void TakeEngine(Engine engine);
        void TakeBody(Body body);
        void TakeAccessories(Accessories accessories);
        void HandleOrder();
        void Send();
        void SetMediator(IMediator<ICar> carMediator);
    }
}
