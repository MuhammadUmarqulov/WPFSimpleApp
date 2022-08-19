using System.Windows;

namespace ExamTask.WPF.UI.Windows
{
    /// <summary>
    /// Interaction logic for MessageViewer.xaml
    /// </summary>
    public partial class MessageViewer : Window
    {
        public MessageViewer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
