using ExamTask.Service.Interfaces;
using ExamTask.Service.Services;
using ExamTask.WPF.UI.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ExamTask.UI.Controls
{
    /// <summary>
    /// Interaction logic for PrivateChat.xaml
    /// </summary>
    public partial class PrivateChat : UserControl
    {
        private IUserService studentService;
        private StudentInfo studentInfo;
        private readonly string url = "https://talabamiz.uz/";

        public PrivateChat()
        {
            studentInfo = new StudentInfo();
            studentService = new UserService();
            InitializeComponent();
        }


        private async void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var privateChat = (PrivateChat)sender;

            var student = await studentService.GetAsync
                (long.Parse(privateChat.StudentId.Text));

            studentInfo = new StudentInfo();

            studentInfo.StudentId.Content = student.Id.ToString();
            studentInfo.StudentFirstName.Content = student.FirstName;
            studentInfo.StudentLastName.Content = student.LastName;
            studentInfo.StudentFaculty.Content = student.Faculty;

            if (student.Image != null)
            {
                studentInfo.Image.ImageSource = new BitmapImage(new Uri(url + student.Image.Path));
            }
            if (student.Passport != null)
            {
                studentInfo.PassportImage.ImageSource = new BitmapImage
                    (new System.Uri(url + student.Passport.Path));
            }

            studentInfo.Show();
        }
    }
}
