using Factory.Core.Interfaces;
using Factory.Core.Mediators;

namespace Factory.Core.Creators.Base
{
    public abstract class BaseDetailsCreator<T> : IDetailsCreator<T>
        where T : class, IDetails
    {
        protected IMediator<T> _detailsMediator;
        protected int _id;
        public BaseDetailsCreator(IMediator<T> detailsMediator = null)
        {
            _detailsMediator = detailsMediator;
        }

        public abstract void Send();
        public abstract T Create();

        public void SetMediator(IMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
    }
}
