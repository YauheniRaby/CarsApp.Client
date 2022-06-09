using System.Windows;
using System.Windows.Controls;

namespace CarsClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListBox ProductsList;
        
        public MainWindow()
        {
            InitializeComponent();
            ProductsList = ProductsListView;            
        }
    }
}
