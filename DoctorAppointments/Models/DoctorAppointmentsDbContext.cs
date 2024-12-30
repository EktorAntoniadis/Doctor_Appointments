using Microsoft.EntityFrameworkCore;

namespace DoctorAppointments.Models
{
    public class DoctorAppointmentsDbContext: DbContext
    {
        public DoctorAppointmentsDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
