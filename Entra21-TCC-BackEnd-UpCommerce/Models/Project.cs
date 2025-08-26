using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string UrlLogo { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ICollection<Cdk> Component { get; set; } = new List<Cdk>();
    }
}
