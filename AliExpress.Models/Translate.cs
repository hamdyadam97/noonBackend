using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AliExpress.Models
{
    public static class Translate
    {
        public static string translate(string word, string fromLanguage = "en", string toLanguage = "ar")
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            using (var webClient = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                try
                {
                    var result = webClient.DownloadString(url);
                    return ParseResult(result);
                }
                catch (WebException ex)
                {
                    // Log error or handle it appropriately
                    return $"Error accessing translation service: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }

        private static string ParseResult(string result)
        {
            try
            {
                int firstQuoteIndex = result.IndexOf('\"') + 1;
                int secondQuoteIndex = result.IndexOf('\"', firstQuoteIndex);
                return result.Substring(firstQuoteIndex, secondQuoteIndex - firstQuoteIndex);
            }
            catch
            {
                return "Error parsing translation result";
            }
        }
    }
}
