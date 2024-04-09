using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;

namespace ProfileConnectionLib.ConnectionServices.RequestService.Interfaces;

public interface IRequestService
{
    Task<CheckUserExistsResponse> CheckUserExistAsync(CheckUserExistsRequest checkUser);
}