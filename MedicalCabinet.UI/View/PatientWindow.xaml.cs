using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.Library.Validator;
using MedicalCabinet.UI.Helpers;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MedicalCabinet.UI.View
{
    public partial class PatientWindow : Window
    {
        private readonly User _currentUser;
        private readonly CaseOfIllness _originalCaseOfIllness;
        private readonly CaseOfIllness _copyOfCaseOfIllness;

        public PatientWindow(User currentUser, CaseOfIllness caseOfIllness = null)
        {
            InitializeComponent();

            _currentUser = currentUser;

            if (caseOfIllness == null)
            {
                _copyOfCaseOfIllness = new CaseOfIllness();
                var newPatient = new Patient//TODO сделать в конструкторе по умолчанию
                {
                    BirthDay = DateTime.Now.Date,//TODO сократить путь к изображению
                    Portrait = ImageUtility.ImageSourceToBytes(ImageUtility.PathToImageSource("pack://application:,,,/Images/Portrait/default.png"))
                };
                _copyOfCaseOfIllness.Patient = newPatient;
                //_copyOfCaseOfIllness.Doctor = new DoctorContext(_currentUser.Doctor).GetDoctorById();
                // _currentUser.Doctor.Clone() as Doctor;
                _copyOfCaseOfIllness.DoctorId = _currentUser.DoctorId;

                deleteBtn.Visibility = Visibility.Collapsed;
                actionBtn.Content = "Добавить";
            }
            else
            {
                _originalCaseOfIllness = caseOfIllness;
                _copyOfCaseOfIllness = caseOfIllness.Clone() as CaseOfIllness;
                actionBtn.Content = "Изменить";
            }

            DataContext = _copyOfCaseOfIllness;
        }

        private void ImageBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new()
            {
                Title = "Выбрать картинку пациента",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png"
            };

            if (op.ShowDialog() == true)
            {
                PortraitImage.ImageSource = null; // Обнуляем значение чтобы работало обновление объекта в DataContext
                PortraitImage.ImageSource = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void ActionBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isPatientValid = ModelValidator.Validate<Patient>(new PatientValidator(), _copyOfCaseOfIllness.Patient, BlackBackGrid);
            if (!isPatientValid)
                return;

            bool isCaseOfIllnessValid = ModelValidator.Validate<CaseOfIllness>(new CaseOfIllnessValidator(), _copyOfCaseOfIllness, BlackBackGrid);
            if (!isCaseOfIllnessValid)
                return;

            if (_originalCaseOfIllness == null)
            {
                AddCase();
            }
            else
            {
                UpdateCase();
            }
            WindowsMaker.ShowPopUp(new CustomMsgBox("Уведомление", "Операция совершенна успешно!"), BlackBackGrid);
            Close();
        }

        private void AddCase()
        {
            PatientContext patientContext = new PatientContext(_copyOfCaseOfIllness.Patient);
            CaseOfIllnessContext caseContext = new CaseOfIllnessContext(_copyOfCaseOfIllness);

            patientContext.AddPatient();
            _copyOfCaseOfIllness.Patient = null;
            _copyOfCaseOfIllness.PatientId = patientContext.FindPatient().Id;

            caseContext.AddCase();

            _currentUser.Doctor.CasesOfIllness.Add(caseContext.GetCaseById());
            //.CasesOfIllness.Add(_copyOfCaseOfIllness);
            //DoctorContext context = new(_currentUser.Doctor);
            //context.UpdateDoctor();
        }

        private void UpdateCase()
        {
            Patient foundPatient = new PatientContext(_copyOfCaseOfIllness.Patient).FindPatient();
            if (foundPatient != null && !_originalCaseOfIllness.Patient.Equals(_copyOfCaseOfIllness.Patient))
            {
                _copyOfCaseOfIllness.PatientId = foundPatient.Id;
                _copyOfCaseOfIllness.Patient = foundPatient;


                UpdateRealCase(_copyOfCaseOfIllness);
                _originalCaseOfIllness.PatientId = _copyOfCaseOfIllness.PatientId;
                _originalCaseOfIllness.Patient = _copyOfCaseOfIllness.Patient;
                CaseOfIllnessContext caseContext = new CaseOfIllnessContext(_originalCaseOfIllness);
                caseContext.UpdateCase();

                int index = _currentUser.Doctor.CasesOfIllness
                    .IndexOf(_currentUser.Doctor.CasesOfIllness
                    .Where(X => X.Id == _originalCaseOfIllness.Id).FirstOrDefault());
                var temp = _currentUser.Doctor.CasesOfIllness[index];
                _currentUser.Doctor.CasesOfIllness.RemoveAt(index);
                _currentUser.Doctor.CasesOfIllness.Insert(index, temp);
            }
            else
            {
                UpdateRealPatient(_copyOfCaseOfIllness.Patient);
                PatientContext patientContext = new PatientContext(_originalCaseOfIllness.Patient);
                patientContext.UpdatePatient();

                UpdateRealCase(_copyOfCaseOfIllness);
                CaseOfIllnessContext caseContext = new CaseOfIllnessContext(_originalCaseOfIllness);
                caseContext.UpdateCase();
            }

        }

        private void UpdateRealPatient(Patient verifiedPatient)
        {
            _originalCaseOfIllness.Patient.FirstName = verifiedPatient.FirstName;
            _originalCaseOfIllness.Patient.LastName = verifiedPatient.LastName;
            _originalCaseOfIllness.Patient.MiddleName = verifiedPatient.MiddleName;
            _originalCaseOfIllness.Patient.BirthDay = verifiedPatient.BirthDay;
            _originalCaseOfIllness.Patient.Insurance = verifiedPatient.Insurance;
            _originalCaseOfIllness.Patient.Portrait = verifiedPatient.Portrait;
        }

        private void UpdateRealCase(CaseOfIllness verifiedCase)
        {
            _originalCaseOfIllness.Diagnosis = verifiedCase.Diagnosis;
            _originalCaseOfIllness.Description = verifiedCase.Description;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CaseOfIllness caseOfIllness = DataContext as CaseOfIllness;
            _currentUser.Doctor.CasesOfIllness.Remove(_originalCaseOfIllness);
            //_currentUser.Doctor.CasesOfIllness.Remove(_currentUser.Doctor.CasesOfIllness.Single(x => x.Id == caseOfIllness.Id));

            CaseOfIllnessContext context = new(caseOfIllness);
            context.DeleteCase();

            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) => Close();
    }
}