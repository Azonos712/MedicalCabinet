using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.Library.Validator;
using MedicalCabinet.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicalCabinet.UI.View
{
    public partial class AppointmentWindow : Window
    {
        private readonly User _currentUser;
        private readonly Appointment _originalAppointment;
        private readonly Appointment _copyOfAppointment;

        public AppointmentWindow(User currentUser, Appointment appointment = null)
        {
            InitializeComponent();

            _currentUser = currentUser;

            if (appointment == null)
            {
                _copyOfAppointment = new Appointment()
                {
                    Date = DateTime.Now.Date,
                    Time = new DateTime(1990, 1, 1, 13, 0, 0)
                };

                ActionBtn.Content = "Добавить";
            }
            else
            {
                _originalAppointment = appointment;
                _copyOfAppointment = appointment.Clone() as Appointment;
                ActionBtn.Content = "Изменить";
            }

            PatientCB.ItemsSource = currentUser.Doctor.CasesOfIllness;
            DataContext = _copyOfAppointment;
            if (_originalAppointment != null)
                PatientCB.SelectedItem = currentUser.Doctor.CasesOfIllness.FirstOrDefault(x => x.Id == _copyOfAppointment.CaseOfIllness.Id);
        }

        private void ActionBtn_Click(object sender, RoutedEventArgs e)
        {
            _copyOfAppointment.Time = new DateTime(1990, 1, 1, int.Parse(TimeControl.Hours), int.Parse(TimeControl.Minutes), int.Parse(TimeControl.Seconds));

            bool isAppointmentValid = ModelValidator.Validate<Appointment>(new AppointmentValidator(), _copyOfAppointment, BackgroundGrid);
            if (!isAppointmentValid)
                return;

            if (_originalAppointment == null)
            {
                AddAppointment();
            }
            else
            {
                UpdateAppointment();
            }
            WindowsMaker.ShowPopUp(new CustomMsgBox("Уведомление", "Операция совершенна успешно!"), BackgroundGrid);
            Close();
        }

        private void AddAppointment()
        {
            AppointmentContext aContext = new AppointmentContext(_copyOfAppointment);

            aContext.AddAppointment();
        }

        private void UpdateAppointment()
        {
            //Patient foundPatient = new PatientContext(_copyOfCaseOfIllness.Patient).FindPatient();
            //if (foundPatient != null && !_originalCaseOfIllness.Patient.Equals(_copyOfCaseOfIllness.Patient))
            //{
            //    _copyOfCaseOfIllness.PatientId = foundPatient.Id;
            //    _copyOfCaseOfIllness.Patient = foundPatient;


            //    UpdateRealCase(_copyOfCaseOfIllness);
            //    _originalCaseOfIllness.PatientId = _copyOfCaseOfIllness.PatientId;
            //    _originalCaseOfIllness.Patient = _copyOfCaseOfIllness.Patient;
            //    CaseOfIllnessContext caseContext = new CaseOfIllnessContext(_originalCaseOfIllness);
            //    caseContext.UpdateCase();

            //    int index = _currentUser.Doctor.CasesOfIllness
            //        .IndexOf(_currentUser.Doctor.CasesOfIllness
            //        .Where(X => X.Id == _originalCaseOfIllness.Id).FirstOrDefault());
            //    var temp = _currentUser.Doctor.CasesOfIllness[index];
            //    _currentUser.Doctor.CasesOfIllness.RemoveAt(index);
            //    _currentUser.Doctor.CasesOfIllness.Insert(index, temp);
            //}
            //else
            //{
            UpdateRealAppointment(_copyOfAppointment);
            AppointmentContext aContext = new AppointmentContext(_originalAppointment);
            aContext.UpdateAppointment();
            //}
        }

        private void UpdateRealAppointment(Appointment verifiedAppointment)
        {
            _originalAppointment.Date = verifiedAppointment.Date;
            _originalAppointment.Time = verifiedAppointment.Time;
            _originalAppointment.Description = verifiedAppointment.Description;
            _originalAppointment.CaseOfIllness = verifiedAppointment.CaseOfIllness;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}