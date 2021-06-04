using System.Collections.Generic;

namespace AplOkien
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int NrIndeksu { get; set; }
        public string Wydzial { get; set; }
        public List<Grade> Oceny { get;}

        public Student(string imie, string nazwisko, int nrIndeksu, string wydzial)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            NrIndeksu = nrIndeksu;
            Wydzial = wydzial;
            Oceny = new List<Grade>();
        }

        public Student()
            :this("","",0,"")
        {
            
        }

    }
}
