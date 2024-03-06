using api.Contextes;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly MainContext _context;

        public CommunicationsController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCommunications()
        {
            try
            {
                
                
                var communications = _context.Communication.ToList();
                return Ok(communications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{communicationId}")]
        public IActionResult GetCommunicationById(int communicationId)
        {
            try
            {
                
                Communication communication = _context.Communication.Find(communicationId);

                if (communication == null)
                    return NotFound("Communication not found");

                return Ok(communication);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public IActionResult AddCommunication([FromBody] Communication communication)
        {
            try
            {
       
                _context.Communication.Add(communication);
                _context.SaveChanges();

                return Ok("Communication added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("{communicationId}")]
        public IActionResult UpdateCommunication(int communicationId, [FromBody] Communication updatedCommunication)
        {
            try
            {
    
                Communication existingCommunication = _context.Communication.Find(communicationId);

                if (existingCommunication == null)
                    return NotFound("Communication not found");

                existingCommunication.adress = updatedCommunication.adress;
                existingCommunication.phone = updatedCommunication.phone;
                existingCommunication.email = updatedCommunication.email;
                existingCommunication.activeTimes = updatedCommunication.activeTimes;
                existingCommunication.coordinateLant = updatedCommunication.coordinateLant;
                existingCommunication.coordinateLng = updatedCommunication.coordinateLng;

                _context.SaveChanges();

                return Ok("Communication updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{communicationId}")]
        public IActionResult DeleteCommunication(int communicationId)
        {
            try
            {

                Communication communication = _context.Communication.Find(communicationId);

                if (communication == null)
                    return NotFound("Communication not found");

                _context.Communication.Remove(communication);
                _context.SaveChanges();

                return Ok("Communication deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
