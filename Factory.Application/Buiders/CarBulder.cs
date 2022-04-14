using Factory.Core.Entities;
using Factory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Buiders
{
    public class CarBulder : ICarBulder
    {
        private int _id;

        private ICar _car = new Car();

        public CarBulder(int id)
        {
            _id = id;
        }

        public void BuildAccessories(Accessories accessories)
        {
            _car.SetAccessories(accessories);
        }

        public void BuildBody(Body body)
        {
            _car.SetBody(body);
        }

        public void BuildEngine(Engine engine)
        {
            _car.SetEngine(engine);
        }

        public ICar GetResult()
        {
            Thread.Sleep(Configure.CarFactoriesCreateTime[_id]);
            var car = (ICar)_car.Clone();
            _car=new Car();
            Console.WriteLine($"CarBulder {_id}: Buld {car}");
            return car;
        }
    }
}
