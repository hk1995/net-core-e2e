using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Features.Admin.Provisioning
{
    public class UsersController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Auth0UserViewModel model)
        {
            var msg = new ProvisionUser(model);
            var metaData = await _mediator.SendAsync(msg).ConfigureAwait(false);

            return Ok(metaData);
        }
    }
}
