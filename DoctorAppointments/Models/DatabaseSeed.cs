using Microsoft.AspNetCore.Identity;

namespace DoctorAppointments.Models
{
    public class DatabaseSeed
    {
        private readonly DoctorAppointmentsDbContext _context;
        private PasswordHasher<User> _hasher;

        public DatabaseSeed(DoctorAppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hasher = new PasswordHasher<User>();
        }

        public void Seed()
        {
            var doctor = new User()
            {
                FirstName = "Hlias",
                LastName = "Papadopoulos",
                Username = "Hliaspap",
                Email = "hlias.papadopoulos@example.com",
                PhoneNumber = "2101234567",
                Role = "Doctor"
            };

            doctor.Password = _hasher.HashPassword(doctor, "!doctor!1234");

            var secretary = new User()
            {
                FirstName = "Esmeralda",
                LastName = "Romanov",
                Username = "EsmeRom",
                Email = "esmeralda.romanov@example.com",
                PhoneNumber = "21056789",
                Role = "Secretary",
            };

            secretary.Password = _hasher.HashPassword(secretary, "!secretary5678");

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(doctor, secretary);
                _context.SaveChanges();
            }
        }
    }
}
