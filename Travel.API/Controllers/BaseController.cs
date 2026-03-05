using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Travel.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
