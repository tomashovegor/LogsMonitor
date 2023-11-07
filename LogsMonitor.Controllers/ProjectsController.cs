using LogsMonitor.Application.Commands;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogsMonitor.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            ProjectDetailsDTO projectDetailsDTO = await _mediator.Send(new GetProjectDetailsQuery() { ProjectId = id });

            return Ok(projectDetailsDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<ProjectDTO> projectsDTOs = await _mediator.Send(new GetProjectsQuery());

            return Ok(projectsDTOs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProjectDTO createProjectDTO)
        {
            await _mediator.Send(new CreateProjectCommand() { CreateProjectDTO = createProjectDTO });

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProjectDTO updateProjectDTO)
        {
            await _mediator.Send(new UpdateProjectCommand() { UpdateProjectDTO = updateProjectDTO });

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove(Guid id)
        {
            await _mediator.Send(new RemoveProjectCommand() { ProjectId = id });

            return Ok();
        }
    }
}
