namespace ContosoUniversityAngular.Features.Instructors
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/instructors")]
    public class InstructorsController : Controller
    {
        public InstructorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpGet("all")]
        public async Task<Index.Response> Index([FromQuery]Index.Query query)
        {
            var response = await _mediator.SendAsync(query);
            return response;
        }

        [HttpGet("details/{id:int}")]
        public async Task<Details.Response> Details([FromRoute]Details.Query query)
        {
            var response = await _mediator.SendAsync(query);
            return response;
        }

        [HttpPost("create")]
        public async Task<CreateEdit.Response> Create([FromBody]CreateEdit.Command command)
        {
            var response = await _mediator.SendAsync(command);
            return response;
        }

        [HttpPost("edit")]
        public async Task<CreateEdit.Response> Edit([FromBody]CreateEdit.Command command)
        {
            var response = await _mediator.SendAsync(command);
            return response;
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<StatusCodeResult> Delete([FromRoute]Delete.Command command)
        {
            var response = await _mediator.SendAsync(command);
            return NoContent();
        }
    }
}
