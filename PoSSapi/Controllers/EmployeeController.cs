using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : GenericController<Employee>
    {
        protected class ShiftReturnObject
        {
            public int totalItems { get; set; }
            public Shift[] itemList { get; set; }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? username, [FromQuery] bool? isManager, [FromQuery] string? locationId, [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalItems;
            if (username != null) {
                totalItems = 1;
            }
            else {
                totalItems = 20;
            }

            int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

            var objectList = new Employee[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Employee>();

                if (username != null) {
                    objectList[i].Username = username;
                }
                if (isManager != null) {
                    objectList[i].IsManager = (bool)isManager;
                }
                if (locationId != null) {
                    objectList[i].LocationId = locationId;
                }
            }

            ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
            return Ok(returnObject);
        }

        /** <summary>Check in for a shift</summary>
         * <param name="employeeId" example="">Id of the employee checking in for a shift</param>
         */
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{id}/shift/check-in")]
        public ActionResult CheckInShift(string employeeId)
        {
            return Ok();
        }

        /** <summary>Check out from a shift</summary>
             * <param name="employeeId" example="">Id of the employee checking out from a shift</param>
             */
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{id}/shift/check-out")]
        public ActionResult CheckOutShift(string employeeId)
        {
            return Ok();
        }
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shift[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}/shift")]
        public ActionResult GetShifts(string employeeId, [FromQuery] DateTime? startDate, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
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
                objectList[i].EmployeeId = employeeId;

                if (startDate != null)
                {
                    objectList[i].StartDate = (DateTime)startDate;
                }
            }

            ShiftReturnObject returnObject = new ShiftReturnObject { totalItems = totalItems, itemList = objectList };
            return Ok(returnObject);
        }
    }
}
