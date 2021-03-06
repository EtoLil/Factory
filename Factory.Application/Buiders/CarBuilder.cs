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
        protected CancellationToken _token;

        public CarBuilder(int id, CancellationToken token = default)
        {
            _id = id;
            _token = token;
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
            if (_token.IsCancellationRequested)
            {
                Console.WriteLine($"Car builder {_id} token.IsCancellationRequested");
                _token.ThrowIfCancellationRequested();
            }
            else
            {
                Thread.Sleep(Configure.CarFactoriesCreateTime[_id]);
            }
            var car = new Car(_engine, _body,_accessories);
            Reset();
            Console.WriteLine($"CarBulder {_id}: Buld {car}");
            return car;
        }
    }
}
