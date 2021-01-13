using System;
namespace Theta.Resources
{
    public static class Images
    {
        const string SvgResourcesUrl = "resource://Theta.Resources";

        // Svg assembly resources
        public static string Menu => GetSvgImageUrl("menu");
        public static string BackArrow => GetSvgImageUrl("back_arrow");
        public static string Plus => GetSvgImageUrl("plus");

        static string GetSvgImageUrl(string name) => $"{SvgResourcesUrl}.{name}.svg";
    }
}
