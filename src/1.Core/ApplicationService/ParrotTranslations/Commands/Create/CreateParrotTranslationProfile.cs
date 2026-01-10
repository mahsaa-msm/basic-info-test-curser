using AutoMapper;
using Master.Data.Core.Domain.ParrotTranslations.Parameters;
using Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Create;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Commands.Create;

public class CreateParrotTranslationProfile : Profile
{
    public CreateParrotTranslationProfile()
    {
        CreateMap<CreateParrotTranslationCommand, CreateParrotTranslationParameter>().ReverseMap();
    }
}
