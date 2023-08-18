# Todo-List Library
Todo-List Library is a simple library that allows authorised users to take down notes.

## Installing Adeola.TodoListLIbrary

### Introduction
This following are needed before proceeding with that installation:
 - Visual Studio installed on your machine.
 - A .NET 6.0 project that you want to add the package to.

### Installation

To install the package using the dotnet add package command, follow the steps below:
- Open the command prompt or terminal.
- Navigate to the directory where your project is located.
- Type the following command:

```
---
Using github nuget

dotnet add package Adeola.TodoListLibrary --version 1.0.24
---
using nuget

dotnet add package TasksLibrary --Version 1.1.1
```

This command will download and install the specified version of the package along with it's dependencies

Alternatively, you can also install the package using the Package Manager Console in Visual Studio. To do this, follow the steps below:
- Open Visual Studio and open the project you want to add the package to.
- Open the Package Manager Console by clicking on Tools > NuGet Package Manager > Package Manager Console.
- In the Package Manager Console, type the following command:

```
---
Using github nuget

Install-Package Adeola.TodoListLibrary --version 1.0.24
---
Using nuget

Install-Package TasksLibrary -Version 1.1.3
```


### How to use

> **Note**
> This shows how to use the library in console application

- Create an instance of the container builder and pass in the connection string

  ```
  var containerBuilder = new TaskContainerBuilder("connection string");
  ```
  
-   Build the migration
    
    ```
    containerBuilder.BuildMigration();
    ```
    
-   Create the container to set up dependencies
   
    ```
    var container = containerBuilder.SetUpDepedencies().Build();
    ```

-  Create an instance of the task application
  
   ```
   ITaskApplication application = new TaskApplication();
   ```

- You can use the application instance to perform queries and execute commands.

### Check how to use the library in web api
 [AdeNoteAPI](https://github.com/Adeola-Aderibigbe/AdeNoteAPI)
