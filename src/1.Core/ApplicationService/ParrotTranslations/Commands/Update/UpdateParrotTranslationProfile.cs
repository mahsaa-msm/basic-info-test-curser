using AutoMapper;
using Master.Data.Core.Domain.ParrotTranslations.Parameters;
using Master.Data.Core.RequestResponse.ParrotTranslations.Commands.Update;

namespace Master.Data.Core.ApplicationService.ParrotTranslations.Commands.Update;

public class UpdateParrotTranslationProfile : Profile
{
    public UpdateParrotTranslationProfile()
    {
        CreateMap<UpdateParrotTranslationCommand, UpdateParrotTranslationParameter>().ReverseMap();
    }
}
