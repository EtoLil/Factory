using Factory.Core.Entities;

namespace Factory.Core.Interfaces
{
    public interface ICar : IEntity, ICloneable
    {
        public void SetEngine(Engine engine);
        public void SetAccessories(Accessories accessories);
        public void SetBody(Body body);
    }
}
