using MedicalCabinet.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalCabinet.Library.Data
{
    public class AppointmentContext
    {
        public Appointment Appointment { get; private set; }

        public AppointmentContext(Appointment _appointment)
        {
            Appointment = _appointment;
        }

        public void AddAppointment()
        {
            using (var db = new ApplicationContext())
            {
                db.Doctors.Attach(Appointment.CaseOfIllness.Doctor);
                db.Appointments.Add(Appointment);
                db.SaveChanges();
            }
        }
        public void UpdateAppointment()
        {
            using (var db = new ApplicationContext())
            {
                db.Appointments.Update(Appointment);
                db.SaveChanges();
            }
        }

        public void DeleteAppointment()
        {
            using (var db = new ApplicationContext())
            {
                db.Appointments.Remove(Appointment);
                db.SaveChanges();
            }
        }

        public List<Appointment> GetAppointmentsByDate(uint docId, DateTime date)
        {
            using (var db = new ApplicationContext())
            {
                return db.Appointments
                    .Include(x => x.CaseOfIllness).ThenInclude(x => x.Patient)
                    .Include(x => x.CaseOfIllness).ThenInclude(x => x.Doctor)
                    .Where(a => a.CaseOfIllness.DoctorId == docId)
                    .Where(a => a.Date.Date == date).ToList();
            }
        }

        public List<Appointment> GetAppointments(uint docId)
        {
            using (var db = new ApplicationContext())
            {
                return db.Appointments
                    .Include(x => x.CaseOfIllness).ThenInclude(x => x.Patient)
                    .Include(x => x.CaseOfIllness).ThenInclude(x => x.Doctor)
                    .Where(a => a.CaseOfIllness.DoctorId == docId).ToList();
            }
        }
    }
}
