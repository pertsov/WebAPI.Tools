using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebAPI.DataAccess;
using WebAPI.Models;
using System.Net.Mime;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/public/[controller]")]
    public class HeadersController : ControllerBase
    {
        /// <summary>
        /// Echo request headers.
        /// </summary>
        /// <returns>Returns request headers as body.</returns>
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IHeaderDictionary))]
        [HttpGet("echo")] // GET localhost:<port>/api/public/headers/echo
        public IActionResult Echo()
        {
            return Ok(Request.Headers);
        }

        /// <summary>
        /// Get formatted response.
        /// </summary>
        /// <returns>Returns a collection of objects serialized based on the values of the Accept request header.</returns>
        [HttpGet("formatResponse")] // GET localhost:<port>/api/public/headers/formatResponse
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetFormatted()
        {
            // Try passing the following content types in the Accept header value and
            // see how the response body format changes:
            //   - text/xml
            //   - application/json
            
            return Ok(Database.Students);
        }

        /// <summary>
        /// Echo request body.
        /// </summary>
        /// <param name="headerPairs">Response headers.</param>
        /// <returns>Returns request body values as response headers.</returns>
        [HttpPost("echoHeaders")] // POST localhost:<port>/api/public/headers/echoHeaders
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult EchoToHeaders([FromBody] Dictionary<string, string> headerPairs)
        {
            foreach (var pair in headerPairs)
            {
                Response.Headers.TryAdd(pair.Key, pair.Value);
            }

            return Ok("Check the response headers");
        }
    }
}
