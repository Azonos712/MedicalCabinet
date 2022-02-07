using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.UI.Helpers;
using MedicalCabinet.UI.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedicalCabinet.UI.View
{
    public partial class WorkWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }

        private bool _isScheduleArea = true;
        private readonly User _currentUser;

        private readonly SchedulePage _schedulePage;
        private readonly PatientPage _patientPage;

        public WorkWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            DataContext = _currentUser;

            _schedulePage = new SchedulePage(_currentUser, BlackGrid);
            _patientPage = new PatientPage(_currentUser, BlackGrid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtonsSettings();
            ExpanderSettings();
        }

        private void RadioButtonsSettings()
        {
            ScheduleRB.IsChecked = true;

            ScheduleRB.DataContext = new
            {
                BlackImg = "\\Images\\Icon\\calendarBlack.png",
                WhiteImg = "\\Images\\Icon\\calendarWhite.png"
            };

            PatientRB.DataContext = new
            {
                BlackImg = "\\Images\\Icon\\personBlack.png",
                WhiteImg = "\\Images\\Icon\\personWhite.png"
            };

            AddEventsToRadioButtons();
        }

        private void AddEventsToRadioButtons()
        {
            Button btnInRB1 = ScheduleRB.Template.FindName("AddButton", ScheduleRB) as Button;
            btnInRB1.Click += (s, e) => {
                WindowsMaker.ShowPopUp(new AppointmentWindow(_currentUser), BlackGrid);
                _schedulePage.RefreshCalendar();
            };

            Button btnInRB2 = PatientRB.Template.FindName("AddButton", PatientRB) as Button;
            btnInRB2.Click += (s, e) => WindowsMaker.ShowPopUp(new PatientWindow(_currentUser), BlackGrid); ;
        }

        private void ExpanderSettings()
        {
            // TODO возможно сделать обновление текущего пользователя из БД по активации рабочего окна 
            DoctorContext context = new(_currentUser.Doctor);
            //_currentUser.Doctor = context.GetDoctorById();

            //List<Note> notes = _currentUser.Doctor.Notes;
            //if (notes == null)
            //    return;

            //NoteCard[] noteCards = new NoteCard[] { _currentUser.Doctor.Notes };

            //ObservableCollection
            //var noteCards = _currentUser.Doctor.Notes?.Select(x => new NoteCard(x, _currentUser.Doctor.Notes));
            //expanderItemsControl.ItemsSource = noteCards;
            ObservableCollection<Note> collection = new ObservableCollection<Note>(_currentUser.Doctor.Notes);
            expanderItemsControl.ItemsSource = _currentUser.Doctor.Notes;
            //expanderPanel.Children.Clear();
            //expanderPanel.Children.Ad;
            //foreach (var note in notes)
            //expanderPanel.Children.Add(new NoteCard(note, expanderPanel.Children));
        }

        //Point oldMousePosition;

        //private void MainScrollViewer_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Point newMousePosition = Mouse.GetPosition((StackPanel)sender);
        //    //tb.Text = newMousePosition.X + " | " + newMousePosition.Y;

        //    if (Mouse.LeftButton == MouseButtonState.Pressed)
        //    {
        //        if (newMousePosition.Y < oldMousePosition.Y)
        //            MainScrollViewer.ScrollToVerticalOffset(MainScrollViewer.VerticalOffset + 1);
        //        if (newMousePosition.Y > oldMousePosition.Y)
        //            MainScrollViewer.ScrollToVerticalOffset(MainScrollViewer.VerticalOffset - 1);

        //        if (newMousePosition.X < oldMousePosition.X)
        //            MainScrollViewer.ScrollToHorizontalOffset(MainScrollViewer.HorizontalOffset + 1);
        //        if (newMousePosition.X > oldMousePosition.X)
        //            MainScrollViewer.ScrollToHorizontalOffset(MainScrollViewer.HorizontalOffset - 1);
        //    }
        //    else
        //    {
        //        oldMousePosition = newMousePosition;
        //    }
        //}


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SwitchWorkArea();
            _schedulePage.RefreshCalendar();
        }

        private void SwitchWorkArea()
        {
            if (_isScheduleArea)
            {
                mainContentControl.Content = _schedulePage;// new SchedulePage(BlackGrid);
                //ScheduleGrid.Visibility = Visibility.Visible;
                //PatientGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                mainContentControl.Content = _patientPage;// new PatientPage(BlackGrid);
                //ScheduleGrid.Visibility = Visibility.Collapsed;
                //PatientGrid.Visibility = Visibility.Visible;
            }
            _isScheduleArea = !_isScheduleArea;
        }

        //private void AddNoteBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    WindowsMaker.ShowPopUp(new AddNoteWindow(_currentUser.Doctor), BlackGrid);
        //}

        private void ProfileStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowsMaker.ShowWindow(this, new ProfileWindow(_currentUser));
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsMaker.ShowWindow(this, new LoginWindow());
        }
    }
}
