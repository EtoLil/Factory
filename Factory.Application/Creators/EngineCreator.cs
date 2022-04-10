using Factory.Core.Creators.Base;
using Factory.Core.Entities;
using Factory.Core.Enums;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseDetailsCreator<Engine>
    {
        public override void Create()
        {
            var engine = new Engine();

            Thread.Sleep(Configure.EngineCreateTime);

            _detailsMediator.Notify(CreatingStatus.Created, engine);
        }
    }
}
