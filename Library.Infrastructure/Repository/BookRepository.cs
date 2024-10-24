using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.ViewModels;
using static Library.Infrastructure.Mappers.BookMapper;

namespace Library.Infrastructure.Repository
{
    public class BookRepository
    {
        public List<BookViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Book.ToList();
                return ExampleMapper.Map(items);
            }
        }

    }
}
