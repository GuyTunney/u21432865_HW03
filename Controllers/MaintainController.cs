using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using u21432865_HW03.Models;

namespace u21432865_HW03.Controllers
{
    public class MaintainController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        public async Task<ActionResult> Index(int typesPage = 1, int authorsPage = 1, int borrowsPage = 1, int pageSize = 10)
        {
            var totalTypes = await db.BookTypes.CountAsync();
            var types = await db.BookTypes.OrderBy(t => t.Name).Skip((typesPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var totalAuthors = await db.Authors.CountAsync();
            var authors = await db.Authors.OrderBy(a => a.Name).Skip((authorsPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var totalBorrows = await db.Borrows.CountAsync();
            var borrows = await db.Borrows.Include(b => b.Book).Include(b => b.Student).OrderBy(b => b.BorrowId).Skip((borrowsPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var model = new MaintainViewModel
            {
                Types = types,
                Authors = authors,
                Borrows = borrows,
                CurrentTypesPage = typesPage,
                TotalTypesPages = (int)Math.Ceiling((double)totalTypes / pageSize),
                CurrentAuthorsPage = authorsPage,
                TotalAuthorsPages = (int)Math.Ceiling((double)totalAuthors / pageSize),
                CurrentBorrowsPage = borrowsPage,
                TotalBorrowsPages = (int)Math.Ceiling((double)totalBorrows / pageSize)
            };

            return View("Maintain", model);
        }

        // Create Type
        [HttpPost]
        public async Task<ActionResult> CreateType(BookType type)
        {
            if (ModelState.IsValid)
            {
                db.BookTypes.Add(type);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(type); // Return to view with model errors
        }

        // Create Author
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // Create Borrow
        [HttpPost]
        public async Task<ActionResult> CreateBorrow(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(borrow);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(borrow);
        }

        // Update Type
        [HttpPost]
        public async Task<ActionResult> UpdateType(BookType type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // Update Author
        [HttpPost]
        public async Task<ActionResult> UpdateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // Update Borrow
        [HttpPost]
        public async Task<ActionResult> UpdateBorrow(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(borrow);
        }

        // Delete Type
        [HttpPost]
        public async Task<ActionResult> DeleteType(int id)
        {
            var type = await db.BookTypes.FindAsync(id);
            if (type != null)
            {
                db.BookTypes.Remove(type);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Delete Author
        [HttpPost]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await db.Authors.FindAsync(id);
            if (author != null)
            {
                db.Authors.Remove(author);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Delete Borrow
        [HttpPost]
        public async Task<ActionResult> DeleteBorrow(int id)
        {
            var borrow = await db.Borrows.FindAsync(id);
            if (borrow != null)
            {
                db.Borrows.Remove(borrow);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
