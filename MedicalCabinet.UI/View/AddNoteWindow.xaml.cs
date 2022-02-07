using MedicalCabinet.Library.Model;
using MedicalCabinet.Library.Validator;
using MedicalCabinet.UI.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using MedicalCabinet.Library.Data;

namespace MedicalCabinet.UI.View
{
    public partial class AddNoteWindow : Window
    {
        private readonly Doctor _doctor;

        public AddNoteWindow(Doctor doctor)
        {
            InitializeComponent();
            _doctor = doctor;
            DataContext = new Note() { Title = "Новая заметка", DateOfCreation = DateTime.Now.Date, DoctorId = _doctor.Id };
        }

        private void AddNoteBtn_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = DataContext as Note;
            bool isNewNoteValid = ModelValidator.Validate<Note>(new NoteValidator(), newNote, BackgroundGrid);
            if (!isNewNoteValid)
                return;

            NoteContext context = new NoteContext(newNote);
            context.AddNote();

            newNote.Doctor = _doctor;
            _doctor.Notes.Add(newNote);

            //DoctorContext context = new(_doctor);
            //context.UpdateDoctor();

            WindowsMaker.ShowPopUp(new CustomMsgBox("Уведомление", "Заметка была добавленна!"), BackgroundGrid);
            this.Close();
        }

        private void TextChanged1(object s, RoutedEventArgs e) => placeHolder1.ToggleVisibility((s as TextBox).Text.Length);

        private void TextChanged2(object s, RoutedEventArgs e) => placeHolder2.ToggleVisibility((s as TextBox).Text.Length);

        private void CancelBtn_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}