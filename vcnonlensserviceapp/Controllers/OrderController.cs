using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using vcnonlensserviceapp.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace vcnonlensserviceapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostOrder([FromBody] OrderRequestModel orderRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create the response object
            var response = new OrderResponseModel
            {
                SessionId_ = orderRequest.SessionId_,
                Filename = orderRequest.Filename,
                Message = orderRequest.Filecontent,
                CompanyId = orderRequest.CompanyId
            };

            // Create directory and file path
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Responses", response.CompanyId);
            var filePath = Path.Combine(directoryPath, $"{response.SessionId_}.json");

            // Check if the file already exists
            if (System.IO.File.Exists(filePath))
            {
                return Conflict(new { Message = "SessionId already exists" });
            }

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            // Serialize the response object to JSON
            var jsonResponse = JsonConvert.SerializeObject(response, Formatting.Indented);

            // Write the JSON response to the file
            System.IO.File.WriteAllText(filePath, jsonResponse, Encoding.UTF8);

            return Ok(response);
        }
    }
}
