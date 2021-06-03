using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplOkien
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int NrIndeksu { get; set; }
        public string Wydzial { get; set; }

        public Student(string imie, string nazwisko, int nrIndeksu, string wydzial)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            NrIndeksu = nrIndeksu;
            Wydzial = wydzial;

        }

        public Student()
            :this("","",0,"")
        {
            
        }

    }
}
