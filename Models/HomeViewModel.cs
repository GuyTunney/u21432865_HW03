using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace u21432865_HW03.Models
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public List<Student> Students { get; set; }
        public List<Book> Books { get; set; }
        public List<BookType> BookTypes { get; set; } // Ensure this is included
        public int CurrentStudentPage { get; set; }
        public int TotalStudentPages { get; set; }
        public int CurrentBookPage { get; set; }
        public int TotalBookPages { get; set; }
    }
}