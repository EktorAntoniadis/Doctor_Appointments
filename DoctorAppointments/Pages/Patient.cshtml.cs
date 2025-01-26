using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorAppointments.Pages
{
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
