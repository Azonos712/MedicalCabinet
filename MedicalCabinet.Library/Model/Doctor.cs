using System;
using System.Collections.ObjectModel;

namespace MedicalCabinet.Library.Model
{
    public class Doctor : Person, ICloneable
    {
        public string Position { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public ObservableCollection<CaseOfIllness> CasesOfIllness { get; set; }

        public object Clone()
        {
            return new Doctor
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                BirthDay = this.BirthDay,
                Portrait = this.Portrait?.Clone() as byte[],
                Position = this.Position
            };
        }
    }
}
