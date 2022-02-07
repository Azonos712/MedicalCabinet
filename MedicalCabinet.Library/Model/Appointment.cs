using System;

namespace MedicalCabinet.Library.Model
{
    public class Appointment : ICloneable
    {
        public uint Id { get; private set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        public uint CaseOfIllnessId { get; private set; }
        public CaseOfIllness CaseOfIllness { get; set; }

        public object Clone()
        {
            return new Appointment
            {
                Id = this.Id,
                Date = this.Date,
                Description = this.Description,
                Time = this.Time,
                CaseOfIllnessId = this.CaseOfIllnessId,
                CaseOfIllness = this.CaseOfIllness.Clone() as CaseOfIllness
            };
        }
    }
}
