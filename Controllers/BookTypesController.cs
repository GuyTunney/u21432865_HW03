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
    public class BookTypesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: BookTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.BookTypes.ToListAsync());
        }

        // GET: BookTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookType bookType = await db.BookTypes.FindAsync(id);
            if (bookType == null)
            {
                return HttpNotFound();
            }
            return View(bookType);
        }

        // GET: BookTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeId,Name")] BookType bookType)
        {
            if (ModelState.IsValid)
            {
                db.BookTypes.Add(bookType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bookType);
        }

        // GET: BookTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookType bookType = await db.BookTypes.FindAsync(id);
            if (bookType == null)
            {
                return HttpNotFound();
            }
            return View(bookType);
        }

        // POST: BookTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeId,Name")] BookType bookType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookType);
        }

        // GET: BookTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookType bookType = await db.BookTypes.FindAsync(id);
            if (bookType == null)
            {
                return HttpNotFound();
            }
            return View(bookType);
        }

        // POST: BookTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookType bookType = await db.BookTypes.FindAsync(id);
            db.BookTypes.Remove(bookType);
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
