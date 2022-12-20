using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using PoSSapi.Controllers;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : GenericController<Location>
{

    protected class ShiftReturnObject
    {
        public int totalItems { get; set; }
        public Shift[] itemList { get; set; }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Location[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet()]
    public ActionResult GetAll([FromQuery] string? name, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
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
        
        var objectList = new Location[itemsToDisplay];
        for (int i = 0; i < itemsToDisplay; ++i)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Location>();
            
            if (name != null)
            {
                objectList[i].Name = name;
            }
        }

        ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shift[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{id}/shift")]
    public ActionResult GetAllShifts(string id, [FromQuery] DateTime? startDate, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
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
            
            if (startDate != null)
            {
                objectList[i].StartDate = (DateTime)startDate;
            }
        }

        ShiftReturnObject returnObject = new ShiftReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/shift/{shiftId}")]
    public ActionResult<Shift> GetShift(string id, string shiftId)
    {
        return Ok(RandomGenerator.GenerateRandom<Shift>(id));
    }

    /*[ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{id}/shift")]
    public ActionResult PostShift(string id, [FromBody] Shift newObject)
    {
        return CreatedAtAction(nameof(Get), new { id = "1" }, newObject);
    }*/

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}/shift/{shiftId}")]
    public ActionResult PutShift(string id, string shiftId, [FromBody] Shift updatedObject)
    {
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}/shift/{shiftId}")]
    public ActionResult DeleteShift(string id)
    {
        return Ok();
    }
}
