using Factory.Core.Mediators;

namespace Factory.Core.Creators
{
    public interface IDetailsCreator<T>
        where T : IDetails
    {
        void Create();
        void SetMediator(DetailsMediator<T> detailsMediator);
    }
}
