using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public class BaseCreator<T>
        where T : IDetails
    {
        protected DetailsMediator<T> _detailsMediator;

        public BaseCreator(DetailsMediator<T> detailsMediator = null)
        {
            _detailsMediator = detailsMediator;
        }

        public void SetMediator(DetailsMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
    }
}
