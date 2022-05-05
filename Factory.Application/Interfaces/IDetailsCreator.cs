using Factory.Core.Enums;
using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsCreator<T>
        where T : class, IDetail
    {
        WorkState State { get; }
        void Send();
        T Create();
        void SetMediator(IMediator<T> detailsMediator);
        int GetCreatedNumber();
    }
}
