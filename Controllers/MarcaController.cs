using Erp_Jornada.Dtos;
using Erp_Jornada.Dtos.Marca;
using Erp_Jornada.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp_Jornada.Controllers
{
    public class MarcaController(MarcaService marcaService) : BaseApiController
    {
        private readonly MarcaService _marcaService = marcaService;

        [HttpGet]
        public async Task<IActionResult> GetList(int? page, int? pageSize)
        {
            return ServiceResponse(await _marcaService.GetList(page ?? Pagination.START_PAGE_DEFAULT, pageSize ?? Pagination.PAGE_SIZE_DEFAULT));


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return ServiceResponse(await _marcaService.GetById(id));

        }

        [HttpPost]
        public async Task<IActionResult> Create(MarcaAddDTO MarcaDTO)
        {
            return ServiceResponse(await _marcaService.Add(MarcaDTO));




        }

        [HttpPut]
        public async Task<IActionResult> Update(MarcaUpdateDTO model)
        {


            return ServiceResponse(await _marcaService.Update(model));

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return ServiceResponse(await _marcaService.Remove(id));

        }
    }
}
