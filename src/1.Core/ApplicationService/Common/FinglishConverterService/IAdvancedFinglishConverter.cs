namespace Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;

public interface IAdvancedFinglishConverter
{
    List<string> ConvertList(IEnumerable<string> persianList, FinglishOptions options = null);
    string ConvertTextWithContext(string persianText, TextContext context = TextContext.General);
    Dictionary<string, string> CreateMappingTable(IEnumerable<string> persianWords);
}
