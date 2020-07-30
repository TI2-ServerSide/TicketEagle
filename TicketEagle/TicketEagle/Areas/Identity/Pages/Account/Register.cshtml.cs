﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TicketEagle.Areas.Identity.Data;
using TicketEagle.Data;

namespace TicketEagle.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<TicketEagleUser> _signInManager;
        private readonly UserManager<TicketEagleUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        //aqui
         private readonly TEDbContext _context;

       

        public RegisterModel(
            TEDbContext context,
            UserManager<TicketEagleUser> userManager,
            SignInManager<TicketEagleUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
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

            [Display(Name="Tipo de Utilizador")]
            public string Name { get; set; }

            [Display(Name="Fotografia")]
            public string Foto { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ViewData["roles"] = new SelectList(_context.Roles,"Id","Name");
            ReturnUrl = returnUrl;

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var role = _roleManager.FindByIdAsync(Input.Name).Result;
            
            
            //criar um novo usuario na tabela respetiva dependendo do role
            if (role.Name.Equals("Utilizador"))
            {
                var user2 = new Models.Utilizador { Nome = Input.Email, Email = Input.Email,Foto=Input.Foto,Password=Input.Password };
                _context.Add(user2);
            }else if (role.Name.Equals("Promotor"))
            {
                var user = new Models.Promotor { Nome = Input.Email };
                _context.Add(user);
            }else if(role.Name.Equals("Admin") && Input.Email.Contains("@t_eagle.com"))
            {
                //se pode ser admin se for do dominio da empresa (email confirmado)
                //se for admin precisa de ter dados utilizador e promotor
                var u= new Models.Utilizador { Nome = Input.Email, Email = Input.Email, Foto = Input.Foto, Password = Input.Password };
                var u2= new Models.Promotor { Nome = Input.Email };
                _context.Add(u);
                _context.Add(u2);
            }

            if (ModelState.IsValid)
            { 
                var user = new TicketEagleUser { UserName = Input.Email, Email = Input.Email, Nome=Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                   await _userManager.AddToRoleAsync(user, role.Name);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                         "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
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
                        ViewData["roles"] = new SelectList(_context.Roles, "Id", "Name");
                        return LocalRedirect(returnUrl);
                    }
                   // await _signInManager.SignInAsync(user, isPersistent: false);
                   // return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ViewData["roles"] = new SelectList(_context.Roles, "Id", "Name");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
