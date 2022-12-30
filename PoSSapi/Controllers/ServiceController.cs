using Classes;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Dtos;
using PoSSapi.Repository;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllServices")]
    public ActionResult GetAllServices([FromQuery] string? locationId, [FromQuery] string? categoryId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0)
            return BadRequest("itemsPerPage must be greater than 0");

        if (pageNum < 0)
            return BadRequest("pageNum must be 0 or greater");

        var services = _serviceRepository.GetAllServices();

        if (locationId != null)
        {
            services = services.Where(s => s.LocationId == locationId);
        }

        if (categoryId != null)
        {
            services = services.Where(s => s.CategoryId == categoryId);
        }

        services = services.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(services);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Service))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetService")]
    public ActionResult<Service> GetService(string id)
    {
        var service = _serviceRepository.GetService(id);

        if (service == null)
        {
            return NoContent();
        }

        return service;
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Service))]
    [HttpPost(Name = "CreateService")]
    public ActionResult<Service> CreateService(CreateServiceDto newService)
    {
        var service = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = newService.Name,
            Price = newService.Price,
            Duration = newService.Duration,
            CategoryId = newService.CategoryId,
            LocationId = newService.LocationId
        };

        _serviceRepository.CreateService(service);

        return CreatedAtAction("GetService", new { id = service.Id }, service);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}", Name = "UpdateService")]
    public ActionResult<Service> UpdateService(string id, Service service) 
    {
        var _service = _serviceRepository.GetService(id);

        if (_service == null) 
        {
            return NotFound();
        }

        service.Id = _service.Id;

        _serviceRepository.UpdateService(service);

        return Ok(service);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}", Name = "DeleteService")]
    public ActionResult<Service> DeleteService(string id)
    {
        var service = _serviceRepository.GetService(id);

        if (service == null)
        {
            return NotFound();
        }

        _serviceRepository.DeleteService(service);
        return Ok();
    }
}
