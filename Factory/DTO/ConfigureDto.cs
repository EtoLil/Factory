namespace Factory.DTO
{
    public class ConfigureDto
    {
        public int EngineCreatorsCount { get; set; }
        public int BodyCreatorsCount { get; set; }
        public int AccessoriesCreatorsCount { get; set; }
        public int CarFactoryCount { get; set; }

        public int DealersCount { get; set; }

        public uint EngineWarehouseCapacity { get; set; }
        public uint BodyWarehouseCapacity { get; set; }
        public uint AccessoriesWarehouseCapacity { get; set; }
        public uint CarWarehouseCapacity { get; set; }
    }
}
