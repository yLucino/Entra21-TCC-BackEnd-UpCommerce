using Entra21_TCC_BackEnd_UpCommerce.Models;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Dtos
{
    public class ProjectDto
    {
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlLogo { get; set; } = string.Empty;
        public int UserId { get; set; }

        public List<object> Component { get; set; }
    }
}
