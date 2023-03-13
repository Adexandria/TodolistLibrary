namespace TasksLibrary.Models
{
    public class UserId
    {
        public UserId(Guid id)
        {
            Id = id;
        }
        public virtual Guid Id { get; set; }
    }
}