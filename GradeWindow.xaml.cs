using System.Windows;

namespace AplOkien
{
    /// <summary>
    /// Interaction logic for GradeWindow.xaml
    /// </summary>
    public partial class GradeWindow : Window
    {
        private Student _student;

        public GradeWindow(Student student)
        {
            InitializeComponent();


            tbName.Text = student.Imie;
            tbLastname.Text = student.Nazwisko;
            _student = student;
            
        }
        private void btnGradeOK_Click(object sender, RoutedEventArgs e)
        {

            if (!decimal.TryParse(tbGrade.Text, out decimal gradeValue))
            {
                MessageBox.Show("Ocena ma być liczbą!", "Błąd!");
                return;
            }
            var reminder = gradeValue - decimal.Truncate(gradeValue);
            if (gradeValue > 5 || gradeValue < 2 || reminder != 0m && reminder != 0.5m)
            {
                MessageBox.Show("Podano błędną ocenę!", "Błąd");
            }
            var grade = new Grade(gradeValue, tbCourse.Text);
            _student.Oceny.Add(grade);
            DialogResult = true;
        }
    }
}
