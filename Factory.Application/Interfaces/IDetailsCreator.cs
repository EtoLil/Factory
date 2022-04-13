using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsCreator<T>
        where T : class, IDetails
    {
        void Send();
        T Create();
        void SetMediator(DetailsMediator<T> detailsMediator);
    }
}
