namespace Factory.Core
{
    public class Car
    {
        public Engine Engine { get; private set; }
        public Body Body { get; private set; }
        public Accessories Accessories { get; private set; }

        public Car(Engine engine, Body body, Accessories accessories)
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
