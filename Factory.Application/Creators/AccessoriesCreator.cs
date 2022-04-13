using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Creators
{
    public class AccessoriesCreator : BaseDetailsCreator<Accessories>
    {
        public AccessoriesCreator(int id, IMediator<Accessories> detailsMediator = null):base(detailsMediator)
        {
            _id = id;
        }

        public override void Send()
        {
            var accessories = Create();           
            _detailsMediator.Notify(CreatingStatus.Created, accessories);
        }
        public override Accessories Create()
        {
            Thread.Sleep(Configure.AccessoriesCreateTime[_id]);
            return new Accessories();
        }
    }
}
