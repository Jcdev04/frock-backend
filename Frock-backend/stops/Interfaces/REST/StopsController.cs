using Frock_backend.stops.Domain.Model.Commands;
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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all stops",
            Description = "Gets all stops in the system",
            OperationId = "GetAllStops")]
        [SwaggerResponse(200, "The stops were found", typeof(IEnumerable<StopResource>))] // Updated return type
        [SwaggerResponse(StatusCodes.Status404NotFound, "No stops found")] // Added 404 response

        public async Task<IActionResult> GetAllStops()
        {
            var getAllStopsQuery = new GetAllStopsQuery();
            var result = await stopQueryService.Handle(getAllStopsQuery);
            if (result == null || !result.Any()) // Check for null or empty collection
            {
                return NotFound(); // Or Ok(Enumerable.Empty<StopResource>()) if an empty list is acceptable
            }
            var resources = result.Select(StopResourceFromEntityAssembler.ToResourceFromEntity); // Transform collection
            return Ok(resources);
        }

        //
        [HttpGet("company/{FkIdCompany}")]
        [SwaggerOperation(
       Summary = "Gets all stops by FkIdCompany",
       Description = "Gets a stop for a given company identifier",
       OperationId = "GetStopsByFkIdCompany")]
        [SwaggerResponse(200, "The stops were found", typeof(IEnumerable<StopResource>))] // Updated return type
        public async Task<ActionResult> GetStopsByFkIdCompany(int FkIdCompany)
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


        /// <summary>
        /// Deletes a stop by its ID.
        /// </summary>
        /// <param name="id">The ID of the stop to delete.</param>
        /// <returns>
        /// 204 No Content if the stop was successfully deleted.
        /// 404 Not Found if the stop was not found.
        /// </returns>
        [HttpDelete("{id}")] // Changed to take ID from route
        [SwaggerOperation(
            Summary = "Deletes a stop by ID",
            Description = "Deletes a stop for a given stop identifier",
            OperationId = "DeleteStop")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The stop was successfully deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The stop was not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request (e.g., malformed ID)")] // Optional, for completeness
        public async Task<IActionResult> DeleteStop(int id) // Changed parameter to take ID from route
        {
            // The DeleteStopResource is no longer needed if ID comes from the route.
            // If DeleteStopCommand only needs the ID, you can create it directly.
            var deleteStopCommand = new DeleteStopCommand(id);
            var deletedStop = await stopCommandService.Handle(deleteStopCommand);

            if (deletedStop is null)
            {
                return NotFound(); // Return 404 if the service indicates the stop was not found
            }

            return NoContent(); // Return 204 No Content for successful deletion
        }


        /// <summary>
        /// Updates an existing stop.
        /// </summary>
        /// <param name="id">The ID of the stop to update.</param>
        /// <param name="resource">The UpdateStopResource containing the updated data.</param>
        /// <returns>
        /// 200 OK with the updated stop resource if successful.
        /// 400 Bad Request if the resource ID in the URL does not match the ID in the body, or if the update fails due to validation.
        /// 404 Not Found if the stop with the given ID was not found.
        /// </returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updates an existing stop by ID.",
            Description = "Updates an existing stop with the provided data. The ID in the URL must match the ID in the request body.",
            OperationId = "UpdateStop")]
        [SwaggerResponse(StatusCodes.Status200OK, "The stop was successfully updated.", typeof(StopResource))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data (e.g., ID mismatch or validation error).")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The stop with the specified ID was not found.")]
        public async Task<IActionResult> UpdateStop(int id, [FromBody] UpdateStopResource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest("ID in URL must match ID in request body.");
            }

            var updateStopCommand = UpdateStopCommandFromResourceAssembler.ToCommandFromResource(resource);
            var updatedStop = await stopCommandService.Handle(updateStopCommand);

            if (updatedStop is null)
            {
                // This could mean "not found" or "failed to update due to other reasons"
                // based on your StopCommandService.Handle(UpdateStopCommand) implementation.
                // Assuming service returns null if not found, or if update fails for other validation reasons not caught by model state.
                // For a more specific 404, your service would need to differentiate "not found" from "other update failure".
                // If your service throws for "not found", this check might not be hit for that case.
                // Let's assume for now that null from service after an update attempt means either not found or a general update failure.
                // A more robust service might return a result object indicating status.
                // For now, we'll try to infer. A common pattern is that if the service's FindByIdAsync (called internally) returns null, then the entity wasn't found.

                // To provide a more accurate 404, we might need to query first, or the service needs to be more explicit.
                // Given the current service signature, if updatedStop is null, it could be "not found" or "bad request".
                // Let's assume the service returns null if the entity to update wasn't found.
                var existingStop = await stopQueryService.Handle(new GetStopByIdQuery(id));
                if (existingStop == null)
                {
                    return NotFound($"Stop with ID {id} not found.");
                }
                // If it exists but update still failed and returned null
                return BadRequest("Could not update the stop with the provided parameters.");
            }

            var stopResource = StopResourceFromEntityAssembler.ToResourceFromEntity(updatedStop);
            return Ok(stopResource);
        }

    }
}
