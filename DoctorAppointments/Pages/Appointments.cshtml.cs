using DoctorAppointments.Common;
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

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public PaginatedList<Appointment> DailyAppointments { get; set; }

        [BindProperty]
        public Appointment NewAppointment { get; set; } = new Appointment();

        public IActionResult OnGet()
        {
            DailyAppointments = _appointmentRepository
                .GetAppointmentsByDay(AppointmentDate, PageIndex, 10, SortColumn, SortDirection);
            return Page();
        }

        public IActionResult OnPost()
        {
            DailyAppointments = _appointmentRepository
                .GetAppointmentsByDay(AppointmentDate, PageIndex, 10, SortColumn, SortDirection);
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                //DailyAppointments = _appointmentRepository.GetAppointmentsByDay(AppointmentDate);
                return Page();
            }

            _appointmentRepository.Add(NewAppointment);
            return RedirectToPage();
        }
    }
}
