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

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionPage.xaml
    /// </summary>
    public partial class ExtraditionPage : Page
    {
        private ExtraditionRepository _extraditionrepository;
        private ReadersRepository _readersRepository;
        private BookRepository _bookRepository;
        public ExtraditionPage()
        {
            InitializeComponent();
            _extraditionrepository = new ExtraditionRepository();
            _readersRepository = new ReadersRepository();
            _bookRepository = new BookRepository();
            UpdateGrid();
            GridNew();

        }

        public void GridNew()
        {
            var notesList = _extraditionrepository.GetList();
            var projectList = _readersRepository.GetList();
            var scientsistList = _bookRepository.GetList();
            var notesStatusList = notesList.Select(s => new ExtraditionStatus
            {
                Id = s.id,
                Data = s.data,
                Name = scientsistList.FirstOrDefault(p => p.id == s.idbook)?.name,
                FullName = projectList.FirstOrDefault(x => x.id == s.idreaders)?.fullname,

            }).ToList();
            
        }

        public void UpdateGrid() // обновление DataGrid в соответствии с бд
        {
            ExtraditionGrid.ItemsSource = _extraditionrepository.GetList();
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            var qrManager = new QRManager();
            QRCode.Source = qrManager.Generate(ClientGrid.SelectedItem);
            */
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
            var item = ExtraditionTextBox.Text.ToString();
            if (item != "" || item == null)
            {
                /*var search = _repository.Search(item);
                BookGrid.ItemsSource = search;
                */
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
            var extraditionCard = new ExtraditionCardWindow(ExtraditionGrid.SelectedItem as ExtraditionStatus);
            extraditionCard.ShowDialog();
            UpdateGrid();
            mainWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // кнопка, которая при нажатии удаляет запись клиента
        {
            var item = ExtraditionGrid.SelectedItem as ExtraditionViewModel;

            if (ExtraditionGrid.SelectedItem == null || item == null)
            {
                MessageBox.Show("Удаление не было произведено");
            }
            else
            {
                _extraditionrepository.Delete(item.id);
                UpdateGrid();
            }
        }

        private void ExtraditionGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) // двойной клик по DataGrid откроет карточку существующего клиента в бд
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            var item = ExtraditionGrid.SelectedItem as ExtraditionViewModel;
            if (item == null)
            {
                MessageBox.Show("Не жмякайте просто так)");
            }
            else
            {
                var id = item.id;
                mainWindow.Hide();
                var extraditionCard = new ExtraditionCardWindow(ExtraditionGrid.SelectedItem as ExtraditionStatus);
                extraditionCard.ShowDialog();
                UpdateGrid();
                mainWindow.Show();
            }
            item = null;
        }
    }
}
