using Classes;
using Dtos;
using Microsoft.AspNetCore.Mvc;
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

    [ProducesResponseType(200)]
    [HttpGet(Name = "GetAllServices")]
    public IEnumerable<Service> GetAllServices([FromQuery] string? locationId, [FromQuery] string? categoryId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        var services = _serviceRepository.GetAllServices();

        if (locationId != null)
        {
            services = services.Where(s => s.LocationId == locationId);
        }

        if (categoryId != null)
        {
            services = services.Where(s => s.CategoryId == categoryId);
        }

        return services.Skip(pageNum).Take(itemsPerPage);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
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

    [ProducesResponseType(201)]
    [HttpPost(Name = "CreateService")]
    public ActionResult<Service> CreateService(Service service)
    {
        _serviceRepository.CreateService(service);
        return CreatedAtAction("GetService", new { id = service.Id }, service);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
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

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
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
