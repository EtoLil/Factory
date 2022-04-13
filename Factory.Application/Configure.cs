﻿namespace Factory.Core
{
    public static class Configure
    {
        public static IList<int> EnginesCreateTime = new List<int>();
        public static IList<int> BodiesCreateTime = new List<int>();
        public static IList<int> AccessoriesCreateTime = new List<int>();

        public static IList<int> CarFactoriesCreateTime = new List<int>();

        public static IList<int> DealersRequestTime = new List<int>();


        public static int EngineCreatorsCount = 2;
        public static int BodyCreatorsCount = 2;
        public static int AccessoriesCreatorsCount = 2;

        public static int CarFactoryCount = 3;

        public static int DealersCount = 4;

        public static uint EngineWarehouseCapacity = 5;
        public static uint BodyWarehouseCapacity = 5;
        public static uint AccessoriesWarehouseCapacity = 5;

        public static uint CarWarehouseCapacity = 2;

        static Configure()
        {
            EnginesCreateTime = new List<int>();
            for (int i = 0; i < EngineCreatorsCount; i++)
            {
                EnginesCreateTime.Add(2000);
            }
            BodiesCreateTime = new List<int>();
            for (int i = 0; i < BodyCreatorsCount; i++)
            {
                BodiesCreateTime.Add(3000);
            }
            AccessoriesCreateTime = new List<int>();
            for (int i = 0; i < AccessoriesCreatorsCount; i++)
            {
                AccessoriesCreateTime.Add(4000);
            }

            CarFactoriesCreateTime = new List<int>();
            for (int i = 0; i < CarFactoryCount; i++)
            {
                CarFactoriesCreateTime.Add(10000);
            }


            DealersRequestTime = new List<int>();
            for (int i = 0; i < DealersCount; i++)
            {
                DealersRequestTime.Add(20000);
            }
        }

    }
}
