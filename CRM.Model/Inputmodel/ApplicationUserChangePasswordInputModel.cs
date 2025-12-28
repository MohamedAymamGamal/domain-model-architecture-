using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.Inputmodel
{
    public class ApplicationUserChangePasswordInputModel
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
