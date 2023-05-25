using NHibernate;
using TasksLibrary.Extensions;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Services.Architecture.TaskModule
{
    public class DatabaseManagementContext<T> : DbContext<T>
    {
        public DatabaseManagementContext(ISession session)
        {
            Session = session;
        }
        public override async Task<ActionResult> CommitAsync()
        {
            using var transaction = Session.BeginTransaction();

            try
            {
                await transaction.CommitAsync();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return ActionResult.Failed(ex.Message).AddError(ex.InnerException?.Message);
            }
            return ActionResult.Successful();
        }

        public ISession Session { get; set; }
    }
}
