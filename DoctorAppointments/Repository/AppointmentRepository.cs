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
            var appointment = GetById(id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            var appointment = _context.Appointments.ToList();
            return appointment;
        }

        public Appointment GetById(int id)
        {
            var appointment = _context.Appointments.Find(id);
            return appointment;
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }
    }
}
