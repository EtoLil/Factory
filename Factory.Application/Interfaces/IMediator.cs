using Factory.Core.Enums;

namespace Factory.Core.Interfaces
{
    public interface IMediator<T>
        where T : class, IEntity
    {
        void Notify(CreatingStatus @event, T? input = default,int creatorId = default);
    }
}
