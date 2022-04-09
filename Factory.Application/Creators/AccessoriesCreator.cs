namespace Factory.Core.Creators
{
    public class AccessoriesCreator : IDetailsCreator
    {
        public IDetails Create()
        {
            return new Accessories(Guid.NewGuid());
        }

        public void Stop()
        {

        }
    }
}
