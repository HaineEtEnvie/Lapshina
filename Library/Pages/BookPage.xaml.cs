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


namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        private QRManager _qrgenerate;
        private BookRepository _repository;
        public BookPage()
        {
            InitializeComponent();
            _repository = new BookRepository();
            UpdateGrid();

        }
        public void UpdateGrid() // обновление DataGrid в соответствии с бд
        {
            BookGrid.ItemsSource = _repository.GetList();
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
            var item = ReadersTextBox.Text.ToString();
            if (item != "" || item == null)
            {
                var search = _repository.Search(item);
                BookGrid.ItemsSource = search;
            }
            else
            {
                UpdateGrid();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии открывает карточку для добавления записи клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Hide();
            var clientCard = new BookCardWindow();
            clientCard.ShowDialog();
            UpdateGrid();
            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {
            var item = BookGrid.SelectedItem as BookViewModel;

            if (BookGrid.SelectedItem == null || item == null)
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _repository.Delete(item.id);
                UpdateGrid();
            }
        }

        private void BookGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) // двойной клик по DataGrid откроет карточку существующего клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var item = BookGrid.SelectedItem as BookViewModel;
            if (item == null)
            {
                MessageBox.Show("Не жмякайте просто так)");
            }
            else
            {
                var id = item.id;
                mainWindow.Hide();
                var bookCard = new BookCardWindow(BookGrid.SelectedItem as BookViewModel);
                bookCard.ShowDialog();
                UpdateGrid();
                mainWindow.Show();
            }
            item = null;
        }
    }
}
