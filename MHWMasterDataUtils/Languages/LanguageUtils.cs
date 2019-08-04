using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MHWMasterDataUtils.Core;
using System.Text.RegularExpressions;

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

        public delegate string LanguageValueProcessor(LanguageIdPrimitive language, string text);

        public static readonly LanguageValueProcessor ReplaceLineFeedWithSpace = (l, x) => x.Replace("\r\n", " ");

        private static readonly Regex GreekLetterIconRegex = new Regex(@"\<ICON\s+([A-Z]+)\>");
        public static string ReplaceGreekLetterSymbol(LanguageIdPrimitive _, string text)
        {
            bool NeedSpace(Match m)
            {
                if (m.Groups[0].Index > 0 && char.IsWhiteSpace(text[m.Groups[0].Index - 1]) == false)
                    return true;

                return false;
            }

            return GreekLetterIconRegex.Replace(text, match =>
            {
                if (match.Groups.Count > 0)
                {
                    switch (match.Groups[1].Value)
                    {
                        case "ALPHA": return NeedSpace(match) ? " α" : "α";
                        case "BETA": return NeedSpace(match) ? " β" : "β";
                        case "GAMMA": return NeedSpace(match) ? " γ" : "γ";
                    }
                }

                return match.Value;
            });
        }

        private static readonly Regex RemoveStyleRegex = new Regex(@"(\<STYL\s+[A-Z_]+\>)|(\</STYL\>)");
        public static string StyleTextRemover(LanguageIdPrimitive _, string text)
        {
            return RemoveStyleRegex.Replace(text, string.Empty);
        }

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
                            resultValue = valueProcessor?.Invoke(languageId, resultValue);
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
