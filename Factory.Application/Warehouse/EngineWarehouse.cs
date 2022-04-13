using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

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

        public override void HandleOrder(CarBuilder carBuilder)
        {
            if (_details.Count > 0)
            {
                Console.WriteLine($"Warehouse has an engine {_details.Count}");


                carBuilder.TakeEngine(_details.Dequeue());
                _event.Set();
                return;
            }

            Console.WriteLine($"Warehouse has no engine {_details.Count}");
            _carBuilders.Enqueue(carBuilder);
        }

        public override void AddDetail(Engine detail)
        {
            if (_carBuilders.Count != 0)
            {
                var carBuilder = _carBuilders.Dequeue();

                carBuilder.TakeEngine(detail);
            }
            else
            {
                _details.Enqueue(detail);
            }

            if (_details.Count() == _capacity)
            {
                _event.Reset();
                Console.WriteLine($"EngineWarehouse Full {_details.Count}");
            }
            _event.WaitOne();
            _detailsMediator.Notify(CreatingStatus.CanCreate);
        }

        public override void Init()
        {
            if (_details.Count() < _capacity)
            {
                _detailsMediator.Notify(CreatingStatus.CanCreate);
            }
        }
        public override void Run()
        {
            _worker.Start();
        }

    }
}
