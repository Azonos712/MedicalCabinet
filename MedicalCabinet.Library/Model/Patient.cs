using System;
using System.Collections.Generic;

namespace MedicalCabinet.Library.Model
{
    public class Patient : Person, ICloneable, IEquatable<Patient>
    {
        public string Insurance { get; set; }
        public List<CaseOfIllness> CasesOfIllness { get; set; }

        public object Clone()
        {
            return new Patient
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                BirthDay = this.BirthDay,
                Portrait = this.Portrait?.Clone() as byte[],
                Insurance = this.Insurance
            };
        }

        public bool Equals(Patient other)
        {
            return this.FirstName == other.FirstName &&
                this.MiddleName == other.MiddleName &&
                this.LastName == other.LastName &&
                this.BirthDay.Date == other.BirthDay.Date &&
                this.Insurance == other.Insurance;
        }
    }
}
