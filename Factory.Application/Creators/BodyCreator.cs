using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;

namespace Factory.Core.Creators
{
    public class BodyCreator : BaseDetailsCreator<Body>
    {
        public BodyCreator(int id, IMediator<Body> detailsMediator = null) : base(detailsMediator)
        {
            _id = id;
        }
        public override void Send()
        {
            var body = Create();
            _detailsMediator.Notify(CreatingStatus.Created, body);
        }
        public override Body Create()
        {
            Thread.Sleep(Configure.BodiesCreateTime[_id]);
            return new Body();
        }
    }
}
