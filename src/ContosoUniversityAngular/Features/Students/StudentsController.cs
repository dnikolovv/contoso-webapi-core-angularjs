namespace ContosoUniversityAngular.Features.Students
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/students")]
    public class StudentsController : Controller
    {
        public StudentsController(IMediator mediator)
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
        public async Task<Create.Response> Create([FromBody]Create.Command command)
        {
            var response = await _mediator.SendAsync(command);
            return response;
        }

        [HttpPost("edit")]
        public async Task<Edit.Response> Edit([FromBody]Edit.Command command)
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
