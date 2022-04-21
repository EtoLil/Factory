using Factory.Core;
using Factory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private IWorker _worker;
        public WorkerController(IWorker worker)
        {
            _worker = worker;
        }

        [Route("run")]
        [HttpPost]
        public ActionResult Run()
        {
            _worker.Init();
            _worker.Run();
            return Ok();
        }

        [Route("state")]
        [HttpGet]
        public ActionResult<State> GetState()
        {
            return Ok(_worker.GetState());
        }
    }
}
