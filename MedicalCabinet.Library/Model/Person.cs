using System;

namespace MedicalCabinet.Library.Model
{
    public abstract class Person : ObservableObject
    {
        public uint Id { get; protected set; }

        private string _firstName;
        public string FirstName {
            get { return _firstName; }
            set {
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _middleName;
        public string MiddleName {
            get { return _middleName; }
            set {
                _middleName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _lastName;
        public string LastName {
            get { return _lastName; }
            set {
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        private DateTime _birthDay;
        public DateTime BirthDay {
            get { return _birthDay; }
            set {
                _birthDay = value;
                OnPropertyChanged();
            }
        }

        public byte[] Portrait { get; set; }

        public string FullName {
            get {
                string value = $"{LastName} {FirstName} {MiddleName}";
                return string.IsNullOrWhiteSpace(value) ? "ФИО не указаны" : value;
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
