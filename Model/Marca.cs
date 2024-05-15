namespace Erp_Jornada.Model
{
    public class Marca : Entidade
    {
        public Marca(string? nome, string? email, string? cnpj,string? telefone, string? celular, string? senha)
        {
            Nome = nome;
            Senha = senha;
            Email = email;
            Cnpj = cnpj;
            Telefone = telefone;
            Celular = celular;
            Ativo = true;
        }

        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
    }
}
