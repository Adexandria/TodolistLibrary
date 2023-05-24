namespace TasksLibrary.Models
{
    public class RefreshToken : BaseClass
    {
        protected RefreshToken()
        {

        }
        public RefreshToken(string token, DateTime expires, UserId userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            IsRevoked = DateTime.UtcNow > Expires;
            
        }
        public virtual string Salt { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime Expires { get; set; }
        public virtual bool IsRevoked { get; set; }
        public virtual UserId UserId { get; set; }  
    }
}

