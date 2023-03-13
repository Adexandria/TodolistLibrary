namespace TasksLibrary.Models
{
    public class AccessToken : BaseClass
    {
        public AccessToken(string token,UserId userId)
        {
            Token = token;
            UserId = userId;
        }
        public virtual string Token { get; set; }
        public virtual UserId UserId { get; set; }
    }
}
