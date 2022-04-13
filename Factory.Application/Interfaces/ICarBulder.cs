using Factory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Interfaces
{
    public interface ICarBulder
    {
        void BuildBody(Body body);
        void BuildEngine(Engine engine);
        void BuildAccessories(Accessories accessories);
        ICar GetResult();
    }
}
