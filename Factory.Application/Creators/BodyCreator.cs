using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class BodyCreator : BaseCreator<Body>, IDetailsCreator<Body>
    {
        public void Create()
        {
            var body = new Body(Guid.NewGuid());

            Thread.Sleep(Configure.BodyCreateTime);

            _detailsMediator.Notify(body, CreatingStatus.Created);
        }
    }
}
