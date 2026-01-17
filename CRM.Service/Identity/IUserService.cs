using CRM.Model.ApplicaitionModels;
using CRM.Model.IdentityModels;
using CRM.Model.Inputmodel;
using CRM.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Identity
{
    public interface IUserService
    {
        Task<ResponseModel<ApplicationUserProfileViewModel>> GetUserProfileAsync(ApplicationUserContext userContext);
        Task<ResponseModel<bool>> UpdateUserProfileAsync(ApplicationUserProfileViewModel model, ApplicationUserContext userContext );

        Task<ResponseModel<bool>> ChangePasswordAsync(ApplicationUserChangePasswordInputModel model, ApplicationUserContext userContext);

    }
}
