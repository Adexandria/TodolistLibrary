using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Application.Commands.Login;
using TasksLibrary.Client;
using TasksLibrary.Services.Architecture.Application;

var containerBuilder = new TaskContainerBuilder("Data Source=(localdb)\\MSSQLLocalDB;Database=TodoList;Integrated Security=True;Connect Timeout=30;");

var container = containerBuilder.Build();

ITaskApplication application = new TaskApplication();

var command = new Command(container,application);


var createUsercommand = new CreateUserCommand()
{
    Password = "1234",
    ConfirmPassword = "1234",
    Email = "adeolaaderibigbe09@gmail.com",
    Name = "Adeola Aderibigbe"
};

var loginCommand = new LoginCommand()
{
    Password ="1234",
    Email ="adeolaaderibigbe09@gmail.com"
};



//To create user
var result = await command.CreateUser(createUsercommand);
Console.WriteLine(result.GetType().Name == "String" ? result : $"{result.Name} and {result.Email} \n User has been added");

/*//To Login user
var result = await command.LoginUser(loginCommand);
Console.WriteLine(result.GetType().Name == "String" ? result : $"AccessToken: {result.AccessToken}, \n RefreshToken: {result.RefreshToken}");*/


