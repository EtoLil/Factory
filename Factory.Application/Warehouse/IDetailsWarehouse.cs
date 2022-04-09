namespace Factory.Core.Warehouse
{
    public interface IDetailsWarehouse<T>
        where T : IDetails
    {
        IList<T> Details { get; set; }
        void AddDetail(T detail);
    }
}
