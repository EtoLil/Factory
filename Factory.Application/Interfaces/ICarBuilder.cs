using Factory.Core.Entities;

namespace Factory.Core.Buiders
{
    public interface ICarBuilder
    {
        void TakeEngine(Engine engine);
        void TakeBody(Body body);
        void TakeAccessories(Accessories accessories);
        void HandleOrder();
        Car TryToCreate(out bool isCreated);
        bool TryToSend();
    }
}
