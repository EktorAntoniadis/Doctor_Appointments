using DoctorAppointments.Common;
using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointments.Pages
{
    [Authorize(Roles = "Doctor, Secretary")]
    public class CreateAppointmentModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentsRepository;
        private readonly IPatientRepository _patientRepository;

        public CreateAppointmentModel(
            IAppointmentRepository appointmentsRepository,
            IPatientRepository patientRepository
            )
        {
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException(nameof(appointmentsRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        [BindProperty]
        public Appointment NewAppointment { get; set; }

        public string ErrorMessage { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPostAddAppointment()
        {           
            var patient = _patientRepository.GetByAMKA(NewAppointment.Patient.AMKA);

            if (patient == null)
            {
                var newPatient = new Patient
                {
                    FirstName = NewAppointment.Patient.FirstName,
                    LastName = NewAppointment.Patient.LastName,
                    AMKA = NewAppointment.Patient.AMKA
                };

                NewAppointment.Patient = newPatient;
            }

            if (!_appointmentsRepository.IsExistingAppointment(
                NewAppointment.Timeslot.Date, 
                NewAppointment.Timeslot.StartTime,
                NewAppointment.Timeslot.EndTime))
            {
                _appointmentsRepository.Add(NewAppointment);
                return RedirectToPage("/Appointments");
            }

            ErrorMessage = "There is an appointment for this time. Please select another time.";
            return Page();
        }
    }
}
