using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseDetailsCreator<Accessories>
    {
        public override void Create()
        {
            var accessories = new Accessories();

            Thread.Sleep(Configure.AccessoriesCreateTime);

            _detailsMediator.Notify(CreatingStatus.Created, accessories);
        }
    }
}
