using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

namespace Factory.Core.Warehouse
{
    public class BodyWarehouse : DetailsWarehouse<Body>
    {
        public BodyWarehouse(
            uint capcity,
            IMediator<Body> detailsMediator = null
            )
            : base(capcity, detailsMediator)
        {
        }

        public override void HandleOrder(ICarDirector carBuilder)
        {
            if (_details.Count > 0)
            {
                Console.WriteLine($"Warehouse has a body {_details.Count}");

                carBuilder.TakeBody(_details.Dequeue());
                _event.Set();
                return;
            }

            Console.WriteLine($"Warehouse has no body {_details.Count}");
            _carDirectors.Enqueue(carBuilder);
        }

        public override void AddDetail(Body detail)
        {
            if (_carDirectors.Count != 0)
            {
                var carBuilder = _carDirectors.Dequeue();

                carBuilder.TakeBody(detail);
            }
            else
            {
                _details.Enqueue(detail);
            }


            if (_details.Count() == _capacity)
            {
                _event.Reset();
                Console.WriteLine($"BodyWarehouse Full {_details.Count}");
            }
            _event.WaitOne();
            _detailsMediator.Notify(CreatingStatus.CanCreate);
        }
    }
}
