using Autofac;
using TasksLibrary.Application.Commands.VerifyToken;
using TasksLibrary.Application.Queries.FetchAllNotes;
using TasksLibrary.Application.Queries.FetchNoteById;
using TasksLibrary.Extensions;
using TasksLibrary.Services.Architecture.Application;

namespace TasksLibrary.Client
{
    public class Query
    {
        public Query(IContainer container, ITaskApplication application)
        {
            Container = container;
            Application = application;
        }

        public async Task<dynamic> GetAllTasks(string accessToken)
        {
            var accessCommand = new VerifyTokenCommand()
            {
                AccessToken = accessToken
            };

            var response = await Application.ExceuteCommand<VerifyTokenCommand, string>(Container, accessCommand);
            if (response.NotSuccessful)
                return response.Errors[0];
            var getAllTasksResponse = await Application.SendQuery<FetchAllNotesQuery, List<NoteDTO>>(Container, new FetchAllNotesQuery() { UserId = new Guid(response.Data)});
            return getAllTasksResponse.Data;
        }


        public async Task<dynamic> GetTaskById(Guid taskId,string accessToken)
        {
            var accessCommand = new VerifyTokenCommand()
            {
                AccessToken = accessToken
            };

            var response = await Application.ExceuteCommand<VerifyTokenCommand, string>(Container, accessCommand);
            if (response.NotSuccessful)
                return response.Errors[0];
            var command = new FetchNoteByIdQuery()
            {
                TaskId = taskId,
                UserId = new Guid(response.Data)
            };
            var taskResponse = await Application.SendQuery<FetchNoteByIdQuery,NoteDTO>(Container, command);

            if (taskResponse.NotSuccessful)
                return taskResponse.Errors[0];

            return taskResponse.Data;
        }

        public IContainer Container;
        public ITaskApplication Application;
    }
}
