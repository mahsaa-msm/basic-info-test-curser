using AutoMapper;
using Vehicle.Insurance.Core.Domain.ParrotTranslations.Parameters;
using Vehicle.Insurance.Core.RequestResponse.ParrotTranslations.Commands.Update;

namespace Vehicle.Insurance.Core.ApplicationService.ParrotTranslations.Commands.Update;

public class UpdateParrotTranslationProfile : Profile
{
    public UpdateParrotTranslationProfile()
    {
        CreateMap<UpdateParrotTranslationCommand, UpdateParrotTranslationParameter>().ReverseMap();
    }
}

