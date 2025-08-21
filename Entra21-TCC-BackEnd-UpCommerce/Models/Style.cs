namespace Entra21_TCC_BackEnd_UpCommerce.Models
{
    public class Style
    {
        public int Id { get; set; }

        public int ?Width { get; set; }
        public int ?Height { get; set; }
        public int ?MarginLeft { get; set; }
        public int ?MarginTop { get; set; }
        public int ?MarginRight { get; set; }
        public int ?MarginBottom { get; set; }

        public int ?PaddingLeft { get; set; }
        public int ?PaddingTop { get; set; } 
        public int ?PaddingRight { get; set; }
        public int ?PaddingBottom { get; set; }

        public int ?BorderSize { get; set; }
        public string ?BorderColor { get; set; }
        public string ?BorderType { get; set; }

        public int ?BorderRadiusTopLeft { get; set; }
        public int ?BorderRadiusTopRight { get; set; }
        public int ?BorderRadiusBottomLeft { get; set; }
        public int ?BorderRadiusBottomRight { get; set; }

        public string ?BackgroundColor { get; set; }
        public string ?Color { get; set; }

        public string ?FontFamily { get; set; }
        public string ?TextContent { get; set; }
        public int ?FontSize { get; set; }
        public string ?FontWeight { get; set; }
        public string ?TextAlign { get; set; }

        public int ?Opacity { get; set; }

        public int ?ShadowX { get; set; }
        public int ?ShadowY { get; set; }
        public int ?ShadowBlur { get; set; }
        public string ?ShadowColor { get; set; }

        public string ?Position { get; set; }
        public int ?Top { get; set; }
        public int ?Left { get; set; }
        public int ?Right { get; set; }
        public int ?Bottom { get; set; }

        public int ?ZIndex { get; set; }

        public string ?HoverScale { get; set; }
        public int ?HoverBorderRadius { get; set; }
        public int ?HoverShadowX { get; set; }
        public int ?HoverShadowY { get; set; }
        public int ?HoverShadowBlur { get; set; }
        public string ?HoverShadowColor { get; set; }

        public string ?Cursor { get; set; }

        public string ?Display { get; set; }
        public string ?FlexDirection { get; set; }
        public string ?FlexJustify { get; set; }
        public string ?FlexAlign { get; set; }
        public string ?FlexWrap { get; set; }
        public int ?FlexGap { get; set; }
        public string ?FlexAlignItems { get; set; }
        public string ?AlignSelf { get; set; }

        public string ?NewComponentId { get; set; }

        public string ?ImageSource { get; set; }
        public string ?IconSource { get; set; }
        public string ?LinkSource { get; set; }

        public int? CdkId { get; set; }
        public Cdk? Cdk { get; set; }
    }
}
