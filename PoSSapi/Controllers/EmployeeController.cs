using Microsoft.AspNetCore.Mvc;
using PoSSapi.Classes;
using PoSSapi.Dtos;
using PoSSapi.Repositories;

namespace PoSSapi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IShiftRepository _shiftRepository;
		private readonly ILocationRepository _locationRepository;

		public EmployeeController(IEmployeeRepository employeeRepository, IShiftRepository shiftRepository, ILocationRepository locationRepository)
		{
			_employeeRepository = employeeRepository;
			_shiftRepository = shiftRepository;
			_locationRepository = locationRepository;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet()]
		public ActionResult GetAll([FromQuery] string? username, [FromQuery] bool? isManager, [FromQuery] string? locationId, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
		{
			if (itemsPerPage <= 0)
				return BadRequest("itemsPerPage must be greater than 0");

			if (pageNum < 0)
				return BadRequest("pageNum must be 0 or greater");

			var employees = _employeeRepository.GetEmployees();

			if (username != null)
				employees = employees.Where(e => e.Username == username);

			if (isManager != null)
				employees = employees.Where(e => e.IsManager == isManager);

			if (locationId != null)
				employees = employees.Where(e => e.LocationId == locationId);

			employees = employees.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

			return Ok(employees);
		}

		/// <summary>Check in for a shift</summary>
		/// <param name="id" example="">Id of the employee checking in for a shift</param>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPost("{id}/shift/check-in")]
		public ActionResult CheckInShift(string id)
		{
			var employee = _employeeRepository.GetEmployeeById(id);
			if (employee == null)
				return NotFound();

			var shifts = _shiftRepository.GetShifts().Where(s => s.EmployeeId == id);
			shifts = shifts.Where(s => s.CheckInDate == null);

			if (!shifts.Any())
				return NotFound();

			var now = DateTime.Now;
			var shiftList = shifts.ToList();
			var bestIdx = 0;
			var diff = now - shiftList[bestIdx].StartDate;

			for (int i = 1; i < shiftList.Count; ++i)
			{
				var tempDiff = now - shiftList[i].StartDate;
				if (diff > tempDiff)
				{
					bestIdx = i;
					diff = tempDiff;
				}
			}

			shiftList[bestIdx].CheckInDate = now;
			_shiftRepository.Save();

			return Ok();
		}

		/// <summary>Check out from a shift</summary>
		/// <param name="id" example="">Id of the employee checking out from a shift</param>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPost("{id}/shift/check-out")]
		public ActionResult CheckOutShift(string id)
		{
			var employee = _employeeRepository.GetEmployeeById(id);
			if (employee == null)
				return NotFound();

			var shifts = _shiftRepository.GetShifts().Where(s => s.EmployeeId == id);
			shifts = shifts.Where(s => s.CheckInDate != null && s.CheckOutDate == null);

			if (!shifts.Any())
				return NotFound();

			var now = DateTime.Now;
			var shiftList = shifts.ToList();
			var bestIdx = 0;
			var diff = now - shiftList[bestIdx].FinishDate;

			for (int i = 1; i < shiftList.Count; ++i)
			{
				var tempDiff = now - shiftList[i].FinishDate;
				if (diff > tempDiff)
				{
					bestIdx = i;
					diff = tempDiff;
				}
			}

			shiftList[bestIdx].CheckInDate = now;
			_shiftRepository.Save();

			return Ok();
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shift[]))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("{id}/shift")]
		public ActionResult GetShifts(string id, [FromQuery] DateTime? startDate, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
		{
			if (itemsPerPage <= 0)
				return BadRequest("itemsPerPage must be greater than 0");

			if (pageNum < 0)
				return BadRequest("pageNum must be 0 or greater");

			var employee = _employeeRepository.GetEmployeeById(id);
			if (employee == null)
				return NotFound();

			var shifts = _shiftRepository.GetShifts().Where(s => s.EmployeeId == id);

			if (startDate != null)
				shifts = shifts.Where(s => s.StartDate == startDate);

			shifts = shifts.Skip(pageNum * itemsPerPage).Take(itemsPerPage);
			return Ok(shifts);
		}

		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public ActionResult CreateEmployee([FromBody] EmployeeCreationDto newEmployee)
		{
			var location = _locationRepository.GetLocationById(newEmployee.LocationId);
			if (location == null)
				return BadRequest();

			var employee = new Employee
			{
				Id = Guid.NewGuid().ToString(),
				Username = newEmployee.Username,
				Address = newEmployee.Address,
				Birthday = newEmployee.Birthday,
				Email = newEmployee.Email,
				IsManager = newEmployee.IsManager,
				LocationId = newEmployee.LocationId,
				MonthlyWorkHours = newEmployee.MonthlyWorkHours,
				Name = newEmployee.Name,
				Surname = newEmployee.Surname,
				Password = newEmployee.Password,
				Phone = newEmployee.Phone
			};

			_employeeRepository.InsertEmployee(employee);
			_employeeRepository.Save();

			return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public ActionResult GetEmployeeById(string id)
		{
			var employee = _employeeRepository.GetEmployeeById(id);
			if (employee == null)
				return NotFound();

			return Ok(employee);
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPut]
		public ActionResult UpdateEmployee([FromBody] Employee updatedEmployee)
		{
			var employee = _employeeRepository.GetEmployeeById(updatedEmployee.Id);
			if (employee == null)
				return NotFound();

			_employeeRepository.UpdateEmployee(updatedEmployee);
			_employeeRepository.Save();

			return Ok();
		}

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpDelete("{id}")]
		public ActionResult DeleteEmployee(string id)
		{
			var employee = _employeeRepository.GetEmployeeById(id);
			if (employee == null)
				return NotFound();

			_employeeRepository.DeleteEmployee(employee);
			_employeeRepository.Save();

			return NoContent();
		}
	}
}
