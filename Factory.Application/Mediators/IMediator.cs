namespace Factory.Core.Mediators
{
    public interface IMediator<T>
    {
        void Notify(T input, CreatingStatus @event);
    }
}
