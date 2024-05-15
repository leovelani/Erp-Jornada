using AutoMapper;
using Erp_Jornada.Dtos.Marca;
using Erp_Jornada.Dtos.UsuarioDTO;
using Erp_Jornada.Model;
using Erp_Jornada.Repository;
using Erp_Jornada.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.RegularExpressions;

namespace Erp_Jornada.Services
{
    public class UsuarioService(UsuarioRepository usuarioRepository, IMapper mapper)
    {
        private readonly UsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ResultModel<dynamic>> Add(RegisterDto usuarioDTO)
        {
            var usuario = new Usuario(
                new(usuarioDTO.Nome), (usuarioDTO.Email),
                 BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha), (usuarioDTO.Role));

            if (await _usuarioRepository.AlredyExist(usuarioDTO.Email))
                return new(HttpStatusCode.Conflict, "Email já cadastrado");

            await _usuarioRepository.Create(usuario);

            return new();

        }

        public async Task<ResultModel<dynamic>> Login(LoginDTO usuarioDTO)
        {
            Usuario usuario = await _usuarioRepository.GetByEmail(usuarioDTO.Email);

            if (usuario?.Senha != null && BCrypt.Net.BCrypt.Verify(usuarioDTO.Senha, usuario.Senha))
                return new ResultModel<dynamic>(new
                {
                    token = TokenService.GenerateToken(usuario)
                });

            return new(HttpStatusCode.BadRequest, "Email ou senha inválida");
        }

        public async Task<ResultModel<UsuarioDTO>> GetById(int id)
        {
            Usuario usuario = await _usuarioRepository.GetById(id);

            if(usuario == null)
                return new(HttpStatusCode.NotFound, "Usuario não encontrado");

            return new(_mapper.Map<UsuarioDTO>(usuario));
        }


        public async Task<ResultModel<dynamic>> Update(UsuarioUpdateDTO model)
        {

            var usuario = await _usuarioRepository.GetById(model.Id);

            if (usuario == null)
                return new(HttpStatusCode.NotFound, "Usuario não encontrada");


            usuario.Email = model.Email;
            usuario.Nome = model.Nome;
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);

            await _usuarioRepository.Update(usuario);

            return new();

        }
    }
}
