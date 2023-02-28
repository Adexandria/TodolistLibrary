namespace TasksLibrary.Models
{
    public class RefreshToken : BaseClass
    {

        public RefreshToken(string token, DateTime expires, UserId userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            IsRevoked = DateTime.UtcNow > Expires;
            
        }
        public string Salt { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public UserId UserId { get; set; }  
    }
}

