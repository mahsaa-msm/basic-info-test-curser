namespace Master.Data.Core.ApplicationService.Common.FinglishConverterService;

public interface IFinglishConverter
{
    string Convert(string persianText);
    string Convert(string persianText, FinglishOptions options);
}