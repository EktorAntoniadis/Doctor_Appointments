using DoctorAppointments.Common;
using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DoctorAppointments.Pages
{
    [Authorize(Roles = "Doctor, Secretary")]
    public class ViewPatientModel : PageModel
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ViewPatientModel(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        public Patient ViewPatient { get; set; }
        

        public IActionResult OnGet(int id)
        {
            ViewPatient = _patientRepository.GetById(id);
            if (ViewPatient == null)
            {
                return RedirectToPage("/Error");
            }
           
            return Page();
        }
    }
}
