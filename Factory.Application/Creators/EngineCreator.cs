using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseCreator<Engine>, IDetailsCreator<Engine>
    {
        public void Create()
        {
            var engine = new Engine(Guid.NewGuid());

            Thread.Sleep(Configure.EngineCreateTime);

            _detailsMediator.Notify(engine, CreatingStatus.Created);
        }
    }
}
