using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21432865_HW03.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Assuming you have this as an identifier
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public int? Point { get; set; }
    }
}