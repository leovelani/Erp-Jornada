namespace Erp_Jornada.Exceptions.HttpErrors
{
    public class PaymentRequiredException : Exception
    {
        public PaymentRequiredException(string message) : base(message)
        {
        }
    }
}
