using DoctorAppointments.Common;
using DoctorAppointments.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DoctorAppointments.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private DoctorAppointmentsDbContext _context;
        public AppointmentRepository(DoctorAppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appointment = GetAppointmentById(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            var appointment = _context.Appointments.ToList();
            return appointment;
        }

        public Appointment GetAppointmentById(int id)
        {
            var appointment = _context.Appointments.Find(id);
            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentsByDay(DateOnly day)
        {
            var appointments = _context.Appointments.Where(x=>x.Timeslot.Date == day).ToList();
            return appointments;
        }

        public IEnumerable<Appointment> GetAppointmentsByMonth(int month)
        {
            var appointments = _context.Appointments.Where(x => x.Timeslot.Date.Month == month).ToList();
            return appointments;
        }

        public IEnumerable<Appointment> GetAppointmentsByMonth(int year, int month)
        {
            var appointments = _context.Appointments
                .Where(x => x.Timeslot.Date.Year == year
                && x.Timeslot.Date.Month == month)
                .ToList();
            return appointments;
        }

        public IEnumerable<Appointment> GetAppointmentsByPatient(int patientId)
        {
            var appointments = _context.Appointments.Where(x=>x.PatientId == patientId).ToList();
            return appointments;
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        PaginatedList<Appointment> IAppointmentRepository.GetAppointmentsByDay(
            DateOnly day, 
            int pageIndex,
            int pageSize, 
            string sortColumn, 
            string sortDirection)
        {
            var query = _context.Appointments.Where(x => x.Timeslot.Date == day).AsQueryable();

            switch (sortColumn)
            {
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Timeslot.StartTime) : query.OrderBy(x => x.Timeslot.StartTime);
                    break;
                case "FirstName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Patient.FirstName) : query.OrderBy(x => x.Patient.FirstName);
                    break;
                case "LastName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Patient.LastName) : query.OrderBy(x => x.Patient.LastName);
                    break;
            }

            var totalRecords = query.Count();

            var appointments = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Appointment>(appointments, totalRecords, pageIndex, pageSize);
        }
    }
}
