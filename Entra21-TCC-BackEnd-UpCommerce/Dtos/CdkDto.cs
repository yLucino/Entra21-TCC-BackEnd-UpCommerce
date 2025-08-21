namespace Entra21_TCC_BackEnd_UpCommerce.Dtos
{
    public class CdkDto
    {
        public string Id { get; set; }
        public string CdkId { get; set; }
        public StyleDto Style { get; set; }

        public List<CdkDto>? Children { get; set; }
    }
}
