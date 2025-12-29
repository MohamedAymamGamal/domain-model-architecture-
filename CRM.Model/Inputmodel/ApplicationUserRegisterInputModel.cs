using CRM.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Model.Inputmodel
{
    public class ApplicationUserRegisterInputModel : ApplicationUserBaseInputModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }


    }

}
