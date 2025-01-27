using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
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
            if(_appointmentRepository.IsExistingAppointment(
                EditAppointment.Timeslot.Date, 
                EditAppointment.Timeslot.StartTime, 
                EditAppointment.Timeslot.EndTime))
            {
                ErrorMessage = "An appointment for this time frame already exists. Please select another date and time";
                return Page();
            }
            _appointmentRepository.Update(EditAppointment);
            return RedirectToPage("/Appointments");
        }
    }
}
