
namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json", "application/problem+json")]
    public class BaseController : ControllerBase
    {
    }
}