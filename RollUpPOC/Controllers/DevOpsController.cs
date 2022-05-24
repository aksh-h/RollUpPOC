using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using System.Net;

namespace RollUpPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevOpsController : ControllerBase
    {
        public IConfiguration _config;

        public DevOpsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("getprojects")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjects(string organizationName)
        {
            try
            {
                if (!string.IsNullOrEmpty(organizationName))
                {
                    string baseURL = _config.GetValue<string>("baseUrl");
                    string pat = _config.GetValue<string>("pat");
                    Basic.ConnectWithPAT(string.Concat(baseURL, organizationName), pat);

                    var projects = await Basic._projectClient.GetProjects();
                    if (projects.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, projects);
                    }
                }
                else
                {
                    return BadRequest("Organization Name is required");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost]
        [Route("witupdated")]
        [Produces("application/json")]
        public async Task<IActionResult> Updated()
        {
            try
            {
               string content = await Request.GetRawBodyAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    RequestModel requestModel = new RequestModel();
                    requestModel = JsonConvert.DeserializeObject<RequestModel>(content);
                    return Ok(requestModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return BadRequest();
        }
    }
}
