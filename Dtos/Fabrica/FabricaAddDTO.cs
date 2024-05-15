using Erp_Jornada.Contracts;

namespace Erp_Jornada.Dtos.Fabrica
{
    public class FabricaAddDTO : BaseDTOValidation
    {
        public FabricaAddDTO(string nome, string cnpj, string email, string senha)
        {
            AddNotifications(new ContractEmail(email),
               new ContractPassword(senha),
               new ContractCnpj(cnpj),
               new ContractName(nome));

            Nome = nome;
            Cnpj = cnpj;
            Email = email;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
    }
}
