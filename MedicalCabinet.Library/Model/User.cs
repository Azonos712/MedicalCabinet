using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalCabinet.Library.Model
{
    public class User
    {
        public uint Id { get; private set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string PasswordConfirmation { get; set; }

        public uint DoctorId { get; private set; }
        public Doctor Doctor { get; set; }
    }
}
