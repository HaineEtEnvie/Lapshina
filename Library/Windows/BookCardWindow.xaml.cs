using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library.Infrastructure;
using Library;
using Library.Infrastructure.ViewModels;
using Library.Pages;
using Library.Infrastructure.Mappers;
using Library.Infrastructure.DataBase;
using Library.Windows;
using System.Windows.Markup;
using Library.Infrastructure.Status;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookCardWindow.xaml
    /// </summary>
    public partial class BookCardWindow : Window
    {
        private BookStatus _selectedItem = null;
        private BookRepository _repository;
        public BookCardWindow()
        {
            InitializeComponent();
        }

        public BookCardWindow(BookStatus selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _selectedItem = selectedItem;
                Name.Text = selectedItem.Name;
                PublishingHouse.Text = selectedItem.Publishinghouse;
                Genre.Text = selectedItem.Genre;
                WriterFullName.Text = selectedItem.Writerfullname;
            }
            else
            {
                _selectedItem = selectedItem;
                Name.Text = null;
                PublishingHouse.Text = null;
                Genre.Text = null;
                WriterFullName.Text = null;
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Name.Text == "" || PublishingHouse.Text == "" || Genre.Text == "" || WriterFullName.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    _repository = new BookRepository();
                    if (_selectedItem != null)
                    {
                        var entity = new BookViewModel
                        {
                            id = _selectedItem.Id,
                            name = Name.Text,
                            publishinghouse = PublishingHouse.Text,
                            genre = Genre.Text,
                            writerfullname = WriterFullName.Text,

                        };
                        if (_repository != null)
                        {
                            _repository.Update(entity);
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            MessageBox.Show("Пусто");
                        }
                    }
                    else
                    {
                        var entity = new BookViewModel
                        {
                            name = Name.Text,
                            publishinghouse = PublishingHouse.Text,
                            genre = Genre.Text,
                            writerfullname = WriterFullName.Text,
                        };
                        if (_repository != null)
                        {
                            _repository.Add(entity);
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            MessageBox.Show("Пусто");
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}
