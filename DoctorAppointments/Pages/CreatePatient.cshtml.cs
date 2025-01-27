using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
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
            var patient = _patientRepository.GetByAMKA(NewPatient.AMKA);

            if (patient == null)
            {
                _patientRepository.Add(NewPatient);
                return RedirectToPage("/Patient");
            }

            ErrorMessage = "Patient with this AMKA already exists. Please enter a new patient.";
            return Page();
        }
    }
}