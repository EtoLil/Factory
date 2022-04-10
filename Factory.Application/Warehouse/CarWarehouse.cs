using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public class CarWarehouse
    {
        private readonly uint _capacity;

        private IList<Car> _cars;
        private CarMediator _carMediator;

        public CarWarehouse(uint capcity, CarMediator carMediator = null)
        {
            _capacity = capcity;
            _carMediator = carMediator;
            _cars = new List<Car>();
        }
        public void AddCar(Car car)
        {
            /*            if (CarBuilders.Count != 0)
                        {
                            carBuilder.BuildEngine(detail);
                        }
                        else
                        {
                        }*/

            _cars.Add(car);

            if (_cars.Count() < _capacity)
            {
                _carMediator.Notify(null, CreatingStatus.CanCreate);
            }
            else
            {
                foreach (var carR in _cars)
                {
                    Console.WriteLine(carR.ToString());
                }
                Console.WriteLine($"CarWarehouse Full");
            }
        }

        public void Start()
        {
            if (_cars.Count() < _capacity)
            {
                _carMediator.Notify(null, CreatingStatus.CanCreate);
            }
        }

        public void SetMediator(CarMediator carMediator)
        {
            _carMediator = carMediator;
        }
    }
}
