namespace TasksLibrary.Extensions
{
    public class ActionResult<T> :ActionResult
    {
        public Data<T> Data { get; set; }

        
        public ActionResult<T> SuccessfulOperation( T data,string accessToken)
        {
            return new ActionResult<T>()
            {
                Data = new Data<T>(data),
                AccessToken = accessToken,
                StatusCode = 200,
                IsSuccessful = true,
                NotSuccessful = false,
                Errors = new List<string>()
                 
            };
        }
        public ActionResult<T> SuccessfulOperation(T data, string accessToken,string refreshToken)
        {
            return new ActionResult<T>()
            {
                Data = new Data<T>(data),
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                StatusCode = 200,
                IsSuccessful = true,
                NotSuccessful = false,
                Errors = new List<string>()

            };
        }

        public ActionResult<T> SuccessfulOperation(T data)
        {
            return new ActionResult<T>()
            {
                Data = new Data<T>(data),
                StatusCode = 200,
                IsSuccessful = true,
                NotSuccessful = false,
                Errors = new List<string>()

            };
        }

        public ActionResult<T> FailedOperation(string error, int statuscode)
        {
            return new ActionResult<T>()
            {
                StatusCode = statuscode,
                IsSuccessful = false,
                NotSuccessful = true,
                Errors = new List<string>()
                {
                    error
                }
            };
            
        }
        public ActionResult<T> FailedOperation(List<string> error, int statuscode)
        {
            return new ActionResult<T>()
            {
                StatusCode = statuscode,
                IsSuccessful = false,
                NotSuccessful = true,
                Errors = error
            };

        }

    }

    
    public class ActionResult
    {
        public virtual Data Data { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public bool NotSuccessful { get; set; } = false;

        public ActionResult Successful()
        {
            return new ActionResult()
            {
                Data = new Data(),
                StatusCode = 200,
                IsSuccessful = true,
                NotSuccessful = false,
                Errors = new List<string>()
            };
        }
        public ActionResult Failed(string error)
        {
            return new ActionResult()
            {
                Data = new Data(),
                StatusCode = 500,
                IsSuccessful = false,
                NotSuccessful = true,
                Errors = new List<string>()
                {
                    error
                }

            };
        }

    }

}