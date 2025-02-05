using DoctorAppointments.Common;
using DoctorAppointments.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Patients
                .Include(x=>x.Appointments)
                .ThenInclude(x=>x.Timeslot)
                .FirstOrDefault(x=>x.PatientId == id);
        }

        public void Update(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            _context.Patients.Update(patient);
            _context.SaveChanges();
        }

        public PaginatedList<Patient> GetPatients(int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? AMKA = null,
            string? sortColumn = "Title",
            string? sortDirection = "asc")
        {

            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = _context.Patients.Where(x => x.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = _context.Patients.Where(x => x.LastName.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(AMKA))
            {
                query = _context.Patients.Where(x => x.AMKA == AMKA);
            }

            switch (sortColumn)
            {
                case "FirstName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.LastName) : query.OrderBy(x => x.LastName);
                    break;
            }

            var totalRecords = query.Count();

            var patients = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Patient>(patients, totalRecords, pageIndex, pageSize);
        }
    }
}
