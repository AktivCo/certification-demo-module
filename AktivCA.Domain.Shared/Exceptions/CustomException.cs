
namespace AktivCA.Domain.Shared.Exceptions
{
    public class CustomException : Exception
    {
        public string Description { get; set; }
        public string LogMessage { get; set; }


        public CustomException(string desc, string logMessage = null)
        {
            Description = desc;
            LogMessage = logMessage;
        }
    }
}
