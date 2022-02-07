using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using MedicalCabinet.UI.Helpers;
using MedicalCabinet.UI.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.ObjectModel;

namespace MedicalCabinet.UI.UserControls
{
    public partial class SchedulePage : UserControl
    {
        private HashSet<DateTime> FutureDates { get; } = new HashSet<DateTime>();
        private HashSet<DateTime> PastDates { get; } = new HashSet<DateTime>();
        private User CurrentUser;
        private Grid BackgroundGrid;
        public SchedulePage(User currentUser, Grid backgroundGrid)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            BackgroundGrid = backgroundGrid;
            AddAppointmentBtn.Click += (s, e) => {
                WindowsMaker.ShowPopUp(new AppointmentWindow(currentUser), backgroundGrid);
                RefreshCalendar();
            };
            AddNoteBtn.Click += (s, e) => WindowsMaker.ShowPopUp(new AddNoteWindow(currentUser.Doctor), backgroundGrid);

            LeftCalendar.SelectedDate = DateTime.Now.Date;
            UpdateUpcomingDates();
        }

        public void RefreshCalendar()
        {
            UpdateUpcomingDates();
            var date = LeftCalendar.SelectedDate;
            LeftCalendar.SelectedDate = date.Value.AddDays(1);
            LeftCalendar.SelectedDate = date.Value;
        }

        private void LeftCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);

            selectedDate.Text = LeftCalendar.SelectedDate.Value.ToString("ddd, dd MMM yyy");
            AppointmentContext aContext = new AppointmentContext(null);
            var list = aContext.GetAppointmentsByDate(CurrentUser.DoctorId, LeftCalendar.SelectedDate.Value).OrderBy(x => x.Time.TimeOfDay);
            appointmentCount.Text = list.Count().ToString();
            ObservableCollection<Appointment> collection = new ObservableCollection<Appointment>(list);
            rightItemsControl.ItemsSource = collection;
        }

        private void CalendarDayButton_Loaded(object sender, RoutedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);

            button.DataContextChanged += new DependencyPropertyChangedEventHandler(CalendarButton_DataContextChanged);
        }

        private void CalendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        private void UpdateUpcomingDates()
        {
            AppointmentContext aContext = new AppointmentContext(null);
            var list = aContext.GetAppointments(CurrentUser.DoctorId);
            //.Where(x => DateTime.Now.AddDays(-30) <= x.Date && x.Date <= DateTime.Now.AddDays(30));
            FutureDates.Clear();
            PastDates.Clear();
            //TODO оптимизировать
            foreach (var item in list)
            {
                if (DateTime.Now.Date <= item.Date)
                    FutureDates.Add(item.Date);
                else
                    PastDates.Add(item.Date);
            }
        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            //GetUpcomingDates();
            Rectangle r = button.Template.FindName("MarkedRectangle", button) as Rectangle;
            r.Opacity = 0.0;

            if (FutureDates.Contains(date))
            {
                r.Opacity = 1.0;
                r.Fill = new BrushConverter().ConvertFrom("#FF1675E4") as Brush;//new SolidColorBrush(Color.FromArgb(0xFF, 0x16, 0x75, 0xE4));
            }

            if (PastDates.Contains(date))
            {
                r.Opacity = 1.0;
                r.Fill = Brushes.Gray;// new SolidColorBrush(Color.FromArgb(0xFF, 0x16, 0x75, 0xE4));
            }
        }

        private void AppointmentCard_EditButtonClick(object sender, EventArgs e)
        {
            Appointment appointment = ((FrameworkElement)sender).DataContext as Appointment;
            WindowsMaker.ShowPopUp(new AppointmentWindow(CurrentUser, appointment), BackgroundGrid);
            RefreshCalendar();
        }

        private void AppointmentCard_DeleteButtonClick(object sender, EventArgs e)
        {
            Appointment appointment = ((FrameworkElement)sender).DataContext as Appointment;

            AppointmentContext context = new(appointment);
            context.DeleteAppointment();

            RefreshCalendar();
        }
    }
}
