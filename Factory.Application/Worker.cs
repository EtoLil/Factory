using Factory.Core.Buiders;
using Factory.Core.Creators;
using Factory.Core.Entities;
using Factory.Core.Interfaces;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core
{
    public class Worker
    {
        public IDetailsWarehouse<Engine> EngineWarehouse { get; set; }
        public IDetailsWarehouse<Body> BodyWarehouse { get; set; }
        public IDetailsWarehouse<Accessories> AccessoriesWarehouse { get; set; }
        public ICarWarehouse CarWarehouse { get; set; }

        public IList<IDetailsCreator<Engine>> EngineCreators { get; set; }
        public IList<IDetailsCreator<Body>> BodyCreators { get; set; }
        public IList<IDetailsCreator<Accessories>> AccessoriesCreators { get; set; }

        public IList<ICarDirector> CarDirectors { get; set; }

        public IList<IMediator<Engine>> EngineMediators { get; set; }
        public IList<IMediator<Body>> BodyMediators { get; set; }
        public IList<IMediator<Accessories>> AccessoriesMediators { get; set; }

        public IList<IMediator<ICar>> CarMediators { get; set; }

        public IDealerCommunity DealerCommunity { get; set; }

        private bool _isInited;

        public Worker()
        {
            _isInited = false;
        }

        public void Init()
        {
            EngineWarehouse = new EngineWarehouse(Configure.EngineWarehouseCapacity);
            BodyWarehouse=new BodyWarehouse(Configure.BodyWarehouseCapacity);
            AccessoriesWarehouse=new AccessoriesWarehouse(Configure.AccessoriesWarehouseCapacity);  

            CarWarehouse=new CarWarehouse(Configure.CarWarehouseCapacity);

            EngineCreators=new List<IDetailsCreator<Engine>>();
            EngineMediators=new List<IMediator<Engine>>();
            for (int i = 0; i < Configure.EngineCreatorsCount; i++)
            {
                EngineCreators.Add(new EngineCreator(i));
                EngineMediators.Add(new DetailsMediator<Engine>(EngineWarehouse, EngineCreators[i]));
            }

            BodyCreators = new List<IDetailsCreator<Body>>();
            BodyMediators= new List<IMediator<Body>>();
            for (int i = 0; i < Configure.BodyCreatorsCount; i++)
            {
                BodyCreators.Add(new BodyCreator(i));
                BodyMediators.Add(new DetailsMediator<Body>(BodyWarehouse, BodyCreators[i]));
            }

            AccessoriesCreators = new List<IDetailsCreator<Accessories>>();
            AccessoriesMediators = new List<IMediator<Accessories>>();
            for (int i = 0; i < Configure.AccessoriesCreatorsCount; i++)
            {
                AccessoriesCreators.Add(new AccessoriesCreator(i));
                AccessoriesMediators.Add(new DetailsMediator<Accessories>(AccessoriesWarehouse, AccessoriesCreators[i]));
            }


            CarDirectors = new List<ICarDirector>();
            CarMediators = new List<IMediator<ICar>>();
            for (int i = 0; i < Configure.CarFactoryCount; i++)
            {
                CarDirectors.Add(new CarDirector(i, EngineWarehouse, BodyWarehouse, AccessoriesWarehouse));
                CarMediators.Add(new CarMediator(CarWarehouse, CarDirectors[i]));
            }

            DealerCommunity = new DealerCommunity(CarWarehouse);

            _isInited = true;
        }
        public void Run()
        {
            if (_isInited)
            {
                EngineWarehouse.Run();
                BodyWarehouse.Run();
                AccessoriesWarehouse.Run();

                CarWarehouse.Run();

                DealerCommunity.Run();
            }
        }
    }
}