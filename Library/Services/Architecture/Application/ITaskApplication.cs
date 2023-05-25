using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Extensions;

namespace TasksLibrary.Services.Architecture.Application
{
    public interface ITaskApplication
    {
        Task<ActionResult<TResponse>> ExceuteCommand<TCommand,TResponse>(IContainer container,TCommand command) 
            where TCommand : Command<TResponse>;
    }
}
