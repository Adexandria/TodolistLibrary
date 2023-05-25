using System.Text.RegularExpressions;

namespace TasksLibrary.Extensions
{

    public class RequestValidator
    {
        public RequestValidator()
        {
            Result = ActionResult.Successful();
        }
        public RequestValidator IsText(string s,string error)
        {
            AddCheck(!string.IsNullOrEmpty(s), error);
            return this;
        }

        public RequestValidator IsGuid( Guid s, string error)
        {
            Regex regex = new("(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
            AddCheck(regex.IsMatch(s.ToString()), error);
            return this;

        }
        public RequestValidator IsEmail(string s, string error)
        {
            Regex regex = new("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$");
            AddCheck(regex.IsMatch(s), error);
            return this;
        }

        private void AddCheck(bool operation,string error)
        {
            if(!operation && Result.IsSuccessful)
            {
                Result = ActionResult.Failed(error);
            }
            else if(!operation && Result.NotSuccessful)
            {
                Result.AddError(error);
            } 
        }

        public ActionResult Result;
    }
}
