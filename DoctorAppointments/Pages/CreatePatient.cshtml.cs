using DoctorAppointments.Common;
using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    [Authorize(Roles = "Doctor, Secretary")]
    public class CreatePatientModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;

        public CreatePatientModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        [BindProperty]
        public Patient NewPatient { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostAddPatient()
        {
            if (_patientRepository.GetByAMKA(NewPatient.AMKA) != null)
            {
                ErrorMessage = "A patient with this AMKA already exists. Please check the AMKA or enter a new one.";
                return Page();
            }

            _patientRepository.Add(NewPatient);

            return RedirectToPage("/Patient");
        }
    }
}
