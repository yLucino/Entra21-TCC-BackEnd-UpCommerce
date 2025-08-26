using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Cdk
    {
        [Key]
        [Column(TypeName = "varchar(255)")]
        public string Id { get; set; }
        public string CdkId { get; set; }

        public string? ParentCdkId { get; set; }
        [JsonIgnore]
        public Cdk? Parent { get; set; }

        public ICollection<Cdk> Children { get; set; } = new List<Cdk>();

        public Style? Style { get; set; }

        public int? ProjectId { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
    }
}
