using Classes;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : GenericController<Service>
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] string? categoryId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }

        var objectList = new Service[itemsPerPage];
        for (var i = 0; i < itemsPerPage; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Service>();

            if (locationId != null)
            {
                objectList[i].LocationId = locationId;
            }

            if (categoryId != null)
            {
                objectList[i].CategoryId = categoryId;
            }
        }

        return Ok(objectList);
    }

    /** <summary>Gets amount of orders for a service in a specified period, if a period isnt specified returns all available order count</summary>
     * <param name="startDate" example="">Period start date</param>
     * <param name="endDate" example="">Period end date</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetServiceStatistics([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var listSize = new Random().Next();

        var resultList = new List<ServiceStatisticDto>(listSize);

        for (int i = 0; i < listSize; i++)
        {
            resultList.Add(RandomGenerator.GenerateRandom<ServiceStatisticDto>());
        }

        return Ok(resultList);
    }
}
