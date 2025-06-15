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
    [Route("api/[controller]")]
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

        //
        [HttpGet("company/{FkIdCompany}")]
        [SwaggerOperation(
       Summary = "Gets all stops by FkIdCompany",
       Description = "Gets a stop for a given company identifier",
       OperationId = "GetStopsByFkIdCompany")]
        [SwaggerResponse(200, "The stops were found", typeof(IEnumerable<StopResource>))] // Updated return type
        public async Task<ActionResult> GetStopsByFkIdCompany(string FkIdCompany)
        {
            var getAllStopsByFkIdCompanyQuery = new GetAllStopsByFkIdCompanyQuery(FkIdCompany); // Corrected query
            var result = await stopQueryService.Handle(getAllStopsByFkIdCompanyQuery);

            if (result == null) // This check might depend on how your service layer indicates "company not found" vs "company found but no stops"
            {
                return NotFound(); // Or Ok(Enumerable.Empty<StopResource>()) if an empty list is acceptable for a non-existent/empty company
            }

            var resources = result.Select(StopResourceFromEntityAssembler.ToResourceFromEntity); // Transform collection
            return Ok(resources);
        }


        //
        [HttpGet("locality/{FkIdLocality}")]
        [SwaggerOperation(
       Summary = "Gets all stops by FkIdLocality",
       Description = "Gets a stop for a given locality identifier",
       OperationId = "GetStopsByFkIdLocality")]
        [SwaggerResponse(200, "The stops were found", typeof(IEnumerable<StopResource>))] // Updated return type
        [SwaggerResponse(StatusCodes.Status200OK, "The stops were found", typeof(IEnumerable<StopResource>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No stops found for the locality or locality not found")] // Added 404 response

        public async Task<ActionResult> GetStopsByFkIdLocality(string FkIdLocality)
        {
            var getAllStopsByFkIdLocalityQuery = new GetAllStopsByFkIdLocalityQuery(FkIdLocality); // Corrected query
            var result = await stopQueryService.Handle(getAllStopsByFkIdLocalityQuery);

            if (result == null) // This check might depend on how your service layer indicates "company not found" vs "company found but no stops"
            {
                return NotFound(); // Or Ok(Enumerable.Empty<StopResource>()) if an empty list is acceptable for a non-existent/empty company
            }

            var resources = result.Select(StopResourceFromEntityAssembler.ToResourceFromEntity); // Transform collection
            return Ok(resources);
        }


        //    /// <summary>
        /// Gets a specific stop by its name and locality ID.
        /// </summary>
        /// <param name="FkIdLocality">The locality ID of the stop.</param>
        /// <param name="name">The name of the stop.</param>
        /// <returns>
        /// A response as an action result containing the stop, or not found if the stop was not found.
        /// </returns>
        [HttpGet("locality/{FkIdLocality}/name/{Name}")]
        [SwaggerOperation(
            Summary = "Gets a stop by locality ID and name",
            Description = "Gets a specific stop for a given locality ID and stop name",
            OperationId = "GetStopByLocalityAndName")]
        [SwaggerResponse(StatusCodes.Status200OK, "The stop was found", typeof(StopResource))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The stop was not found for the given locality and name")]
        public async Task<ActionResult> GetStopByNameAndFkIdLocality(string FkIdLocality, string Name)
        {
            // Assuming GetStopByNameAndFkIdLocalityQuery takes (string Name, string fkIdLocality)
            // Adjust if the constructor parameters are different.
            var getStopByNameAndLocalityQuery = new GetStopByNameAndFkIdLocalityQuery(Name, FkIdLocality);
            var result = await stopQueryService.Handle(getStopByNameAndLocalityQuery);

            if (result is null)
            {
                return NotFound();
            }

            var resource = StopResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

    }
}
