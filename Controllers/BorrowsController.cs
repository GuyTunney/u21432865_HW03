using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21432865_HW03.Data;
using u21432865_HW03.Models;

namespace u21432865_HW03.Controllers
{
    public class BorrowsController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Borrows
        public async Task<ActionResult> Index()
        {
            var borrows = db.Borrows.Include(b => b.Book).Include(b => b.Student);
            return View(await borrows.ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: Borrows/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BorrowId,BookId,StudentId,TakenDate,BroughtDate")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(borrow);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookName", borrow.BookId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", borrow.StudentId);
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookName", borrow.BookId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", borrow.StudentId);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BorrowId,BookId,StudentId,TakenDate,BroughtDate")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookName", borrow.BookId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", borrow.StudentId);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Borrow borrow = await db.Borrows.FindAsync(id);
            db.Borrows.Remove(borrow);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
