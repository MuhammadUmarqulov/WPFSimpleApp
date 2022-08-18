using ExamTask.Main.Contracts;
using ExamTask.Main.Models;
using ExamTask.Main.Services;
using ExamTask.UI.Controls;
using ExamTask.UI.Pages;
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

namespace ExamTask.WPF
{
#pragma warning disable
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IStudentService studentService;
        private Thread thread;
        public MainWindow()
        {
            studentService = new StudentService();

            InitializeComponent();
        }

        private IEnumerable<Student> allStudents;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(async () =>
            {
                Dispatcher.Invoke(() => StudentsList.Items.Clear());


                allStudents = await studentService.GetAllAsync();

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
                allStudents = await studentService.GetAllAsync();

                allStudents = allStudents.Where(p => p.FirstName.ToLower().Contains(text)
                    || p.LastName.ToLower().Contains(text)
                    || p.Faculty.ToLower().Contains(text));

                await LoadStudents(allStudents);
            });
            thread.Start();
        }

        private async Task LoadStudents(IEnumerable<Student> users)
        {
            foreach (var user in users)
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    PrivateChat privateChat = new PrivateChat();
                    privateChat.NameTxt.Text = user.FirstName + " " + user.LastName;
                    privateChat.FacultyMsgTxt.Text = user.Faculty;
                    privateChat.DateTimeTxt.Text = user.CreatedAt.ToString();
                    privateChat.UserImg.ImageSource = user.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + user.Image.Path))
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

        private void CloseWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
