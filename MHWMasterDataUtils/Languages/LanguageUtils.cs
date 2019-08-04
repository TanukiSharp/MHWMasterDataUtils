using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;

namespace MHWMasterDataUtils.Languages
{
    public static class LanguageUtils
    {
        public static LanguageIdPrimitive[] Languages { get; } = Enum.GetValues(typeof(LanguageIdPrimitive)).Cast<LanguageIdPrimitive>().ToArray();

        public static bool IsValidText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (text == "Invalid Message")
                return false;

            if (text.Contains("dummy"))
                return false;

            return true;
        }

        public static bool IsValidText(Dictionary<string, string> text)
        {
            if (text == null)
                return false;

            if (IsValidText(text[LanguageUtils.LanguageIdToLanguageCode(LanguageIdPrimitive.English)]) == false)
                return false;

            if (IsValidText(text[LanguageUtils.LanguageIdToLanguageCode(LanguageIdPrimitive.Japanese)]) == false)
                return false;

            return true;
        }

        public static bool IsValidText(Dictionary<LanguageIdPrimitive, Dictionary<uint, LanguageItem>> text, uint index)
        {
            if (IsValidText(text[LanguageIdPrimitive.English][index].Value) == false)
                return false;
            if (IsValidText(text[LanguageIdPrimitive.Japanese][index].Value) == false)
                return false;

            return true;
        }

        public delegate string LanguageValueProcessor(LanguageItem originalItem, string text);

        public static readonly LanguageValueProcessor ReplaceLineFeedWithSpace = (l, x) => x.Replace("\r\n", " ");
        public static readonly LanguageValueProcessor ReplaceAlphaSymbol = (l, x) => x.Replace("<ICON ALPHA>", l.Key == "eng" ? " α" : "α");
        public static readonly LanguageValueProcessor ReplaceBetaSymbol = (l, x) => x.Replace("<ICON BETA>", l.Key == "eng" ? " β" : "β");
        public static readonly LanguageValueProcessor ReplaceGammaSymbol = (l, x) => x.Replace("<ICON GAMMA>", l.Key == "eng" ? " γ" : "γ");

        public static Dictionary<string, string> CreateLocalizations(Dictionary<LanguageIdPrimitive, Dictionary<uint, LanguageItem>> source, uint entryId, LanguageValueProcessor[] valueProcessors = null)
        {
            var result = new LocalizedText();

            foreach (LanguageIdPrimitive languageId in Languages)
            {
                if (source.TryGetValue(languageId, out Dictionary<uint, LanguageItem> language) == false)
                    continue;

                LanguageItem item = language[entryId];
                string resultValue = item.Value;

                if (valueProcessors != null)
                {
                    foreach (LanguageValueProcessor valueProcessor in valueProcessors)
                    {
                        if (valueProcessor != null)
                            resultValue = valueProcessor?.Invoke(item, resultValue);
                    }
                }

                result.Add(LanguageIdToLanguageCode(languageId), resultValue);
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
