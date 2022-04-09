namespace Factory.Core.Warehouse
{
    public class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : IDetails
    {
        private readonly uint _capacity;
        public IList<T> Details { get; set; }

        public DetailsWarehouse(uint capcity)
        {
            _capacity = capcity;
            Details = new List<T>();
        }

        public T GetDetail()
        {
            return Details.First();
        }

        public void AddDetail(T detail)
        {
            Details.Add(detail);

            if (Details.Count() == _capacity)
            {

            }
        }
    }

    public class EngineWarehouse : DetailsWarehouse<Engine>
    {
        public EngineWarehouse(uint capcity) : base(capcity)
        {
        }
    }

    public class BodyWarehouse : DetailsWarehouse<Body>
    {
        public BodyWarehouse(uint capcity) : base(capcity)
        {
        }
    }

    public class AccessoriesWarehouse : DetailsWarehouse<Accessories>
    {
        public AccessoriesWarehouse(uint capcity) : base(capcity)
        {
        }
    }
}
