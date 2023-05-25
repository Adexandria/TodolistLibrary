

namespace TasksLibrary.Models
{
    public class Note: BaseClass
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

        public virtual void SetUser(User user)
        {
            User = user;
        }

        public virtual string Task { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual User User { get; set; }
    }
}
