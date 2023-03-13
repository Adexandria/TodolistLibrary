using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Extensions
{
    public class Command
    {
        public ActionResult ActionResult;
        
        public virtual ActionResult Validator()
        {
            return new ActionResult();
        }
        
        
    }

    public class Command<T> : Command  
    {
        
    }
    
}
