using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogsMonitor.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/logs")]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение лога по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор лога</param>
        /// <response code="200">Подробная информация о логе</response>
        /// <response code="400">Ошибка</response>
        /// <response code="404">Лог не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LogDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get(Guid id)
        {
            LogDetailsDTO logDetailsDTO = await _mediator.Send(new GetLogDetailsQuery() { LogId = id });

            return Ok(logDetailsDTO);
        }

        /// <summary>
        /// Получение списка логов проекта за дату
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="occurrenceDate">Дата записи логов</param>
        /// <response code="200">Список логов проекта за выбранную дату</response>
        /// <response code="400">Ошибка</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LogDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get(Guid projectId, DateTime occurrenceDate)
        {
            IEnumerable<LogDTO> logsDTOs = await _mediator.Send(new GetLogsQuery()
            {
                ProjectId = projectId,
                OccurrenceDate = occurrenceDate
            });

            return Ok(logsDTOs);
        }
    }
}
