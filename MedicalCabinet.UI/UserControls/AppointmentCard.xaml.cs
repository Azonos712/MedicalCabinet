using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MedicalCabinet.Library.Model;

namespace MedicalCabinet.UI.UserControls
{
    public partial class AppointmentCard : UserControl
    {
        public AppointmentCard()
        {
            InitializeComponent();
        }

        public AppointmentCard(Appointment appoiuntment) : base()
        {
            this.DataContext = appoiuntment;
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler EditButtonClick;

        protected void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.EditButtonClick != null)
                this.EditButtonClick(sender, e);
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteButtonClick;

        protected void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.DeleteButtonClick != null)
                this.DeleteButtonClick(sender, e);
        }
    }
}
