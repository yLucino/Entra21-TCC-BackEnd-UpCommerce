using System.ComponentModel.DataAnnotations.Schema;

namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Cdk
    {
        public int Id { get; set; }
        public string CdkId { get; set; }

        public int? ParentCdkId { get; set; }
        [ForeignKey("ParentCdkId")]
        public Cdk? Parent { get; set; }

        public ICollection<Cdk>? Children { get; set; }

        public Style? Style { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
