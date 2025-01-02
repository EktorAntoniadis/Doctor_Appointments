using DoctorAppointments.Models;

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
    }
}
