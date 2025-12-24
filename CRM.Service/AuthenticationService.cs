using CRM.Model.ApplicaitionModels;
using CRM.Model.IdentityModels;
using CRM.Model.Inputmodel;
using CRM.Service.IService;
using CRM.Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , ApplicationEmailSender emailService) : IAuthenticationService 
    {
       
        private short GenerateCode()
        {
            var random = new Random();
            return (short)random.Next(1000, 9999);
        }
        public Task<bool> ChangePasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserRegisterInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            var user = userManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    Data = false
                };
            }
            if(user.EmailConfirmed)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Email already confirmed",
                    Data = false
                };
            }
            user.VerificationCode = GenerateCode();
            var result = userManager.UpdateAsync(user).Result;
            
            if (!result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Failed to save verification code",
                   
                };

            }

        }

        public Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            ArgumentNullException.ThrowIfNull(model.Password);

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Data = true
                };
            }

            string errorMessage = result.IsLockedOut ? "User is locked out." :
                                  result.IsNotAllowed ? "Login is not allowed." :
                                  result.RequiresTwoFactor ? "Two-factor authentication is required." :
                                  "Invalid login attempt.";

            return new ResponseModel<bool>
            {
                IsSuccess = false,
                Message = errorMessage,
                Data = false
            };

        }

       

        public Task<bool> RefreshTokenAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> RegisterAsync(ApplicationUserRegisterInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            ArgumentNullException.ThrowIfNull(model.Password);

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                    Message = "User created successfully",
                    Data = true
                };
            }

            string errorMessage = result.Errors.Any()
                ? string.Join("; ", result.Errors.Select(e => e.Code))
                : "Unable to register user due to unknown errors.";

            return new ResponseModel<bool>
            {
                IsSuccess = false,
                Message = errorMessage,
                Data = false
            };
        }

        public Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ConfirmEmailVerifyCodeAsync(ApplicationUserConfirmEmailInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
