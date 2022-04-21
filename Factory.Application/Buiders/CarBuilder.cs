using Factory.Core.Entities;
using Factory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Buiders
{
    public class CarBuilder : IBuilder
    {
        private int _id;

        private Engine? _engine;
        private Accessories? _accessories;
        private Body? _body;

        public CarBuilder(int id)
        {
            _id = id;
        }

        public IBuilder Accessories(Accessories accessories)
        {
            _accessories = accessories;
            return this;
        }

        public IBuilder Body(Body body)
        {
            _body = body;
            return this;
        }

        public IBuilder Engine(Engine engine)
        {
            _engine = engine;  
            return this;
        }
        public IBuilder Reset()
        {
            _engine = null;
            _accessories = null;
            _body = null;
            return this;
        }
        public ICar GetResult()
        {
            Thread.Sleep(Configure.CarFactoriesCreateTime[_id]);
            var car = new Car(_engine, _body,_accessories);
            Reset();
            Console.WriteLine($"CarBulder {_id}: Buld {car}");
            return car;
        }
    }
}
