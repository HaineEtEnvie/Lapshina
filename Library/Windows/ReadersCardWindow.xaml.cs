using Library.Infrastructure.DataBase;
using Library.Infrastructure.Status;
using Library.Infrastructure.ViewModels;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReadersCardWindow.xaml
    /// </summary>
    public partial class ReadersCardWindow : Window
    {
        private ReadersStatus _selectedItem = null;
        private ReadersRepository _repository;
        public ReadersCardWindow()
        {
            InitializeComponent();
        }

        public ReadersCardWindow(ReadersStatus selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _selectedItem = selectedItem;
                FullName.Text = selectedItem.Fullname;
                Phonenumber.Text = selectedItem.Phonenumber;
                Adress.Text = selectedItem.Adress;
            }
            else
            {
                _selectedItem = selectedItem;
                FullName.Text = null;
                Phonenumber.Text = null;
                Adress.Text = null;
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
                if (FullName.Text == "" || Phonenumber.Text == "" || Adress.Text == "")
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    _repository = new ReadersRepository();
                    if (_selectedItem != null)
                    {
                        var entity = new ReadersViewModel
                        {
                            id = _selectedItem.Id,
                            fullname = FullName.Text,
                            phonenumber = Phonenumber.Text,
                            adress = Adress.Text,
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
                        var entity = new ReadersViewModel
                        {
                            fullname = FullName.Text,
                            phonenumber = Phonenumber.Text,
                            adress = Adress.Text,
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
