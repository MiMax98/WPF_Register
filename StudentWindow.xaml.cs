using System.Text.RegularExpressions;
using System.Windows;

namespace AplOkien
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student student;
        public StudentWindow(Student student=null)
        {
            InitializeComponent();
            if (student!=null)
            {
                tbImie.Text = student.Imie;
                tbNazwisko.Text = student.Nazwisko;
                tbNrAlbumu.Text = student.NrIndeksu.ToString();
                tbWydzial.Text = student.Wydzial;

            }
            this.student = student ?? new Student();
        }

        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if(!Regex.IsMatch(tbImie.Text,@"^\p{Lu}\p{Ll}{1,12}$") ||
                !Regex.IsMatch(tbNazwisko.Text,@"^\p{Lu}\p{Ll}{1,12}$") ||
                !Regex.IsMatch(tbWydzial.Text, @"^\p{Lu}\p{Ll}{1,12}$") ||
                !Regex.IsMatch(tbNrAlbumu.Text, @"^[0-9]{4,10}$"))
            {
                MessageBox.Show("Podano błędne dane.");
                return;
            }
            student.Imie = tbImie.Text;
            student.Nazwisko = tbNazwisko.Text;
            student.NrIndeksu = int.Parse(tbNrAlbumu.Text);
            student.Wydzial = tbWydzial.Text;

            this.DialogResult = true;


        }
    }
}
