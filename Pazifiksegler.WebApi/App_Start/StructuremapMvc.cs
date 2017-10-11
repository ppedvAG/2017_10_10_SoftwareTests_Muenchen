using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Pazifiksegler.WebApi.App_Start;
using Pazifiksegler.WebApi.DependencyResolution;
using StructureMap;
using System.Web.Mvc;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace Pazifiksegler.WebApi.App_Start
{

    public static class StructuremapMvc {

        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }
		
		public static void End() {
            StructureMapDependencyScope.Dispose();
        }
		
        public static void Start() {
            IContainer container = IoC.Initialize();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }
    }
}