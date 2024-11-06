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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library.Infrastructure.DataBase;
using Library.Infrastructure.QR;
using Library.Infrastructure.ViewModels;
using Library.Windows;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        private ReadersRepository _repository;
        public ReadersPage()
        {
            InitializeComponent();
            _repository = new ReadersRepository();
            UpdateGrid();

        }
        public void UpdateGrid() // обновление DataGrid в соответствии с бд
        {
            ReadersGrid.ItemsSource = _repository.GetList();
        }
        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            var qrManager = new QRManager();
            QRCode.Source = qrManager.Generate(ReadersGrid.SelectedItem);
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
                ReadersGrid.ItemsSource = search;
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
            var readersCard = new ReadersCardWindow();
            readersCard.ShowDialog();
            UpdateGrid();
            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {
            var item = ReadersGrid.SelectedItem as ReadersViewModel;

            if (ReadersGrid.SelectedItem == null || item == null)
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _repository.Delete(item.id);
                UpdateGrid();
            }
        }

        private void ReadersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) // двойной клик по DataGrid откроет карточку существующего клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var item = ReadersGrid.SelectedItem as ReadersViewModel;
            if (item == null)
            {
                MessageBox.Show("Не жмякайте просто так)");
            }
            else
            {
                var id = item.id;
                mainWindow.Hide();
                var readersCard = new ReadersCardWindow(ReadersGrid.SelectedItem as ReadersViewModel);
                readersCard.ShowDialog();
                UpdateGrid();
                mainWindow.Show();
            }
            item = null;
        }
    }
}
