using Microsoft.AspNetCore.Mvc;
using PoSSapi.Classes;
using PoSSapi.Dtos;
using PoSSapi.Repositories;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
	private readonly ILocationRepository _locationRepository;
	private readonly IShiftRepository _shiftRepository;
	private readonly IEmployeeRepository _employeeRepository;

	public LocationController(ILocationRepository locationRepository, IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
	{
		_locationRepository = locationRepository;
		_shiftRepository = shiftRepository;
		_employeeRepository = employeeRepository;
	}

	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Location[]))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpGet()]
	public ActionResult GetAll([FromQuery] string? name, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
	{
		if (itemsPerPage <= 0)
			return BadRequest("itemsPerPage must be greater than 0");

		if (pageNum < 0)
			return BadRequest("pageNum must be 0 or greater");

		var locations = _locationRepository.GetLocations();

		if (name != null)
			locations = locations.Where(l => l.Name == name);

		locations = locations.Skip(itemsPerPage * pageNum).Take(itemsPerPage);
		return Ok(locations);
	}

	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shift[]))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet("{id}/shift")]
	public ActionResult GetAllShifts(string id, [FromQuery] DateTime? startDate, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
	{
		if (itemsPerPage <= 0)
			return BadRequest("itemsPerPage must be greater than 0");

		if (pageNum < 0)
			return BadRequest("pageNum must be 0 or greater");

		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		var shifts = _shiftRepository.GetShifts();

		shifts = shifts.Where(s =>
		{
			var employee = _employeeRepository.GetEmployeeById(s.EmployeeId);
			if (employee == null)
				return false;

			return id == employee.LocationId;
		});

		if (startDate != null)
			shifts = shifts.Where(s => s.StartDate == startDate);

		shifts.Skip(pageNum * itemsPerPage).Take(itemsPerPage);
		return Ok(shifts);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet("{id}/shift/{shiftId}")]
	public ActionResult<Shift> GetShift(string id, string shiftId)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		var shift = _shiftRepository.GetShiftById(shiftId);
		return Ok(shift);
	}

	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPost("{id}/shift")]
	public ActionResult PostShift(string id, [FromBody] ShiftCreationDto newObject)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		var employee = _employeeRepository.GetEmployeeById(newObject.EmployeeId);
		if (employee == null)
			return NotFound();

		var shift = new Shift()
		{
			Id = Guid.NewGuid().ToString(),
			CheckInDate = newObject.CheckInDate,
			CheckOutDate = newObject.CheckOutDate,
			StartDate = newObject.StartDate,
			FinishDate = newObject.FinishDate,
			EmployeeId = newObject.EmployeeId
		};

		_shiftRepository.InsertShift(shift);
		_shiftRepository.Save();

		return CreatedAtAction(nameof(GetShift), new { shiftId = shift.Id }, shift);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPut("{id}/shift/{shiftId}")]
	public ActionResult PutShift(string id, string shiftId, [FromBody] ShiftCreationDto updatedObject)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		var employee = _employeeRepository.GetEmployeeById(updatedObject.EmployeeId);
		if (employee == null)
			return NotFound();

		var shift = new Shift()
		{
			Id = shiftId,
			CheckInDate = updatedObject.CheckInDate,
			CheckOutDate = updatedObject.CheckOutDate,
			StartDate = updatedObject.StartDate,
			FinishDate = updatedObject.FinishDate,
			EmployeeId = updatedObject.EmployeeId
		};

		_shiftRepository.UpdateShift(shift);
		_shiftRepository.Save();

		return Ok();
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}/shift/{shiftId}")]
	public ActionResult DeleteShift(string id, string shiftId)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		var shift = _shiftRepository.GetShiftById(shiftId);
		if (shift == null)
			return NotFound();

		_shiftRepository.DeleteShift(shift);
		_shiftRepository.Save();

		return NoContent();
	}

	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Location))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost()]
	public ActionResult CreateLocation([FromBody] LocationCreationDto newLocation)
	{
		var location = new Location()
		{
			Id = Guid.NewGuid().ToString(),
			Address = newLocation.Address,
			Name = newLocation.Name
		};

		_locationRepository.InsertLocation(location);
		_locationRepository.Save();

		return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
	}

	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Location))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet("{id}")]
	public ActionResult GetLocationById(string id)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		return Ok(location);
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpPut]
	public ActionResult UpdateLocation([FromBody] Location updatedLocation)
	{
		var location = _locationRepository.GetLocationById(updatedLocation.Id);
		if (location == null)
			return NotFound();

		_locationRepository.UpdateLocation(updatedLocation);
		_locationRepository.Save();

		return Ok();
	}

	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}")]
	public ActionResult DeleteLocation(string id)
	{
		var location = _locationRepository.GetLocationById(id);
		if (location == null)
			return NotFound();

		_locationRepository.DeleteLocation(location);
		_locationRepository.Save();

		return NoContent();
	}
}
