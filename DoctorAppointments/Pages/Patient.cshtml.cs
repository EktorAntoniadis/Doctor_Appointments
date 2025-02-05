using DoctorAppointments.Common;
using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    [Authorize(Roles = "Doctor, Secretary")]
    public class PatientsModel : PageModel
    {        
        private readonly IPatientRepository _patientRepository;

        public PatientsModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public PaginatedList<Patient> Patients { get; set; }

        [FromQuery]
        public string? FirstName { get; set; }

        [FromQuery]
        public string? LastName { get; set; }

        [FromQuery]
        public string? AMKA { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet()
        {
            Patients = _patientRepository.GetPatients(PageIndex, 10, FirstName, LastName, AMKA, SortColumn, SortDirection);
            return Page();
        }

    }
}
