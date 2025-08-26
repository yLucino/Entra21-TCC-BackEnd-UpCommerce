using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Style
    {
        [Key]
        public int Id { get; set; }

        public string CdkId { get; set; } = null!;
        [JsonIgnore]
        public Cdk Cdk { get; set; } = null!;

        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public int MarginLeft { get; set; } = 0;
        public int MarginTop { get; set; } = 0;
        public int MarginRight { get; set; } = 0;
        public int MarginBottom { get; set; } = 0;

        public int PaddingLeft { get; set; } = 0;
        public int PaddingTop { get; set; } = 0;
        public int PaddingRight { get; set; } = 0;
        public int PaddingBottom { get; set; } = 0;

        public int BorderSize { get; set; } = 0;
        public string BorderColor { get; set; } = "";
        public string BorderType { get; set; } = "";

        public int BorderRadiusTopLeft { get; set; } = 0;
        public int BorderRadiusTopRight { get; set; } = 0;
        public int BorderRadiusBottomLeft { get; set; } = 0;
        public int BorderRadiusBottomRight { get; set; } = 0;

        public string BackgroundColor { get; set; } = "";
        public string Color { get; set; } = "";

        public string FontFamily { get; set; } = "";
        public string TextContent { get; set; } = "";
        public int FontSize { get; set; } = 0;
        public string FontWeight { get; set; } = "";
        public string TextAlign { get; set; } = "";

        public double Opacity { get; set; } = 1;

        public int ShadowX { get; set; } = 0;
        public int ShadowY { get; set; } = 0;
        public int ShadowBlur { get; set; } = 0;
        public string ShadowColor { get; set; } = "#000000";

        public string Position { get; set; } = "";
        public int Top { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Right { get; set; } = 0;
        public int Bottom { get; set; } = 0;

        public int ZIndex { get; set; } = 0;

        public string HoverScale { get; set; } = "";
        public int HoverBorderRadius { get; set; } = 0;
        public int HoverShadowX { get; set; } = 0;
        public int HoverShadowY { get; set; } = 0;
        public int HoverShadowBlur { get; set; } = 0;
        public string HoverShadowColor { get; set; } = "#000000";

        public string Cursor { get; set; } = "";

        public string Display { get; set; } = "";
        public string FlexDirection { get; set; } = "";
        public string FlexJustify { get; set; } = "";
        public string FlexAlign { get; set; } = "";
        public string FlexWrap { get; set; } = "";
        public int FlexGap { get; set; } = 0;
        public string FlexAlignItems { get; set; } = "";
        public string AlignSelf { get; set; } = "";

        public string NewComponentId { get; set; } = "";
        public string ImageSource { get; set; } = "";
        public string IconSource { get; set; } = "";
        public string LinkSource { get; set; } = "";
    }
}
