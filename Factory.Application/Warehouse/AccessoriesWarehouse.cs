using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

namespace Factory.Core.Warehouse
{
    public class AccessoriesWarehouse : DetailsWarehouse<Accessories>
    {
        public AccessoriesWarehouse(uint capcity, IMediator<Accessories> detailsMediator = null)
            : base(capcity, detailsMediator)
        {
        }

        public override void HandleOrder(ICarDirector carBuilder)
        {
            if (_details.Count > 0)
            {
                Console.WriteLine($"Warehouse has accessories {_details.Count}");

                carBuilder.TakeAccessories(_details.Dequeue());
                _event.Set();
                return;
            }

            _carDirectors.Enqueue(carBuilder);
            Console.WriteLine($"Warehouse has no accessories {_details.Count}");
        }

        public override void AddDetail(Accessories detail)
        {
            if (_carDirectors.Count != 0)
            {
                var carBuilder = _carDirectors.Dequeue();

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
    }
}
