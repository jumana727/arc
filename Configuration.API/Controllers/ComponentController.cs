using Microsoft.AspNetCore.Mvc;
using Arc.Common.Enums;
using Arc.Common.Models;
using Configuration.Application.DTOs;
using Configuration.Application.Interfaces;
using Logging.Core;

namespace Configuration.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ComponentController : ControllerBase
    {

        #region Declaration

        private readonly IComponentService componentService;

        #endregion

        #region Ctor

        public ComponentController(IComponentService _componentService)
        {
            componentService = _componentService;
        }

        #endregion

        #region Actions

        #region Components

        [HttpGet("get/byDevice")]
        public async Task<IActionResult> GetComponents([FromQuery] int deviceId)
        {
            var response = new DataModel<List<Component>> { Data = new List<Component>() };

            try
            {
                response.Data = componentService.GetComponents(deviceId);

                if (response.Data != null && response.Data.Count > 0)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Components retrieved successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("get/byComponent")]
        public async Task<IActionResult> GetComponent([FromQuery] int deviceId, [FromQuery] int componentId)
        {
            var response = new DataModel<Component> { Data = new Component() };

            try
            {
                response.Data = componentService.GetComponent(deviceId, componentId);

                if (response.Data != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Component retrieved successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpPost("addUpdateComponent")]
        public async Task<IActionResult> AddUpdateComponent([FromBody] Component component)
        {
            var response = new DataModel<int> { Data = 0 };

            if (ModelState.IsValid)
            {
                try
                {
                    response.Data = componentService.AddUpdateComponent(component);

                    if (response.Data > 0)
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Message = component.Id > 0 ? "Component updated successfully." : "Component added successfully.";
                        Logger.LogMessage("ConfigApi","Information",response.Message);
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.InternalServerError;
                        response.Message = "Error while performing operation.";
                        Logger.LogMessage("ConfigApi","Error",response.Message);
                        return UnprocessableEntity(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = $"Error while performing operation. Error: {ex.Message}";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return UnprocessableEntity(response);
                }
            }
            else
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("deleteComponent")]
        public IActionResult DeleteComponent(int id)
        {
            var response = new DataModel<bool> { Data = false };

            try
            {
                response.Data = componentService.DeleteComponent(id);

                if (response.Data)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Component deleted successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "Error while performing operation.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        #endregion

        #region Stream Profiles

        [HttpGet("getStreamProfiles/byComponent")]
        public IActionResult GetStreamProfiles([FromQuery] int deviceId, [FromQuery] int componentId)
        {
            var response = new DataModel<List<StreamProfile>> { Data = new List<StreamProfile>() };

            try
            {
                response.Data = componentService.GetStreamProfiles(deviceId, componentId);

                if (response.Data != null && response.Data.Count > 0)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Stream profiles retrieved successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("getStreamProfile/byComponent")]
        public IActionResult GetStreamProfile([FromQuery] int deviceId, [FromQuery] int componentId, [FromQuery] int profileNo)
        {
            var response = new DataModel<StreamProfile> { Data = new StreamProfile() };

            try
            {
                response.Data = componentService.GetStreamProfile(deviceId, componentId, profileNo);

                if (response.Data != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Stream profile retrieved successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "No data found.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpPost("addUpdateStreamProfile")]
        public IActionResult AddUpdateStreamProfile([FromBody] StreamProfile streamProfile)
        {
            var response = new DataModel<int> { Data = 0 };

            if (ModelState.IsValid)
            {
                try
                {
                    response.Data = componentService.AddUpdateStreamProfile(streamProfile);

                    if (response.Data > 0)
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Message = streamProfile.Id > 0 ? "Stream profile updated successfully." : "Stream profile added successfully.";
                        Logger.LogMessage("ConfigApi","Information",response.Message);
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.InternalServerError;
                        response.Message = "Error while performing operation.";
                        Logger.LogMessage("ConfigApi","Error",response.Message);
                        return UnprocessableEntity(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = $"Error while performing operation. Error: {ex.Message}";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return UnprocessableEntity(response);
                }
            }
            else
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = "Invalid data.";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("deleteStreamProfile")]
        public IActionResult DeleteStreamProfile(int id)
        {
            var response = new DataModel<bool> { Data = false };

            try
            {
                response.Data = componentService.DeleteStreamProfile(id);

                if (response.Data)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Stream profile deleted successfully.";
                    Logger.LogMessage("ConfigApi","Information",response.Message);
                    return Ok(response);
                }
                else
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "Error while performing operation.";
                    Logger.LogMessage("ConfigApi","Error",response.Message);
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = $"Invalid data. Error: {ex.Message}";
                Logger.LogMessage("ConfigApi","Error",response.Message);
                return BadRequest(response);
            }
        }

        #endregion

        #endregion

    }

}