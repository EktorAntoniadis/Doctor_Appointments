﻿using DoctorAppointments.Models;

namespace DoctorAppointments.Repository
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        
        void Add(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
    }    
}
 