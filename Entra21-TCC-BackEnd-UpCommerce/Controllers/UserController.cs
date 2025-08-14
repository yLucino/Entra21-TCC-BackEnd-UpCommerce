using Entra21_TCC_BackEnd_UpCommerce.Context;
using Entra21_TCC_BackEnd_UpCommerce.Dtos;
using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Entra21_TCC_BackEnd_UpCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDb _context;
        private readonly IConfiguration _configuration;

        public UserController(AppDb context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ==========================
        // JWT Token Generator
        // ==========================
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("role", user.Role),
                new Claim("name", user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ==========================
        // GET - Listar todos sem senha
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.Name, u.Email, u.Role })
                .ToListAsync();

            return Ok(users);
        }

        // ==========================
        // GET - Buscar por ID sem senha
        // ==========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new { u.Id, u.Name, u.Email, u.Role })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }

        // ==========================
        // POST - Login com JWT
        // ==========================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return Unauthorized("E-mail ou senha inválidos.");

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role
                }
            });
        }

        // ==========================
        // POST - Registro com hash de senha
        // ==========================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("E-mail já registrado.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso.");
        }

        // ==========================
        // PUT - Atualizar usuário com hash de senha
        // ==========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;

            // Atualiza a senha apenas se foi enviada
            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Role
            });
        }
    }
}
