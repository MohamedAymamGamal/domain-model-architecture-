using CRM.Model.Enums;
using CRM.Model.IdentityModels;
using CRM.Model.ViewModel;
using System.Diagnostics;

namespace CRM.Model.ViewModels
{
    public class ApplicationUserProfileViewModel : ApplicaitonUserViewModel
    {
        public string? FullName => $"{FirstName} {LastName}";
        public string? Token { get; set; }

        
        public ApplicationUserProfileViewModel(ApplicationUser model)
        {
            Id = model.Id;
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Gender = model.Gender;
            DateOfBirth = model.DateOfBirth;
            RegistrationDate = model.RegistrationDate;
            ImageName = model.ImageName;
            Activity = model.Activity;
        }
    }
}