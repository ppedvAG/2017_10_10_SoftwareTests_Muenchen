
using Pazifiksegler.WebApi.DependencyResolution;
using System.Web.Http;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Pazifiksegler.WebApi.App_Start.StructuremapWebApi), "Start")]

namespace Pazifiksegler.WebApi.App_Start
{
    public static class StructuremapWebApi
    {
        public static void Start()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}