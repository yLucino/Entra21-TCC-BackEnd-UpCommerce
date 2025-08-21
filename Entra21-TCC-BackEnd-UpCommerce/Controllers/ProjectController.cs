using Entra21_TCC_BackEnd_UpCommerce.Context;
using Entra21_TCC_BackEnd_UpCommerce.Dtos;
using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // ------------------ GET ALL DO USUÁRIO ------------------
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetProjectsByUser(int userId)
        {
            var projects = await _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.SubTitle,
                    p.Description,
                    p.UrlLogo
                })
                .ToListAsync();

            return Ok(projects);
        }

        // ------------------ GET BY ID DE UM PROJETO DE UM USUÁRIO ------------------
        [HttpGet("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> GetProjectByUser(int userId, int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Component)
                    .ThenInclude(c => c.Children)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
                return NotFound("Projeto não encontrado para esse usuário.");

            return Ok(project);
        }

        // ------------------ POST ------------------
        [HttpPost("user/{userId:int}")]
        public async Task<IActionResult> CreateProject(int userId, [FromBody] ProjectDto projectDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            var project = MapProject(projectDto);
            project.UserId = userId; // associa ao usuário
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        // ------------------ PUT ------------------
        [HttpPut("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> UpdateProject(int userId, int projectId, [FromBody] ProjectDto projectDto)
        {
            var project = await _context.Projects
                .Include(p => p.Component)
                    .ThenInclude(c => c.Children)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
                return NotFound("Projeto não encontrado para esse usuário.");

            project.Title = projectDto.Title;
            project.SubTitle = projectDto.SubTitle;
            project.Description = projectDto.Description;
            project.UrlLogo = projectDto.UrlLogo;

            UpdateCdks(project.Component, projectDto.Component);

            await _context.SaveChangesAsync();
            return Ok(project);
        }

        // ------------------ DELETE ------------------
        [HttpDelete("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> DeleteProject(int userId, int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Component)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
                return NotFound("Projeto não encontrado para esse usuário.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ------------------ Função recursiva para atualizar Cdks ------------------
        private void UpdateCdks(ICollection<Cdk> existingCdks, ICollection<CdkDto> newCdks)
        {
            if (newCdks == null) return;

            var toRemove = existingCdks.Where(e => !newCdks.Any(n => n.CdkId == e.CdkId)).ToList();
            foreach (var rem in toRemove)
                existingCdks.Remove(rem);

            foreach (var newCdk in newCdks)
            {
                var existing = existingCdks.FirstOrDefault(e => e.CdkId == newCdk.CdkId);
                if (existing != null)
                {
                    existing.Style = MapStyles(newCdk.Style);

                    if (newCdk.Children != null)
                    {
                        if (existing.Children == null)
                            existing.Children = new List<Cdk>();

                        UpdateCdks(existing.Children, newCdk.Children);
                    }
                    else
                    {
                        existing.Children = null;
                    }
                }
                else
                {
                    existingCdks.Add(MapCdk(newCdk));
                }
            }
        }

        // ------------------ Mapeamentos ------------------
        private Project MapProject(ProjectDto dto)
        {
            return new Project
            {
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                Description = dto.Description,
                UrlLogo = dto.UrlLogo,
                Component = dto.Component?.Select(c => MapCdk(c)).ToList()
            };
        }

        private Cdk MapCdk(CdkDto dto)
        {
            return new Cdk
            {
                CdkId = dto.CdkId,
                Style = MapStyles(dto.Style),
                Children = dto.Children?.Select(c => MapCdk(c)).ToList()
            };
        }

        private Style MapStyles(StyleDto dto)
        {
            return new Style
            {
                Width = dto.Width,
                Height = dto.Height,
                MarginLeft = dto.MarginLeft,
                MarginTop = dto.MarginTop,
                MarginRight = dto.MarginRight,
                MarginBottom = dto.MarginBottom,
                PaddingLeft = dto.PaddingLeft,
                PaddingTop = dto.PaddingTop,
                PaddingRight = dto.PaddingRight,
                PaddingBottom = dto.PaddingBottom,
                BorderSize = dto.BorderSize,
                BorderColor = dto.BorderColor,
                BorderType = dto.BorderType,
                BorderRadiusTopLeft = dto.BorderRadiusTopLeft,
                BorderRadiusTopRight = dto.BorderRadiusTopRight,
                BorderRadiusBottomLeft = dto.BorderRadiusBottomLeft,
                BorderRadiusBottomRight = dto.BorderRadiusBottomRight,
                BackgroundColor = dto.BackgroundColor,
                Color = dto.Color,
                FontFamily = dto.FontFamily,
                TextContent = dto.TextContent,
                FontSize = dto.FontSize,
                FontWeight = dto.FontWeight,
                TextAlign = dto.TextAlign,
                Opacity = dto.Opacity,
                ShadowX = dto.ShadowX,
                ShadowY = dto.ShadowY,
                ShadowBlur = dto.ShadowBlur,
                ShadowColor = dto.ShadowColor,
                Position = dto.Position,
                Top = dto.Top,
                Left = dto.Left,
                Right = dto.Right,
                Bottom = dto.Bottom,
                ZIndex = dto.ZIndex,
                HoverScale = dto.HoverScale,
                HoverBorderRadius = dto.HoverBorderRadius,
                HoverShadowX = dto.HoverShadowX,
                HoverShadowY = dto.HoverShadowY,
                HoverShadowBlur = dto.HoverShadowBlur,
                HoverShadowColor = dto.HoverShadowColor,
                Cursor = dto.Cursor,
                Display = dto.Display,
                FlexDirection = dto.FlexDirection,
                FlexJustify = dto.FlexJustify,
                FlexAlign = dto.FlexAlign,
                FlexWrap = dto.FlexWrap,
                FlexGap = dto.FlexGap,
                FlexAlignItems = dto.FlexAlignItems,
                AlignSelf = dto.AlignSelf,
                NewComponentId = dto.NewComponentId,
                ImageSource = dto.ImageSource,
                IconSource = dto.IconSource,
                LinkSource = dto.LinkSource
            };
        }
    }
}
