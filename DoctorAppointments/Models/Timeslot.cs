using System.ComponentModel.DataAnnotations;

namespace DoctorAppointments.Models
{
    public class Timeslot
    {
        [Key]
        public int TimeslotId { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
