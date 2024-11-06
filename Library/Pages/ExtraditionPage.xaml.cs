using Library.Infrastructure.DataBase;
using Library.Infrastructure.ViewModels;
using Library.Windows;
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
using Library.Pages;
using Library.Infrastructure;
using Library.Infrastructure.Status;
using Library.Infrastructure.QR;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionPage.xaml
    /// </summary>
    public partial class ExtraditionPage : Page
    {
        public ExtraditionRepository _extraditionrepository;
        public ReadersRepository _readersRepository;
        public BookRepository _bookRepository;
        List<string> _listCreate = new List<string> { "Все выдачи", "Старое", "Выдача за год", "Выдача за полгода" };

        public ExtraditionPage()
        {
            InitializeComponent();
            _extraditionrepository = new ExtraditionRepository();
            _readersRepository = new ReadersRepository();
            _bookRepository = new BookRepository();
            FastFilter.ItemsSource = _listCreate;
            FastFilter.SelectedItem = _listCreate[0];
            GridNew();
        }

        public void GridNew()
        {
            var extraditionList = _extraditionrepository.GetList();
            var readersList = _readersRepository.GetList();
            var bookList = _bookRepository.GetList();
            var extraditionStatusList = extraditionList.Select(s => new ExtraditionStatus
            {
                Id = s.id,
                Data = s.data,
                Name = bookList.FirstOrDefault(p => p.id == s.idbook)?.name,
                FullName = readersList.FirstOrDefault(x => x.id == s.idreaders)?.fullname,

            }).ToList();
            if (FastFilter.SelectedItem.ToString() == _listCreate[0])
            {
                ExtraditionGrid.ItemsSource = extraditionStatusList;

            }
            else
            {
                if (FastFilter.SelectedItem.ToString() == _listCreate[1])
                {
                    ExtraditionGrid.ItemsSource = extraditionStatusList.Where(x => DateTime.Parse(x.Data) < DateTime.Today.AddYears(-1));
                }
                else if (FastFilter.SelectedItem.ToString() == _listCreate[2])
                {
                    ExtraditionGrid.ItemsSource = extraditionStatusList.Where(x => DateTime.Parse(x.Data) >= DateTime.Today.AddYears(-1) && DateTime.Parse(x.Data) <= DateTime.Today);
                }
                else
                {
                    ExtraditionGrid.ItemsSource = extraditionStatusList.Where(x => DateTime.Parse(x.Data) >= DateTime.Today.AddMonths(-6) && DateTime.Parse(x.Data) <= DateTime.Today);
                }

            }
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            var qrManager = new QRManager();
            QRCode.Source = qrManager.Generate(ExtraditionGrid.SelectedItem);
        }

        private void MenuPage_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии переходит в меню
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);
        }



        private void AddButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии открывает карточку для добавления записи клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Hide();
            var extraditionCard = new ExtraditionCardWindow(ExtraditionGrid.SelectedItem as ExtraditionStatus);
            extraditionCard.ShowDialog();
            GridNew();

            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GridNew();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {

            if (ExtraditionGrid.SelectedItem == null || !(ExtraditionGrid.SelectedItem is ExtraditionStatus _selecteditem))
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _extraditionrepository.Delete(_selecteditem.Id);
                GridNew();
            }
        }

        
        private void ExtraditionGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) // двойной клик по DataGrid откроет карточку существующего клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var _selectedItem = ExtraditionGrid.SelectedItem;
            if (_selectedItem == null)
            {
                MessageBox.Show("Не жмякайте просто так)");
            }
            else
            {
                mainWindow.Hide();
                var extraditionCard = new ExtraditionCardWindow(ExtraditionGrid.SelectedItem as ExtraditionStatus);
                extraditionCard.ShowDialog();
                GridNew();
                mainWindow.Show();
            }
        }
        private void FastFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridNew();
        }

    }
}

