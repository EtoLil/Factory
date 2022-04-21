namespace Factory.Core
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

        public static int CarFactoryCount = 2;

        public static int DealersCount = 2;

        public static uint EngineWarehouseCapacity = 5;
        public static uint BodyWarehouseCapacity = 5;
        public static uint AccessoriesWarehouseCapacity = 5;

        public static uint CarWarehouseCapacity = 5;
    }
}
