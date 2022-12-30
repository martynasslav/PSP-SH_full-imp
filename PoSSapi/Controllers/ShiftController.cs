using Microsoft.AspNetCore.Mvc;
using PoSSapi.Classes;
using PoSSapi.Dtos;
using PoSSapi.Repositories;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : ControllerBase
{
	private readonly IShiftRepository _shiftRepository;
	private readonly IEmployeeRepository _employeeRepository;

	public ShiftController(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
	{
		_shiftRepository = shiftRepository;
		_employeeRepository = employeeRepository;
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpGet()]
	public ActionResult GetAll([FromQuery] string? employeeId, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
	{
		if (itemsPerPage <= 0)
			return BadRequest("itemsPerPage must be greater than 0");

		if (pageNum < 0)
			return BadRequest("pageNum must be 0 or greater");

		var shifts = _shiftRepository.GetShifts();

		if (employeeId != null)
			shifts = shifts.Where(s => s.EmployeeId == employeeId);

		shifts = shifts.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

		return Ok(shifts);
	}

	/// <summary>Check in for a shift</summary>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPost("{id}/check-in")]
	public ActionResult CheckInShift(string id)
	{
		var shift = _shiftRepository.GetShiftById(id);
		if (shift == null)
			return NotFound();

		shift.CheckInDate = DateTime.Now;
		_shiftRepository.Save();

		return Ok();
	}

	/// <summary>Check out from a shift</summary>
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPost("{id}/check-out")]
	public ActionResult CheckOutShift(string id)
	{
		var shift = _shiftRepository.GetShiftById(id);
		if (shift == null)
			return NotFound();

		shift.CheckOutDate = DateTime.Now;
		_shiftRepository.Save();

		return Ok();
	}

	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost]
	public ActionResult CreateShift([FromBody] ShiftCreationDto newShift)
	{
		var employee = _employeeRepository.GetEmployeeById(newShift.EmployeeId);
		if (employee == null)
			return BadRequest();

		var shift = new Shift()
		{
			Id = Guid.NewGuid().ToString(),
			CheckInDate = newShift.CheckInDate,
			CheckOutDate = newShift.CheckOutDate,
			EmployeeId = newShift.EmployeeId,
			StartDate = newShift.StartDate,
			FinishDate = newShift.FinishDate
		};

		_shiftRepository.InsertShift(shift);
		_shiftRepository.Save();

		return CreatedAtAction(nameof(GetShiftById), new { id = shift.Id }, shift);
	}

	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shift))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet("{id}")]
	public ActionResult GetShiftById(string id)
	{
		var shift = _shiftRepository.GetShiftById(id);
		if (shift == null)
			return NotFound();

		return Ok(shift);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPut]
	public ActionResult UpdateShift([FromBody] Shift updatedShift)
	{
		var shift = _shiftRepository.GetShiftById(updatedShift.Id);
		if (shift == null)
			return NotFound();

		_shiftRepository.UpdateShift(shift);
		_shiftRepository.Save();

		return Ok();
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}")]
	public ActionResult DeleteShiftById(string id)
	{
		var shift = _shiftRepository.GetShiftById(id);
		if (shift == null)
			return NotFound();

		_shiftRepository.DeleteShift(shift);
		_shiftRepository.Save();

		return NoContent();
	}
}
