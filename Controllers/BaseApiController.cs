using Erp_Jornada.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Erp_Jornada.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        public IActionResult ServiceResponse<T>(ResultModel<T> response)
        {

            return StatusCode(response.Status, response);
        }
    }
}
