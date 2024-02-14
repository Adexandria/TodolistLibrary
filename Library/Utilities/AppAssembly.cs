using System.Reflection;

namespace TasksLibrary.Utilities
{
    public class AppAssembly
    {
        public AppAssembly(Assembly[] _assemblies)
        {
           assemblies = _assemblies;
        }
        public Assembly[] GetAssemblies()
        {
            return assemblies;
        }

        private readonly Assembly[] assemblies;
    }
}
