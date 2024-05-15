using Flunt.Validations;

namespace Erp_Jornada.Contracts
{
    public class ContractEmail : Contract<string>
    {
        public ContractEmail(string emailAddress = "")
        {
            Requires()
                .IsEmail(emailAddress, nameof(emailAddress), "Email inválido");
        }
    }
}
