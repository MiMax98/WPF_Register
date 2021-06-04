using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AplOkien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Student> ListaStudentow { get; set; }

        public MainWindow()
        {
            ListaStudentow = new List<Student>()
            {
                new Student("Jan","Kowalski",1234,"KIS"),
                new Student("Anna", "Nowak", 4321, "KIS"),
                new Student("Michał", "Jacek", 34562, "KIS")


            };
            InitializeComponent();
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Imię", Binding = new Binding("Imie") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nazwisko", Binding = new Binding("Nazwisko") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nr indeksu", Binding = new Binding("NrIndeksu") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Wydzial", Binding = new Binding("Wydzial") });

            dgStudent.AutoGenerateColumns = false;
            dgStudent.ItemsSource = ListaStudentow;


        }

        private void b_AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StudentWindow();
            if(dialog.ShowDialog()==true)
            {
                ListaStudentow.Add(dialog.student);
                dgStudent.Items.Refresh();
            }
        }

        private void b_RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudent.SelectedItem is Student)
                ListaStudentow.Remove((Student)dgStudent.SelectedItem);
            dgStudent.Items.Refresh();
        }

        private void bAddSchGrades_Click(object sender, RoutedEventArgs e)
        {
            var student = dgStudent.SelectedItem as Student;
            if (student == null)
            {
                return;
            }
            var dialog = new GradeWindow(student);
            dialog.ShowDialog();

        }

        private void bSchGrades_Click(object sender, RoutedEventArgs e)
        {
            var student = dgStudent.SelectedItem as Student;
            if (student == null)
            {
                return;
            }
            var dialog = new Grades(student);
            dialog.ShowDialog();
        }

        private void bEditStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = dgStudent.SelectedItem as Student;
            if (student == null)
            {
                return;
            }
            var dialog = new StudentWindow(student);
            if (dialog.ShowDialog() == true)
            {
                dgStudent.Items.Refresh();
            }
        }
    }
}
