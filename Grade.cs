using System;

namespace AplOkien
{
    public class Grade
    {
        public decimal Value { get; set; }
        public string Course { get; set; }
        public DateTime Date { get; set; }

        public Grade(decimal value, string course)
        {
            Value = value;
            Course = course;
            Date = DateTime.Now;
        }
    }
}
