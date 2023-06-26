

using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.GenerateToken
{
    public class GenerateTokenCommand : Command<string>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                .IsText(RefreshToken, "Invalid token")
                .Result;
        }
        public string RefreshToken { get; set; }
    }
}
