using api.Contextes;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly MainContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CertificateController(MainContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("uploadCertificate")]
        public IActionResult UploadCertificate(IFormFile file, [FromForm] string certificateName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();

                    Certificates certificate = new Certificates
                    {
                        ImageData = imageBytes,
                        certificateName = certificateName,
                    };

                    _context.Certificates.Add(certificate);
                    _context.SaveChanges();
                }

                return Ok("Certificate uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("getCertificate/{certificateId}")]
        public IActionResult GetCertificate(int certificateId)
        {
            try
            {
                Certificates certificate = _context.Certificates.Find(certificateId);

                if (certificate == null)
                    return NotFound("Certificate not found");

                var certificateData = new
                {
                    CertificateName = certificate.certificateName,
                    ImageData = Convert.ToBase64String(certificate.ImageData),
                };

                return Ok(certificateData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("deleteCertificate/{certificateId}")]
        public IActionResult DeleteCertificate(int certificateId)
        {
            try
            {
                Certificates certificate = _context.Certificates.Find(certificateId);

                if (certificate == null)
                    return NotFound("Certificate not found");

                // CertificateName'i kontrol etmek için ek bir işlem yapabilirsiniz
                string certificateName = certificate.certificateName;

                _context.Certificates.Remove(certificate);
                _context.SaveChanges();

                return Ok($"Certificate '{certificateName}' deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetCertificates()
        {
            try
            {
                var certificates = _context.Certificates.ToList();
                return Ok(certificates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
