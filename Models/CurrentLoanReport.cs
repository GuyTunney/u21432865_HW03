using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21432865_HW03.Models
{
    public class CurrentLoanReport
    {
        public string BookName { get; set; }
        public string StudentName { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime? BroughtDate { get; set; }
    }
}

