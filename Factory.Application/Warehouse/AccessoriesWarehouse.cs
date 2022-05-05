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
        public AccessoriesWarehouse(uint capcity, IList<IMediator<Accessories>> detailsMediators = null, CancellationToken token = default)
            : base(capcity, detailsMediators,token)
        {
        }

        public override void HandleOrder(ICarDirector carDirector)
        {
            lock (_lockerGetNewOrder)
            {
                if (_token.IsCancellationRequested)
                {
                    Console.WriteLine($"AccessoriesWarehouse token.IsCancellationRequested from car Director {carDirector.Id}");
                    _token.ThrowIfCancellationRequested();
                }
                if (_details.Count > 0 && _details.TryDequeue(out Accessories? accessories))
                {
                    Console.WriteLine($"AccessoriesWarehouse Can Give Details: deteils {_details.Count} left");
                    carDirector.TakeAccessories(accessories);
                    Console.WriteLine($"AccessoriesWarehouse Gave Detail: deteils {_details.Count} left");
                    if (_details.Count() + _notyfyCount < _capacity)
                    {
                        _event.Set();
                    }
                    return;
                }

                Console.WriteLine($"AccessoriesWarehouse Empty");
                _carDirectors.Enqueue(carDirector);
                Console.WriteLine($"AccessoriesWarehouse Queue Add 1: {_carDirectors.Count} directors are waiting Accessories");
            }
        }

        public override void AddDetail(Accessories detail, int creatorId)
        {
            _notyfyCount--;
            Console.WriteLine($"AccessoriesWarehouse Got Deteil From Creator-{creatorId} (notyfyCount = {_notyfyCount})");

            lock (_lockerGetNewDetail)
            {
                if (_carDirectors.Count != 0)
                {
                    var carBuilder = _carDirectors.Dequeue();
                    carBuilder.TakeAccessories(detail);
                    Console.WriteLine($"AccessoriesWarehouse Queue Minus 1: {_carDirectors.Count} dealers are waiting Accessories");
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
                    Console.WriteLine($"AccessoriesWarehouse token.IsCancellationRequested from detail creator {creatorId}");
                    _token.ThrowIfCancellationRequested();
                }
                if (_notyfyCount + _details.Count() >= _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"AccessoriesWarehouse Is Full: deteils {_details.Count} left");
                    Console.WriteLine($"AccessoriesWarehouse Creator-{creatorId} Waiting For Start");
                }
                _event.WaitOne();
                _notyfyCount++;
            }
            Console.WriteLine($"AccessoriesWarehouse Notify Creator-{creatorId} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
            _detailsMediator[creatorId].Notify(CreatingStatus.CanCreate);

        }
    }
}
