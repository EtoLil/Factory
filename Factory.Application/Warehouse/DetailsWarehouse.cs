using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public abstract class DetailsWarehouse<T> : IDetailsWarehouse<T>
        where T : IDetails
    {
        protected readonly uint _capacity;

        protected DetailsMediator<T> _detailsMediator;
        protected Queue<T> Details { get; set; }
        protected Queue<CarBuilder> CarBuilders { get; set; }

        public DetailsWarehouse(uint capcity, DetailsMediator<T> detailsMediator = null)
        {
            _capacity = capcity;
            _detailsMediator = detailsMediator;
            Details = new Queue<T>();
            CarBuilders = new Queue<CarBuilder>();
        }

        public abstract void MakeOrder(CarBuilder carBuilder);

        public abstract void AddDetail(T detail);

        public void SetMediator(DetailsMediator<T> detailsMediator)
        {
            _detailsMediator = detailsMediator;
        }
    }

    public class AccessoriesWarehouse : DetailsWarehouse<Accessories>
    {
        public AccessoriesWarehouse(uint capcity, DetailsMediator<Accessories> detailsMediator = null)
            : base(capcity, detailsMediator)
        {
        }

        public override void MakeOrder(CarBuilder carBuilder)
        {
            if (Details.Count > 0)
            {
                Console.WriteLine($"Warehouse has accessories");

                carBuilder.PassAccessories(Details.Dequeue());
                return;
            }

            CarBuilders.Enqueue(carBuilder);
            Console.WriteLine($"Warehouse has no accessories");
        }

        public override void AddDetail(Accessories detail)
        {
            if (CarBuilders.Count != 0)
            {
                var carBuilder = CarBuilders.Dequeue();

                carBuilder.PassAccessories(detail);
            }
            else
            {
                Details.Enqueue(detail);
            }

            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(null, CreatingStatus.CanCreate);
            }
            else
            {
                Console.WriteLine($"AccessoriesWarehouse Full");
            }
        }

        public void Start()
        {
            if (Details.Count() < _capacity)
            {
                _detailsMediator.Notify(null, CreatingStatus.CanCreate);
            }
        }
    }
}
