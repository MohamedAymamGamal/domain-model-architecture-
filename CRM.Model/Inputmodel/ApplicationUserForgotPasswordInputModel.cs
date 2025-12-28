using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.Inputmodel
{
    public class ApplicationUserForgotPasswordInputModel : ApplicationUserVerificationBaseInputModel

    {

                public string? Password { get; set; }
    }
}
