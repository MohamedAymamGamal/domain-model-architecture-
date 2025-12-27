using Azure.Core;
using CRM.API.Service;
using CRM.Model.ApplicaitionModels;
using CRM.Model.IdentityModels;
using CRM.Model.Inputmodel;
using CRM.Service.Helper;
using CRM.Service.IService;
using CRM.Utility;
using CRM.Utility.IUtitlity;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace CRM.Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationEmailSender emailService,JsonLocalizationService localize,IApplicationEmailSender applicationEmailSender

        ) : IAuthenticationService
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

        public async Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserConfirmEmailInputModel model)
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
            model.Code = user.VerificationCode.ToString();
            model.FullName = $"{user.FirstName} {user.LastName}";
            await SendEmailConfirmationCodeAsync(model);
            return new ResponseModel<bool>
            {
                IsSuccess = true,
                Message = "Verification code sent to email",
               
            };

        }

        public Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserForgotPasswordInputModel model)
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
                    Message = localize.localize("Register.Login_successful"),
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

        public  Task<ResponseModel<bool>> ResetPasswordAsync(ApplicationUserForgotPasswordInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> ConfirmEmailVerifyCodeAsync(ApplicationUserConfirmEmailInputModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            model.FullName = $"{user.FirstName} {user.LastName}";
            bool isCodeValid = user.VerificationCode != null && user.VerificationCode.ToString() == model.Code;
            if (isCodeValid) { 
                user.EmailConfirmed = true;
                user.Activity = true;
                var result = await userManager.UpdateAsync(user);
                return new ResponseModel<bool>
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Succeeded ? "Email confirmed successfully" : "Failed to confirm email",
                    Data = result.Succeeded
                };
            }else
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid verification code",
                    
                };
            }
        }


        private async Task SendEmailConfirmationCodeAsync(ApplicationUserVerificationBaseInputModel model)
        {
            var emailContent = TemplateHelper.LoadTemplate("confirm-email.html", new Dictionary<string, string>
            {
                { "FullName", model.FullName },
                { "Code", model.Code }
            });

            MailMessage mail = new();
            mail.To.Add(model.Email);
            mail.Subject = "CRM | Email Confirmation";

            var alternateView = AlternateView.CreateAlternateViewFromString(
                emailContent,
                null,
                MediaTypeNames.Text.Html
            );
            
            mail.AlternateViews.Add(alternateView);

            await emailService.SendEmailAsync(mail);
        }


    }
}
