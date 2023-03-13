using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Extensions
{
    
    public class CommandHandler<TCommand, TDbcontext,TResponse> : CommandHandler<TCommand,TDbcontext> where TCommand: Command<TResponse>
    {
        public TDbcontext DbContext;
        public virtual Task<ActionResult<TResponse>> HandleCommand()
        {
            return Task.FromResult(new ActionResult<TResponse>().SuccessfulOperation(default));
        }
    }
    
    public class CommandHandler<TCommand,TDbcontext> : CommandHandler where TCommand : Command
    {
        public TCommand command;
        public override Task<ActionResult> HandleCommand()
        {
            return base.HandleCommand();
        }
    }

    public class CommandHandler 
    {
        public virtual Task<ActionResult> HandleCommand()
        {
            return Task.FromResult(new ActionResult().Successful());
        }
    }
}
    
