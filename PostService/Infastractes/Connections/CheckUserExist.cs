using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;

namespace Infastractes.Connections;

public class CheckUserExist(IConnectionService connectionService) : ICheckUserExist
{
    private readonly IConnectionService connectionService = connectionService;

    public async Task CheckUserExistAsync(Guid userId)
    {
        await connectionService.CheckUserExistAsync(new CheckUserExistsRequest { Id = userId });
    }
}
