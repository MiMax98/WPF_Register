using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Serialization;

namespace AplOkien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Student> ListaStudentow { get; set; }
        public List<Car> CarList { get; set; }

        public MainWindow()
        {
            ListaStudentow = new List<Student>()
            {
                new Student("Jan","Kowalski",1234,"KIS"),
                new Student("Anna", "Nowak", 4321, "KIS"),
                new Student("Michał", "Jacek", 34562, "KIS")
            };

            CarList = new List<Car>()
            {
                new Car{Brand="Mercedes", Color = "Czerwony", CreationYear=2019, RegistrationNumber="RZ 12345"},
                new Car{Brand="Audi", Color = "Czarny", CreationYear=2018, RegistrationNumber="KR 54321"},
                new Car{Brand="BMW", Color = "Biały", CreationYear=2016, RegistrationNumber="RPZ 42563"}
            };
            InitializeComponent();
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Imię", Binding = new Binding("Imie") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nazwisko", Binding = new Binding("Nazwisko") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nr indeksu", Binding = new Binding("NrIndeksu") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Wydzial", Binding = new Binding("Wydzial") });

            dgStudent.AutoGenerateColumns = false;
            dgStudent.ItemsSource = ListaStudentow;

            dgSamochod.Columns.Add(new DataGridTextColumn() { Header = "Marka", Binding = new Binding("Brand") });
            dgSamochod.Columns.Add(new DataGridTextColumn() { Header = "Kolor", Binding = new Binding("Color") });
            dgSamochod.Columns.Add(new DataGridTextColumn() { Header = "Rok produkcji", Binding = new Binding("CreationYear") });
            dgSamochod.Columns.Add(new DataGridTextColumn() { Header = "Numer rejestracyjny", Binding = new Binding("RegistrationNumber") });

            dgSamochod.AutoGenerateColumns = false;
            dgSamochod.ItemsSource = CarList;


        }

        private void b_AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StudentWindow();
            if (dialog.ShowDialog() == true)
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

        private void b_SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new SaveFileDialog(), "Text documents", ".txt");
            if (path == null)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var item in ListaStudentow)
            {
                sw.WriteLine("[[Student]]");
                sw.WriteLine("[FirstName]");
                sw.WriteLine(item.Imie);
                sw.WriteLine("[SurName]");
                sw.WriteLine(item.Nazwisko);
                sw.WriteLine("[StudentNo]");
                sw.WriteLine(item.NrIndeksu);
                sw.WriteLine("[Faculty]");
                sw.WriteLine(item.Wydzial);
                sw.WriteLine("[[]]");
            }
            sw.Close();
            MessageBox.Show("Pomyślnie zapisano do pliku");
        }

        private void bLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new OpenFileDialog(), "Text documents", ".txt");
            if (path == null)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            ListaStudentow.Clear();
            bool isCorrect = true;

            while (!sr.EndOfStream)
            {
                var student = ReadStudent(sr);
                if (student == null)
                {
                    MessageBox.Show("Nieprawidłowy format", "Błąd");
                    isCorrect = false;
                    break;
                }
                ListaStudentow.Add(student);
                sr.ReadLine();
            }

            sr.Close();
            dgStudent.Items.Refresh();
            if (isCorrect)
            {
                MessageBox.Show("Pomyślnie wczytano plik");
            }
        }

        private Student ReadStudent(StreamReader sr)
        {
            if (sr.ReadLine() != "[[Student]]") return null;
            var student = new Student();
            if (sr.ReadLine() != "[FirstName]") return null;
            if ((student.Imie = sr.ReadLine()) == null) return null;
            if (sr.ReadLine() != "[SurName]") return null;
            if ((student.Nazwisko = sr.ReadLine()) == null) return null;
            if (sr.ReadLine() != "[StudentNo]") return null;
            string number = sr.ReadLine();
            if (!int.TryParse(number, out int studentNo)) return null;
            student.NrIndeksu = studentNo;
            if (sr.ReadLine() != "[Faculty]") return null;
            if ((student.Wydzial = sr.ReadLine()) == null) return null;
            return student;
        }

        private void b_SaveToFileDynamic_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new SaveFileDialog(), "Text documents", ".txt");
            if (path == null)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var student in ListaStudentow)
            {
                DynamicSerializer.Save(student, sw);
            }
            sw.Close();
            MessageBox.Show("Pomyślnie zapisano do pliku");
        }

        private void bLoadFromFileDynamic_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new OpenFileDialog(), "Text documents", ".txt");
            if (path == null)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            bool isCorrect = true;
            ListaStudentow.Clear();
            while (!sr.EndOfStream)
            {
                var student = DynamicSerializer.Load<Student>(sr);
                if (student == null)
                {
                    MessageBox.Show("Nieprawidłowy format", "Błąd");
                    isCorrect = false;
                    break;
                }
                ListaStudentow.Add(student);
            }

            sr.Close();
            dgStudent.Items.Refresh();
            if (isCorrect)
            {
                MessageBox.Show("Pomyślnie wczytano plik");
            }
        }

        private void bLoadFromFileDynamicNew_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new OpenFileDialog(), "Text documents", ".txt");
            if (path == null)
            {
                return;
            }
            bool isCorrect = true;
            ListaStudentow.Clear();
            var students = new StreamEnumerable<Student>(path);
            foreach (var student in students)
            {
                if (student == null)
                {
                    MessageBox.Show("Nieprawidłowy format", "Błąd");
                    isCorrect = false;
                    break;
                }
                ListaStudentow.Add(student);
            }

            dgStudent.Items.Refresh();
            if (isCorrect)
            {
                MessageBox.Show("Pomyślnie wczytano plik");
            }
        }

        private void b_SaveToXml_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new SaveFileDialog(), "XML files", ".xml");
            if (path == null)
            {
                return;
            }
            var fs = new FileStream(path, FileMode.Create);
            var serializer = new XmlSerializer(typeof(List<Car>), new Type[] { typeof(Car) });
            serializer.Serialize(fs, CarList);
            fs.Close();
            MessageBox.Show("Pomyślnie zapisano do pliku");
        }

        private void bLoadFromXml_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPathFromFileDialog(new OpenFileDialog(), "XML files", ".xml");
            if (path == null)
            {
                return;
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            var serializer = new XmlSerializer(typeof(List<Car>), new Type[] { typeof(Car) });
            CarList.Clear();
            try
            {
                CarList.AddRange((List<Car>)serializer.Deserialize(fs));
                MessageBox.Show("Pomyślnie wczytano plik");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Błąd");
            }
            finally
            {
                fs.Close();
                dgSamochod.Items.Refresh();
            }
        }

        private string GetPathFromFileDialog(FileDialog dialog, string fileType, string extension)
        {
            dialog.FileName = "dane";
            dialog.DefaultExt = extension;
            dialog.Filter = $"{fileType} ({extension})| *{extension}";
            if (dialog.ShowDialog() != true)
            {
                return null;
            }
            return dialog.FileName;
        }
    }
}
