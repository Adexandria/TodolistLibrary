using System.Text.RegularExpressions;


namespace TasksLibrary.Extensions
{

    public static class RequestValidator
    {
        public static ActionResult IsText(this ActionResult  actionResult,string s,string error)
        {
            if (string.IsNullOrEmpty(s))
            {
                return actionResult.Failed(error);
            }
            return actionResult.Successful();
        }

        public static ActionResult IsGuid(this ActionResult actionResult, Guid s, string error)
        {
            Regex regex = new("(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
            if (regex.IsMatch(s.ToString()))
            {
                return actionResult.Successful();
            }
            return actionResult.Failed(error);

        }
        public static ActionResult IsEmail(this ActionResult actionResult, string s, string error)
        {
            Regex regex = new("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$");
            if (regex.IsMatch(s))
            {
                return actionResult.Successful();
            }
            return actionResult.Failed(error);

        }
    }
}
