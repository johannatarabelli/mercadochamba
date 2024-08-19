using backnc.Common.Response;
using backnc.Models;
using Microsoft.AspNetCore.Mvc;

namespace backnc.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> Authenticate(LoginUser userLogin);
        Task<BaseResponse> Register(RegisterUser userRegister);

        Task<BaseResponse> ValidateToken(string token);
    }
}
