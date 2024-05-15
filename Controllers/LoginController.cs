using Erp_Jornada.Dtos.UsuarioDTO;
using Erp_Jornada.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp_Jornada.Controllers
{
    public class LoginController(UsuarioService usuarioService) : BaseApiController
    {
        private readonly UsuarioService _usuarioService = usuarioService;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO usuarioLoginDTO)
        {
            return ServiceResponse(await _usuarioService.Login(usuarioLoginDTO));
        }
    }
}
