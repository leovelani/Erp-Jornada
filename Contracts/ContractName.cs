using Flunt.Validations;

namespace Erp_Jornada.Contracts
{
    public class ContractName : Contract<string>
    {
        public ContractName(string name = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(name, nameof(name), "Nome é requirido")
                    .IsGreaterThan(name?.Length ?? 0, 3, nameof(name), "Nome deve ter ao menos 3 caracteres");
        }
    }
}
