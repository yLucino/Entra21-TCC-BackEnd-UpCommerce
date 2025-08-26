using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlLogo { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public string ComponentJson { get; set; }

        public object GetComponentsObject()
        {
            if (string.IsNullOrWhiteSpace(ComponentJson))
                return new object[0];

            return JsonSerializer.Deserialize<object[]>(ComponentJson);
        }
    }
}
