using Factory.Core.Buiders;
using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using Factory.Core.Warehouse.Base;

namespace Factory.Core.Warehouse
{
    public class EngineWarehouse : DetailsWarehouse<Engine>
    {
        public EngineWarehouse(
            uint capcity,
            IList<IMediator<Engine>> detailsMediators = null
            )
            : base(capcity, detailsMediators)
        {
        }

        public override void HandleOrder(ICarDirector carDirector)
        {
            lock (_lockerGetNewOrder)
            {
                if (_details.Count > 0 && _details.TryDequeue(out Engine? engine))
                {
                    Console.WriteLine($"EngineWarehouse Can Give Details: deteils {_details.Count} left");
                    carDirector.TakeEngine(engine);
                    Console.WriteLine($"EngineWarehouse Gave Detail: deteils {_details.Count} left");
                    _event.Set();
                    return;
                }

                Console.WriteLine($"EngineWarehouse Empty");
                _carDirectors.Enqueue(carDirector);
                Console.WriteLine($"EngineWarehouse Queue Add 1: {_carDirectors.Count} directors are waiting Engine");
            }
        }

        public override void AddDetail(Engine detail, int creatorId)
        {
            lock (_lockerGetNewDetail)
            {
                _notyfyCount--;
                Console.WriteLine($"EngineWarehouse Got Deteil From Creator-{creatorId} (notyfyCount = {_notyfyCount})");
                if (_carDirectors.Count != 0)
                {
                    var carBuilder = _carDirectors.Dequeue();

                    carBuilder.TakeEngine(detail);
                    Console.WriteLine($"EngineWarehouse Queue Minus 1: {_carDirectors.Count} dealers are waiting Engine");
                }
                else
                {
                    _details.Enqueue(detail);
                    Console.WriteLine($"EngineWarehouse Get Details: deteils {_details.Count} left");
                }
            }
            lock (_lockerWaitSapace)
            {
                if (_details.Count()+ _notyfyCount == _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"EngineWarehouse Is Full: deteils {_details.Count} left");
                    Console.WriteLine($"EngineWarehouse Creator-{creatorId} Waiting For Start");
                }
                _event.WaitOne();
                _notyfyCount++;
            }
            Console.WriteLine($"EngineWarehouse Notify Creator-{creatorId} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
            _detailsMediator[creatorId].Notify(CreatingStatus.CanCreate);
        }

    }
}
