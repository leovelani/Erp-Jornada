using Flunt.Validations;

namespace Erp_Jornada.Contracts
{
    public class ContractPassword : Contract<string>
    {
        public ContractPassword(string password = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(password, nameof(password), "Senha inválida");
        }
    }
}
