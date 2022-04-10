namespace Factory.Core.Interfaces
{
    public interface IDealer : IEntity
    {
        IList<ICar> Cars { get; set; }
        void TakeCar(ICar car);
        void Start();
    }
}
