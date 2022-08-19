using ExamTask.Service.Interfaces;
using ExamTask.Service.Models.DTOs;
using ExamTask.Service.Services;
using ExamTask.WPF.UI.Windows;
using System.Windows;
using System.Windows.Controls;

namespace ExamTask.UI.Pages
{
    /// <summary>
    /// Interaction logic for StudentAddPage.xaml
    /// </summary>
    public partial class StudentAddPage : Page
    {

        public StudentAddPage()
        {

            InitializeComponent();
        }

        private async void StudentAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewStudentFirstName.Text) &&
                !string.IsNullOrEmpty(NewStudentLastName.Text) &&
                !string.IsNullOrEmpty(NewStudentFaculty.Text))
            {

                var newStudent = new StudentForCreation()
                {
                    FirstName = NewStudentFirstName.Text,
                    LastName = NewStudentLastName.Text,
                    Faculty = NewStudentFaculty.Text
                };

                using IUserService studentService = new UserService();

                var response = await studentService.CreateAsync(newStudent);

                if (response is null)
                {
                    ErrorResponse.Text = "Something went wrong";
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
                ErrorResponse.Text = "Please fill all fields";
                ErrorResponse.Visibility = Visibility.Visible;
            }

        }

    }
}
