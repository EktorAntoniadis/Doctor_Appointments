using DoctorAppointments.Common;
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
        PaginatedList<Appointment> GetAppointmentsByDay(
            DateOnly day,
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortDirection);
        IEnumerable<Appointment> GetAppointmentsByMonth(int year, int month);
        IEnumerable<Appointment> GetAppointmentsByMonth(int month);
    }
}