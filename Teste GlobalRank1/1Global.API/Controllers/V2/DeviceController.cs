using _1Global.API.Controllers.V1;
using _1Global.Application.V2.Services.Interfaces;
using _1Global.Data.V2.Enum;
using Microsoft.AspNetCore.Mvc;

namespace _1Global.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [EndpointGroupName("v2")]
    [ApiController]
    [Route("v2/[controller]")]
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

        [HttpGet("list")]
        public async Task<IActionResult> GetAllDevices([FromHeader] EnumOrderBy OrderBy)
        {
            var devices = await _service.GetAllDevices(OrderBy);
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
