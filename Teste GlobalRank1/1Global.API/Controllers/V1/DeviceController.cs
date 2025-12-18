using _1Global.Application.V1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _1Global.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [EndpointGroupName("v1")]
    [ApiController]
    [Route("v1/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IDeviceService _service;
        public DeviceController(ILogger<DeviceController> logger, IDeviceService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _service.GetDevice(id);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllDevices()
        {
            var devices = await _service.GetAllDevices();
            return Ok(devices);
        }

        [HttpGet("List/Brand")]
        public async Task<IActionResult> GetAllDevicesByBrand()
        {
            var devices = await _service.GetAllDevicesByBrand();
            return Ok(devices);
        }

        [HttpGet("List/State")]
        public async Task<IActionResult> GetAllDevicesByState()
        {
            var devices = await _service.GetAllDevicesByState();
            return Ok(devices);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewDevice([FromBody] Data.DTO.Device item)
        {
            var createdDevice = await _service.CreateNewDevice(item);
            return CreatedAtAction(nameof(GetDevice), new { id = createdDevice.Id }, createdDevice);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditNewDevice([FromBody] Data.DTO.Device item)
        {
            var updatedDevice = await _service.EditNewDevice(item);
            if (updatedDevice == null)
            {
                return NotFound();
            }
            return Ok(updatedDevice);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteNewDevice(int Id)
        {
            var result = await _service.DeleteDevice(Id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
