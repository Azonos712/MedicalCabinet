using MedicalCabinet.Library.Data;
using MedicalCabinet.Library.Model;
using System.Windows;
using System.Windows.Controls;

namespace MedicalCabinet.UI.UserControls
{
    public partial class NoteCard : UserControl
    {
        public NoteCard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Note thisNote = DataContext as Note;

            thisNote.Doctor.Notes.Remove(thisNote);
            NoteContext context = new(thisNote);
            context.DeleteNote();
        }
    }
}
