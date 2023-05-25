using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Extensions
{
    public interface IRequestValidator
    {
        ActionResult Validate();
    }
}
