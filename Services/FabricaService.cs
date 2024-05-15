using AutoMapper;
using Erp_Jornada.Dtos;
using Erp_Jornada.Dtos.Fabrica;
using Erp_Jornada.Dtos.UsuarioDTO;
using Erp_Jornada.Model;
using Erp_Jornada.Repository;
using Erp_Jornada.Tools;
using Erp_Jornada.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace Erp_Jornada.Services
{
    public class FabricaService
    {
        private readonly FabricaRepository _fabricaRepository;
        private readonly UsuarioService _usuarioService;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public FabricaService(FabricaRepository fabricaRepository, UsuarioService usuarioService, IMapper mapper, UsuarioRepository usuarioRepository)
        {
            _fabricaRepository = fabricaRepository;
            _usuarioService = usuarioService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<dynamic>> Add(FabricaAddDTO fabricaDTO)
        {
            var fabrica = new Fabrica(
                fabricaDTO.Nome,
                fabricaDTO.Email,
                fabricaDTO.Cnpj,
                fabricaDTO.Telefone,
                fabricaDTO.Celular,
                BCrypt.Net.BCrypt.HashPassword(fabricaDTO.Senha));

            if (await _fabricaRepository.AlredyExist(fabricaDTO.Email))
                return new(HttpStatusCode.Conflict, "Email já cadastrado");

            await _fabricaRepository.Create(fabrica);

            var register = new RegisterDto(fabrica.Nome, fabrica.Email, fabrica.Senha, "Fabrica");

            await _usuarioService.Add(register);

            return new();
        }

        public async Task<ResultModel<FabricaDTO>> GetById(int id)
        {
            var fabrica = await _fabricaRepository.GetById(id);

            if (fabrica == null)
                return new(HttpStatusCode.NotFound, "Fabrica não encontrada");

            return new(_mapper.Map<FabricaDTO>(fabrica));
        }

        public async Task<ResultModel<IList<FabricaListDTO>>> GetList(int pageNumber, int pageSize)
        {
            var fabricas = await _fabricaRepository.GetItens();

            if (fabricas.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhuma fabrica foi encontrada");

            return new(_mapper.Map<IList<FabricaListDTO>>(fabricas));
        }

        public async Task<ResultModel<dynamic>> Remove(int id)
        {
            var fabrica = await _fabricaRepository.GetById(id);

            if (fabrica == null)
                return new(HttpStatusCode.NotFound, "Fabrica não encontrada");

            var usuario = await _usuarioRepository.GetByEmail(fabrica.Email);

            await _fabricaRepository.Remove(fabrica);
            await _usuarioRepository.Remove(usuario);

            return new();
        }

        public async Task<ResultModel<dynamic>> Update(FabricaUpdateDTO model)
        {
            var fabrica = await _fabricaRepository.GetById(model.Id);

            if (fabrica == null)
                return new(HttpStatusCode.NotFound, "Fabrica não encontrada");

            var usuario = await _usuarioRepository.GetByEmail(fabrica.Email);

            fabrica.Nome = model.Nome;
            fabrica.Cnpj = model.Cnpj;
            fabrica.Email = model.Email;
            fabrica.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);
            fabrica.Telefone = model.Telefone;
            fabrica.Celular = model.Celular;
            usuario.Email = model.Email;
            usuario.Nome = model.Nome;
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);

            await _fabricaRepository.Update(fabrica);
            await _usuarioRepository.Update(usuario);

            return new();
        }
    }
}
