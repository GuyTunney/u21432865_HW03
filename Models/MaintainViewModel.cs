using System;
using System.Collections.Generic;

namespace u21432865_HW03.Models
{
    public class MaintainViewModel
    {
        public List<BookType> Types { get; set; }
        public List<Author> Authors { get; set; }
        public List<Borrow> Borrows { get; set; }
        public int CurrentTypesPage { get; set; }
        public int TotalTypesPages { get; set; }
        public int CurrentAuthorsPage { get; set; }
        public int TotalAuthorsPages { get; set; }
        public int CurrentBorrowsPage { get; set; }
        public int TotalBorrowsPages { get; set; }
    }
}

