using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Library.Infrastructure.ViewModels;
using Library.Infrastructure.Mappers;
using System.Windows.Forms;

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
        public BookViewModel Update(BookViewModel entity) // метод редактирования существующей записи клиента в бд
        {
            entity.name = entity.name.Trim();
            entity.publishinghouse = entity.publishinghouse.Trim();
            entity.genre = entity.genre.Trim();
            entity.writerfullname = entity.writerfullname.Trim();

            if (string.IsNullOrEmpty(entity.name))
                MessageBox.Show("Имя Пользователя не может быть пустым");

            using (var context = new Context())
            {
                var item = context.Book.FirstOrDefault(x => x.id == entity.id);
                if (item != null)
                {
                    item.name = entity.name;
                    item.publishinghouse = entity.publishinghouse;
                    item.genre = entity.genre;
                    item.writerfullname = entity.writerfullname;
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("");
                    MessageBox.Show("Ничего не было сохранено");
                }
                return BookMapper.Map(item);
            }
        }

        public BookViewModel Add(BookViewModel entity) // метод добавления клиента в бд
        {
            entity.name = entity.name.Trim();
            entity.publishinghouse = entity.publishinghouse.Trim();
            entity.genre = entity.genre.Trim();
            entity.writerfullname = entity.writerfullname.Trim();
            if (string.IsNullOrEmpty(entity.name) || string.IsNullOrEmpty(entity.publishinghouse))
            {
                throw new Exception("Имя Пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var item = BookMapper.Map(entity);
                context.Book.Add(item);
                if (item != null)
                {
                    item.name = entity.name;
                    item.publishinghouse = entity.publishinghouse;
                    item.genre = entity.genre;
                    item.writerfullname = entity.writerfullname;
                    context.Book.Add(item);
                    context.SaveChanges();
                    MessageBox.Show("Успешное Сохранение");
                }
                else
                {
                    MessageBox.Show("Ничего не было сохранено");
                }
                return BookMapper.Map(item);
            }
        }

        public void Delete(long id) // метод удаления существующей записи клиента в бд
        {
            // удаляем клиента
            using (var context = new Context())
            {
                var user = context.Book.FirstOrDefault(x => x.id == id);
                if (user != null)
                {
                    context.Book.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        /*public List<BookViewModel> Search(string search) // метод поиска существующей записи клиента в грид
        {
            search = search.Trim();
            using (var context = new Context())
            {
                var result = context.Book.Where(x => x.name.Contains(search) && x.name.StartsWith(search) || x.publishinghouse.ToString().Contains(search)).ToList();
                return BookMapper.Map(result);
            }
        }
        */
    }
}
