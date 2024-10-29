using Library.Infrastructure.Mappers;
using Library.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.DataBase
{
    public class ExtraditionRepository
    {
        public List<ExtraditionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Extradition.ToList();
                return ExtraditionMapper.Map(items);
            }

        }
        public ExtraditionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Extradition.FirstOrDefault(x => x.id == id);
                return ExtraditionMapper.Map(item);
            }
        }
    }
}
