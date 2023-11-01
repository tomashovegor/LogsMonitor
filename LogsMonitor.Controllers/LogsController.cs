using LogsMonitor.Application.DTOs;
using LogsMonitor.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogsMonitor.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly Mediator _mediator;

        public LogsController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            LogDetailsDTO logDetailsDTO = await _mediator.Send(new GetLogDetailsQuery() { LogId = id });

            return Ok(logDetailsDTO);
        }

        [HttpGet]
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
