using System.Windows;

namespace ExamTask.WPF.UI.Windows
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class StudentInfo : Window
    {

        public StudentInfo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
