using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MHWMasterDataUtils.Languages
{
    public static class LanguageUtils
    {
        public const string DefaultLanguageCode = "eng";

        public static LanguageIdPrimitive[] Languages { get; } = Enum.GetValues(typeof(LanguageIdPrimitive)).Cast<LanguageIdPrimitive>().ToArray();

        public static bool IsValidText(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;

            if (message == "Invalid Message")
                return false;

            return true;
        }

        public static bool IsValidText(Dictionary<string, string> message)
        {
            if (message == null)
                return false;

            if (message["jpn"].Contains("dummy"))
                return false;

            return IsValidText(message[DefaultLanguageCode]);
        }

        public static Dictionary<string, string> CreateLocalizations(Dictionary<LanguageIdPrimitive, Dictionary<uint, LanguageItem>> source, uint entryId)
        {
            var result = new Dictionary<string, string>();

            foreach (LanguageIdPrimitive languageId in Languages)
            {
                if (source.TryGetValue(languageId, out Dictionary<uint, LanguageItem> language) == false)
                    continue;

                result.Add(LanguageIdToLanguageCode(languageId), language[entryId].Value);
            }

            return result;
        }

        public static string LanguageIdToLanguageCode(LanguageIdPrimitive language)
        {
            switch (language)
            {
                case LanguageIdPrimitive.Japanese: return "jpn";
                case LanguageIdPrimitive.English: return "eng";
                case LanguageIdPrimitive.French: return "fre";
                case LanguageIdPrimitive.Spanish: return "spa";
                case LanguageIdPrimitive.German: return "ger";
                case LanguageIdPrimitive.Italian: return "ita";
                case LanguageIdPrimitive.Korean: return "kor";
                case LanguageIdPrimitive.ChineseTraditional: return "cnt";
                case LanguageIdPrimitive.ChineseSimplified: return "cns";
                case LanguageIdPrimitive.Russian: return "rus";
                case LanguageIdPrimitive.Polish: return "pol";
                case LanguageIdPrimitive.Portuguese: return "ptb";
                case LanguageIdPrimitive.Arabic: return "ara";
            }

            throw new ArgumentException($"Invalid '{nameof(language)}' argument. Unknown value '{language}'.");
        }
    }
}
