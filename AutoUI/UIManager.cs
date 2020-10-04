using AutoUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoUI {
    public class UIManager {
        internal List<ControllerModel> GetControllerProperties() {
            var controllersModel = new List<ControllerModel>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            var exportedControllers = assembly.ExportedTypes.Where(x => x.BaseType.Name == nameof(ControllerBase));
            foreach (var exportedController in exportedControllers) {
                var controller = new ControllerModel();
                var methods = ((MethodInfo[])((TypeInfo)exportedController).DeclaredMethods).ToList();

                bool hasRouteAttribute = exportedController.CustomAttributes.Any(x => x.AttributeType.Name == nameof(RouteAttribute));
                if (!hasRouteAttribute)
                    throw new Exception();
                string controllerName = exportedController.CustomAttributes.First(x => x.AttributeType.Name == nameof(RouteAttribute)).ConstructorArguments[0].Value.ToString() == "[controller]" ?
                    exportedController.Name.Replace("Controller", "") :
                    exportedController.CustomAttributes.First(x => x.AttributeType.Name == nameof(RouteAttribute)).ConstructorArguments[0].Value.ToString();

                controller.route = controllerName;
                controller.methods = new List<MethodModel>();
                foreach (var method in methods) {
                    string methodName = method.Name;
                    bool isGet = method.CustomAttributes.Any(x => x.AttributeType.Name == nameof(HttpGetAttribute));
                    bool isPost = method.CustomAttributes.Any(x => x.AttributeType.Name == nameof(HttpPostAttribute));
                    if (!isGet && !isPost)
                        continue;
                    var methodModel = new MethodModel();
                    methodModel.serviceType = isGet ? ServiceType.Get : ServiceType.Post;
                    var serviceRouteArguments = method.CustomAttributes.First(x => isGet ? x.AttributeType.Name == nameof(HttpGetAttribute) : x.AttributeType.Name == nameof(HttpPostAttribute)).ConstructorArguments;
                    var serviceRoute = serviceRouteArguments != null && serviceRouteArguments.Count > 0 ? serviceRouteArguments[0].Value.ToString() : null;
                    methodModel.name = method.Name;
                    methodModel.endpoint = serviceRoute != null ? string.Concat(controllerName,"/", serviceRoute) : controllerName;

                    controller.methods.Add(methodModel);
                }
                controllersModel.Add(controller);
            }
            return controllersModel.Where(x=>x.route != "UIDrawer").ToList();
        }
    }
}
