using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    public class EditPatientModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;

        public EditPatientModel(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        [BindProperty]
        public Patient EditPatient { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            EditPatient = _patientRepository.GetById(id);

            if (EditPatient == null)
            {
                return RedirectToPage("/Error"); 
            }

            return Page();
        }

        public IActionResult OnPostEditPatient()
        {
            _patientRepository.Update(EditPatient);
            return RedirectToPage("/Patient");
        }
    }
}
