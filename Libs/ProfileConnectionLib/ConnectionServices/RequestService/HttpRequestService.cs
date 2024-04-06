using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ExampleCore.HttpLogic.Services.Requests.Data;
using ExampleCore.HttpLogic.Services.Interfaces;
using ExampleCore.HttpLogic.Services.Connection.Data;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;
using ProfileConnectionLib.ConnectionServices.RequestService.Interfaces;

namespace ProfileConnectionLib.ConnectionServices.RequestService;

public class HttpRequestService(IHttpRequestService httpRequestService) : IRequestService
{
    private readonly IHttpRequestService httpRequestService = httpRequestService;
    
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