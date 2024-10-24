using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.ViewModels;

namespace Library.Infrastructure.Mappers
{
    internal class BookMapper
    {
        public static class ExampleMapper
        {
            public static BookViewModel Map(BookEntity entity)
            {
                var viewModel = new BookViewModel
                {
                    id = entity.id,
                    name = entity.name,
                    publishinghouse = entity.publishinghouse,
                    genre = entity.genre,
                    writerfirstname = entity.writerfirstname,
                    writesecondrname = entity.writesecondrname,
                    writerlastname = entity.writerlastname,
                };
                return viewModel;
            }

            public static List<BookViewModel> Map(List<BookEntity> entities)
            {
                var viewModels = entities.Select(x => Map(x)).ToList();
                return viewModels;
            }
        }

    }
}
