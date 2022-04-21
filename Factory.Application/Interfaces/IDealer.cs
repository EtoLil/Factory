using Factory.Core.Entities;

namespace Factory.Core.Interfaces
{
    public interface IDealer : IEntity
    {
        IList<Car> Cars { get; set; }
        void TakeCar(ICar car);
        void Run();
    }
}
