using Factory.Core.Entities;
using Factory.Core.Enums;
using Factory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core
{
    public class State
    {
        public int WarehouseEnginesNumbers { get; set; }
        public int WarehouseAccessoriesNumbers { get; set; }
        public int WarehouseBodiesNumbers { get; set; }
        public int WarehouseCarsNumbers { get; set; }

        
        public IList<WorkState> EngineCreatorsWorkStates { get; set; } = new List<WorkState>();
        public IList<WorkState> AccessoriesCreatorsWorkStates { get; set; } = new List<WorkState>();
        public IList<WorkState> BodyCreatorsWorkStates { get; set; } = new List<WorkState>();
        public IList<WorkState> CarBuilderWorkStates { get; set; } = new List<WorkState>();
        

        public IList<int> EngineCreatedNumbers { get; set; } = new List<int>();
        public IList<int> AccessoriesCreatedNumbers { get; set; } = new List<int>();
        public IList<int> BodyCreatedNumbers { get; set; } = new List<int>();
        public IList<int> CarBuiltNumbers { get; set; } = new List<int>();


        public IList<int> EngineWarehouseQueue { get; set; } = new List<int>();
        public IList<int> AccessoriesWarehouseQueue { get; set; } = new List<int>();
        public IList<int> BodyWarehouseQueue { get; set; } = new List<int>();
        public IList<string> CarWarehouseQueue { get; set; } = new List<string>();

        public IList<Engine> WarehouseEngines { get; set; } = new List<Engine>();
        public IList<Accessories> WarehouseAccessories { get; set; } = new List<Accessories>();
        public IList<Body> WarehouseBodies { get; set; } = new List<Body>();
        public IList<Car> WarehouseCars { get; set; } = new List<Car>();
  

        public IList<IDealer> Dealers { get; set; } = new List<IDealer>();

    }
}
