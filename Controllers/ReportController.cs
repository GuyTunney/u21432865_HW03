using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using u21432865_HW03.Models;

namespace u21432865_HW03.Controllers
{
    public class ReportController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // Generate Current Loans Report
        public async Task<ActionResult> GenerateCurrentLoansReport()
        {
            var currentLoans = await db.Borrows
                .Include(b => b.Book)
                .Include(b => b.Student)
                .Where(b => b.BroughtDate == null) // Not returned
                .Select(b => new CurrentLoanReport
                {
                    BookName = b.Book.Name,
                    StudentName = b.Student.Name + " " + b.Student.Surname,
                    TakenDate = b.TakenDate,
                    BroughtDate = b.BroughtDate
                }).ToListAsync();

            return View(currentLoans); // Ensure this matches your view's expected model
        }


        // Save the generated report
        [HttpPost]
        public async Task<ActionResult> SaveReport(string filename, string filetype)
        {
            try
            {
                // Fetch current loans for the report
                var currentLoans = await db.Borrows
                    .Include(b => b.Book)
                    .Include(b => b.Student)
                    .Where(b => b.BroughtDate == null) // Not returned
                    .Select(b => new CurrentLoanReport
                    {
                        BookName = b.Book.Name,
                        StudentName = b.Student.Name + " " + b.Student.Surname,
                        TakenDate = b.TakenDate,
                        BroughtDate = b.BroughtDate
                    }).ToListAsync();

                // Save report logic based on the file type
                string filePath = Server.MapPath("~/Reports/" + filename + "." + filetype);
                using (var writer = new StreamWriter(filePath))
                {
                    await writer.WriteLineAsync("Book Name,Student Name,Taken Date,Brought Date");
                    foreach (var loan in currentLoans)
                    {
                        await writer.WriteLineAsync($"{loan.BookName},{loan.StudentName},{loan.TakenDate.ToShortDateString()},{(loan.BroughtDate.HasValue ? loan.BroughtDate.Value.ToShortDateString() : "Not Returned")}");
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // View for Document Archive
        public ActionResult DocumentArchive()
        {
            var reportDirectory = Server.MapPath("~/Reports");
            var savedReports = Directory.GetFiles(reportDirectory)
                .Select(f => new FileInfo(f))
                .Select(file => new { Name = file.Name, Path = file.FullName })
                .ToList();

            return View(savedReports);
        }

        // Delete report method
        [HttpPost]
        public ActionResult DeleteReport(string filename)
        {
            try
            {
                var filePath = Server.MapPath("~/Reports/" + filename);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "File not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    public class CurrentLoanReport
    {
        public string BookName { get; set; }
        public string StudentName { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime? BroughtDate { get; set; }
    }
}
