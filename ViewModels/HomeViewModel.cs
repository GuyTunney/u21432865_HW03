using System.Collections.Generic;
using u21432865_HW03.Models;

namespace u21432865_HW03.ViewModels
{
    public class HomeViewModel
    {
        public List<Student> Students { get; set; }
        public List<Book> Books { get; set; }
        public int TotalStudentPages { get; set; }
        public int TotalBookPages { get; set; }
    }

}


