using Automappify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Extensions
{
    public static class MapCommand
    {
        public static TModel MapToEntity<TCommand, TModel>(this TCommand command)
           where TCommand : IValidator
          where TModel : IEntity
        {
            var type = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(s => s.GetInterface(typeof(TModel).Name) == typeof(TModel))
                ?? typeof(TModel).Assembly.GetTypes().FirstOrDefault(s => s.GetInterface(typeof(TModel).Name) == typeof(TModel));

            var instance = (TModel)Activator.CreateInstance(type);

            instance.Map(command);

            return instance;
        }
    }
}
