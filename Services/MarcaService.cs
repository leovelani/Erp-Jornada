using AutoMapper;
using Erp_Jornada.Dtos;
using Erp_Jornada.Dtos.Marca;
using Erp_Jornada.Dtos.UsuarioDTO;
using Erp_Jornada.Model;
using Erp_Jornada.Repository;
using Erp_Jornada.Tools;
using Erp_Jornada.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Erp_Jornada.Services
{
    public class MarcaService(MarcaRepository marcaRepository, UsuarioService usuarioService, IMapper mapper, UsuarioRepository usuarioRepository)
    {
        private readonly MarcaRepository _marcaRepository = marcaRepository;
        private readonly UsuarioService _usuarioService = usuarioService;
        private readonly UsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ResultModel<dynamic>> Add(MarcaAddDTO marcaDTO)
        {
            var marca = new Marca(
                new(marcaDTO.Nome),
                (marcaDTO.Email),
                (marcaDTO.Cnpj),
                (marcaDTO.Telefone),
                (marcaDTO.Celular),
                BCrypt.Net.BCrypt.HashPassword(marcaDTO.Senha));


            if (await _marcaRepository.AlredyExist(marcaDTO.Email))
                return new(HttpStatusCode.Conflict, "Email já cadastrado");

            await _marcaRepository.Create(marca);

            RegisterDto register = new RegisterDto(marca.Nome, marca.Email, marca.Senha, "Marca");

            await _usuarioService.Add(register);

            return new();

        }

        public async Task<ResultModel<MarcaDTO>> GetById(int id)
        {
            Marca marca = await _marcaRepository.GetById(id);

            if (marca == null)
                return new(HttpStatusCode.NotFound, "Marca não encontrada");

            return new(_mapper.Map<MarcaDTO>(marca));
        }

        public async Task<ResultModel<IList<MarcaListDTO>>> GetList(int pageNumber, int pageSize)
        {
            var marcas = await _marcaRepository.GetItens();

            if (marcas.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhuma marca foi encontrada");

            return new(_mapper.Map<IList<MarcaListDTO>>(marcas));
        }

        public async Task<ResultModel<dynamic>> Remove(int id)
        {
            Marca marca = await _marcaRepository.GetById(id);

            if (marca == null)
                return new(HttpStatusCode.NotFound, "Marca não encontrada");

            Usuario usuario = await _usuarioRepository.GetByEmail(marca.Email);

            await _marcaRepository.Remove(marca);
            await _usuarioRepository.Remove(usuario);

            return new();

        }

        public async Task<ResultModel<dynamic>> Update(MarcaUpdateDTO model)
        {

            var marca = await _marcaRepository.GetById(model.Id);

            if (marca == null)
                return new(HttpStatusCode.NotFound, "Marca não encontrada");

            Usuario usuario = await _usuarioRepository.GetByEmail(marca.Email);

            marca.Nome = model.Nome;
            marca.Cnpj = model.Cnpj;
            marca.Email = model.Email;
            marca.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);
            marca.Telefone = model.Telefone;
            marca.Celular = model.Celular;
            usuario.Email = model.Email;
            usuario.Nome = model.Nome;
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);

            await _marcaRepository.Update(marca);
            await _usuarioRepository.Update(usuario);

            return new();

        }

      
    }
}
