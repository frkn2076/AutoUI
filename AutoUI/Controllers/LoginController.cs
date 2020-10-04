using AutoUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoUI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase {
        public LoginController() {
        }

        [HttpGet]
        public string Login() {
            return "Success";
        }

        [HttpPost("register")]
        public string Register([FromBody]UserModel user) {
            return $"{user?.name} registered";
        }
    }
}
