namespace Entra21_TCC_BackEnd_UpCommerce.Dtos
{
    public class ProjectDto
    {
        public string UrlLogo { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public List<CdkDto> Component { get; set; }
    }
}
