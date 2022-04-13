using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseDetailsCreator<Accessories>
    {
        public override void Send()
        {
            var accessories = Create();           
            _detailsMediator.Notify(CreatingStatus.Created, accessories);
        }
        public override Accessories Create()
        {
            Thread.Sleep(Configure.AccessoriesCreateTime);
            return new Accessories();
        }
    }
}
