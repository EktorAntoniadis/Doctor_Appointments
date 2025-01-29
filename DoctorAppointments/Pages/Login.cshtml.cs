using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

public class LoginModel : PageModel
{
    private readonly IUserRepository _userRepository;
    private PasswordHasher<User> _hasher;

    public LoginModel(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _hasher = new PasswordHasher<User>();
    }
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPost()
    {
        if(Username  == null || Password == null)
        {
            ErrorMessage = "Please fill all the fields";
            return Page();
        }

        var user = _userRepository.GetByUserName(Username);

        if (user == null) 
        {
            ErrorMessage = "Wrong username";
            return Page();
        }

        var hashedPasswordResult = _hasher.VerifyHashedPassword(user, user.Password, Password);

        if(hashedPasswordResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var newHashedPassword = _hasher.HashPassword(user, Password);
            user.Password = newHashedPassword;
            _userRepository.Update(user);
        }

        if(hashedPasswordResult == PasswordVerificationResult.Failed)
        {
            ErrorMessage = "Wrong password";
            return Page();
        }

        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetInt32("UserId", user.UserId);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "DoctorAppointmentsScheme");

        var authenticationProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddHours(1),
        };

        await HttpContext.SignInAsync("DoctorAppointmentsScheme", new ClaimsPrincipal(claimsIdentity), authenticationProperties);

        return RedirectToPage("/Appointments");
    }
}