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

        // GET todos os projetos de um usuário
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserProjects(int userId)
        {
            var projects = await _context.Projects
                .Where(p => p.UserId == userId)
                .Include(p => p.Component)
                    .ThenInclude(c => c.Children)
                .Include(p => p.Component)
                    .ThenInclude(c => c.Style)
                .ToListAsync();

            return Ok(projects);
        }

        // GET projeto específico de um usuário
        [HttpGet("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> GetProject(int userId, int projectId)
        {
            var project = await _context.Projects
                .Where(p => p.UserId == userId && p.Id == projectId)
                .Include(p => p.Component)
                    .ThenInclude(c => c.Children)
                .Include(p => p.Component)
                    .ThenInclude(c => c.Style)
                .FirstOrDefaultAsync();

            if (project == null)
                return NotFound("Projeto não encontrado.");

            return Ok(project);
        }

        // POST criar projeto
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
                Component = dto.Component.Select(c => MapCdkDtoToModel(c)).ToList()
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Projeto criado com sucesso.", projectId = project.Id });
        }

        // PUT atualizar projeto
        [HttpPut("user/{userId:int}/{projectId:int}")]
        public async Task<IActionResult> UpdateProject(int userId, int projectId, [FromBody] ProjectDto dto)
        {
            if (dto == null || dto.UserId != userId)
                return BadRequest("Dados inválidos.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Busca o projeto existente
                var project = await _context.Projects
                    .Include(p => p.Component)
                        .ThenInclude(c => c.Children)
                    .Include(p => p.Component)
                        .ThenInclude(c => c.Style)
                    .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

                if (project == null)
                    return NotFound("Projeto não encontrado.");

                // 2️⃣ Atualiza os campos do projeto
                project.Title = dto.Title;
                project.SubTitle = dto.SubTitle;
                project.UrlLogo = dto.UrlLogo;
                project.Description = dto.Description;

                await _context.SaveChangesAsync();

                // 3️⃣ Atualiza ou adiciona componentes recursivamente
                foreach (var cdkDto in dto.Component)
                {
                    AddOrUpdateComponentRecursive(cdkDto, project.Id, null);
                }

                // 4️⃣ Salva tudo de uma vez
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(project);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE apagar projeto
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

        // -----------------------------
        // Mapeia CdkDto -> Cdk
        // -----------------------------
        private Cdk MapCdkDtoToModel(CdkDto dto)
        {
            var cdk = new Cdk
            {
                Id = dto.Id,
                CdkId = dto.CdkId,
                ParentCdkId = dto.ParentCdkId,
                Style = dto.Style != null ? MapStyleDtoToModel(dto.Style, dto.Id) : null,
                Children = dto.Children?.Select(c => MapCdkDtoToModel(c)).ToList() ?? new List<Cdk>()
            };

            return cdk;
        }

        // -----------------------------
        // Mapeia StyleDto -> Style
        // -----------------------------
        private Style MapStyleDtoToModel(StyleDto dto, string cdkId)
        {
            return new Style
            {
                CdkId = cdkId,
                Width = dto.Width ?? 0,
                Height = dto.Height ?? 0,

                MarginLeft = dto.MarginLeft ?? 0,
                MarginTop = dto.MarginTop ?? 0,
                MarginRight = dto.MarginRight ?? 0,
                MarginBottom = dto.MarginBottom ?? 0,

                PaddingLeft = dto.PaddingLeft ?? 0,
                PaddingTop = dto.PaddingTop ?? 0,
                PaddingRight = dto.PaddingRight ?? 0,
                PaddingBottom = dto.PaddingBottom ?? 0,

                BorderSize = dto.BorderSize ?? 0,
                BorderColor = dto.BorderColor ?? "",
                BorderType = dto.BorderType ?? "",

                BorderRadiusTopLeft = dto.BorderRadiusTopLeft ?? 0,
                BorderRadiusTopRight = dto.BorderRadiusTopRight ?? 0,
                BorderRadiusBottomLeft = dto.BorderRadiusBottomLeft ?? 0,
                BorderRadiusBottomRight = dto.BorderRadiusBottomRight ?? 0,

                BackgroundColor = dto.BackgroundColor ?? "",
                Color = dto.Color ?? "",

                FontFamily = dto.FontFamily ?? "",
                TextContent = dto.TextContent ?? "",
                FontSize = dto.FontSize ?? 0,
                FontWeight = dto.FontWeight ?? "",
                TextAlign = dto.TextAlign ?? "",

                Opacity = dto.Opacity ?? 1,

                ShadowX = dto.ShadowX ?? 0,
                ShadowY = dto.ShadowY ?? 0,
                ShadowBlur = dto.ShadowBlur ?? 0,
                ShadowColor = dto.ShadowColor ?? "#000000",

                Position = dto.Position ?? "",
                Top = dto.Top ?? 0,
                Left = dto.Left ?? 0,
                Right = dto.Right ?? 0,
                Bottom = dto.Bottom ?? 0,

                ZIndex = dto.ZIndex ?? 0,

                HoverScale = dto.HoverScale ?? "",
                HoverBorderRadius = dto.HoverBorderRadius ?? 0,
                HoverShadowX = dto.HoverShadowX ?? 0,
                HoverShadowY = dto.HoverShadowY ?? 0,
                HoverShadowBlur = dto.HoverShadowBlur ?? 0,
                HoverShadowColor = dto.HoverShadowColor ?? "#000000",

                Cursor = dto.Cursor ?? "",

                Display = dto.Display ?? "",
                FlexDirection = dto.FlexDirection ?? "",
                FlexJustify = dto.FlexJustify ?? "",
                FlexAlign = dto.FlexAlign ?? "",
                FlexWrap = dto.FlexWrap ?? "",
                FlexGap = dto.FlexGap ?? 0,
                FlexAlignItems = dto.FlexAlignItems ?? "",
                AlignSelf = dto.AlignSelf ?? "",
            
                NewComponentId = dto.NewComponentId ?? "",
                ImageSource = dto.ImageSource ?? "",
                IconSource = dto.IconSource ?? "",
                LinkSource = dto.LinkSource ?? "",
            };
        }

        // -----------------------------
        // Atualiza ou adiciona componente e style recursivamente
        // -----------------------------
        private void AddOrUpdateComponentRecursive(CdkDto cdkDto, int projectId, string? parentCdkId)
        {
            var cdk = _context.Cdks
                .Include(c => c.Style)
                .FirstOrDefault(c => c.Id == cdkDto.Id);

            if (cdk == null)
            {
                cdk = new Cdk
                {
                    Id = cdkDto.Id,
                    CdkId = cdkDto.CdkId,
                    ParentCdkId = parentCdkId,
                    ProjectId = projectId
                };
                _context.Cdks.Add(cdk);
            }
            else
            {
                cdk.CdkId = cdkDto.CdkId;
                cdk.ParentCdkId = parentCdkId;
            }

            // Atualiza ou cria Style
            if (cdkDto.Style != null)
            {
                if (cdk.Style == null)
                {
                    cdk.Style = MapStyleDtoToModel(cdkDto.Style, cdk.Id);
                    _context.Styles.Add(cdk.Style);
                }
                else
                {
                    var s = cdk.Style;
                    var dto = cdkDto.Style;

                    s.Width = dto.Width ?? 0;
                    s.Height = dto.Height ?? 0;

                    s.MarginLeft = dto.MarginLeft ?? 0;
                    s.MarginTop = dto.MarginTop ?? 0;
                    s.MarginRight = dto.MarginRight ?? 0;
                    s.MarginBottom = dto.MarginBottom ?? 0;

                    s.PaddingLeft = dto.PaddingLeft ?? 0;
                    s.PaddingTop = dto.PaddingTop ?? 0;
                    s.PaddingRight = dto.PaddingRight ?? 0;
                    s.PaddingBottom = dto.PaddingBottom ?? 0;

                    s.BorderSize = dto.BorderSize ?? 0;
                    s.BorderColor = dto.BorderColor ?? "";
                    s.BorderType = dto.BorderType ?? "";

                    s.BorderRadiusTopLeft = dto.BorderRadiusTopLeft ?? 0;
                    s.BorderRadiusTopRight = dto.BorderRadiusTopRight ?? 0;
                    s.BorderRadiusBottomLeft = dto.BorderRadiusBottomLeft ?? 0;
                    s.BorderRadiusBottomRight = dto.BorderRadiusBottomRight ?? 0;

                    s.BackgroundColor = dto.BackgroundColor ?? "";
                    s.Color = dto.Color ?? "";

                    s.FontFamily = dto.FontFamily ?? "";
                    s.TextContent = dto.TextContent ?? "";
                    s.FontSize = dto.FontSize ?? 0;
                    s.FontWeight = dto.FontWeight ?? "";
                    s.TextAlign = dto.TextAlign ?? "";

                    s.Opacity = dto.Opacity ?? 1;

                    s.ShadowX = dto.ShadowX ?? 0;
                    s.ShadowY = dto.ShadowY ?? 0;
                    s.ShadowBlur = dto.ShadowBlur ?? 0;
                    s.ShadowColor = dto.ShadowColor ?? "#000000";

                    s.Position = dto.Position ?? "";
                    s.Top = dto.Top ?? 0;
                    s.Left = dto.Left ?? 0;
                    s.Right = dto.Right ?? 0;
                    s.Bottom = dto.Bottom ?? 0;

                    s.ZIndex = dto.ZIndex ?? 0;

                    s.HoverScale = dto.HoverScale ?? "";
                    s.HoverBorderRadius = dto.HoverBorderRadius ?? 0;
                    s.HoverShadowX = dto.HoverShadowX ?? 0;
                    s.HoverShadowY = dto.HoverShadowY ?? 0;
                    s.HoverShadowBlur = dto.HoverShadowBlur ?? 0;
                    s.HoverShadowColor = dto.HoverShadowColor ?? "#000000";

                    s.Cursor = dto.Cursor ?? "";

                    s.Display = dto.Display ?? "";
                    s.FlexDirection = dto.FlexDirection ?? "";
                    s.FlexJustify = dto.FlexJustify ?? "";
                    s.FlexAlign = dto.FlexAlign ?? "";
                    s.FlexWrap = dto.FlexWrap ?? "";
                    s.FlexGap = dto.FlexGap ?? 0;
                    s.FlexAlignItems = dto.FlexAlignItems ?? "";
                    s.AlignSelf = dto.AlignSelf ?? "";

                    s.NewComponentId = dto.NewComponentId ?? "";
                    s.ImageSource = dto.ImageSource ?? "";
                    s.IconSource = dto.IconSource ?? "";
                    s.LinkSource = dto.LinkSource ?? "";
                }
            }

            // Processa filhos recursivamente
            if (cdkDto.Children != null && cdkDto.Children.Any())
            {
                foreach (var child in cdkDto.Children)
                {
                    AddOrUpdateComponentRecursive(child, projectId, cdk.Id);
                }
            }
        }
    }
}
