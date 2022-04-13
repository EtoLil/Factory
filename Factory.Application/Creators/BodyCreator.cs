using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class BodyCreator : BaseDetailsCreator<Body>
    {
        public override void Send()
        {
            var body = Create();
            _detailsMediator.Notify(CreatingStatus.Created, body);
        }
        public override Body Create()
        {
            Thread.Sleep(Configure.BodyCreateTime);
            return new Body();
        }
    }
}
