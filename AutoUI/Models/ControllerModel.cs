using System.Collections.Generic;

namespace AutoUI.Models {
    public class ControllerModel {
        public string route { get; internal set; }
        public List<MethodModel> methods { get; internal set; }
    }
}
