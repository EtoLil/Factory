using Factory.Core.Buiders;
using Factory.Core.Mediators;

namespace Factory.Core.Warehouse
{
    public class EngineWarehouse : DetailsWarehouse<Engine>
    {
        public EngineWarehouse(
            uint capcity,
            DetailsMediator<Engine> detailsMediator = null
            )
            : base(capcity, detailsMediator)
        {
        }

        public override void MakeOrder(CarBuilder carBuilder)
        {
            if (Details.Count > 0)
            {
                Console.WriteLine($"Warehouse has an engine");

                carBuilder.PassEngine(Details.Dequeue());
                return;
            }

            Console.WriteLine($"Warehouse has no engine");
            CarBuilders.Enqueue(carBuilder);
        }

        public override void AddDetail(Engine detail)
        {
            if (CarBuilders.Count != 0)
            {
                var carBuilder = CarBuilders.Dequeue();

                carBuilder.PassEngine(detail);
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
                Console.WriteLine($"EngineWarehouse Full");
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
