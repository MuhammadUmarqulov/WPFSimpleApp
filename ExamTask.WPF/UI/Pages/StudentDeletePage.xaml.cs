using ExamTask.Main.Contracts;
using ExamTask.Main.Services;
using ExamTask.WPF.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for StudentDeletePage.xaml
    /// </summary>
    public partial class StudentDeletePage : Page
    {

        private readonly MessageViewer messageViewer;
        public StudentDeletePage()
        {
            messageViewer = new MessageViewer();
            InitializeComponent();
        }

        private async void StudentDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValidId = long.TryParse(DeletedStudentId.Text, out long result);
            
            if (IsValidId)
            {
                using IStudentService studentService = new StudentService();
                
                var response = await studentService.DeleteAsync(result);

                if (!response)
                {
                    ErrorResponse.Text = "Student not found";
                    ErrorResponse.Visibility = Visibility.Visible;
                }

                else
                    messageViewer.Show();
                
            }
            
            else
            {
                ErrorResponse.Text = "Invalid Id";
                ErrorResponse.Visibility = Visibility.Visible;
            }

        }
    }
}
