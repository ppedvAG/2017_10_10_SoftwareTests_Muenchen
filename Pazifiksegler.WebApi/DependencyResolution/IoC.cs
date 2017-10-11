namespace Pazifiksegler.WebApi.DependencyResolution
{
    using StructureMap;
	
    public static class IoC
    {
        public static IContainer Initialize() => new Container(c => c.AddRegistry<DefaultRegistry>());
    }
}