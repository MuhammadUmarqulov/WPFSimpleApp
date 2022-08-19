using ExamTask.Domain.Entities;
using ExamTask.Service.Interfaces;
using ExamTask.Service.Services;
using ExamTask.UI.Controls;
using ExamTask.UI.Pages;
using ExamTask.WPF.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ExamTask.WPF
{
#pragma warning disable
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserService userService;
        private StudentInfo studentInfo;
        private Thread thread;
        public MainWindow()
        {

            userService = new UserService();

            InitializeComponent();
        }

        private IEnumerable<User> allStudents;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(async () =>
            {
                Dispatcher.Invoke(() => StudentsList.Items.Clear());

                allStudents = await userService.GetAllAsync();

                await LoadStudents(allStudents);
            });

            thread.Start();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            StudentsList.Items.Clear();

            string text = SearchBar.Text.ToLower();


            thread = new Thread(async () =>
            {
                allStudents = await userService.GetAllAsync();

                allStudents = allStudents.Where(p => p.FirstName.ToLower().Contains(text)
                    || p.LastName.ToLower().Contains(text)
                    || p.Faculty.ToLower().Contains(text));

                await LoadStudents(allStudents);
            });
            thread.Start();
        }

        private async Task LoadStudents(IEnumerable<User> students)
        {
            foreach (var student in students)
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    PrivateChat privateChat = new PrivateChat();

                    privateChat.NameTxt.Text = student.FirstName + " " + student.LastName;
                    privateChat.FacultyMsgTxt.Text = student.Faculty;
                    privateChat.StudentId.Text = student.Id.ToString();
                    privateChat.DateTimeTxt.Text = student.CreatedAt.ToString();


                    privateChat.UserImg.ImageSource = student.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + student.Image.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));

                    StudentsList.Items.Add(privateChat);
                });
            }
        }

        private void SaveStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            InputerArea.Content = new StudentSavePage();
        }

        private void DeleteStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            InputerArea.Content = new StudentDeletePage();
        }

        private void AddStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            InputerArea.Content = new StudentAddPage();
        }


        private async void PrivateChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var privateChat = (PrivateChat)sender;

            var student = await userService.GetAsync
                (long.Parse(privateChat.StudentId.Text));

            studentInfo = new StudentInfo();

            studentInfo.StudentId.Content = student.Id.ToString();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FullScreenBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
