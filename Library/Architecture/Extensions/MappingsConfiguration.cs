using FluentNHibernate;
using FluentNHibernate.Cfg;
using System.Reflection;
using TasksLibrary.DB.Mappings;

namespace TasksLibrary.Architecture.Extensions
{
    public static class MappingsConfiguration
    {
        public static FluentConfiguration AddMappings(this FluentConfiguration configuration, Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                configuration.Mappings(x => x.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly));
                return configuration;
            }

            var currentTypes = typeof(UserMap).Assembly.GetTypes().Where(s => s.GetInterfaces().Contains(typeof(IMappingProvider)) && !s.IsGenericType );

            var executingTypes = GetExecutingTypes(assemblies);

            foreach (var currentType in currentTypes)
            {
                var executingType = executingTypes.FirstOrDefault(t => t.BaseType?.GetGenericArguments()[0]?.GetInterfaces()[0] == currentType.BaseType?.GetGenericArguments()[0]?.GetInterfaces()[0]);
                if (executingType != null)
                    configuration.Mappings(x => x.FluentMappings.Add(executingType));
                else
                {
                    configuration.Mappings(x => x.FluentMappings.Add(currentType));
                }
            }
            return configuration;
        }

        private static IEnumerable<Type> GetExecutingTypes(Assembly[] assemblies)
        {
            IEnumerable<Type> executingTypes = null;
            foreach (var assembly in assemblies)
            {
                executingTypes = assembly.GetTypes().Where(s => s.GetInterfaces().Contains(typeof(IMappingProvider)));
                if (executingTypes != null)
                    break;
            }
            return executingTypes;
        }
    }
}
