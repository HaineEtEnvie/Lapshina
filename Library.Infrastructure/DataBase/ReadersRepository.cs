using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.ViewModels;
using Library.Infrastructure.Mappers;
using System.Windows.Forms;

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
        public ReadersViewModel Update(ReadersViewModel entity) // метод редактирования существующей записи клиента в бд
        {
            entity.fullname = entity.fullname.Trim();
            entity.phonenumber = entity.phonenumber.Trim();
            entity.adress = entity.adress.Trim();

            if (string.IsNullOrEmpty(entity.fullname))
                MessageBox.Show("Имя Читателя не может быть пустым");

            using (var context = new Context())
            {
                var item = context.Readers.FirstOrDefault(x => x.id == entity.id);
                if (item != null)
                {
                    item.fullname= entity.fullname;
                    item.adress = entity.adress;
                    item.adress = entity.adress;
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("");
                    MessageBox.Show("Ничего не было сохранено");
                }
                return ReadersMapper.Map(item);
            }
        }

        public ReadersViewModel Add(ReadersViewModel entity) // метод добавления клиента в бд
        {
            entity.fullname = entity.fullname.Trim();
            entity.phonenumber = entity.phonenumber.Trim();
            entity.adress= entity.adress.Trim();
            if (string.IsNullOrEmpty(entity.fullname) || string.IsNullOrEmpty(entity.phonenumber))
            {
                throw new Exception("Имя Пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var item = ReadersMapper.Map(entity);
                context.Readers.Add(item);
                if (item != null)
                {
                    item.fullname = entity.fullname;
                    item.phonenumber = entity.phonenumber;
                    item.adress = entity.adress;
                    context.Readers.Add(item);
                    context.SaveChanges();
                    MessageBox.Show("Успешное Сохранение");
                }
                else
                {
                    MessageBox.Show("Ничего не было сохранено");
                }
                return ReadersMapper.Map(item);
            }
        }

        public void Delete(long id) // метод удаления существующей записи клиента в бд
        {
            using (var context = new Context())
            {
                var user = context.Readers.FirstOrDefault(x => x.id == id);
                if (user != null)
                {
                    context.Readers.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        public List<ReadersViewModel> Search(string search) // метод поиска существующей записи клиента в грид
        {
            search = search.Trim();
            using (var context = new Context())
            {
                var result = context.Readers.Where(x => x.fullname.Contains(search) && x.fullname.StartsWith(search) || 
                x.phonenumber.ToString().Contains(search) || 
                x.adress.ToString().Contains(search)).ToList();

                return ReadersMapper.Map(result);
            }
        }
    }
}
