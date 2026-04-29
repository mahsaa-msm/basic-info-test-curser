using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;

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

