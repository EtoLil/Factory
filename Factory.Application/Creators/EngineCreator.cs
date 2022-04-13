using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseDetailsCreator<Engine>
    {
        public override void Send()
        {
            var engine = Create();
            _detailsMediator.Notify(CreatingStatus.Created, engine);
        }
        public override Engine Create()
        {
            Thread.Sleep(Configure.EngineCreateTime);
            return new Engine();
        }
    }
}
