using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointments.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("Timeslot")]
        public int TimeslotId { get; set; }
        public virtual Timeslot Timeslot { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
