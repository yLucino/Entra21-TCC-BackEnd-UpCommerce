using Entra21_TCC_BackEnd_UpCommerce.Context;
using Entra21_TCC_BackEnd_UpCommerce.Dtos;
using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Entra21_TCC_BackEnd_UpCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDb _context;

        public ProjectController(AppDb context)
        {
            _context = context;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserProjects(int userId)
        {
            var projects = await _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new
                {
                    id = p.Id,
                    title = p.Title,
                    subTitle = p.SubTitle,
                    description = p.Description,
                    urlLogo = p.UrlLogo
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> GetProject(int userId, int projectId)
        {
            var project = await _context.Projects
                .Where(p => p.UserId == userId && p.Id == projectId)
                .FirstOrDefaultAsync();

            if (project == null)
                return NotFound("Projeto não encontrado.");

            var components = project.GetComponentsObject();
            return Ok(new
            {
                project.Id,
                project.Title,
                project.SubTitle,
                project.Description,
                project.UrlLogo,
                project.UserId,
                component = components
            });
        }

        [HttpPost("user/{userId:int}")]
        public async Task<IActionResult> CreateProject(int userId, [FromBody] ProjectDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                Description = dto.Description,
                UrlLogo = dto.UrlLogo,
                UserId = userId,
                ComponentJson = JsonSerializer.Serialize(dto.Component),
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projeto criado com sucesso.", projectId = project.Id });
        }

        [HttpPut("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> UpdateProject(int userId, int projectId, [FromBody] ProjectDto dto)
        {
            if (dto == null || dto.UserId != userId)
                return BadRequest("Dados inválidos.");

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);
            if (project == null)
                return NotFound("Projeto não encontrado.");

            project.Title = dto.Title;
            project.SubTitle = dto.SubTitle;
            project.Description = dto.Description;
            project.UrlLogo = dto.UrlLogo;
            project.ComponentJson = JsonSerializer.Serialize(dto.Component);

            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpDelete("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> DeleteProject(int userId, int projectId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
                return NotFound("Projeto não encontrado.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projeto deletado com sucesso." });
        }
    }
}