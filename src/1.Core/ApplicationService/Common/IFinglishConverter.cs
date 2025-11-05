namespace Master.Data.Core.ApplicationService.Common;

public interface IFinglishConverter
{
    string Convert(string persianText);
    string Convert(string persianText, FinglishOptions options);
}