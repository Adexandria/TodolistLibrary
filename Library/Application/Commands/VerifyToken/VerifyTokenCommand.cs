using TasksLibrary.Services;
using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.VerifyToken
{
    public class VerifyTokenCommand : Command<UserDTO>
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
