using MedicalCabinet.Library.Model;
using System.Linq;

namespace MedicalCabinet.Library.Data
{
    public class PatientContext
    {
        public Patient Patient { get; private set; }

        public PatientContext(Patient patient)
        {
            Patient = patient;
        }

        public Patient FindPatient()
        {
            using (var db = new ApplicationContext())
            {
                return db.Patients.SingleOrDefault(p => p.FirstName == Patient.FirstName &&
                                                    p.MiddleName == Patient.MiddleName &&
                                                    p.LastName == Patient.LastName &&
                                                    p.BirthDay.Date == Patient.BirthDay.Date &&
                                                    p.Insurance == Patient.Insurance);
            }
        }

        public void AddPatient()
        {
            using (var db = new ApplicationContext())
            {
                if (FindPatient() == null)
                    db.Patients.Add(Patient);
                db.SaveChanges();
            }
        }

        public void UpdatePatient()
        {
            using (var db = new ApplicationContext())
            {
                db.Patients.Update(Patient);
                db.SaveChanges();
            }
        }
    }
}
