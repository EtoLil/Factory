using Factory.Core.Entities;
using Factory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core
{
    public class DealerCommunity: IDealerCommunity
    {
        IList<Dealer> dealers;


        public DealerCommunity(ICarWarehouse carWarehouse)
        {
            dealers = new List<Dealer>();
            for (int i = 0; i < Configure.DealersCount; i++)
            {
                dealers.Add(new Dealer(carWarehouse));
            }
        }

        public void Run()
        {
            for (int i = 0; i < dealers.Count; i++)
            {
                Task.Run(() => dealers[i].Run(Configure.DealersRequestTime[i]));
                Thread.Sleep(500);
            }
        }

    }
}
