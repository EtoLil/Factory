namespace Factory.Core
{
    public class Car
    {
        public Engine Engine { get; private set; }
        public Body Body { get; private set; }
        public Accessories Accessories { get; private set; }

        public void AddEngine(Engine engine)
        {
            Engine = engine;
        }

        public void AddBody(Body body)
        {
            Body = body;
        }

        public void AddAccessories(Accessories accessories)
        {
            Accessories = accessories;
        }
    }
}
