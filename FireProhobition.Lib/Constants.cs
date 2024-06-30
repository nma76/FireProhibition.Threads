using System.Globalization;
using System.Text.Json;

namespace FireProhobition.Lib
{
    internal class Constants
    {
        // Serialization
        internal static JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Number Format
        internal static NumberFormatInfo NumberFormat = new() { NumberDecimalSeparator = "." };

        // Data
        internal const string MunicipalityDataPath = @"./Data/Municipality.json";

        // API
        internal const string ApiBase = "https://api.msb.se/brandrisk/v2/";
        internal const string FireProhobitionEndpoint = "FireProhibition/sv/{0}/{1}";
    }
}
