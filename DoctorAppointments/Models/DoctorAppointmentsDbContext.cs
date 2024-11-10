using Microsoft.EntityFrameworkCore;

namespace DoctorAppointments.Models
{
    public class DoctorAppointmentsDbContext: DbContext
    {
        public DoctorAppointmentsDbContext(DbContextOptions options): base(options)
        {
        }

        DbSet<Patient> Patients { get; set; }
        DbSet<Appointment> Appointments { get; set; }
    }
}
