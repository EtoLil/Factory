using Factory.Core.Interfaces;
using Factory.Core.Mediators;

namespace Factory.Core.Creators.Base
{
    public abstract class BaseDetailsCreator<T> : IDetailsCreator<T>
        where T : class, IDetails
    {
        protected DetailsMediator<T> _detailsMediator;

        public BaseDetailsCreator(DetailsMediator<T> detailsMediator = null)
        {
            _detailsMediator = detailsMediator;
        }

        public abstract void Create();

        public void SetMediator(DetailsMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
    }
}
