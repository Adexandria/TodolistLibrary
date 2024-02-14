using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Utilities;

namespace TasksLibrary.Client.Services
{
    public class CreateClientCommand : CreateUserCommand
    {
        public override ActionResult Validate()
        {
            var result = base.Validate();
            if (result.NotSuccessful)
            {
                return result;
            }

            return new RequestValidator()
                .IsText(AuthenticationType, "Invalid authentication type")
                .Result;
        }
        public string AuthenticationType { get; set; }
    }
}
