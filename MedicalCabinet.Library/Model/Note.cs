using System;

namespace MedicalCabinet.Library.Model
{
    public class Note
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Description { get; set; }

        public uint DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public override string ToString()
        {
            return $"{Title}-{DateOfCreation.Date}";
        }
    }
}
