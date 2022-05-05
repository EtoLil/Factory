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
    public class Worker : IWorker
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

        public IList<IDealer> Dealers { get; set; }

        private bool _isInited;
        private CancellationTokenSource _cancellationTokenSource;

        public Worker()
        {
            _isInited = false;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Init()
        {

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            EngineWarehouse?.ContinueManualResetEvent();
            AccessoriesWarehouse?.ContinueManualResetEvent();
            BodyWarehouse?.ContinueManualResetEvent();
            CarWarehouse?.ContinueManualResetEvent();

            _cancellationTokenSource = new CancellationTokenSource();

            EngineWarehouse = new EngineWarehouse(Configure.EngineWarehouseCapacity);
            BodyWarehouse = new BodyWarehouse(Configure.BodyWarehouseCapacity);
            AccessoriesWarehouse = new AccessoriesWarehouse(Configure.AccessoriesWarehouseCapacity);

            CarWarehouse = new CarWarehouse(Configure.CarWarehouseCapacity);

            EngineCreators = new List<IDetailsCreator<Engine>>();
            EngineMediators = new List<IMediator<Engine>>();
            Configure.EnginesCreateTime = new List<int>();
            for (int i = 0; i < Configure.EngineCreatorsCount; i++)
            {
                Configure.EnginesCreateTime.Add(5000);
                EngineCreators.Add(new EngineCreator(id: i, token: _cancellationTokenSource.Token));
                EngineMediators.Add(new DetailsMediator<Engine>(EngineWarehouse, EngineCreators[i]));
            }

            BodyCreators = new List<IDetailsCreator<Body>>();
            BodyMediators = new List<IMediator<Body>>();
            Configure.BodiesCreateTime = new List<int>();
            for (int i = 0; i < Configure.BodyCreatorsCount; i++)
            {
                Configure.BodiesCreateTime.Add(5000);
                BodyCreators.Add(new BodyCreator(id: i, token: _cancellationTokenSource.Token));
                BodyMediators.Add(new DetailsMediator<Body>(BodyWarehouse, BodyCreators[i]));
            }

            AccessoriesCreators = new List<IDetailsCreator<Accessories>>();
            AccessoriesMediators = new List<IMediator<Accessories>>();
            Configure.AccessoriesCreateTime = new List<int>();
            for (int i = 0; i < Configure.AccessoriesCreatorsCount; i++)
            {
                Configure.AccessoriesCreateTime.Add(5000);
                AccessoriesCreators.Add(new AccessoriesCreator(id: i, token: _cancellationTokenSource.Token));
                AccessoriesMediators.Add(new DetailsMediator<Accessories>(AccessoriesWarehouse, AccessoriesCreators[i]));
            }


            CarDirectors = new List<ICarDirector>();
            CarMediators = new List<IMediator<ICar>>();
            Configure.CarFactoriesCreateTime = new List<int>();
            for (int i = 0; i < Configure.CarFactoryCount; i++)
            {
                Configure.CarFactoriesCreateTime.Add(5000);
                CarDirectors.Add(new CarDirector(i, EngineWarehouse, BodyWarehouse, AccessoriesWarehouse));
                CarMediators.Add(new CarMediator(CarWarehouse, CarDirectors[i]));
            }

            Dealers = new List<IDealer>();
            for (int i = 0; i < Configure.DealersCount; i++)
            {
                Dealers.Add(new Dealer(CarWarehouse, i));
                Configure.DealersRequestTime.Add(5000);
            }

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

                for (int i = 0; i < Configure.DealersCount; i++)
                {
                    Dealers[i].Run();
                }
            }
        }

        public State GetState()
        {
            if (!_isInited)
            {
                return null;
            }

            var state = new State();

            state.WarehouseEnginesNumbers = EngineWarehouse.GetDetailsNumber();
            state.EngineWarehouseQueue = EngineWarehouse.GetOrders();
            state.WarehouseEngines = EngineWarehouse.GetDetailsList();

            state.WarehouseAccessoriesNumbers = AccessoriesWarehouse.GetDetailsNumber();
            state.AccessoriesWarehouseQueue = AccessoriesWarehouse.GetOrders();
            state.WarehouseAccessories = AccessoriesWarehouse.GetDetailsList();

            state.WarehouseBodiesNumbers = BodyWarehouse.GetDetailsNumber();
            state.BodyWarehouseQueue = BodyWarehouse.GetOrders();
            state.WarehouseBodies = BodyWarehouse.GetDetailsList();

            state.WarehouseCarsNumbers = CarWarehouse.GetCarsNumber();
            state.CarWarehouseQueue = CarWarehouse.GetOrders();
            state.WarehouseCars = CarWarehouse.GetCarsList();

            foreach (var creator in EngineCreators)
            {
                state.EngineCreatorsWorkStates.Add(creator.State);
                state.EngineCreatedNumbers.Add(creator.GetCreatedNumber());
            }
            foreach (var creator in AccessoriesCreators)
            {
                state.AccessoriesCreatorsWorkStates.Add(creator.State);
                state.AccessoriesCreatedNumbers.Add(creator.GetCreatedNumber());
            }
            foreach (var creator in BodyCreators)
            {
                state.BodyCreatorsWorkStates.Add(creator.State);
                state.BodyCreatedNumbers.Add(creator.GetCreatedNumber());
            }
            foreach (var director in CarDirectors)
            {
                state.CarBuilderWorkStates.Add(director.State);
                state.CarBuiltNumbers.Add(director.GetBuiltCarNumber());
            }

            state.Dealers = Dealers;

            return state;
        }
    }
}