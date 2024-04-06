using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExampleCore.HttpLogic.Services.Requests.Data;
using ExampleCore.HttpLogic.Services.Interfaces;
using ExampleCore.HttpLogic.Services.Connection.Data;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;
using ProfileConnectionLib.ConnectionServices.RequestService.Interfaces;

namespace ProfileConnectionLib.ConnectionServices;

public class ConnectionService(IRequestService requestService) : IConnectionService
{
    private readonly IRequestService requestService = requestService;

    public Task<CheckUserExistsResponse> CheckUserExistAsync(CheckUserExistsRequest checkUser)
    {
        return requestService.CheckUserExistAsync(checkUser);
    }
}
