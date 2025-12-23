using CRM.Model.ApplicaitionModels;
using CRM.Model.Inputmodel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.IService
{
    internal interface IAuthenticationService
    {
        Task<ResponseModel<bool>> LoginAsync(ApplicationUserRegisterInputModel model);
        Task<bool> RegisterAsync(ApplicationUserRegisterInputModel model);
        Task<bool> ForgotPasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> ChangePasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> RefreshTokenAsync(ApplicationUserRegisterInputModel model);
    }
}
