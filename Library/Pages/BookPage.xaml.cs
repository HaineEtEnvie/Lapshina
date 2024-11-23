using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using Library.Infrastructure.DataBase;
using Library.Infrastructure.ViewModels;
using Library.Windows;
using Library.Infrastructure.QR;
using System.Linq;
using Library.Infrastructure.Status;


namespace Library.Pages
{
    public partial class BookPage : Page
    {
        private QRManager _qrgenerate;
        public BookRepository _bookRepository;
        public BookPage()
        {
            InitializeComponent();
            _bookRepository = new BookRepository();
            GridNew();

        }

        public void GridNew()
        {
            var bookList = _bookRepository.GetList();
            var bookStatusList = bookList.Select(s => new BookStatus
            {
                Id = s.id,
                Name = s.name,
                Publishinghouse = s.publishinghouse,
                Genre = s.genre,
                Writerfullname = s.writerfullname,

            }).ToList();
                BookGrid.ItemsSource = bookStatusList;
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            var qrManager = new QRManager();
            QRCode.Source = qrManager.Generate(BookGrid.SelectedItem);
        }

        private void MenuPage_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии переходит в меню
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var item = BookTextBox.Text.ToString().Trim().ToLower();
            if (item != "" || item == null)
            {
                var search = _bookRepository.Search(item);
                BookGrid.ItemsSource = search;
            }
            else
            {
                GridNew();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии открывает карточку для добавления записи клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Hide();
            var clientCard = new BookCardWindow();
            clientCard.ShowDialog();
            GridNew();
            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GridNew();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {
            var item = BookGrid.SelectedItem as BookStatus;

            if (BookGrid.SelectedItem == null || item == null)
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _bookRepository.Delete(item.Id);
                GridNew();
            }
        }

        private void BookGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var _selectedItem = BookGrid.SelectedItem;
            if (_selectedItem == null)
            {
                MessageBox.Show("Нужно выбрать строку");
            }
            else
            {
                mainWindow.Hide();
                var bookCard = new BookCardWindow(_selectedItem as BookStatus);
                bookCard.ShowDialog();
                GridNew();
                mainWindow.Show();
            }
        }
    }
}
