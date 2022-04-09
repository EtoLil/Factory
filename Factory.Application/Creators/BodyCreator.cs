namespace Factory.Core.Creators
{
    public class BodyCreator : IDetailsCreator
    {
        public IDetails Create()
        {
            return new Body(Guid.NewGuid());
        }

        public void Stop()
        {

        }
    }
}
