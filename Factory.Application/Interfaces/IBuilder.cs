using Factory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Interfaces
{
    public interface IBuilder
    {
        IBuilder Body(Body body);
        IBuilder Engine(Engine engine);
        IBuilder Accessories(Accessories accessories);
        IBuilder Reset();
        ICar GetResult();
    }
}
