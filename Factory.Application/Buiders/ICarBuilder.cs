namespace Factory.Core.Buiders
{
    public interface ICarBuilder
    {
        void BuildEngine();
        void BuildBody();
        void BuildAccessories();
    }
}
