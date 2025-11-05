
namespace Master.Data.Core.ApplicationService.Common;

public interface IAdvancedFinglishConverter
{
    List<string> ConvertList(IEnumerable<string> persianList, FinglishOptions options = null);
    string ConvertTextWithContext(string persianText, TextContext context = TextContext.General);
    Dictionary<string, string> CreateMappingTable(IEnumerable<string> persianWords);
}