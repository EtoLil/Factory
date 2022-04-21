using Factory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Interfaces
{
    public interface IWorker
    {
        IDetailsWarehouse<Engine> EngineWarehouse { get; set; }
        IDetailsWarehouse<Body> BodyWarehouse { get; set; }
        IDetailsWarehouse<Accessories> AccessoriesWarehouse { get; set; }
        ICarWarehouse CarWarehouse { get; set; }

        IList<IDetailsCreator<Engine>> EngineCreators { get; set; }
        IList<IDetailsCreator<Body>> BodyCreators { get; set; }
        IList<IDetailsCreator<Accessories>> AccessoriesCreators { get; set; }

        IList<ICarDirector> CarDirectors { get; set; }

        IList<IMediator<Engine>> EngineMediators { get; set; }
        IList<IMediator<Body>> BodyMediators { get; set; }
        IList<IMediator<Accessories>> AccessoriesMediators { get; set; }

        IList<IMediator<ICar>> CarMediators { get; set; }

        IList<IDealer> Dealers { get; set; }

        void Init();
        void Run();
        State GetState();
    }
}
