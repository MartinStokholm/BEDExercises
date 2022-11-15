// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using HotelRestaurantAPI.Models;
using HotelRestaurantAPI.Models.Staff;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace HotelRestaurantAPI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public enum UserTypes
        {
            Receptionist,
            Waiter,
            KitchenStaff
        }

        [BindProperty]
        public InputModel Input { get; set; }

        
        public string ReturnUrl { get; set; }
        
        public class InputModel
        {
            [Required]
            public UserTypes UserType { get; set; }
            
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        private List<Claim> userClaims;
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {

                var user = CreateUser(Input.UserType);
                if (user == null)
                {
                    return Page();
                }

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var nameClaim = new Claim("FullName", Input.FirstName + " " + Input.LastName);
                    userClaims.Add(nameClaim);

                    foreach (var claim in userClaims)
                    {
                        var res = await _userManager.AddClaimAsync(user, claim);
                        Console.WriteLine(res.Succeeded);
                    }
                    
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            Console.WriteLine("Something broke");
            return Page();
        }
        

        // All the allowed claims are shown here
        private readonly Claim[] _claims = new Claim[4]
        {
            new Claim("FullName", ""),
            new Claim("ReceptionAccess","true"),
            new Claim("WaiterAccess", "true"),
            new Claim("KitchenAccess", "true")
        };
        private IdentityUser CreateUser(UserTypes type)
        {
            try
            {
                userClaims = new List<Claim>();
                switch (type)
                {
                    case UserTypes.Receptionist:
                    {
                        userClaims.Add(_claims[1]);
                        userClaims.Add(_claims[3]);
                        return new ReceptionUser(){FirstName = Input.FirstName, LastName = Input.LastName};
                    }
                    case UserTypes.Waiter:
                    {
                        userClaims.Add(_claims[2]);
                        userClaims.Add(_claims[3]);
                        return new WaiterUser(){ FirstName = Input.FirstName, LastName = Input.LastName };
                    }
                    case UserTypes.KitchenStaff:
                    {
                        userClaims.Add(_claims[3]);
                        return new KitchenstaffUser(){ FirstName = Input.FirstName, LastName = Input.LastName };
                    }
                }

                throw (new Exception("Invalid Usertype"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
