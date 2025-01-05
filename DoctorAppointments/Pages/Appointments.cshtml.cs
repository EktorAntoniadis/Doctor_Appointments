using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{

    public class AppointmentsModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsModel(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        [BindProperty]
        public DateOnly AppointmentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public IEnumerable<Appointment> DailyAppointments { get; set; }

        public IActionResult OnGet()
        {
            DailyAppointments = _appointmentRepository.GetAppointmentsByDay(AppointmentDate);
            return Page();
        }

        public IActionResult OnPost()
        {
            DailyAppointments = _appointmentRepository.GetAppointmentsByDay(AppointmentDate);
            return Page();
        }
    }

}
