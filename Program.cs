using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Patient
{
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

    public string Address { get;set; }

    public string Email { get;set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    [Required]
    public DateOnly AppointmentDate { get; set; }

    [Required]
    public TimeOnly AppointmentTime { get; set; }

    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }
}