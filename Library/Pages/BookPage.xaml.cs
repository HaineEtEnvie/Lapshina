using System.Windows.Controls;
using Library.Infrastructure.Repository;


namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        private BookRepository _repository;
        public BookPage()
        {
            InitializeComponent();
            _repository = new BookRepository();
            BookGrid.ItemsSource = _repository.GetList();

        }
    }
}
