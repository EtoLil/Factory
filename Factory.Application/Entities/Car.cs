using Factory.Core.Entities.Base;
using Factory.Core.Interfaces;

namespace Factory.Core.Entities
{
    public class Car : BaseEntity, ICar
    {
        public Engine Engine { get; private set; }
        public Body Body { get; private set; }
        public Accessories Accessories { get; private set; }

        public Car(Engine engine, Body body, Accessories accessories) : base()
        {
            Engine = engine;
            Body = body;
            Accessories = accessories;
        }

        public override string ToString()
        {
            return $"Car - Engine: {Engine.Id }; Body: {Body.Id }; Accessories: {Accessories.Id };";
        }
    }
}
