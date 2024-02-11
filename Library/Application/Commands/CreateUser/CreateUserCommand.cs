using Automapify.Services.Attributes;
using System.ComponentModel.DataAnnotations;
using TasksLibrary.Utilities;

namespace TasksLibrary.Application.Commands.CreateUser
{
    public class CreateUserCommand : Command<CreateUserDTO>
    {
        public override ActionResult Validate()
        {
            return new RequestValidator().IsEmail(Email, "Invalid email")
                .IsText(FirstName, "Invalid name")
                .IsText(LastName, "Invalid name")
                .IsText(Password, "Invalid password")
                .Result;
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Ignore]
        public string Password { get; set; }

        [Ignore]
        [Compare("Password", ErrorMessage = "Password and confirm Password not equal")]
        public string ConfirmPassword { get; set; }
    }
}
