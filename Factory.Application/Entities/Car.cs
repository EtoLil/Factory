using Factory.Core.Entities.Base;
using Factory.Core.Interfaces;

namespace Factory.Core.Entities
{
    public class Car : BaseEntity, ICar
    {
        public Engine Engine { get; private set; }
        public Body Body { get; private set; }
        public Accessories Accessories { get; private set; }

        public Car() { }

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

        public void SetEngine(Engine engine)
        {
            Engine=engine;
        }

        public void SetAccessories(Accessories accessories)
        {
            Accessories=accessories;
        }

        public void SetBody(Body body)
        {
           Body=body;
        }

        public object Clone()
        {
            return new Car(Engine, Body, Accessories);
        }
    }
}
