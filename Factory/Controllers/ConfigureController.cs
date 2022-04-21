using Factory.Core;
using Factory.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigureController: ControllerBase
    {
        [Route("EnginesCreateTime")]
        [HttpPost]
        public ActionResult SetEnginesCreateTime([FromBody] CreationTimeDto dto)
        {
            Configure.EnginesCreateTime[dto.Id] = dto.CreationTime;
            return Ok();
        }
        [Route("AccessoriesCreateTime")]
        [HttpPost]
        public ActionResult SetAccessoriesCreateTime([FromBody] CreationTimeDto dto)
        {
            Configure.AccessoriesCreateTime[dto.Id] = dto.CreationTime;
            return Ok();
        }
        [Route("BodiesCreateTime")]
        [HttpPost]
        public ActionResult SetBodiesCreateTime([FromBody] CreationTimeDto dto)
        {
            Configure.BodiesCreateTime[dto.Id] = dto.CreationTime;
            return Ok();
        }
        [Route("CarFactoriesCreateTime")]
        [HttpPost]
        public ActionResult SetCarFactoriesCreateTime([FromBody] CreationTimeDto dto)
        {
            Configure.CarFactoriesCreateTime[dto.Id] = dto.CreationTime;
            return Ok();
        }
        [Route("DealersRequestTime")]
        [HttpPost]
        public ActionResult SetDealersRequestTime([FromBody] CreationTimeDto dto)
        {
            Configure.DealersRequestTime[dto.Id] = dto.CreationTime;
            return Ok();
        }
        [Route("Configuration")]
        [HttpPost]
        public ActionResult<ConfigureDto> SetConfiguration([FromBody] ConfigureDto dto)
        {
            if (dto is not null)
            {
                Configure.EngineWarehouseCapacity = dto.EngineWarehouseCapacity;
                Configure.AccessoriesWarehouseCapacity = dto.AccessoriesWarehouseCapacity;
                Configure.BodyWarehouseCapacity = dto.BodyWarehouseCapacity;
                Configure.CarWarehouseCapacity = dto.CarWarehouseCapacity;

                Configure.EngineCreatorsCount = dto.EngineCreatorsCount;
                Configure.AccessoriesCreatorsCount = dto.AccessoriesCreatorsCount;
                Configure.BodyCreatorsCount = dto.BodyCreatorsCount;
                Configure.CarFactoryCount = dto.CarFactoryCount;

                Configure.DealersCount = dto.DealersCount;

                return Ok(dto);
            }
            return BadRequest();
        }

        [Route("Configuration")]
        [HttpGet]
        public ActionResult<ConfigureDto> GetConfiguration()
        {
            var a = new ConfigureDto()
            {
                EngineWarehouseCapacity = Configure.EngineWarehouseCapacity,
                AccessoriesWarehouseCapacity = Configure.AccessoriesWarehouseCapacity,
                BodyWarehouseCapacity = Configure.BodyWarehouseCapacity,
                CarWarehouseCapacity = Configure.CarWarehouseCapacity,
                EngineCreatorsCount = Configure.EngineCreatorsCount,
                AccessoriesCreatorsCount = Configure.AccessoriesCreatorsCount,
                BodyCreatorsCount = Configure.BodyCreatorsCount,
                CarFactoryCount = Configure.CarFactoryCount,
                DealersCount = Configure.DealersCount,
            };
            return Ok(a);
        }
    }
}
