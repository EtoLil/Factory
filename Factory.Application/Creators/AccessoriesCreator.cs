using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseCreator<Accessories>, IDetailsCreator<Accessories>
    {
        public void Create()
        {
            var accessories = new Accessories(Guid.NewGuid());

            Thread.Sleep(3000);

            _detailsMediator.Notify(accessories, EventType.DetailCreated);
        }
    }
}
