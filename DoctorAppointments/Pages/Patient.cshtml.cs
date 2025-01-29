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

        public IEnumerable<Patient> Patients { get; set; }

        public IActionResult OnGetAsync()
        {
            Patients = _patientRepository.GetAll();
            return Page();
        }          
    }
}
