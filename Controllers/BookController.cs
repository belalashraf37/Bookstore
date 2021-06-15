using Bookstroe.Models;
using Bookstroe.Models.Repositroies;
using Bookstroe.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstroe.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepositroy<Book> bookRepositroy;
        private readonly IBookstoreRepositroy<Author> authorRepositroy;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookstoreRepositroy<Book> bookRepositroy ,
            IBookstoreRepositroy<Author> authorRepositroy , IHostingEnvironment hosting)
        {
            this.bookRepositroy = bookRepositroy;
            this.authorRepositroy = authorRepositroy;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var book = bookRepositroy.List();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepositroy.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel{

                Authors = fillselectlist()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = string.Empty;
                    if(model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        filename = model.File.FileName;
                        string fullpath = Path.Combine(uploads, filename);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                    }
                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "Please Select An Author From List!";
                       
                        return View(getallauthers());
                    }
                    var author = authorRepositroy.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        Imgurl = filename

                    };
                    bookRepositroy.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("" , "You Have To Fill All The Required Fields!");
            return View(getallauthers());
            
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepositroy.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
               BookId = book.Id,
               Title = book.Title,
               Description = book.Description,
               AuthorId = authorId,
               Authors = authorRepositroy.List().ToList(),
               Imgurl = book.Imgurl

            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewmodel)
        {
            try
            {
                string filename = string.Empty;
                if (viewmodel.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    filename = viewmodel.File.FileName;
                    string fullpath = Path.Combine(uploads, filename);

                    //delete the old file
                    string oldFileName = viewmodel.Imgurl;
                    string fulloldpath = Path.Combine(uploads, oldFileName);
                    if (fullpath != fulloldpath) {

                        System.IO.File.Delete(fulloldpath);

                        //save the new file
                        viewmodel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                }
                var author = authorRepositroy.Find(viewmodel.AuthorId);
                Book book = new Book
                {
                   Id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author,
                    Imgurl = filename

                };
                bookRepositroy.Update(viewmodel.BookId , book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepositroy.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id )
        {
            try
            {
                bookRepositroy.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> fillselectlist()
        {
           var authors = authorRepositroy.List().ToList();
            authors.Insert(0 , new Author { Id = -1, FullName = "---Please Select An Author---" });
            return authors;
        }

        BookAuthorViewModel getallauthers()
        {
            var vmodel = new BookAuthorViewModel
            {

                Authors = fillselectlist()
            };
            return vmodel;
        }
        public ActionResult Search(string term)
        {
            var result = bookRepositroy.Search(term);
            return View("Index" , result);
        }
    }
}
