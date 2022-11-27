using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using System.ComponentModel.DataAnnotations;
using PoSSapi.Controllers;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : GenericController<Shift>
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet()]
    public ActionResult GetAll([FromQuery] string? employeeId, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0)
        {
            return BadRequest("itemsPerPage must be greater than 0");
        }
        if (pageNum < 0)
        {
            return BadRequest("pageNum must be 0 or greater");
        }

        int totalItems = 20;
        int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

        var objectList = new Shift[itemsToDisplay];
        for (int i = 0; i < itemsToDisplay; ++i)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Shift>();

            if (employeeId != null)
            {
                objectList[i].EmployeeId = employeeId;
            }
        }

        ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }

    /** <summary>Check in for a shift</summary>
     * <param name="employeeId" example="">Id of the employee checking in for a shift</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/check-in")]
    public ActionResult CheckInShift(string employeeId)
    {
        return Ok();
    }

    /** <summary>Check out from a shift</summary>
     * <param name="employeeId" example="">Id of the employee checking out from a shift</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/check-out")]
    public ActionResult CheckOutShift(string employeeId)
    {
        return Ok();
    }
}
