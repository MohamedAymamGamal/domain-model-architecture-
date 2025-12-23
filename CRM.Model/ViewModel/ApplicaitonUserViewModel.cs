using CRM.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewModel
{
    public class ApplicaitonUserViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public short? VerificationCode { get; set; }
        public string? ImageName { get; set; }
        public bool? Activity { get; set; }
    }
}
