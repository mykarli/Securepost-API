using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FullStackBitirme.Api.Models;
using FullStackBitirme.Api.Services;

namespace FullStackBitirme.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(IAuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
                return BadRequest("Eksik bilgi gönderildi.");

            try
            {
                var user = await _authService.RegisterAsync(request.FullName, request.Email, request.Password);
                var token = _tokenService.CreateToken(user);

                return Ok(new
                {
                    message = "Kayıt başarılı",
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest("Eksik bilgi gönderildi.");

            var user = await _authService.LoginAsync(request.Email, request.Password);
            if (user == null)
                return Unauthorized(new { error = "E-posta veya şifre hatalı." });

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                message = "Giriş başarılı",
                token
            });
        }
    }

    // DTO modelleri
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
