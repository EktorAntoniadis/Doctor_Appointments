using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private DoctorAppointmentsDbContext _context;
        public PatientRepository(DoctorAppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {
            var patients = _context.Patients.ToList();
            return patients;
        }

        public Patient? GetByAMKA(string amka)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.AMKA == amka);
            return patient;
        }

        public Patient? GetById(int id)
        {
            var patient = _context.Patients.Find(id);
            return patient;
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
        }
    }
}
