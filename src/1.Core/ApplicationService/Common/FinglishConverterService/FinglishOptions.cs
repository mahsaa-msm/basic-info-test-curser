namespace Master.Data.Core.ApplicationService.Common.FinglishConverterService;

public class FinglishOptions
{
    public bool ToLowerCase { get; set; } = true;
    public bool RemoveDiacritics { get; set; } = false;
    public bool ReplaceSpacesWithHyphens { get; set; } = false;
}