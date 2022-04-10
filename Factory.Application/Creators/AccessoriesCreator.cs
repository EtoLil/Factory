using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseCreator<Accessories>, IDetailsCreator<Accessories>
    {
        public void Create()
        {
            var accessories = new Accessories(Guid.NewGuid());

            Thread.Sleep(Configure.AccessoriesCreateTime);

            _detailsMediator.Notify(accessories, CreatingStatus.Created);
        }
    }
}
