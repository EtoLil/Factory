using Factory.Core.Warehouse;

namespace Factory.Core.Buiders
{
    public class CarBuilder : ICarBuilder
    {
        private Car _car;
        private DetailsWarehouse<Body> _bodyWarehouse;
        private DetailsWarehouse<Engine> _engineWarehouse;
        private DetailsWarehouse<Accessories> _accessoriesWarehouse;

        public CarBuilder()
        {
            _car = new Car();
            _bodyWarehouse = new DetailsWarehouse<Body>(1);
            _engineWarehouse = new DetailsWarehouse<Engine>(1);
            _accessoriesWarehouse = new DetailsWarehouse<Accessories>(1);
        }

        public void Reset()
        {
            _car = new Car();
        }

        public void BuildEngine()
        {
            _car.AddEngine(_engineWarehouse.GetDetail());
        }

        public void BuildBody()
        {
            _car.AddBody(_bodyWarehouse.GetDetail());
        }

        public void BuildAccessories()
        {
            _car.AddAccessories(_accessoriesWarehouse.GetDetail());
        }

        public Car BuildCar()
        {
            //TODO: ThreadPool

            BuildEngine();
            BuildBody();
            BuildAccessories();

            return _car;
        }
    }
}
