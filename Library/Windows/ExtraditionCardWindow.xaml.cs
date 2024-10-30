using Library.Infrastructure.DataBase;
using Library.Infrastructure.ViewModels;
using Library.Infrastructure.Status;
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

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionCardWindow.xaml
    /// </summary>
    public partial class ExtraditionCardWindow : Window
    {
        private ExtraditionStatus _selectedItem = null;
        private ExtraditionRepository _repository;
        private ReadersRepository _readersRepository;
        private BookRepository _bookRepository;

        public ExtraditionCardWindow(ExtraditionStatus selectedItem)
        {
            InitializeComponent();
            _repository = new ExtraditionRepository();
            _bookRepository = new BookRepository();
            _readersRepository = new ReadersRepository();
            Name.ItemsSource = _bookRepository.GetList().Distinct();
            FullName.ItemsSource = _readersRepository.GetList().Distinct();
            if (selectedItem != null)
            {
                _selectedItem = selectedItem;
                Data.Text = selectedItem.Data;
                Name.Text = selectedItem.Name;
                FullName.Text = selectedItem.FullName;
            }
            else
            {
                _selectedItem = selectedItem;
                Data.Text = null;
                Name.Text = null;
                FullName.Text = null;
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
                if (Name.Text == null || FullName.Text == null /*||!DateTime.TryParse(Date.Text, out var _) || Date.Text.Count() != 10*/)
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    var bookId = ((BookViewModel)Name.SelectedItem).id;
                    var readerId = ((ReadersViewModel)FullName.SelectedItem).id;
                    _repository = new ExtraditionRepository();
                    if (_selectedItem != null)
                    {
                        var entity = new ExtraditionViewModel
                        {
                            id = _selectedItem.Id,
                            data = Data.Text,
                            idbook = bookId,
                            idreaders = readerId,
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
                        var entity = new ExtraditionViewModel
                        {
                            data = Data.Text,
                            idbook = bookId,
                            idreaders = readerId,
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
