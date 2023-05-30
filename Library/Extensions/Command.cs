using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Extensions
{
    public abstract class Command
    {
        public abstract ActionResult Validate();
    }

    public abstract class Command<T> : Command, ICommand<T>
    {

    }

    public interface ICommand<T> : IValidator 
    {
    }

}
