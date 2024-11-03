using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21432865_HW03.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int Point { get; set; }
        public int AuthorId { get; set; }
        public int TypeId { get; set; }

        // Navigation properties
        public virtual Author Author { get; set; }
        public virtual BookType Type { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; } // Collection of borrow records

        // Computed Status Property
        public string Status
        {
            get
            {
                // Check if there are any borrows where the book hasn't been returned
                return Borrows != null && Borrows.Any(b => b.BroughtDate == null) ? "Out" : "Available";
            }
        }
    }

}