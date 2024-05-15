using Erp_Jornada.Tools;
using Flunt.Validations;

namespace Erp_Jornada.Contracts
{
    public class ContractCnpj : Contract<string>
    {
        public ContractCnpj(string Cnpj = "")
        {
            Cnpj = Tool.FormatCpnj(Cnpj);

            Requires()
                .IsNotNullOrWhiteSpace(Cnpj, nameof(Cnpj), "CNPJ é requirido")
                .IsGreaterThan(Cnpj.Length, 13, nameof(Cnpj), "CNPJ deve conter 14 dígitos")
                .IsLowerThan(Cnpj.Length, 15, nameof(Cnpj), "CNPJ deve conter 14 dígitos");

        }
    }
}
