﻿using Erp_Jornada.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Erp_Jornada.Dtos.UsuarioDTO
{
    public class LoginDTO : BaseDTOValidation
    {
        public LoginDTO(string email, string senha)
        {
            AddNotifications(new ContractEmail(email),
                new ContractPassword(senha));
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
