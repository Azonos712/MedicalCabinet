using System.Windows;

namespace MedicalCabinet.UI.View
{
    public partial class CustomMsgBox : Window
    {
        public CustomMsgBox(string title, string text)
        {
            InitializeComponent();
            MsgBox.Title = title;
            MsgBoxText.Text = text;
            MsgBoxBtn.Click += (s, e) => this.Close();
        }
    }
}
