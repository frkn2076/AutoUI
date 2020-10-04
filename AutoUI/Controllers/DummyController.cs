using Microsoft.AspNetCore.Mvc;

namespace AutoUI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase {
        public DummyController() {
        }

        [HttpGet("sayHi")]
        public string GiveMeHello() {
            return "Heyyooo";
        }
    }
}
