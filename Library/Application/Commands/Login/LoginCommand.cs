using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.Login
{
    public class LoginCommand : Command<LoginDTO>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator()
                .IsText(Password,"Invalid password")
                .IsText(Email,"Invalid email")
                .Result;                    
        }


        public string Email { get; set; }
        public string Password { get; set; }
    }
}
