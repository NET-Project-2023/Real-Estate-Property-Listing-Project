using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBuyTicket.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender mediator = null!;

        protected ISender Mediator => mediator
            ??= HttpContext.RequestServices
            .GetRequiredService<ISender>();
    }
}
