using Library.Infrastructure.DataBase;
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

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionPage.xaml
    /// </summary>
    public partial class ExtraditionPage : Page
    {
        private ExtraditionRepository _repository;
        public ExtraditionPage()
        {
            InitializeComponent();
            _repository = new ExtraditionRepository();
            UpdateGrid();

        }

        public void UpdateGrid() // обновление DataGrid в соответствии с бд
        {
            ExtraditionGrid.ItemsSource = _repository.GetList();
        }
    }
}
