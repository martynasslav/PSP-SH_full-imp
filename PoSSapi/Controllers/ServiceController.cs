using Classes;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : GenericController<Service>
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] string? categoryId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }

        int totalItems = 20;
        int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

        var objectList = new Service[itemsToDisplay];
        for (var i = 0; i < itemsToDisplay; i++)
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

        ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }

    /** <summary>Gets amount of orders for a service in a specified period, if a period isnt specified returns all available order count</summary>
     * <param name="id" example="">Id of the service</param>
     * <param name="startDate" example="">Period start date</param>
     * <param name="endDate" example="">Period end date</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orders")]
    public ActionResult GetServiceStatistics(string id, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var listSize = new Random().Next(1, 10);

        var resultList = new List<ServiceStatisticDto>(listSize);

        for (int i = 0; i < listSize; i++)
        {
            var randomStatistic = RandomGenerator.GenerateRandom<ServiceStatisticDto>();
            randomStatistic.ServiceId = id;
            resultList.Add(randomStatistic);
        }

        return Ok(resultList);
    }
}
