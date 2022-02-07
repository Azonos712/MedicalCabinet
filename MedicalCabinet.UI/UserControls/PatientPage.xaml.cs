using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.UI.Helpers;
using MedicalCabinet.UI.View;
using System.Windows;
using System.Windows.Controls;

namespace MedicalCabinet.UI.UserControls
{
    public partial class PatientPage : UserControl
    {
        private readonly User _user;
        private readonly Grid _background;

        public PatientPage(User currentUser, Grid backgroundGrid)
        {
            InitializeComponent();
            _user = currentUser;
            _background = backgroundGrid;
            AddNewPatientBtn.Click += (s, e) => WindowsMaker.ShowPopUp(new PatientWindow(_user), _background);
            patientDataGrid.ItemsSource = _user.Doctor.CasesOfIllness;
        }

        private void EditInGridBtn_Click(object sender, RoutedEventArgs e)
        {
            CaseOfIllness caseOfIllness = ((FrameworkElement)sender).DataContext as CaseOfIllness;
            WindowsMaker.ShowPopUp(new PatientWindow(_user, caseOfIllness), _background);
        }

        private void DeleteInGridBtn_Click(object sender, RoutedEventArgs e)
        {
            CaseOfIllness caseOfIllness = ((FrameworkElement)sender).DataContext as CaseOfIllness;

            CaseOfIllnessContext context = new(caseOfIllness);
            context.DeleteCase();

            _user.Doctor.CasesOfIllness.Remove(caseOfIllness);
        }
    }
}