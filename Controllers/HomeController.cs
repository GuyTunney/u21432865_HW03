using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using u21432865_HW03.Models;
using PagedList;
using PagedList.Mvc;
using System.Linq;
using System;

public class HomeController : Controller
{
    private LibraryDbContext db = new LibraryDbContext();

    public async Task<ActionResult> Index(int studentPage = 1, int bookPage = 1, int pageSize = 12)
    {
        // Pagination for Students
        var totalStudents = await db.Students.CountAsync();
        var totalStudentPages = (int)Math.Ceiling((double)totalStudents / pageSize);
        var students = await db.Students
                               .OrderBy(s => s.StudentId)
                               .Skip((studentPage - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        // Pagination for Books
        var totalBooks = await db.Books.CountAsync();
        var totalBookPages = (int)Math.Ceiling((double)totalBooks / pageSize);
        var books = await db.Books
                            .Include(b => b.Author)
                            .Include(b => b.Type)
                            .OrderBy(b => b.BookId)
                            .Skip((bookPage - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        var bookTypes = await db.BookTypes.ToListAsync();

        var model = new HomeViewModel
        {
            Students = students,
            Books = books,
            CurrentStudentPage = studentPage,
            TotalStudentPages = totalStudentPages,
            CurrentBookPage = bookPage,
            TotalBookPages = totalBookPages,
            BookTypes = bookTypes // Ensure this is set correctly
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> CreateStudent(Student student)
    {
        if (ModelState.IsValid)
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index"); // Redirect to refresh the page
        }
        return View(student); // Return to the form if invalid
    }

    [HttpPost]
    public async Task<ActionResult> CreateBook(Book book)
    {
        if (ModelState.IsValid)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // If validation fails, load the BookTypes again for the dropdown
        var bookTypes = await db.BookTypes.ToListAsync();
        var model = new HomeViewModel
        {
            Students = await db.Students.ToListAsync(),
            Books = await db.Books.ToListAsync(),
            BookTypes = bookTypes, // Include book types for dropdown
            CurrentStudentPage = 1,
            TotalStudentPages = 1,
            CurrentBookPage = 1,
            TotalBookPages = 1
        };

        return View("Index", model); // Return to the Index view with the updated model
    }



}
