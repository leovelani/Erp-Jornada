using Erp_Jornada.Dtos.UsuarioDTO;
using Erp_Jornada.Model;
using Erp_Jornada.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Erp_Jornada.Controllers
{
    public class UsuarioController(UsuarioService usuarioService, IHttpContextAccessor httpContextAccessor) : BaseApiController
    {
        private readonly UsuarioService _usuarioService = usuarioService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(RegisterDto usuario)
        {
            return ServiceResponse(await _usuarioService.Add(usuario));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return ServiceResponse(await _usuarioService.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UsuarioUpdateDTO usuario)
        {
            return ServiceResponse(await _usuarioService.Update(usuario));
        }
    }
}
