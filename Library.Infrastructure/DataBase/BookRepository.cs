using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.ViewModels;
using Library.Infrastructure.Mappers;

namespace Library.Infrastructure.DataBase
{
    public class BookRepository
    {
        public List<BookViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Book.ToList();
                return BookMapper.Map(items);
            }

        }
        public BookViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Book.FirstOrDefault(x => x.id == id);
                return BookMapper.Map(item);
            }
        }
    }
}
