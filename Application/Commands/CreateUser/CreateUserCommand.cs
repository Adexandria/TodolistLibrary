using System.ComponentModel.DataAnnotations;
using TasksLibrary.Extensions;

namespace TasksLibrary.Application.Commands.CreateUser
{
    public class CreateUserCommand : Command<CreateUserDTO>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator().IsEmail(Email, "Invalid email")
                .IsText(Name, "Invalid name")
                .IsText(Password, "Invalid password")
                .Result;
        }


        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and confirm Password not question")]
        public string ConfirmPassword { get; set; }
    }
}
