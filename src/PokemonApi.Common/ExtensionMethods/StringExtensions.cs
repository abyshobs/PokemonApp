using System.Text.RegularExpressions;

namespace PokemonApi.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveNewLineCharacters(this string rawString)
        {
            return Regex.Replace(rawString, @"\t|\n|\f|\r", " ");
        }
    }
}
