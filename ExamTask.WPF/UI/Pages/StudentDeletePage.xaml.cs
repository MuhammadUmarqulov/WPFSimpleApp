using ExamTask.Service.Interfaces;
using ExamTask.Service.Services;
using ExamTask.WPF.UI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace ExamTask.UI.Pages
{
    /// <summary>
    /// Interaction logic for StudentDeletePage.xaml
    /// </summary>
    public partial class StudentDeletePage : Page
    {


        public StudentDeletePage()
        {
            InitializeComponent();
        }

        private async void StudentDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValidId = long.TryParse(DeletedStudentId.Text, out long result);

            if (IsValidId)
            {
                using IUserService studentService = new UserService();

                var response = await studentService.DeleteAsync(result);

                if (!response)
                {
                    ErrorResponse.Text = "Student not found";
                    ErrorResponse.Visibility = Visibility.Visible;
                }

                else
                {
                    MessageViewer messageViewer = new MessageViewer();
                    messageViewer.Show();
                }
            }

            else
            {
                ErrorResponse.Text = "Invalid Id";
                ErrorResponse.Visibility = Visibility.Visible;
            }

        }
    }
}
