namespace Catalog.API.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json", "application/problem+json")]
    public class BaseController : ControllerBase
    {
    }
}