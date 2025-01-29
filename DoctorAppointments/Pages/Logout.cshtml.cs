using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {
            await HttpContext.SignOutAsync("DoctorAppointmentsScheme");
            return RedirectToPage("/Index");
        }
    }
}
