using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.VerifyToken
{
    public class VerifyTokenCommand : Command<string>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                  .IsText(AccessToken, "Invalid token")
                  .Result;
        }
        public string AccessToken { get; set; }
    }
}
