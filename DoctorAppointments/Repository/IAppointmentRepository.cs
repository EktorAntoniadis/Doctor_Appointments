using System.Collections.Generic;
using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(int id);
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
        IEnumerable<Appointment> GetAppointmentsByPatient(int patientId);
        IEnumerable<Appointment> GetAppointmentsByDay(DateOnly day);
        IEnumerable<Appointment> GetAppointmentsByMonth(int month);
    }
}