using MedicalCabinet.Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCabinet.Library.Data
{
    public class DoctorContext
    {
        public Doctor Doctor { get; private set; }

        public DoctorContext(Doctor doctor)
        {
            Doctor = doctor;
        }

        public void UpdateDoctor()
        {
            using (var db = new ApplicationContext())
            {
                //var doctorDB = GetDoctorById();
                //doctorDB.Notes.Remove(doctorDB.Notes.Last());

                //db.Doctors.Attach(Doctor);
                db.Doctors.Update(Doctor);
                //db.Doctors.Update(Doctor);
                db.SaveChanges();
            }
        }

        public Doctor GetDoctorById()
        {
            using (var db = new ApplicationContext())
            {
                return db.Doctors
                .Include(d => d.Notes)
                .Include(d => d.CasesOfIllness)
                    .ThenInclude(c => c.Patient)
                .Single(d => d.Id == Doctor.Id);
            }
        }
    }
}
