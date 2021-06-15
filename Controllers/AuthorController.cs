using Bookstroe.Models;
using Bookstroe.Models.Repositroies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstroe.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreRepositroy<Author> authorRepositroy;

        public AuthorController(IBookstoreRepositroy<Author> authorRepositroy)
        {
            this.authorRepositroy = authorRepositroy;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var authors = authorRepositroy.List();
            return View(authors);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepositroy.Find(id);
            return View(author);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepositroy.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepositroy.Find(id);
            return View(author);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepositroy.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepositroy.Find(id);
            return View(author);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepositroy.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
