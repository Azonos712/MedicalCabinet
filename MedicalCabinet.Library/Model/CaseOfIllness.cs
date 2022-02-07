using System;

namespace MedicalCabinet.Library.Model
{
    public class CaseOfIllness : ObservableObject, ICloneable
    {
        public uint Id { get; private set; }

        private string _diagnosis;
        public string Diagnosis {
            get { return _diagnosis; }
            set {
                _diagnosis = value;
                OnPropertyChanged();
            }
        }

        public string Description { get; set; }

        public uint DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public uint PatientId { get; set; }
        public Patient Patient { get; set; }

        public object Clone()
        {
            return new CaseOfIllness
            {
                Id = this.Id,
                Diagnosis = this.Diagnosis,
                Description = this.Description,
                DoctorId = this.DoctorId,
                Doctor = this.Doctor.Clone() as Doctor,// this.Doctor,
                PatientId = this.PatientId,
                Patient = this.Patient.Clone() as Patient// this.Patient
            };
        }
        public override string ToString()
        {
            return Patient.ToString() + '-' + Diagnosis;
        }
    }
}
