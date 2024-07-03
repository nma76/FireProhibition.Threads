using System.Globalization;
using System.Text.Json;

namespace FireProhibition.Lib
{
    internal class Constants
    {
        // Number Format
        internal static NumberFormatInfo NumberFormat = new() { NumberDecimalSeparator = "." };

        // Data
        internal const string DataPath = @"./Data/Locations.json";

        // API
        internal const string ApiBase = "https://api.msb.se/brandrisk/v2/";
        internal const string FireProhibitionEndpoint = "FireProhibition/sv/{0}/{1}";
    }
}
