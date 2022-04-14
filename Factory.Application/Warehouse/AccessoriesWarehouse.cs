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
        public AccessoriesWarehouse(uint capcity, IList<IMediator<Accessories>> detailsMediators = null)
            : base(capcity, detailsMediators)
        {
        }

        public override void HandleOrder(ICarDirector carDirector)
        {
            if (_details.Count > 0 && _details.TryDequeue(out Accessories? accessories))
            {
                Console.WriteLine($"AccessoriesWarehouse Can Give Details: deteils {_details.Count} left");
                carDirector.TakeAccessories(accessories);
                Console.WriteLine($"AccessoriesWarehouse Gave Detail: deteils {_details.Count} left");
                _event.Set();
                return;
            }

            Console.WriteLine($"AccessoriesWarehouse Empty");
            _carDirectors.Enqueue(carDirector);
            Console.WriteLine($"Director Queue Add 1: {_carDirectors.Count} directors are waiting Accessories");

        }

        public override void AddDetail(Accessories detail, int creatorId)
        {
            lock (_locker)
            {
                _notyfyCount--;
                Console.WriteLine($"AccessoriesWarehouse Got Deteil From Creator-{creatorId} (notyfyCount = {_notyfyCount})");
                if (_carDirectors.Count != 0)
                {
                    var carBuilder = _carDirectors.Dequeue();
                    carBuilder.TakeAccessories(detail);
                    Console.WriteLine($"Director Queue Minus 1: {_carDirectors.Count} dealers are waiting Accessories");
                }
                else
                {
                    _details.Enqueue(detail);
                    Console.WriteLine($"EngineWarehouse Get Details: deteils {_details.Count} left");
                }


                if (_notyfyCount + _details.Count() == _capacity)
                {
                    _event.Reset();
                    Console.WriteLine($"AccessoriesWarehouse Is Full: deteils {_details.Count} left");
                }
                _event.WaitOne();
                _notyfyCount++;

                Console.WriteLine($"AccessoriesWarehouse Notify Creator-{creatorId} To Create (notyfyCount = {_notyfyCount}): deteils {_details.Count} left");
                _detailsMediator[creatorId].Notify(CreatingStatus.CanCreate);
            }
        }
    }
}
