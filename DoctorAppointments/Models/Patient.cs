using System.ComponentModel.DataAnnotations;

namespace DoctorAppointments.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
