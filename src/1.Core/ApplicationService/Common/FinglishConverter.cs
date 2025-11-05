using System.Text;
using System.Text.RegularExpressions;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Core.ApplicationService.Common;
public class FinglishConverter : ISingletoneLifetime, IFinglishConverter
{
    private readonly Dictionary<char, string> _persianToEnglishMap;
    private readonly Dictionary<string, string> _specialWordsMap;

    public FinglishConverter()
    {
        _persianToEnglishMap = CreateCharacterMap();
        _specialWordsMap = CreateSpecialWordsMap();
    }

    private Dictionary<char, string> CreateCharacterMap()
    {
        var map = new Dictionary<char, string>
    {
        // حروف پایه
        {'ا', "a"}, {'آ', "a"}, {'أ', "a"}, {'إ', "e"}, {'ء', "'"},
        {'ب', "b"}, {'پ', "p"}, {'ت', "t"}, {'ث', "s"},
        {'ج', "j"}, {'چ', "ch"}, {'ح', "h"}, {'خ', "kh"},
        {'د', "d"}, {'ذ', "z"}, {'ر', "r"}, {'ز', "z"},
        {'ژ', "zh"}, {'س', "s"}, {'ش', "sh"}, {'ص', "s"},
        {'ض', "z"}, {'ط', "t"}, {'ظ', "z"}, {'ع', "'"},
        {'غ', "gh"}, {'ف', "f"}, {'ق', "gh"}, {'ک', "k"},
        {'گ', "g"}, {'ل', "l"}, {'م', "m"}, {'ن', "n"},
        {'و', "v"}, {'ه', "h"}, {'ی', "y"}, {'ئ', "'"},
        {'ة', "h"}, {'ك', "k"}, {'ي', "y"}, {'ؤ', "'"},
        
        // اعداد
        {'۰', "0"}, {'۱', "1"}, {'۲', "2"}, {'۳', "3"}, {'۴', "4"},
        {'۵', "5"}, {'۶', "6"}, {'۷', "7"}, {'۸', "8"}, {'۹', "9"},
        
        // علائم نگارشی
        {' ', " "}, {'.', "."}, {',', ","}, {'،', ","}, {'؛', ";"},
        {'?', "?"}, {'؟', "?"}, {'!', "!"}, {'(', "("},
        {')', ")"}, {'[', "["}, {']', "]"}, {'{', "{"}, {'}', "}"}
    };

        // بررسی تکراری نبودن کلیدها (برای دیباگ)
#if DEBUG
        var duplicates = map.GroupBy(x => x.Key)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .ToList();
        if (duplicates.Any())
        {
            throw new InvalidOperationException($"کلیدهای تکراری در دیکشنری: {string.Join(", ", duplicates)}");
        }
#endif

        return map;
    }
    private Dictionary<string, string> CreateSpecialWordsMap()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"الله", "allah"},
                {"الهی", "elaahi"},
                {"سلام", "salaam"},
                {"خداحافظ", "khodaahaafez"},
                {"متشکرم", "motashakkeram"},
                {"بله", "bale"},
                {"خیر", "kheyr"},
                {"باشد", "baashad"},
                {"می", "mi"},
                {"نمی", "nemi"},
                {"ها", "ha"},
                {"های", "haaye"},
                {"تر", "tar"},
                {"ترین", "tarin"}
            };
    }

    public string Convert(string persianText)
    {
        if (string.IsNullOrEmpty(persianText))
            return string.Empty;

        // نرمال سازی متن
        string normalizedText = NormalizeText(persianText);

        // تبدیل کلمات خاص
        string processedText = ProcessSpecialWords(normalizedText);

        // تبدیل کاراکتر به کاراکتر
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < processedText.Length; i++)
        {
            char currentChar = processedText[i];

            // بررسی برای حروف "ا" و "و" که ممکن است مصوت باشند
            if (currentChar == 'ا' || currentChar == 'و')
            {
                string converted = HandleVowels(currentChar, processedText, i, result);
                if (converted != null)
                {
                    result.Append(converted);
                    continue;
                }
            }

            // بررسی برای "ه" در پایان کلمات
            if (currentChar == 'ه' && IsEndOfWord(processedText, i))
            {
                result.Append("eh");
                continue;
            }

            // تبدیل معمول
            if (_persianToEnglishMap.TryGetValue(currentChar, out string englishChar))
            {
                result.Append(englishChar);
            }
            else
            {
                result.Append(currentChar);
            }
        }

        return PostProcess(result.ToString());
    }

    private string NormalizeText(string text)
    {
        // حذف نیم‌فاصله و تبدیل به فاصله معمولی
        text = text.Replace("\u200C", " ");

        // نرمال سازی شکل حروف
        text = text.Replace('ك', 'ک').Replace('ي', 'ی');

        return text;
    }

    private string ProcessSpecialWords(string text)
    {
        string result = text;
        foreach (var specialWord in _specialWordsMap)
        {
            result = Regex.Replace(result,
                $@"\b{Regex.Escape(specialWord.Key)}\b",
                specialWord.Value,
                RegexOptions.RightToLeft);
        }
        return result;
    }

    private string HandleVowels(char currentChar, string text, int currentIndex, StringBuilder currentResult)
    {
        if (currentChar == 'ا')
        {
            // اگر "ا" در ابتدای کلمه باشد یا بعد از صامت باشد، معمولاً "a" است
            if (currentIndex == 0 || IsConsonant(text[currentIndex - 1]))
            {
                return "a";
            }
            // اگر بعد از مصوت باشد، ممکن است نشان‌دهنده کشیدگی باشد
            else if (IsVowel(text[currentIndex - 1]))
            {
                return "a";
            }
        }
        else if (currentChar == 'و')
        {
            // "و" می‌تواند "v" یا "o" یا "u" باشد
            if (currentIndex > 0 && IsConsonant(text[currentIndex - 1]))
            {
                if (currentIndex + 1 < text.Length && IsConsonant(text[currentIndex + 1]))
                {
                    return "o"; // و بین دو صامت
                }
                else
                {
                    return "u"; // و در پایان هجا
                }
            }
        }

        return null; // از تبدیل معمول استفاده شود
    }

    private bool IsConsonant(char c)
    {
        string consonants = "بپتثجچحخدذرزژسشصضطظعغفقکگلمن";
        return consonants.Contains(c);
    }

    private bool IsVowel(char c)
    {
        string vowels = "اایو";
        return vowels.Contains(c);
    }

    private bool IsEndOfWord(string text, int index)
    {
        return index == text.Length - 1 ||
               char.IsWhiteSpace(text[index + 1]) ||
               char.IsPunctuation(text[index + 1]);
    }

    private string PostProcess(string text)
    {
        // اصلاحات نهایی
        text = Regex.Replace(text, @"a+a", "a"); // حذف aهای تکراری
        text = Regex.Replace(text, @"e+e", "e"); // حذف eهای تکراری
        text = Regex.Replace(text, @"o+o", "o"); // حذف oهای تکراری
        text = Regex.Replace(text, @"u+u", "u"); // حذف uهای تکراری

        // اصلاح حروف تعریف
        text = Regex.Replace(text, @"\b(a|e) (a|e)i\b", "$1 $2i");

        return text.Trim();
    }

    // متد برای تبدیل با گزینه‌های پیشرفته
    public string Convert(string persianText, FinglishOptions options)
    {
        string result = Convert(persianText);

        if (options != null)
        {
            if (options.ToLowerCase)
                result = result.ToLower();

            if (options.RemoveDiacritics)
                result = RemoveDiacritics(result);

            if (options.ReplaceSpacesWithHyphens)
                result = result.Replace(' ', '-');
        }

        return result;
    }

    private string RemoveDiacritics(string text)
    {
        return text.Replace("'", "").Replace("`", "");
    }
}

