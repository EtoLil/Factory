using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

namespace Factory.Core.Warehouse
{
    public class AccessoriesWarehouse : DetailsWarehouse<Accessories>
    {
        public AccessoriesWarehouse(uint capcity, DetailsMediator<Accessories> detailsMediator = null)
            : base(capcity, detailsMediator)
        {
        }

        public override void HandleOrder(CarBuilder carBuilder)
        {
            if (_details.Count > 0)
            {
                Console.WriteLine($"Warehouse has accessories {_details.Count}");

                carBuilder.TakeAccessories(_details.Dequeue());
                _event.Set();
                return;
            }

            _carBuilders.Enqueue(carBuilder);
            Console.WriteLine($"Warehouse has no accessories {_details.Count}");
        }

        public override void AddDetail(Accessories detail)
        {
            if (_carBuilders.Count != 0)
            {
                var carBuilder = _carBuilders.Dequeue();

                carBuilder.TakeAccessories(detail);
            }
            else
            {
                _details.Enqueue(detail);
            }

            if (_details.Count() == _capacity)
            {
                _event.Reset();
                Console.WriteLine($"AccessoriesWarehouse Full {_details.Count}");
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
