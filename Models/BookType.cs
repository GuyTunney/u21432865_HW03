using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace u21432865_HW03.Models
{
    public class BookType
    {
        public int TypeId { get; set; } // Primary key
        public string Name { get; set; }
    }
}