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
            Console.WriteLine($"BodyCreator {_id}: Create Body-{body.Id}");
            _detailsMediator.Notify(CreatingStatus.Created, body, _id);
        }
        public override Body Create()
        {
            State = WorkState.Working;
            Thread.Sleep(Configure.BodiesCreateTime[_id]);
            createdNumber++;
            State = WorkState.Waiting;
            return new Body();
        }
    }
}
