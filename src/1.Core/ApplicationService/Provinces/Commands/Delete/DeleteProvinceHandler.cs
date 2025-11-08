using Master.Data.Core.Contracts.Provinces.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Provinces.Entities;
using Master.Data.Core.RequestResponse.Provinces.Commands.Delete;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Provinces.Commands.Delete;

public class DeleteProvinceHandler : CommandHandler<DeleteProvinceCommand>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;

    public DeleteProvinceHandler(ZaminServices zaminServices,
                                IProvinceCommandRepository provinceCommandRepository)
        : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteProvinceCommand command)
    {
        var province = await _provinceCommandRepository.GetAsync(command.ProvinceId);
        EntityGuard.ThrowIfNull<Province, long>(province, ProjectTranslation.PROVINCE);

        province.Delete();

        var subordinates = await _provinceCommandRepository.GetSubordinateProvinces(province.Priority);

        subordinates?.ForEach(country => country.PullUp());

        await _provinceCommandRepository.CommitAsync();

        return Ok();
    }
}