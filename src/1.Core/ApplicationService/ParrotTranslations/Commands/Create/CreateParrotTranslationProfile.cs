using AutoMapper;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Parameters;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Create;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Commands.Create;

public class CreateParrotTranslationProfile : Profile
{
    public CreateParrotTranslationProfile()
    {
        CreateMap<CreateParrotTranslationCommand, CreateParrotTranslationParameter>().ReverseMap();
    }
}

