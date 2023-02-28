namespace TasksLibrary.Models
{
    public class AccessToken : BaseClass
    {
        public AccessToken(string token,UserId userId)
        {
            Token = token;
            UserId = userId;
        }
        public string Token { get; set; }
        public UserId UserId { get; set; }
    }
}
