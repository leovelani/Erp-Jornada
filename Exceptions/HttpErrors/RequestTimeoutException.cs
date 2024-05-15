namespace Erp_Jornada.Exceptions.HttpErrors
{
    public class RequestTimeoutException : Exception
    {
        public RequestTimeoutException(string message) : base(message)
        {

        }
    }
}
