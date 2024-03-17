using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExampleCore.HttpLogic.Services.Requests.Data;
using ExampleCore.HttpLogic.Services.Interfaces;
using ExampleCore.HttpLogic.Services.Connection.Data;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;

namespace ProfileConnectionLib.ConnectionServices;

public class ConnectionService : IConnectionService
{
    private readonly IHttpRequestService httpRequestService;

    public ConnectionService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        if (configuration.GetSection("Connection").Value == "http")
        {
            httpRequestService = serviceProvider.GetRequiredService<IHttpRequestService>();
        }
        else
        {
            // RPC по rabbit
        }
    }

    public async Task<CheckUserExistsResponse> CheckUserExistAsync(CheckUserExistsRequest checkUser)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri("https://localhost:7002/api/users/id"),
            QueryParameterList = [new("userId", checkUser.Id.ToString())]
        };

        var connectionData = new HttpConnectionData();
        var response = await httpRequestService.SendRequestAsync<CheckUserExistsResponse>(requestData, connectionData);

        if (!response.IsSuccessStatusCode)
            throw new Exception("The user is not found");

        return response.Body;
    }
}
