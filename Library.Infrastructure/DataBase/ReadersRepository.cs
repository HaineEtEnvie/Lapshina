using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.ViewModels;
using Library.Infrastructure.Mappers;

namespace Library.Infrastructure.DataBase
{
    public class ReadersRepository
    {
        public List<ReadersViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Readers.ToList();
                return ReadersMapper.Map(items);
            }

        }
        public ReadersViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Readers.FirstOrDefault(x => x.id == id);
                return ReadersMapper.Map(item);
            }
        }
    }
}
