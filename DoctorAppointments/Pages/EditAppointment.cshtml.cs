using DoctorAppointments.Common;
using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    [Authorize(Roles = "Doctor, Secretary")]
    public class EditAppointmentModel : PageModel
    {
        private IAppointmentRepository _appointmentRepository;

        public EditAppointmentModel(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        [BindProperty]
        public Appointment EditAppointment { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            EditAppointment = _appointmentRepository.GetAppointmentById(id);
            return Page();
        }

        public IActionResult OnPostEditAppointment() 
        { 
            _appointmentRepository.Update(EditAppointment);
            return RedirectToPage("/Appointments");
        }
    }
}
