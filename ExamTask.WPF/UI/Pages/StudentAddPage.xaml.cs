using ExamTask.Main.Contracts;
using ExamTask.Main.Models.DTOs;
using ExamTask.Main.Services;
using ExamTask.WPF.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamTask.UI.Pages
{
    /// <summary>
    /// Interaction logic for StudentAddPage.xaml
    /// </summary>
    public partial class StudentAddPage : Page
    {
        private readonly MessageViewer messageViewer;
        public StudentAddPage()
        {
            messageViewer = new MessageViewer();

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

                using IStudentService studentService = new StudentService();

                var response = await studentService.CreateAsync(newStudent);

                if (response is null)
                {
                    ErrorResponse.Text = "Something went wrong";
                    ErrorResponse.Visibility = Visibility.Visible;
                }
                else
                    messageViewer.Show();
            }
            else
            {
                ErrorResponse.Text = "Please fill all fields";
                ErrorResponse.Visibility = Visibility.Visible;
            }

        }

    }
}
