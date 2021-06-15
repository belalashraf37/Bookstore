using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstroe.Models.Repositroies
{
   public interface IBookstoreRepositroy<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int Id);
        void Add(TEntity entity);
        void Update(int id , TEntity entity);
        void Delete(int id );
        List<TEntity> Search(string term);
    }
}
