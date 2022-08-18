using ExamTask.Main.Contracts;
using ExamTask.Main.Models.DTOs;
using ExamTask.Main.Services;
using ExamTask.WPF.UI.Windows;
using Microsoft.Win32;
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
    /// Interaction logic for StudentSavePage.xaml
    /// </summary>
    public partial class StudentSavePage : Page
    {
        private string imagePath;
        private string passportImagePath;
        private MessageViewer messageViewer;

        public StudentSavePage()
        {
            messageViewer = new MessageViewer();
            InitializeComponent();
        }

        private async void StudentSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValidId = long.TryParse(StudentId.Text, out long result);

            if (IsValidId)
            {
                using IStudentService studentService = new StudentService();

                var oldStudent = await studentService.GetAsync(result); 

                if (oldStudent is null )
                {
                    ErrorResponse.Text = "Student not fount";
                    ErrorResponse.Visibility = Visibility.Visible;
                    
                    return;
                }

                var updateStudentInfo = new StudentForCreation();

                if (!string.IsNullOrEmpty(StudentFirstName.Text))
                    updateStudentInfo.FirstName = StudentFirstName.Text;
                else
                    updateStudentInfo.FirstName = oldStudent.FirstName;

                if (!string.IsNullOrEmpty(StudentLastName.Text))
                    updateStudentInfo.LastName = StudentLastName.Text;
                else
                    updateStudentInfo.LastName = oldStudent.LastName;

                if (!string.IsNullOrEmpty(StudentFaculty.Text))
                    updateStudentInfo.Faculty = StudentFaculty.Text;
                else
                    updateStudentInfo.Faculty = oldStudent.Faculty;


                var response = await studentService.UpdateAsync(result, updateStudentInfo);

                if (imagePath != null && passportImagePath != null) 
                    await studentService.UploadPicturesAsync(oldStudent.Id, imagePath, passportImagePath);

                else if (imagePath != null && passportImagePath == null ||
                    imagePath == null && passportImagePath != null)
                {
                    ErrorResponse.Text = "Please upload both images";
                    ErrorResponse.Visibility = Visibility.Visible;
                }

                if (response is not null)
                    messageViewer.Show();

            }
            else
            {
                ErrorResponse.Text = "Please enter a valid ID";
                ErrorResponse.Visibility = Visibility.Visible;
            }
        }

        private void ImageUploader_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG;)|*.JPG;*.PNG";
            
            openFileDialog.InitialDirectory = Environment.GetFolderPath
                (Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() is true)
            {
                imagePath = openFileDialog.FileName;

                Image.Text = imagePath.Split('\\').Last();
            }                
        }

        private void PasspostImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG;)|*.JPG;*.PNG";
            openFileDialog.InitialDirectory = Environment.GetFolderPath
                (Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                passportImagePath = openFileDialog.FileName;

                PassportImage.Text = passportImagePath.Split('\\').Last();
            }
        }
    }
}
