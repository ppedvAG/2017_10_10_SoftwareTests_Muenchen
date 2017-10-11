using Pazifiksegler.Core.Repositories;
using Pazifiksegler.Data.Repositories;
using StructureMap;

namespace Pazifiksegler.WebApi.DependencyResolution
{

    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<ICustomerRepository>().Use<SampleCustomerRepository>();
        }
    }
}