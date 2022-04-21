using Factory.Core.Entities;

namespace Factory.Core.Interfaces
{
    public interface ICar : IEntity, ICloneable
    {
        Engine Engine { get; }
        Body Body { get; }
        Accessories Accessories { get; }
        void SetEngine(Engine engine);
        void SetAccessories(Accessories accessories);
        void SetBody(Body body);
    }
}
