using LogsMonitor.Application.Commands;
using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogsMonitor.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение проекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <response code="200">Подробная информация о проекте</response>
        /// <response code="400">Ошибка</response>
        /// <response code="404">Проект не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get(Guid id)
        {
            ProjectDetailsDTO projectDetailsDTO = await _mediator.Send(new GetProjectDetailsQuery() { ProjectId = id });

            return Ok(projectDetailsDTO);
        }

        /// <summary>
        /// Получение списка всех проектов
        /// </summary>
        /// <response code="200">Список всех проектов</response>
        /// <response code="400">Ошибка</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get()
        {
            IEnumerable<ProjectDTO> projectsDTOs = await _mediator.Send(new GetProjectsQuery());

            return Ok(projectsDTOs);
        }

        /// <summary>
        /// Создание проекта
        /// </summary>
        /// <param name="createProjectDTO">Проект</param>
        /// <response code="200">Проект создан</response>
        /// <response code="400">Ошибка</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create(CreateProjectDTO createProjectDTO)
        {
            await _mediator.Send(new CreateProjectCommand() { CreateProjectDTO = createProjectDTO });

            return Ok();
        }

        /// <summary>
        /// Обновление проекта
        /// </summary>
        /// <param name="updateProjectDTO">Проект</param>
        /// <response code="200">Проект обновлен</response>
        /// <response code="400">Ошибка</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateProjectDTO updateProjectDTO)
        {
            await _mediator.Send(new UpdateProjectCommand() { UpdateProjectDTO = updateProjectDTO });

            return Ok();
        }

        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <response code="200">Проект удален</response>
        /// <response code="400">Ошибка</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Remove(Guid id)
        {
            await _mediator.Send(new RemoveProjectCommand() { ProjectId = id });

            return Ok();
        }
    }
}
