using Factory.Core.Entities;

namespace Factory.Core.Buiders
{
    public interface ICarBuilder
    {
        void PassEngine(Engine engine);
        void PassBody(Body body);
        void PassAccessories(Accessories accessories);
        void MakeOrder();
    }
}
