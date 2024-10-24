using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.Infrastructure.DataBase
{
    internal class BookRepository
    {
        public List<BookEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Book.ToList();
                return items;
            }

        }
        public BookEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Book.FirstOrDefault(x => x.id == id);
                return item;
            }
        }


    }
}
