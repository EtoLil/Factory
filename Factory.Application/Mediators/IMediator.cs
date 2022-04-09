namespace Factory.Core.Mediators
{
    public interface IMediator<T, TEventType>
    {
        void Notify(T input, TEventType @event);
    }
}
