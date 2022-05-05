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
            IList<IMediator<Body>> detailsMediators = null, CancellationToken token = default)
            : base(capcity, detailsMediators, token)
        {
        }

        public override void HandleOrder(ICarDirector carDirector)
        {
            lock (_lockerGetNewOrder)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"BodyWarehouse token.IsCancellationRequested from car Director {carDirector.Id}");
                    _token.ThrowIfCancellationRequested();
                }
                if (_details.Count > 0 && _details.TryDequeue(out Body? body))
                {
                    Console.WriteLine($"BodyWarehouse Can Give Details: deteils {_details.Count} left");
                    carDirector.TakeBody(body);
                    Console.WriteLine($"BodyWarehouse Gave Detail: deteils {_details.Count} left");
                    if (_details.Count() + _notyfyCount < _capacity)
                    {
                        _event.Set();
                    }
                    return;
                }

                Console.WriteLine($"BodyWarehouse Empty");
                _carDirectors.Enqueue(carDirector);
                Console.WriteLine($"BodyWarehouse Queue Add 1: {_carDirectors.Count} directors are waiting Body");
            }
        }

        public override void AddDetail(Body detail, int creatorId)
        {
            _notyfyCount--;
            Console.WriteLine($"BodyWarehouse Got Deteil From Creator-{creatorId} (notyfyCount = {_notyfyCount})");
            lock (_lockerGetNewDetail)
            {
                if (_carDirectors.Count != 0)
                {
                    var carBuilder = _carDirectors.Dequeue();

                    carBuilder.TakeBody(detail);
                    Console.WriteLine($"BodyWarehouse Queue Minus 1: {_carDirectors.Count} dealers are waiting Body");
                }
                else
                {
                    _details.Enqueue(detail);
                    Console.WriteLine($"EngineWarehouse Get Details: deteils {_details.Count} left");
                }
            }
            lock (_lockerWaitSapace)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"BodyWarehouse token.IsCancellationRequested from detail creator {creatorId}");
                    _token.ThrowIfCancellationRequested();
                }
                if (_details.Count()+ _notyfyCount >= _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"BodyWarehouse Is Full: deteils {_details.Count} left");
                    Console.WriteLine($"BodyWarehouse Creator-{creatorId} Waiting For Start");
                }
                _event.WaitOne();
                _notyfyCount++;
            }
            Console.WriteLine($"BodyWarehouse Notify Creator-{creatorId} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
            _detailsMediator[creatorId].Notify(CreatingStatus.CanCreate);
        }
    }
}
