using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNight.Core.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove diacritics from string, put it to lower case and remove spaces.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacriticsSpacesLower(this string text)
        {
            text = text.ToLower();
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var res = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            res = res.Replace(" ", "");
            return res;
        }
    }
}
