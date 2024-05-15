using Erp_Jornada.Contracts;

namespace Erp_Jornada.Dtos.UsuarioDTO
{
    public class UsuarioUpdateDTO : BaseDTOValidation
    {
        public UsuarioUpdateDTO(string nome, string email, string senha, string? role)
        {
            AddNotifications(new ContractEmail(email),
                new ContractPassword(senha),
                new ContractName(nome));
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Role { get; set; }
    }
}