public class AdvancedFinglishConverter : FinglishConverter, ISingletoneLifetime, IAdvancedFinglishConverter
{
    private readonly Dictionary<string, string> _commonPhrasesMap;

    public AdvancedFinglishConverter()
    {
        _commonPhrasesMap = CreateCommonPhrasesMap();
    }

    private Dictionary<string, string> CreateCommonPhrasesMap()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"علیه السلام", "alayhe salaam"},
                {"رضی الله عنه", "razi allaho anho"},
                {"سلام علیکم", "salaam alaykom"},
                {"وعلیکم السلام", "va alaykom al-salaam"},
                {"ان شاء الله", "inshaallah"},
                {"ما شاء الله", "mashaallah"},
                {"الحمد لله", "alhamdulillah"},
                {"سبحان الله", "sobhanallah"},
                {"استغفر الله", "astaghfrollah"},
                {"یا الله", "ya allah"}
            };
    }

    public string ConvertTextWithContext(string persianText, TextContext context = TextContext.General)
    {
        string result = Convert(persianText);

        // پردازش بر اساس context
        switch (context)
        {
            case TextContext.Names:
                result = ProcessNames(result);
                break;
            case TextContext.Religious:
                result = ProcessReligiousText(result);
                break;
            case TextContext.Scientific:
                result = ProcessScientificText(result);
                break;
        }

        return result;
    }

    private string ProcessReligiousText(string text)
    {
        foreach (var phrase in _commonPhrasesMap)
        {
            text = text.Replace(phrase.Key, phrase.Value);
        }
        return text;
    }

    private string ProcessNames(string text)
    {
        // برای نام‌ها، اولین حرف بزرگ می‌ماند
        return text;
    }

    private string ProcessScientificText(string text)
    {
        // حفظ اصطلاحات علمی
        return text;
    }

    public List<string> ConvertList(IEnumerable<string> persianList, FinglishOptions options = null)
    {
        return persianList.Select(text => Convert(text, options ?? new FinglishOptions())).ToList();
    }

    public Dictionary<string, string> CreateMappingTable(IEnumerable<string> persianWords)
    {
        return persianWords.ToDictionary(
            word => word,
            word => Convert(word),
            StringComparer.OrdinalIgnoreCase);
    }
}

public enum TextContext
{
    General,
    Names,
    Religious,
    Scientific
}

public class FinglishOptions
{
    public bool ToLowerCase { get; set; } = true;
    public bool RemoveDiacritics { get; set; } = false;
    public bool ReplaceSpacesWithHyphens { get; set; } = false;
}