using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;

namespace Factory.Core.Creators.Base
{
    public abstract class BaseDetailsCreator<T> : IDetailsCreator<T>
        where T : class, IDetail
    {
        public WorkState State { get; protected set; } = WorkState.Waiting;
        protected IMediator<T> _detailsMediator;
        protected int _id;
        protected int createdNumber = 0;
        protected CancellationToken _token;
        public BaseDetailsCreator(IMediator<T> detailsMediator = null, CancellationToken token = default)
        {
            _detailsMediator = detailsMediator;
            _token = token;
        }

        public abstract void Send();
        public abstract T Create();

        public void SetMediator(IMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }

        public int GetCreatedNumber()
        {
            return createdNumber;
        }
    }
}
