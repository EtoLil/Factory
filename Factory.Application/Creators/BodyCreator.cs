using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class BodyCreator : BaseDetailsCreator<Body>
    {
        public override void Create()
        {
            var body = new Body();

            Thread.Sleep(Configure.BodyCreateTime);

            _detailsMediator.Notify(CreatingStatus.Created, body);
        }
    }
}
