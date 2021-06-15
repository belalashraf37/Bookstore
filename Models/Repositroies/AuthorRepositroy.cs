using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstroe.Models.Repositroies
{
    public class AuthorRepositroy : IBookstoreRepositroy<Author>
    {
        IList<Author> authors;
        public AuthorRepositroy()
        {
            authors = new List<Author>()
            {
                new Author{Id = 1, FullName = "Belal Ashraf"},
                new Author{Id = 2, FullName = "ali ahmed"},
                new Author{Id = 3, FullName = "mohamed khalid"},
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a => a.Id)+ 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
            author.FullName = newauthor.FullName;
        }
    }
}
