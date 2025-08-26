using Entra21_TCC_BackEnd_UpCommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Dtos
{
    public class CdkDto
    {
        public string Id { get; set; }
        public string CdkId { get; set; }
        public string? ParentCdkId { get; set; }
        public StyleDto Style { get; set; }
        public List<CdkDto> Children { get; set; } = new List<CdkDto>();
    }

}
