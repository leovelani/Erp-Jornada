using System.Data;

namespace Erp_Jornada.Model
{
    public class Usuario : Entidade
    {
        public string? Nome { get; set; }
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public Usuario(string nome, string email, string senha, string role)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
            Ativo = true;
        }
    }
}
