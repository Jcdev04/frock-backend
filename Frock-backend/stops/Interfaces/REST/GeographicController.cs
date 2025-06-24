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
        //getRegionById
        [HttpGet("regions/{id}")]
        [SwaggerOperation(
                Summary = "Gets a region by id",
                Description = "Gets a region for a given identifier",
                OperationId = "GetRegionById")]
        [SwaggerResponse(StatusCodes.Status200OK, "The region", typeof(RegionResource))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Region not found")]
        public async Task<IActionResult> GetRegionById(string id) {
            var query = new GetRegionByIdQuery(id);
            var region = await regionQueryService.Handle(query);
            if (region == null) {
                return NotFound();
            }
            var regionResource = RegionResourceFromEntityAssembler.ToResourceFromEntity(region);
            return Ok(regionResource);
        }

        //getAllRegions
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
        //getProvinceById
        [HttpGet("provinces/{id}")]
        [SwaggerOperation(
               Summary = "Gets a province by id",
               Description = "Gets a province for a given identifier",
               OperationId = "GetProvinceById")]
        [SwaggerResponse(200, "The province", typeof(ProvinceResource))]
        [SwaggerResponse(404, "Province not found")]
        public async Task<IActionResult> GetProvinceById(string id) {
            var query = new GetProvinceByIdQuery(id);
            var province = await provinceQueryService.Handle(query);
            if (province == null) {
                return NotFound();
            }
            var provinceResource = ProvinceResourceFromEntityAssembler.ToResourceFromEntity(province);
            return Ok(provinceResource);
        }

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
               Summary = "Gets a province by regionid",
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
        //getDistrictById
        [HttpGet("districts/{id}")]
        [SwaggerOperation(
               Summary = "Gets a district by id",
               Description = "Gets a district for a given identifier",
               OperationId = "GetDistrictById")]
        [SwaggerResponse(200, "The district", typeof(DistrictResource))]
        [SwaggerResponse(404, "District not found")]
        public async Task<IActionResult> GetDistrictById(string id) {
            var query = new GetDistrictByIdQuery(id);
            var district = await districtQueryService.Handle(query);
            if (district == null) {
                return NotFound();
            }
            var districtResource = DistrictResourceFromEntityAssembler.ToResourceFromEntity(district);
            return Ok(districtResource);
        }


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
        //getLocalityById
        [HttpGet("localities/{id}")]
        [SwaggerOperation(
               Summary = "Gets a locality by id",
               Description = "Gets a locality for a given identifier",
               OperationId = "GetLocalityById")]
        [SwaggerResponse(200, "The locality", typeof(LocalityResource))]
        [SwaggerResponse(404, "Locality not found")]
        public async Task<IActionResult> GetLocalityById(string id) {
            var query = new GetLocalityByIdQuery(id);
            var locality = await localityQueryService.Handle(query);
            if (locality == null) {
                return NotFound();
            }
            var localityResource = LocalityResourceFromEntityAssembler.ToResourceFromEntity(locality);
            return Ok(localityResource);
        }


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

        //by district id
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
