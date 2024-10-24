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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private void Button_Book(object sender, RoutedEventArgs e)
        {
            BookPage servicePage = new BookPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = servicePage.Title;
            mainWindow.MainFrame.Navigate(servicePage);
        }

        private void Button_Readers(object sender, RoutedEventArgs e)
        {
            ReadersPage servicePage = new ReadersPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = servicePage.Title;
            mainWindow.MainFrame.Navigate(servicePage);
        }

        private void Button_Extradition(object sender, RoutedEventArgs e)
        {
            ExtraditionPage servicePage = new ExtraditionPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = servicePage.Title;
            mainWindow.MainFrame.Navigate(servicePage);
        }
    }
}
