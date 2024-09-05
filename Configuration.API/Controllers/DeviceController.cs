using Microsoft.AspNetCore.Mvc;
using Arc.Common.Enums;
using Arc.Common.Models;
using Configuration.Application.Interfaces;
using Configuration.Application.DTOs;
using Configuration.API.Models;

namespace Configuration.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {

        #region Declaration

        private readonly IDeviceService deviceService;

        #endregion

        #region Ctor

        public DeviceController(IDeviceService _deviceService)
        {
            deviceService = _deviceService;
        }

        #endregion

        #region Actions

        [HttpGet("get")]
        public IActionResult Get([FromQuery] int? id)
        {
            if (id.HasValue)
            {
                return GetDeviceById(id.Value);
            }
            else
            {
                return GetAllDevices();
            }
        }

        [HttpGet("devicecomponent")]
        public IActionResult DeviceComponent()
        {
            DataModel<List<ComponentDevice>> response = new DataModel<List<ComponentDevice>>() { Data = new List<ComponentDevice>() };

            try
            {
                response.Data = deviceService.GetDeviceComponents();

                if (response.Data != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Devices retrieved successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    return NotFound(response);
                }
            }
            catch (Exception)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] DeviceParams device)
        {
            DataModel<bool> response = new DataModel<bool>() { Data = false };

            if (ModelState.IsValid)
            {
                Device _device = new Device()
                {
                    Name = device.Name,
                    Type = device.Type,
                    Address = device.Address,
                    HttpPort = device.HttpPort,
                    RtspPort = device.RtspPort,
                    UserName = device.UserName,
                    Password = device.Password,
                };

                response.Data = deviceService.AddUpdateDevice(_device);

                if (response.Data)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Device added successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "Error while performing operation.";
                    return UnprocessableEntity(response);
                }
            }
            else
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] Device device)
        {
            DataModel<bool> response = new DataModel<bool>() { Data = false };

            if (ModelState.IsValid)
            {
                response.Data = deviceService.AddUpdateDevice(device);

                if (response.Data)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Device updated successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "Error while performing operation.";
                    return UnprocessableEntity(response);
                }
            }
            else
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            DataModel<bool> response = new DataModel<bool>() { Data = false };

            try
            {
                response.Data = deviceService.DeleteDevice(id);

                if (response.Data)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Device deleted successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "Error while performing operation.";
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        #endregion

        #region Methods

        private IActionResult GetAllDevices()
        {
            DataModel<List<Device>> response = new DataModel<List<Device>>() { Data = new List<Device>() };

            try
            {
                response.Data = deviceService.GetDevices();

                if (response.Data != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Devices retrieved successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    return NotFound(response);
                }
            }
            catch (Exception)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        private IActionResult GetDeviceById(int id)
        {
            DataModel<Device> response = new DataModel<Device>() { Data = new Device() };

            try
            {
                response.Data = deviceService.GetDevice(id);

                if (response.Data != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Device retrieved successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    return NotFound(response);
                }
            }
            catch (Exception)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                return BadRequest(response);
            }
        }

        #endregion

    }

}