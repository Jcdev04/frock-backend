using Frock_backend.stops.Domain.Model.Commands.Geographic;
using Frock_backend.stops.Domain.Model.Queries.Geographic;
using Frock_backend.stops.Domain.Services.Geographic;
using Frock_backend.stops.Interfaces.REST.Resources;
using Frock_backend.stops.Interfaces.REST.Resources.Geographic;
using Frock_backend.stops.Interfaces.REST.Transform.Geographic;

using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Frock_backend.stops.Interfaces.REST
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Geographic")]
    public class GeographicController(
        IRegionCommandService regionCommandService, IRegionQueryService regionQueryService,
        IProvinceCommandService provinceCommandService, IProvinceQueryService provinceQueryService,
        IDistrictCommandService districtCommandService, IDistrictQueryService districtQueryService,
        ILocalityCommandService localityCommandService, ILocalityQueryService localityQueryService
        ) : ControllerBase

    {
        // Endpoints para Regions
        [HttpGet("regions")]
        [SwaggerOperation(
        Summary = "Get all regions",
        Description = "Get all regions",
        OperationId = "GetAllRegions")]
        [SwaggerResponse(StatusCodes.Status200OK, "The list of regions", typeof(IEnumerable<RegionResource>))]
        public async Task<IActionResult> GetAllRegions() {
            var regions = await regionQueryService.Handle(new GetAllRegionsQuery());
            var regionResources = regions.Select(RegionResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(regionResources);
        }

        // Endpoints para Provinces

        //getAllprovinces
        [HttpGet("provinces")]
        [SwaggerOperation(
               Summary = "Gets all provinces",
               Description = "Gets all provinces",
               OperationId = "GetAllProvinces")]
        [SwaggerResponse(200, "The list of provinces", typeof(IEnumerable<ProvinceResource>))]
        public async Task<IActionResult> GetAllProvinces() {
            var provinces = await provinceQueryService.Handle(new GetAllProvincesQuery());
            var provinceResources = provinces.Select(ProvinceResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(provinceResources);
        }


        [HttpGet("provinces/region/{regionId}")]
        [SwaggerOperation(
               Summary = "Gets a province by id",
               Description = "Gets a province for a given region identifier",
               OperationId = "GetProvincesByFkIdRegion")]
        [SwaggerResponse(200, "The list of provinces", typeof(IEnumerable<RegionResource>))]
        public async Task<IActionResult> GetProvincesByFkIdRegion(string regionId) {

            var query = new GetProvincesByFkIdRegionQuery(regionId);
            var provinces = await provinceQueryService.Handle(query);

            if (provinces == null) //
            {
                return NotFound();
            }

            var provinceResources = provinces.Select(ProvinceResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(provinceResources);
        }

        // Endpoints para Districts
        //getAlldistricts
        [HttpGet("districts")]
        [SwaggerOperation(
               Summary = "Gets all districts",
               Description = "Gets all districts",
               OperationId = "GetAllDistricts")]
        [SwaggerResponse(200, "The list of districts", typeof(IEnumerable<DistrictResource>))]
        public async Task<IActionResult> GetAllDistricts() {
            var districts = await districtQueryService.Handle(new GetAllDistrictsQuery());
            var districtResources = districts.Select(DistrictResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(districtResources);
        }

        [HttpGet("districts/province/{provinceId}")]
        [SwaggerOperation(
               Summary = "Gets districts by province id",
               Description = "Gets all districts for a given province identifier",
               OperationId = "GetDistrictsByFkIdProvince")]
        public async Task<ActionResult> GetDistrictsByFkIdProvince(string provinceId) {
            var query = new GetDistrictsByFkIdProvinceQuery(provinceId);
            var districts = await districtQueryService.Handle(query);
            if (districts == null || !districts.Any())
            {
                return NotFound();
            }
            var districtResources = districts.Select(DistrictResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(districtResources);
        }

        // Endpoints para Localities
        //getAllLocalities
        [HttpGet("localities")]
        [SwaggerOperation(
               Summary = "Gets all localities",
               Description = "Gets all localities",
               OperationId = "GetAllLocalities")]
        [SwaggerResponse(200, "The list of localities", typeof(IEnumerable<LocalityResource>))]
        public async Task<IActionResult> GetAllLocalities() {
            var localities = await localityQueryService.Handle(new GetAllLocalitiesQuery());
            var localityResources = localities.Select(LocalityResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(localityResources);
        }


        [HttpGet("localities/district/{districtId}")]
        [SwaggerOperation(
               Summary = "Gets localities by district id",
               Description = "Gets all localities for a given district identifier",
               OperationId = "GetLocalitiesByFkIdDistrict")]
        public async Task<ActionResult> GetLocalitiesByFkIdDistrict(string districtId) {
            var query = new GetLocalitiesByFkIdDistrictQuery(districtId);
            var localities = await localityQueryService.Handle(query);
            if (localities == null || !localities.Any())
            {
                return NotFound();
            }
            var localityResources = localities.Select(LocalityResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(localityResources);
        }        
    }
}
