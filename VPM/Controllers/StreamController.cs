using Arc.Common.Enums;
using Arc.Common.Models;
using Microsoft.AspNetCore.Mvc;
using VPM.Application.Interfaces;

namespace VPM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StreamController : Controller
    {

        #region Declaration

        private readonly IStreamService streamService;

        #endregion

        #region Ctor

        public StreamController(IStreamService _streamService)
        {
            streamService = _streamService;
        }

        #endregion

        #region Actions

        [HttpGet("getstreamurl")]
        public IActionResult GetStreamUrl([FromQuery] string rtspUrl)
        {
            DataModel<string> response = new DataModel<string>() { Data = string.Empty };

            try
            {
                if (!string.IsNullOrEmpty(rtspUrl))
                {
                    string webRTCUrl = streamService.GetStreamUrl(rtspUrl);

                    if (!string.IsNullOrEmpty(webRTCUrl))
                    {
                        Console.WriteLine(webRTCUrl);
                        response.Data = webRTCUrl;
                        response.Status = HttpStatusCode.OK;
                        response.Message = "Stream Url retrieved successfully.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "No data found.";
                        return NotFound(response);
                    }
                }
                else
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Invalid parameters.";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.ToString(), instance: nameof(VPM), statusCode: (int)HttpStatusCode.InternalServerError, title: "Internal server error.", type: "Internal Server");
            }
        }

        [HttpGet("playstream")]
        public IActionResult PlayStream([FromQuery] string rtspUrl)
        {
            DataModel<string> response = new DataModel<string>() { Data = string.Empty };

            try
            {
                if (!string.IsNullOrEmpty(rtspUrl))
                {
                    bool status = streamService.StartStream(rtspUrl);

                    if (status)
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Message = "Stream started successfully.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "No data found.";
                        return NotFound(response);
                    }
                }
                else
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Invalid parameters.";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.ToString(), instance: nameof(VPM), statusCode: (int)HttpStatusCode.InternalServerError, title: "Internal server error.", type: "Internal Server");
            }
        }

        [HttpGet("pausestream")]
        public IActionResult PauseStream([FromQuery] string rtspUrl)
        {
            DataModel<string> response = new DataModel<string>() { Data = string.Empty };

            try
            {
                if (!string.IsNullOrEmpty(rtspUrl))
                {
                    bool status = streamService.PauseStream(rtspUrl);

                    if (status)
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Message = "Stream paused successfully.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "No data found.";
                        return NotFound(response);
                    }
                }
                else
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Invalid parameters.";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.ToString(), instance: nameof(VPM), statusCode: (int)HttpStatusCode.InternalServerError, title: "Internal server error.", type: "Internal Server");
            }
        }

        [HttpGet("stopstream")]
        public IActionResult StopStream([FromQuery] string rtspUrl)
        {
            DataModel<string> response = new DataModel<string>() { Data = string.Empty };

            try
            {
                if (!string.IsNullOrEmpty(rtspUrl))
                {
                    bool status = streamService.StopStream(rtspUrl);

                    if (status)
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Message = "Stream stopped successfully.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "No data found.";
                        return NotFound(response);
                    }
                }
                else
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Invalid parameters.";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.ToString(), instance: nameof(VPM), statusCode: (int)HttpStatusCode.InternalServerError, title: "Internal server error.", type: "Internal Server");
            }
        }

        #endregion

    }

}