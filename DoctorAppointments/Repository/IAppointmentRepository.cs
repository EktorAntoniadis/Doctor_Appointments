using System.Collections.Generic;
using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);

        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
    }
}