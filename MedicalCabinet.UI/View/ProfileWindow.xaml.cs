using MedicalCabinet.Library;
using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.Library.Validator;
using MedicalCabinet.UI.Helpers;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MedicalCabinet.UI.View
{
    public partial class ProfileWindow : Window
    {
        private readonly User _currentUser;
        private readonly Doctor _copyOfDoctor;

        public ProfileWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _copyOfDoctor = _currentUser.Doctor.Clone() as Doctor;
            DataContext = _copyOfDoctor;
            PatientCountTxtBlock.Text = _currentUser.Doctor.CasesOfIllness.Select(x => x.PatientId).Distinct().Count().ToString();
        }

        private void ImageBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new()
            {
                Title = "Выбрать картинку профиля",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png"
            };

            if (op.ShowDialog() == true)
            {
                PortraitImage.ImageSource = null; // Обнуляем значение чтобы работало обновление объекта в DataContext
                PortraitImage.ImageSource = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void SaveDataBtn_Click(object sender, RoutedEventArgs e)
        {
            //bool isUpdatedDoctorValid = ValidateDoctor(_copyOfDoctor);
            bool isUpdatedDoctorValid = ModelValidator.Validate<Doctor>(new DoctorValidator(), _copyOfDoctor, BlackBackGrid);
            if (!isUpdatedDoctorValid)
                return;

            UpdateRealDoctor(_copyOfDoctor);
            DoctorContext context = new(_currentUser.Doctor);
            context.UpdateDoctor();
            WindowsMaker.ShowPopUp(new CustomMsgBox("Уведомление", "Данные доктора были обновлены!"), BlackBackGrid);
        }

        private void UpdateRealDoctor(Doctor verifiedDoctor)
        {
            _currentUser.Doctor.LastName = verifiedDoctor.LastName;
            _currentUser.Doctor.FirstName = verifiedDoctor.FirstName;
            _currentUser.Doctor.MiddleName = verifiedDoctor.MiddleName;
            _currentUser.Doctor.BirthDay = verifiedDoctor.BirthDay;
            _currentUser.Doctor.Position = verifiedDoctor.Position;
            _currentUser.Doctor.Portrait = verifiedDoctor.Portrait;
        }

        private void TextChanged1(object s, RoutedEventArgs e) => placeHolder1.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged2(object s, RoutedEventArgs e) => placeHolder2.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged3(object s, RoutedEventArgs e) => placeHolder3.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged4(object s, RoutedEventArgs e) => placeHolder4.ToggleVisibility((s as DatePicker).Text.Length);

        private void TextChanged5(object s, RoutedEventArgs e) => placeHolder5.ToggleVisibility((s as TextBox).Text.Length);

        private void BackTextBlockBtn_MouseUp(object s, MouseButtonEventArgs e) => WindowsMaker.ShowWindow(this, new WorkWindow(_currentUser));
    }
}