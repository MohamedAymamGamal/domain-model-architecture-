using CRM.Model.ApplicaitionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Identity
{
    public interface IUserService
    {
        Task<ResponseModel<bool>> UpdateUserProfileAsync();

        Task<ResponseModel<bool>> ChangePasswordAsync();

    }
}
