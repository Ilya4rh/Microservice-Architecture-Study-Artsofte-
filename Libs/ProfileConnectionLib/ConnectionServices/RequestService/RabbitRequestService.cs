using ExampleCore.RabbitLogic.Service.Interfaces;
using Newtonsoft.Json;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;
using ProfileConnectionLib.ConnectionServices.RequestService.Interfaces;

namespace ProfileConnectionLib.ConnectionServices.RequestService;

public class RabbitRequestService(IRabbitService rabbitService) : IRequestService
{
    private readonly IRabbitService rabbitService = rabbitService;
    
    public async Task<CheckUserExistsResponse> CheckUserExistAsync(CheckUserExistsRequest checkUser)
    {
        var responseJson = await rabbitService.SendMessage(JsonConvert.SerializeObject(checkUser));
        
        if (string.IsNullOrEmpty(responseJson))
            throw new Exception("Тест не найден");
        
        return JsonConvert.DeserializeObject<CheckUserExistsResponse>(responseJson);
    }
}