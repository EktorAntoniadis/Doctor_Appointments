using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DoctorAppointmentsDbContext _context;

        public PatientRepository(DoctorAppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient == null) throw new ArgumentNullException("Patient not found.");

            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient? GetByAMKA(string amka)
        {
            if (string.IsNullOrWhiteSpace(amka)) return null;

            return _context.Patients.FirstOrDefault(x => x.AMKA == amka);
        }

        public Patient? GetById(int id)
        {
            return _context.Patients.Find(id);
        }

        public void Update(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            _context.Patients.Update(patient);
            _context.SaveChanges();
        }
    }
}
