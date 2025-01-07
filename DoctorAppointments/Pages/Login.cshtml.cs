using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public IActionResult OnPost()
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
        if(hashedPasswordResult == PasswordVerificationResult.Success)
        {
            return RedirectToPage("/Appointments");
        }
        else
        {
            ErrorMessage = "Wrong password";
            return Page();
        }
    }
}