using Erp_Jornada.Dtos;
using Erp_Jornada.Dtos.Fabrica;
using Erp_Jornada.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Erp_Jornada.Controllers
{
    public class FabricaController : BaseApiController
    {
        private readonly FabricaService _fabricaService;

        public FabricaController(FabricaService fabricaService)
        {
            _fabricaService = fabricaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? page, int? pageSize)
        {
            return ServiceResponse(await _fabricaService.GetList(page ?? Pagination.START_PAGE_DEFAULT, pageSize ?? Pagination.PAGE_SIZE_DEFAULT));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return ServiceResponse(await _fabricaService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(FabricaAddDTO FabricaDTO)
        {
            return ServiceResponse(await _fabricaService.Add(FabricaDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update(FabricaUpdateDTO model)
        {
            return ServiceResponse(await _fabricaService.Update(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ServiceResponse(await _fabricaService.Remove(id));
        }
    }
}
