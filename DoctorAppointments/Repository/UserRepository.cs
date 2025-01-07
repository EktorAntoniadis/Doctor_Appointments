using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public class UserRepository : IUserRepository
    {
        private DoctorAppointmentsDbContext _context;
        public UserRepository(DoctorAppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User? GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public User? GetByUserName(string userName)
        {
           var user =_context.Users.Where(x=>x.Username == userName).FirstOrDefault();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
