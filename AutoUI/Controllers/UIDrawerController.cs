using AutoUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoUI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UIDrawerController : ControllerBase {

        private readonly UIManager uiManager;

        public UIDrawerController() {
            this.uiManager = new UIManager();
        }

        [HttpGet]
        public List<ControllerModel> UIDrawer() {
            var controllerModels = uiManager.GetControllerProperties();
            return controllerModels;
        }
    }
}
