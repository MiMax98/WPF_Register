using System.Windows;

namespace AplOkien
{
    /// <summary>
    /// Interaction logic for Grades.xaml
    /// </summary>
    public partial class Grades : Window
    {
        //private Student _student;
        public Grades(Student student)
        {
            InitializeComponent();
            tbGname.Text = student.Imie;
            tbGLastName.Text = student.Nazwisko;
            //_student = student;
            dgGrades.ItemsSource = student.Oceny;
        }

        
    }
}
