using Library.Infrastructure.Mappers;
using Library.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
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
    }
}
