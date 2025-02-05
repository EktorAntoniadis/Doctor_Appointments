using DoctorAppointments.Common;
using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        Patient? GetByAMKA(string amka);        
        void Add(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
        PaginatedList<Patient> GetPatients(int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? AMKA = null,
            string? sortColumn = "Title",
            string? sortDirection = "asc");
    }    
}
 