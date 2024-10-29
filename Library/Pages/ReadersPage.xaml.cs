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
    }
}
