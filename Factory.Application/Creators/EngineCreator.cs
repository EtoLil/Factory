using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class EngineCreator : BaseCreator<Engine>, IDetailsCreator<Engine>
    {
        public void Create()
        {
            var engine = new Engine(Guid.NewGuid());

            Thread.Sleep(2000);

            _detailsMediator.Notify(engine, EventType.DetailCreated);
        }
    }
}
