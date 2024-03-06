using api.Contextes;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly MainContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PartnersController(MainContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("uploadPartner")]
        public IActionResult UploadPartner(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    Partners partner = new Partners
                    {
                        ImageData = imageBytes,

                    };
                    _context.Partners.Add(partner);
                    _context.SaveChanges();
                }

                return Ok("Image uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }




        [HttpGet("getPartner/{partnerId}")]
        public IActionResult GetPartner(int partnerId)
        {
            try
            {

                Partners partner = _context.Partners.Find(partnerId);
                if (partner == null)
                    return NotFound("Certificate not found");
                
                
                
                var partnerData = new
                {

                    ImageData = Convert.ToBase64String(partner.ImageData),
                };

                return Ok(partnerData);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("deletePartner/{partnerId}")]
        public IActionResult DeletePartner(int partnerId)
        {
            try
            {
                
                Partners partner = _context.Partners.Find(partnerId);
                if (partner == null)
                {
                    return NotFound("Certiciate not found!");
                }


                _context.Partners.Remove(partner);
                _context.SaveChanges();

                return Ok("Image deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetPartners()
        {
            try
            {
                var partners = _context.Partners.ToList();
                return Ok(partners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
