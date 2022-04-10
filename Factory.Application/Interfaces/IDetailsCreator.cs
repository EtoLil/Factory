using Factory.Core.Mediators;

namespace Factory.Core.Interfaces
{
    public interface IDetailsCreator<T>
        where T : class, IDetails
    {
        void Create();
        void SetMediator(DetailsMediator<T> detailsMediator);
    }
}
