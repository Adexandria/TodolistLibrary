using Automappify.Services;
using TasksLibrary.Models;

namespace TasksLibrary.Utilities
{
    public class MapCommand
    {
        public TModel MapToEntity<TCommand, TModel>(TCommand command)
           where TCommand : IValidator
          where TModel : IEntity
        {
            Type type = null;
            if (AppAssembly.GetAssemblies().Any())
            {
                foreach (var assembly in AppAssembly.GetAssemblies())
                {
                    type = assembly.GetTypes().FirstOrDefault(s => s.GetInterface(typeof(TModel).Name) == typeof(TModel) && !s.IsInterface);
                    if (type != null)
                        break;
                }
            }
            else
            {
                type = typeof(TModel).Assembly.GetTypes().FirstOrDefault(s => s.GetInterface(typeof(TModel).Name) == typeof(TModel) && !s.IsInterface);
            }
            var instance = (TModel)Activator.CreateInstance(type);

            instance.Map(command);

            return instance;
        }

        public AppAssembly AppAssembly { get; set; }
    }
}
