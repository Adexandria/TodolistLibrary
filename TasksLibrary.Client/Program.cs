using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Application.Commands.Login;
using TasksLibrary.Application.Commands.UpdateTask;
using TasksLibrary.Client;
using TasksLibrary.Architecture.Application;

var containerBuilder = new TaskContainerBuilder("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=true;");

containerBuilder.BuildMigration();
var container = containerBuilder.SetUpDepedencies("SecretKeyApp@1234").Build();

ITaskApplication application = new TaskApplication();

var command = new Command(container,application);
var query  = new Query(container,application);


var createUsercommand = new CreateUserCommand()
{
    Password = "1234",
    ConfirmPassword = "1234",
    Email = "adeolaaderibigbe@gmail.com",
    Name = "Adeola Aderibigbe"
};

var loginCommand = new LoginCommand()
{
    Password ="1234",
    Email = "adeolaaderibigbe@gmail.com"
};
var UpdatedCommand = new UpdateTaskCommand()
{
    Description = "This is an updated description",
    Task = "Updated task",
    TaskId = new Guid("ce0e4999-c9ca-4ebc-8cd9-b00d00d92bd5")
};

var dto = new CreateTaskDTO()
{
    Task = "First task created",
    Description = "This is my first task"
};

/*//To create user
var result = await command.CreateUser(createUsercommand);
Console.WriteLine(result.GetType().Name == "String" ? result : $"{result.Name} and {result.Email} \n User has been added");*/

//To Login user
var loginResult = await command.LoginUser(loginCommand);
Console.WriteLine(loginResult.GetType().Name == "String" ? loginResult : $"AccessToken: {loginResult.Data.AccessToken}\nRefreshToken: {loginResult.RefreshToken}");

/*if (loginResult.IsSuccessful)
{

    //Create a new to-do list
    *//*
     * 
      var result = await command.CreateTask(dto, loginResult.AccessToken);
      Console.WriteLine(result.GetType().Name == "String" ? $"Error: {result}" : $"TaskId: {result} created successfully");*//*

    //Update to-do list
    
   *//* var updatedResult = await command.UpdateTask(UpdatedCommand, loginResult.AccessToken);
    Console.WriteLine(updatedResult.GetType().Name == "String" ? updatedResult : "Updated successfully");

    // Get all to-do lists
    var allTasksResult = await query.GetAllTasks(loginResult.AccessToken);
    if(allTasksResult.GetType().Name != "String")
    {
        foreach (var note in allTasksResult)
        {
            Console.WriteLine($"Task: {note.Task}");
        }

    }
    else
    {
        Console.WriteLine(allTasksResult);
    }

    // Get individual list
    var getTaskResult = await query.GetTaskById(new Guid("ce0e4999-c9ca-4ebc-8cd9-b00d00d92bd5"), loginResult.AccessToken);
    Console.WriteLine(getTaskResult.GetType().Name == "String" ? getTaskResult : $"Task: {getTaskResult.Task}");

    //Delete list
    var deletedResult = await command.DeleteTask(new Guid("ce0e4999-c9ca-4ebc-8cd9-b00d00d92bd5"), loginResult.AccessToken);
    Console.WriteLine(deletedResult.GetType().Name == "String" ? deletedResult : "Deleted successfully");*//*
}*/

