using Library.Infrastructure.Mappers;
using Library.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public ExtraditionViewModel Update(ExtraditionViewModel entity) // метод редактирования существующей записи клиента в бд
        {
            entity.data = entity.data.Trim();
            entity.idbook = entity.idbook;
            entity.idreaders = entity.idreaders;
            if (string.IsNullOrEmpty(entity.data))
                MessageBox.Show("Дата не может быть пустой");

            using (var context = new Context())
            {
                var item = context.Extradition.FirstOrDefault(x => x.id == entity.id);
                if (item != null)
                {
                    item.data = entity.data;
                    item.idbook = entity.idbook;
                    item.idreaders = entity.idreaders;
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("");
                    MessageBox.Show("Ничего не было сохранено");
                }
                return ExtraditionMapper.Map(item);
            }
        }

        public ExtraditionViewModel Add(ExtraditionViewModel entity) // метод добавления клиента в бд
        {
            entity.data = entity.data.Trim();
            entity.idbook = entity.idbook;
            entity.idreaders = entity.idreaders;
            if (string.IsNullOrEmpty(entity.data))
            {
                throw new Exception("Дата не может быть пустым");
            }
            using (var context = new Context())
            {
                var item = ExtraditionMapper.Map(entity);
                context.Extradition.Add(item);
                if (item != null)
                {
                    item.data = entity.data;
                    item.idbook = entity.idbook;
                    item.idreaders = entity.idreaders;
                    context.Extradition.Add(item);
                    context.SaveChanges();
                    MessageBox.Show("Успешное Сохранение");
                }
                else
                {
                    MessageBox.Show("Ничего не было сохранено");
                }
                return ExtraditionMapper.Map(item);
            }
        }

        public void Delete(long id) // метод удаления существующей записи клиента в бд
        {
            // удаляем клиента
            using (var context = new Context())
            {
                var user = context.Extradition.FirstOrDefault(x => x.id == id);
                if (user != null)
                {
                    context.Extradition.Remove(user);
                    context.SaveChanges();
                }
            }
        }


    }
}
