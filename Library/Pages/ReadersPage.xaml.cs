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
using Library.Infrastructure.Status;
using Library.Infrastructure.ViewModels;
using Library.Windows;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        public ReadersRepository _readersRepository;
        public ReadersPage()
        {
            InitializeComponent();
            _readersRepository = new ReadersRepository();
            GridNew();  

        }
        public void GridNew()
        {
            var readersList = _readersRepository.GetList();
            var readersStatusList = readersList.Select(s => new ReadersStatus
            {
                Id = s.id,
                Fullname = s.fullname,
                Phonenumber = s.phonenumber,
                Adress = s.adress,
            }).ToList();
            ReadersGrid.ItemsSource = readersStatusList;
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
                var search = _readersRepository.Search(item);
                ReadersGrid.ItemsSource = search;
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
            var readersCard = new ReadersCardWindow();
            readersCard.ShowDialog();
            GridNew();
            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GridNew();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {
            var item = ReadersGrid.SelectedItem as ReadersStatus;

            if (ReadersGrid.SelectedItem == null || item == null)
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _readersRepository.Delete(item.Id);
                GridNew();
            }
        }

        private void ReadersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var _selectedItem = ReadersGrid.SelectedItem;
            if (_selectedItem == null)
            {
                MessageBox.Show("Нужно выбрать строку");
            }
            else
            {
                mainWindow.Hide();
                var readersCard = new ReadersCardWindow(_selectedItem as ReadersStatus);
                readersCard.ShowDialog();
                GridNew();
                mainWindow.Show();
            }
        }
    }
}
