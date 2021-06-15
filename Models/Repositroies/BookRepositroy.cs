using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstroe.Models.Repositroies
{
    public class BookRepositroy : IBookstoreRepositroy<Book>
    {
        List<Book> books;
        public BookRepositroy()
        {
            books = new List<Book>()
            {
                 new Book
                 {
                     Id = 1,
                     Title = " C# Programming",
                     Description = "No Descript",
                     Imgurl = "1.png",
                     Author = new Author{ Id = 2}
                     
                 },
                 new Book
                 {
                     Id = 2,
                     Title = " Java Programming",
                     Description = "Nothing",
                      Imgurl = "2.png",
                     Author = new Author()
                 },
                 new Book
                 {
                     Id = 3,
                     Title = " Dart Programming",
                     Description = "No Data",
                      Imgurl = "3.png",
                     Author = new Author()
                 }
            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b=> b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
           return books;
        }

        public List<Book> Search(string term)
        {
           
         return books.Where(a => a.Title.Contains(term)).ToList();
           
        }

        public void Update(int id , Book newBook)
        {
            var book = Find(id);
       
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.Imgurl = newBook.Imgurl;
        }
    }
}
