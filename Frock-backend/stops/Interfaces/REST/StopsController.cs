using Frock_backend.stops.Domain.Model.Queries;
using Frock_backend.stops.Domain.Services;
using Frock_backend.stops.Interfaces.REST.Resources;
using Frock_backend.stops.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Frock_backend.stops.Interfaces.REST
{
    /// <summary>
    /// Stops controller.
    /// </summary>
    /// <param name="stopCommandService">The Stop Command Service</param>
    /// <param name="stopCommandServiceQueryService">The Stop Query Service</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Stops")]
    public class StopsController(IStopCommandService stopCommandService, IStopQueryService stopQueryService) : ControllerBase
    {
        /// <summary>
        /// Creates a new stop.
        /// </summary>
        /// <param name="resource">The CreateStopResource resource</param>
        /// <returns>
        /// A response as an action result containing the created stop, or bad request if the stop was not created.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new stop.",
            Description = "Creates a new stop with a given parameters",
            OperationId = "CreateStop"
            )]
        [SwaggerResponse(201, "The stop was created", typeof(StopResource))]
        [SwaggerResponse(400, "The stop was not created")]
        public async Task<ActionResult> CreateStop([FromBody] CreateStopResource resource)
        {
            var createStopCommand = CreateStopCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await stopCommandService.Handle(createStopCommand);
            if (result is null) return BadRequest();
            return CreatedAtAction(nameof(GetStopById), new { id = result.Id }, StopResourceFromEntityAssembler.ToResourceFromEntity(result));
        }

        /// <summary>
        /// Gets a stop by ID.
        /// </summary>
        /// <param name="id">The ID of the stop</param>
        /// <returns>
        /// A response as an action result containing the stop, or not found if the stop was not found.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
               Summary = "Gets a stop by id",
               Description = "Gets a stop for a given stop identifier",
               OperationId = "GetStopById")]
        [SwaggerResponse(200, "The stop was found", typeof(StopResource))]
        public async Task<ActionResult> GetStopById(int id)
        {
            var getStopByIdQuery = new GetStopByIdQuery(id);
            var result = await stopQueryService.Handle(getStopByIdQuery);
            if (result is null) return NotFound();
            var resource = StopResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }
    }
}
