using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Requests;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists.Responses;

namespace ProfileConnectionLib.ConnectionServices.Interfaces;

public interface IConnectionService
{
    /// <summary>
    /// Проверка наличия пользователя в системе
    /// </summary>
    /// <param name="checkUser"> модель пользователя, которого нужно проверить </param>
    /// <returns> 
    /// true - при отсутствии
    /// false - при наличии               
    /// </returns>
    Task<CheckUserExistsResponse> CheckUserExistAsync(CheckUserExistsRequest checkUser);
}
