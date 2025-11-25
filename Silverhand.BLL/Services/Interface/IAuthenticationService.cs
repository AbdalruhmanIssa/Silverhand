using Microsoft.AspNetCore.Http;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgotPassword(ForgetPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
