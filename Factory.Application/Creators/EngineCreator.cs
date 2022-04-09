using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class EngineCreator : IDetailsCreator
    {
        private DetailsMediator<Engine> _detailsMediator;

        public EngineCreator(DetailsMediator<Engine> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }

        public void Create()
        {
            var engine = new Engine(Guid.NewGuid());

            Task.Delay(1);

            _detailsMediator.Notify(engine, EventType.DetailCreated);
        }
    }
}
