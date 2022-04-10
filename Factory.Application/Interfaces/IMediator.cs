using Factory.Core.Enums;

namespace Factory.Core.Interfaces
{
    public interface IMediator<T>
        where T : class
    {
        void Notify(CreatingStatus @event, T? input = default);
    }
}
