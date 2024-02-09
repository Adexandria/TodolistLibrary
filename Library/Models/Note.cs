

namespace TasksLibrary.Models
{
    public class Note: BaseClass,INote
    {
        protected Note()
        {

        }

        public Note(string task)
        {
            Task = task;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public virtual void SetDescription(string description)
        {
            Description = description;
        }

        public virtual void SetUser(IUser user)
        {
            user.Notes.Add(this);
            user.Id = UserId.Id;
        }

        public virtual string Task { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual UserId UserId { get; set; }
    }
}
