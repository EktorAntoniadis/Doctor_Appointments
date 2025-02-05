using Bogus;
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

            var docAppointmentFaker = new Faker("el");
            var listOfPatients = new List<Patient>();

            for (int i = 0; i < 100; i++)
            {
                var patient = new Patient()
                {
                    Address = docAppointmentFaker.Address.StreetAddress(),
                    Email = docAppointmentFaker.Internet.Email(),
                    FirstName = docAppointmentFaker.Name.FirstName(),
                    LastName = docAppointmentFaker.Name.LastName(),
                    PhoneNumber = docAppointmentFaker.Phone.PhoneNumber(),
                    Notes = docAppointmentFaker.Random.Words(250),
                    AMKA = docAppointmentFaker.Random.Long(10000000000, 99999999999).ToString(),
                };

                listOfPatients.Add(patient);
            }

            if (!_context.Patients.Any())
            {
                _context.Patients.AddRange(listOfPatients);
                _context.SaveChanges();
            }

            var listOfAppointments = new List<Appointment>();

            foreach (var patient in listOfPatients)
            {
                TimeOnly startTime = new TimeOnly(9, 0); 
                int intervals = docAppointmentFaker.Random.Int(0, 16);

                TimeOnly randomTime = startTime.AddMinutes(intervals * 30);

                if (listOfAppointments.Any(x => x.Timeslot?.StartTime == startTime))
                {
                    startTime = startTime.AddHours(1);
                }

                var appointment = new Appointment()
                {

                    PatientId = patient.PatientId,
                    Description = docAppointmentFaker.Random.Words(250),
                    CreatedAt = docAppointmentFaker.Date.Between(new DateTime(2025, 1, 1), new DateTime(2025, 2, 1)),
                    Timeslot = new Timeslot
                    {
                        Date = docAppointmentFaker.Date.BetweenDateOnly(new DateOnly(2025, 1, 1), new DateOnly(2025, 2, 1)),
                        StartTime = randomTime,
                        EndTime = randomTime.AddMinutes(30)
                    }
                };
                listOfAppointments.Add(appointment);
            }

            if (!_context.Appointments.Any())
            {
                _context.Appointments.AddRange(listOfAppointments);
                _context.SaveChanges();
            }
        }
    }
}
